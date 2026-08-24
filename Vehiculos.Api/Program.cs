using Microsoft.EntityFrameworkCore;
using Vehiculos.Api.Services;
using Vehiculos.Api.Services.Interfaces;
using Vehiculos.Infrastructure.Persistence;
using Vehiculos.Infrastructure.Repositories;
using Vehiculos.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();

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