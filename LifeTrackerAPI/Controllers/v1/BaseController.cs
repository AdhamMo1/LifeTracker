using AutoMapper;
using LifeTrackerDataService.IConfiguration;
using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LifeTrackerAPI.Controllers.v1
{
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [EnableRateLimiting("fixed")]
   
    public class BaseController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public UserManager<ApplicationUser> _userManager;
        public BaseController( IUnitOfWork unitOfWork , IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public static Error ErrorResult(int code, string type, string message)
        {
            return new Error()
            {
                Code = code,
                Type = type,
                Message = message
            };
        }
    }
    
}
