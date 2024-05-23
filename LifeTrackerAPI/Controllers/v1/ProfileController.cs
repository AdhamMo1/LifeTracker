using AutoMapper;
using LifeTrackerDataService.IConfiguration;
using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Generic;
using LifeTrackerEntities.Dto.Incoming;
using LifeTrackerEntities.Dto.Outgoing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace LifeTrackerAPI.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, mapper, userManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var Result = new Result<UserOutDto>();
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            
            var userProfile = await _unitOfWork.Users.GetProfileByIdentity(loggedInUser.Id);
            if (userProfile == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            Result.Content = _mapper.Map<ApplicationUser, UserOutDto>(userProfile);
            return Ok(Result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto updateUserDto)
        {
            var Result = new Result<UserOutDto>();
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            var userProfile = await _unitOfWork.Users.UpdateProfileByIdentity(loggedInUser.Id,updateUserDto);
            if (userProfile == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            Result.Content = _mapper.Map<ApplicationUser, UserOutDto>(userProfile);
            return Ok(Result);
        }
        
    }
}
