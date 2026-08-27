using Microsoft.EntityFrameworkCore;
using Vehiculos.Domain.Entities;
using Vehiculos.Infrastructure.Persistence;
using Vehiculos.Infrastructure.Repositories.Interfaces;

namespace Vehiculos.Infrastructure.Repositories;

public class InspeccionRepository : IInspeccionRepository
{
    private readonly AppDbContext _context;

    public InspeccionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> ObtenerKilometrajeVehiculo(int vehiculoId)
    {
        return await _context.inspecciones
            .Where(x => x.VehiculoId == vehiculoId)
            .Select(x => (int?)x.Kilometraje)
            .MaxAsync();
    }

    public async Task<int> CrearInspeccion(Inspeccion inspeccion)
    {
        await _context.inspecciones.AddAsync(inspeccion);
        return await _context.SaveChangesAsync();
    }

    public async Task<(IEnumerable<Inspeccion>, int Total)> InspeccioneVehiculo(int vehiculoId, int page, int pageSize)
    {
        var consulta = _context.inspecciones.AsQueryable();
        int totalInspeccionesVehiculo = await consulta.Where(x => x.VehiculoId == vehiculoId).CountAsync();
        var resultado = await consulta
            .OrderByDescending(f => f.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Where(x=> x.VehiculoId == vehiculoId)
            .ToListAsync();

        return (resultado, totalInspeccionesVehiculo);
    }
}  