using AutoMapper;
using LifeTrackerAuthentication.Configuration;
using LifeTrackerAuthentication.Dto.Incoming;
using LifeTrackerAuthentication.Dto.Outgoing;
using LifeTrackerDataService.IConfiguration;
using LifeTrackerDataService.IRepository;
using LifeTrackerEntities.DbSet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LifeTrackerAPI.Controllers.v1
{
    public class AccountsController : BaseController
    {
        private ITokenService _tokenGenerator { get; set; }
        public AccountsController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, ITokenService tokenGenerator) : base(unitOfWork, mapper, userManager)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterationDto userRegisterationDto)
        {
            if(ModelState.IsValid)
            {
                var checkEmail = await _userManager.FindByEmailAsync(userRegisterationDto.Email);
                if (checkEmail is not null)
                {
                    return BadRequest(new AuthResultDto() { Errors = new List<string>() { "Email in use" } });
                }
                var newUser = new ApplicationUser();
                newUser.Email = userRegisterationDto.Email;
                newUser.FirstName = userRegisterationDto.FirstName;
                newUser.LastName = userRegisterationDto.LastName;
                newUser.EmailConfirmed = true;
                newUser.UserName = userRegisterationDto.FirstName;
                var isCreated = await _userManager.CreateAsync(newUser,userRegisterationDto.Password);
                if (!isCreated.Succeeded)
                {
                    return BadRequest(new AuthResultDto() {Success = isCreated.Succeeded ,Errors = isCreated.Errors.Select(x=> x.Description).ToList()});
                }
                var Token = _tokenGenerator.CreateToken(newUser);
                var RefreshToken =await _tokenGenerator.CreateRefreshToken(newUser);
                return Ok(new AuthResultDto() { Success = true,isAuthanticated=true,Token = Token,RefreshToken=RefreshToken.Token});
            }
            else
            {
                return BadRequest(new AuthResultDto() { Errors = new List<string>() { "Errors in payload" } });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto userLoginDto)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user is null)
                {
                    return NotFound(new AuthResultDto() { Errors=new List<string>() { "Error in Email or password" } });
                }
                else
                {
                    var checkPassword =  await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
                    return checkPassword ? Ok(new AuthResultDto() { isAuthanticated = true, Token = _tokenGenerator.CreateToken(user),RefreshToken=_tokenGenerator.CreateRefreshToken(user).Result.Token, Success = true }) : NotFound(new AuthResultDto() { Errors = new List<string>() { "Error in Email or password" } });
                }
            }
            else
            {
                return BadRequest(new AuthResultDto { Errors = new List<string>() { "Error in payload." } });
            }
        }
    }
}
