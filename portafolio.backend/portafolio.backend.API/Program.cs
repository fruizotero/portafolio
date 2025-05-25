using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Contexto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext de EF core 
builder.Services.AddDbContext<ContextoPortafolio>(options => options.UseSqlServer("name=DefaultConnection"));
// DbContext de EF core

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
