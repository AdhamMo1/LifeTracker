using AspNetCoreRateLimit;
using LifeTrackerAPI.Helpers;
using LifeTrackerAuthentication.Configuration;
using LifeTrackerAuthentication.Services;
using LifeTrackerDataService.Data;
using LifeTrackerDataService.IConfiguration;
using LifeTrackerDataService.IRepository;
using LifeTrackerDataService.Repository;
using LifeTrackerEntities.DbSet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService , TokenService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Api Verisoning
builder.Services.AddApiVersioning(options =>
                { 
                      // Provides to the client the different Api versions that we have
                     options.ReportApiVersions = true;
                      // This will allow the api to automatically provide to default version
                     options.AssumeDefaultVersionWhenUnspecified = true;

                     options.DefaultApiVersion = ApiVersion.Default;
                }
);
// Rate Limit 
builder.Services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 4;
                    options.Window = TimeSpan.FromSeconds(12);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 2;
                }));
// Config JWT
// Getting the secret from the config(appsettings)
var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"]);
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,// true in production
    ValidateAudience = false,// true in production
    RequireExpirationTime = false,// true in production
    ValidateLifetime = true,
};
builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
  
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameters;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
