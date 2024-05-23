using LifeTrackerAuthentication.Configuration;
using LifeTrackerDataService.Data;
using LifeTrackerDataService.IRepository;
using LifeTrackerEntities.DbSet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerAuthentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _Key;
        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        public TokenService(IConfiguration config, IOptionsMonitor<JwtConfig> optionsMonitor, UserManager<ApplicationUser> userManager, AppDbContext context = null)
        {
            _config = config;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            _jwtConfig = optionsMonitor.CurrentValue;
            _userManager = userManager;
            _context = context;
        }

        public async Task<RefreshToken> CreateRefreshToken(ApplicationUser user)
        {
            var random = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(random);
            var refreshToken =  new RefreshToken()
            {
                
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                IsRevoked = false,
                IsUsed = false,
                IsExpired = false,
                Token = Convert.ToBase64String(random),
                Status = 1,
                ApplicationUserId = user.Id,
               
            };
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            return refreshToken;
        }

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id",user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            
            var cred = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _config["Token:Issuer"],
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
                SigningCredentials = cred
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
