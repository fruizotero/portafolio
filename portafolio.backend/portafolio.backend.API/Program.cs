using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using portafolio.backend.API.Contexto;
using portafolio.backend.API.Utilidades;
using portafolio.backend.API.Servicios;
using portafolio.backend.API.Contexto.Repositorios;

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
