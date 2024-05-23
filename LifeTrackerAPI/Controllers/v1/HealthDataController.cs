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

namespace LifeTrackerAPI.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HealthDataController : BaseController
    {
        public HealthDataController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : base(unitOfWork, mapper, userManager)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetHealthData()
        {
            var Result = new Result<HealthDataOutDto>();
            var LoggedInUser= await _userManager.GetUserAsync(User);
            if (LoggedInUser == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            var HealthData = _unitOfWork.HealthData.GetHealthData(LoggedInUser.Id);
            if (HealthData == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            Result.Content = _mapper.Map<HealthData,HealthDataOutDto>(HealthData);
            return Ok(Result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHealthData(HealthDataInDto HealthDataInDto)
        {
            var Result = new Result<HealthDataOutDto>();
            var LoggedInUser = await _userManager.GetUserAsync(User);
            if (LoggedInUser == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }
            var UpdatedHealthData =await _unitOfWork.HealthData.UpdateHealthData(LoggedInUser.Id, HealthDataInDto);
            if (UpdatedHealthData == null)
            {
                Result.Error = ErrorResult(400, "BadRequest", "User Not Found try again..");
                return BadRequest(Result);
            }

            Result.Content = _mapper.Map<HealthData, HealthDataOutDto>(UpdatedHealthData);
            return Ok(Result);
        }
    }
}
