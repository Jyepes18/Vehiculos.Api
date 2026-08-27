using Microsoft.EntityFrameworkCore;
using Vehiculos.Domain.Entities;
using Vehiculos.Infrastructure.Persistence;
using Vehiculos.Infrastructure.Repositories.Interfaces;

namespace Vehiculos.Infrastructure.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
    private readonly AppDbContext _context;

    public VehiculoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ExistePlaca(string palca)
    {
        return await _context.vehiculos.AnyAsync(x => x.Placa == palca);
    }

    public async Task<int> Crear(Vehiculo vehiculo)
    {
        await _context.vehiculos.AddAsync(vehiculo);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<(IEnumerable<Vehiculo>, int total)> ObtnerVehiculos(int page, int pageSize, int? anio, string? placa)
    {
        var consulta =  _context.vehiculos.AsQueryable();

        if (!string.IsNullOrEmpty(placa))
            consulta = consulta.Where(f => EF.Functions.ILike(f.Placa, $"%{placa}%"));

        if (anio.HasValue)
            consulta = consulta.Where(f => f.Anio == anio);

        int totalVehiculos = await consulta.CountAsync();

        var resultado = await consulta
            .OrderByDescending(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (resultado, totalVehiculos);
    }

    public async Task<Vehiculo?> ObtenerVehiculo(int id)
    {
        return await _context.vehiculos.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> DesactivarVehiculo(Vehiculo vehiculo)
    {
        vehiculo.Activo = false;
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> ActivarVehiculo(Vehiculo vehiculo)
    {
        vehiculo.Activo = true;
        return await _context.SaveChangesAsync() > 0;
    }

    
    public async Task<bool> ValidarPlacaRepetida(int id, string placa)
    {
        return await _context.vehiculos.AnyAsync(x => x.Placa == placa && x.Id != id);
    }
    
    public async Task<bool> ActualizarVehiculo(Vehiculo vehiculo)
    {
        _context.vehiculos.Update(vehiculo);
        return await _context.SaveChangesAsync() > 0;
    }
}