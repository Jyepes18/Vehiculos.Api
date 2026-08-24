using Vehiculos.Domain.Entities;

namespace Vehiculos.Infrastructure.Repositories.Interfaces;

public interface IInspeccionRepository
{
    Task<int?> ObtenerKilometrajeVehiculo(int vehiculoId);
    Task<int> CrearInspeccion(Inspeccion inspeccion);
    Task<(IEnumerable<Inspeccion>, int Total)> InspeccioneVehiculo(int vehiculoId, int page, int pageSize);
}