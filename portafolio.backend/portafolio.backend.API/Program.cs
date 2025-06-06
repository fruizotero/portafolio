using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using portafolio.backend.API.Contexto;
using portafolio.backend.API.Utilidades;
using portafolio.backend.API.Servicios;
using portafolio.backend.API.Contexto.Repositorios;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using portafolio.backend.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Inyección de dependencias de la clase JWTHelper
builder.Services.AddScoped<JWTHelper>();
builder.Services.AddScoped<AutenticacionServicio>();
builder.Services.AddScoped<UsuariosAdministradoresRepositorio>();
builder.Services.AddScoped<PerfilServicio>();
builder.Services.AddScoped<PerfilRespositorio>();
builder.Services.AddScoped<HabilidadRepositorio>();
builder.Services.AddScoped<HabilidadServicio>();
builder.Services.AddScoped<ProyectoRepositorio>();
builder.Services.AddScoped<ProyectoServicio>();
builder.Services.AddScoped<ConocimientoRepositorio>();
builder.Services.AddScoped<ConocimientoServicio>();
builder.Services.AddScoped<EducacionRepositorio>();
builder.Services.AddScoped<EducacionServicio>();
builder.Services.AddScoped<EmpleoRepositorio>();
builder.Services.AddScoped<EmpleoServicio>();
builder.Services.AddScoped<RedSocialContactoRepositorio>();
builder.Services.AddScoped<RedSocialContactoServicio>();
builder.Services.AddScoped<ServicioImagenes, ServicioImagenesCloudinary>();

// DbContext de EF core 
builder.Services.AddDbContext<ContextoPortafolio>(options => options.UseSqlServer("name=DefaultConnection"));
// DbContext de EF core

//JWT
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = jwtAudience,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
//JWT

// 1) Leemos las credenciales desde IConfiguration:
var cloudName = builder.Configuration["Cloudinary:CloudName"];
var apiKey = builder.Configuration["Cloudinary:ApiKey"];
var apiSecret = builder.Configuration["Cloudinary:ApiSecret"];

// 2) Creamos el Account y la instancia de Cloudinary:
var account = new Account(cloudName, apiKey, apiSecret);
var cloudinary = new Cloudinary(account);

// 3) Registramos Cloudinary como singleton para inyectarlo donde haga falta:
builder.Services.AddSingleton(cloudinary);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// cors policy * para permitir cualquier origen
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
