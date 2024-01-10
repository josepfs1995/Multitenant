using System.Text;
using Application;
using Application.Common.Interfaces;
using Domain.Configuration;
using Domain.Models;
using Infra;
using Infra.Data.Security;
using Infra.Data.Tenants;
using Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tenant.API.Apis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Security");

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
})
.AddEntityFrameworkStores<SecurityDbContext>();
   
builder.Services.AddDbContext<SecurityDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddDbContext<TenantDbContext>();

builder.Services.AddScoped<ISecurityDbContext>(provider => provider.GetService<SecurityDbContext>());
builder.Services.AddScoped<ITenantDbContext>(provider => provider.GetService<TenantDbContext>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddApplication();

var sectionConfig = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(sectionConfig);
var config = sectionConfig.Get<JwtConfig>();
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret)),
            ValidateIssuer = true,
            ValidIssuer = config.Issuer,
            ValidateAudience = true,
            ValidAudience = config.Audience
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/auth")
    .WithTags("Auth API")
    .MapAuthApi();

app.MapGroup("/api/{slugTenant}/product")
    .RequireAuthorization()
    .WithTags("Product API")
    .MapProductApi();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
