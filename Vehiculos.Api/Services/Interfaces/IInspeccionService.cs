using Vehiculos.Api.DTOs.Inspecciones;
using Vehiculos.Api.Filters;
using Vehiculos.Domain.Entities;

namespace Vehiculos.Api.Services.Interfaces;

public interface IInspeccionService
{
    Task<Result<string>> CrearInspeccion(int vehiculoId, InspeccionDto inspeccionDto);

    Task<Result<(IEnumerable<Inspeccion>, int Total)>> InspeccioneVehiculo(int vehiculoId,
        InspeccionFilter inspeccionFilter);
}