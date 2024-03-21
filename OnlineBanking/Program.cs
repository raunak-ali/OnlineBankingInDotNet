using OnlineBanking.Models;
using OnlineBanking.Controllers;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Interfaces;
using OnlineBanking.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OnlineBanking.Services;


 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(p => p.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(
    options=>
    {options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer=true,//Vlidates the server
        ValidateAudience=true,//Validates the 
        ValidateLifetime=true,
        ValidIssuer=builder.Configuration["Jwt:Issuer"],
        ValidAudience=builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    

    }
);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy =>
    {
        policy.RequireClaim("isAdmin", "True");
    });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BankingDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IAccountUser, AccountUserRepo>();
builder.Services.AddScoped<AccountUserServices, AccountUserServices>();

builder.Services.AddScoped<IUserProfile, UserProfileRepo>();
// /Services with a scoped lifetime are created once per client request within 
//the scope of that request. 
//This means that the same instance of the service is shared within a single HTTP request, 
//but different requests will have different instances.
builder.Services.AddScoped<UserProfileServices, UserProfileServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseAuthentication();//For Authentication
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
