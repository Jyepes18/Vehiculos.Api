using Vehiculos.Api.DTOs;
using Vehiculos.Api.Filters;
using Vehiculos.Domain.Entities;

namespace Vehiculos.Api.Services.Interfaces;

public interface IVehiculoService
{
    Task<Result<string>> CrearVehiculo(VehiculoDto vehiculoDto);
    Task<Result<(IEnumerable<Vehiculo>, int Total)>> ObtenerVehiculos(VehiculoFilter vehiculoFilter);
    Task<Result<Vehiculo>> ObtenerVehiculo(int id);
    Task<Result<string>> DesactivarVehiculo(int id);
    Task<Result<string>> ActualizarVehiculo(int id, VehiculoDto vehiculoDto);
}