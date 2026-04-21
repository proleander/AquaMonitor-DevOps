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

// ===============================================
// ===============================================

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Ajustado para 8080 
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
    opt.RequireHttpsMetadata = false; 
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// ===============================================
// SWAGGER – SEMPRE ATIVO
// ===============================================
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty; // Faz o Swagger abrir direto no http://localhost:8080
});

app.UseCors("AllowAll");
app.UseGlobalErrorHandling();

// PIPELINE (Sem redirecionamento HTTPS para evitar erros de certificado)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }