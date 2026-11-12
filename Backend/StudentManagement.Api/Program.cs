
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Api.Configuration;
using StudentManagement.Api.Data;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Services;
using StudentManagement.Api.Middleware;
using StudentManagement.Api.Models;

var builder = WebApplication.CreateBuilder(args);
// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

// Get JWT settings
var jwtSettings = builder.Configuration
    .GetSection(JwtSettings.SectionName)
    .Get<JwtSettings>() ?? new JwtSettings();

// Validate JWT Secret Key
if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey)
    || Encoding.UTF8.GetByteCount(jwtSettings.SecretKey) < 32)
{
    throw new InvalidOperationException(
        "Configure Jwt:SecretKey with a secret of at least 32 UTF-8 bytes using user-secrets or an environment variable.");
}

// Validate JWT configuration
if (string.IsNullOrWhiteSpace(jwtSettings.Issuer)
    || string.IsNullOrWhiteSpace(jwtSettings.Audience)
    || jwtSettings.ExpirationInMinutes <= 0)
{
    throw new InvalidOperationException(
        "Configure Jwt:Issuer, Jwt:Audience, and a positive Jwt:ExpirationInMinutes value.");
}

// Register JWT settings
builder.Services.AddSingleton(jwtSettings);

// Configure JWT Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

// Register services
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

// Build application
var app = builder.Build();

// Global exception middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
