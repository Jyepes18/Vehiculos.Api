using Vehiculos.Api.DTOs.Inspecciones;
using Vehiculos.Api.Filters;
using Vehiculos.Api.Services.Interfaces;
using Vehiculos.Domain.Entities;
using Vehiculos.Infrastructure.Repositories.Interfaces;

namespace Vehiculos.Api.Services;

public class InspeccionService : IInspeccionService
{
    private readonly IInspeccionRepository _inspeccionRepository;
    private readonly IVehiculoRepository _vehiculoRepository;

    public InspeccionService(IInspeccionRepository inspeccionRepository, IVehiculoRepository vehiculoRepository)
    {
        _inspeccionRepository = inspeccionRepository;
        _vehiculoRepository = vehiculoRepository;
    }
    
    public async Task<Result<string>> CrearInspeccion(int vehiculoId, InspeccionDto inspeccionDto)
    {
        var datosVehiculo = await _vehiculoRepository.ObtenerVehiculo(vehiculoId);
        if (datosVehiculo is null)
            return Result<string>.Fail("El vehículo no existe.");
        if (!datosVehiculo.Activo)
            return Result<string>.Fail("No puede hacerle inspeccion a un vehiculo que no esta activo");
        
        int? kilometrajeMaxVehiculo = await _inspeccionRepository.ObtenerKilometrajeVehiculo(vehiculoId);
        if (kilometrajeMaxVehiculo is not null)
        {
            if (inspeccionDto.Kilometraje <= kilometrajeMaxVehiculo)
                return Result<string>.Fail(
                    $"Este kilometraje {inspeccionDto.Kilometraje} no es valido, el que tiene registrado es {kilometrajeMaxVehiculo}");
        }
        
        Inspeccion inspeccion = new Inspeccion()
        {
            VehiculoId = vehiculoId,
            Fecha = DateTime.UtcNow,
            Kilometraje = inspeccionDto.Kilometraje,
            Resultado = inspeccionDto.Resultado.ToString(),
            Observaciones = inspeccionDto.Observaciones
        };

        int nuevaInspeccion = await _inspeccionRepository.CrearInspeccion(inspeccion);
        if (nuevaInspeccion == 0) return Result<string>.Fail("No se pudo insertar inspeccion");

        return Result<string>.Success("Inspeccion insertada con exito");
    }

    public async Task<Result<(IEnumerable<Inspeccion>, int Total)>> InspeccioneVehiculo(int vehiculoId,
        InspeccionFilter inspeccionFilter)
    {
        var inspeccionesVehiculo =
            await _inspeccionRepository.InspeccioneVehiculo(vehiculoId, inspeccionFilter.Page,
                inspeccionFilter.PageSize);

        return Result<(IEnumerable<Inspeccion>, int Total)>.Success((inspeccionesVehiculo.Item1,
            inspeccionesVehiculo.Total));
    }
}