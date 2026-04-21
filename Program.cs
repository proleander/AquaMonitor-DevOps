using AquaMonitor.Api.Data;
using AquaMonitor.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using AquaMonitor.Api.Validators;
using AquaMonitor.Api.Middlewares;
using AquaMonitor.Api.Filters;

var builder = WebApplication.CreateBuilder(args);


// KESTREL — Porta 80 FIXA 

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});


// CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// ORACLE + EF CORE

builder.Services.AddDbContext<AquaContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
);


// SERVICES (DI)

builder.Services.AddScoped<IConsumoAguaService, ConsumoAguaService>();


// VALIDATION (FluentValidation)

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateConsumoAguaValidator>();


// CONTROLLERS + FILTERS GLOBAIS

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<LoggingFilter>();
});


// SWAGGER 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// JWT

var key = Encoding.ASCII.GetBytes("FIAP-TOKEN-SUPER-SECRETO-2024");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false; // Necessário no Docker
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// BUILD

var app = builder.Build();


// SWAGGER — SEMPRE ATIVO!

app.UseSwagger();
app.UseSwaggerUI();

// ===============================================
// CORS
// ===============================================
app.UseCors("AllowAll");

// ===============================================
// GLOBAL ERROR HANDLER
// ===============================================
app.UseGlobalErrorHandling();

// ===============================================
// PIPELINE (SEM HTTPS NO DOCKER)
// ===============================================
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ===============================================
// RUN
// ===============================================
app.Run();

// 
public partial class Program { }
