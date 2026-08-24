using Microsoft.EntityFrameworkCore;
using Vehiculos.Domain.Entities;

namespace Vehiculos.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Vehiculo> vehiculos => Set<Vehiculo>();
    public DbSet<Inspeccion> inspecciones => Set<Inspeccion>();
}