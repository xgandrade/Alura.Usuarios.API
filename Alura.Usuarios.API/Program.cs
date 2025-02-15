using Alura.Usuarios.API.Authorization;
using Alura.Usuarios.API.Data;
using Alura.Usuarios.API.Models;
using Alura.Usuarios.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = builder.Configuration["ConnectionStrings:DBConnection"];
var policyConnectionString = builder.Configuration.GetConnectionString("Policy");

builder.Services.AddDbContext<UsuarioDbContext>(options =>
    {
        options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString));
    });

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Habilita a validação da chave de assinatura do emissor
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])), // Define a chave de assinatura para validar tokens JWT
            ValidateAudience = false, // Indica que a validação do público (audience) não será realizada
            ValidateIssuer = false, // Indica que a validação do emissor (issuer) não será realizada
            ClockSkew = TimeSpan.Zero, // Elimina a tolerância de tempo para expiração do token (evita atraso na validação)
        };
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(policyConnectionString, policy => 
            policy.AddRequirements(new IdadeMinima(18))
        );
    });

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
