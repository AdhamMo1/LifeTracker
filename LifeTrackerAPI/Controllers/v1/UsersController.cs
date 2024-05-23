using AutoMapper;
using LifeTrackerDataService.IConfiguration;
using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Generic;
using LifeTrackerEntities.Dto.Incoming;
using LifeTrackerEntities.Dto.Outgoing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace LifeTrackerAPI.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, mapper, userManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _mapper.Map<List<ApplicationUser>, List<UserOutDto>>(await _unitOfWork.Users.All());
            var Result = new PagedResult<UserOutDto>();
            Result.Content = users;
            Result.ResultCount = users.Count();
            return Ok(Result);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user =await _unitOfWork.Users.GetById(id);
            var Result = new Result<UserOutDto>();
            if (user == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            Result.Content = _mapper.Map<ApplicationUser, UserOutDto>(await _unitOfWork.Users.GetById(id));
            return Ok(Result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserInDto user)
        {
            var newUser = _mapper.Map<UserInDto, ApplicationUser>(user);
            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetUser", new { id = newUser.Id }, user);
        }
    }
}
