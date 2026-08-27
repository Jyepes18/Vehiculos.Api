using Vehiculos.Domain.Entities;

namespace Vehiculos.Infrastructure.Repositories.Interfaces;

public interface IVehiculoRepository
{
    Task<bool> ExistePlaca(string palca);
    Task<int> Crear(Vehiculo vehiculo);
    Task<(IEnumerable<Vehiculo>, int total)> ObtnerVehiculos(int page, int pageSize, int? anio, string? placa);
    Task<Vehiculo?> ObtenerVehiculo(int id);
    Task<bool> DesactivarVehiculo(Vehiculo vehiculo);
    Task<bool> ValidarPlacaRepetida(int id, string placa);
    Task<bool> ActualizarVehiculo(Vehiculo vehiculo);
    Task<bool> ActivarVehiculo(Vehiculo vehiculo);
}