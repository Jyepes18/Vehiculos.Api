using Vehiculos.Api.DTOs;
using Vehiculos.Api.Filters;
using Vehiculos.Api.Services.Interfaces;
using Vehiculos.Domain.Entities;
using Vehiculos.Infrastructure.Repositories.Interfaces;

namespace Vehiculos.Api.Services;

public class VehiculoService : IVehiculoService
{
    private readonly IVehiculoRepository _vehiculoRepository;

    public VehiculoService(IVehiculoRepository vehiculoRepository)
    {
        _vehiculoRepository = vehiculoRepository;
    }
    
    public async Task<Result<string>> CrearVehiculo(VehiculoDto vehiculoDto)
    {
        bool existePlaca = await _vehiculoRepository.ExistePlaca(vehiculoDto.Placa);
        if (existePlaca) return Result<string>.Fail($"Esta palca {vehiculoDto.Placa} ya esta registrada");

        var nuevoVehiculo = new Vehiculo()
        {
            Placa = vehiculoDto.Placa.ToUpper(),
            Marca = vehiculoDto.Marca,
            Modelo = vehiculoDto.Modelo,
            Anio = vehiculoDto.Anio,
            FechaRegistro = DateTime.UtcNow,
            Activo = true
        };

        int vehiculoNuevo = await _vehiculoRepository.Crear(nuevoVehiculo);
        if(vehiculoNuevo == 0) return Result<string>.Fail("No se pudo insertar este vehiculo");

        return Result<string>.Success("Vehiculo insertado exitosamente");
    }

    public async Task<Result<(IEnumerable<Vehiculo>, int Total)>> ObtenerVehiculos(VehiculoFilter vehiculoFilter)
    {
        var resultado = await _vehiculoRepository.ObtnerVehiculos(vehiculoFilter.Page, 
            vehiculoFilter.PageSize, vehiculoFilter.Anio, vehiculoFilter.Placa);

        return Result<(IEnumerable<Vehiculo>, int Total)>.Success((resultado.Item1, resultado.total));
    }

    public async Task<Result<Vehiculo>> ObtenerVehiculo(int id)
    {
        var vehiculo = await _vehiculoRepository.ObtenerVehiculo(id);
        if (vehiculo is null) return Result<Vehiculo>.Fail("No se econtro este vehiculo");

        return Result<Vehiculo>.Success(vehiculo);
    }
    
    public async Task<Result<string>> DesactivarVehiculo(int id)
    {
        var vehiculo = await _vehiculoRepository.ObtenerVehiculo(id);
        if (vehiculo is null)
            return Result<string>.Fail("El vehículo no existe.");

        if (!vehiculo.Activo)
            return Result<string>.Fail("El vehículo ya está desactivado.");
        
        var resultado = await _vehiculoRepository.DesactivarVehiculo(vehiculo);

        if (!resultado)
            return Result<string>.Fail("No se pudo desactivar el vehículo.");

        return Result<string>.Success("Vehículo desactivado correctamente.");
    }

    public async Task<Result<string>> ActualizarVehiculo(int id, VehiculoDto vehiculoDto)
    {
        bool validarSiExistePlaca = await _vehiculoRepository.ValidarPlacaRepetida(id, vehiculoDto.Placa);
        if (validarSiExistePlaca) return Result<string>.Fail($"Esta placa {vehiculoDto.Placa} ya esta registrada");
        
        var vehiculo = await _vehiculoRepository.ObtenerVehiculo(id);
        if (vehiculo is null) return Result<string>.Fail("El vehículo no existe");

        vehiculo.Placa = vehiculoDto.Placa.ToUpper();
        vehiculo.Marca = vehiculoDto.Marca;
        vehiculo.Modelo = vehiculoDto.Modelo;
        vehiculo.Anio = vehiculoDto.Anio;
        vehiculo.FechaRegistro = DateTime.UtcNow;

        bool actualizado = await _vehiculoRepository.ActualizarVehiculo(vehiculo);
        if (!actualizado) return Result<string>.Fail("No se pudo actualizar el vehículo.");

        return Result<string>.Success("Vehículo actualizado correctamente.");
    }
    
    public async Task<Result<string>> ActivarVehiculo(int id)
    {
        var vehiculo = await _vehiculoRepository.ObtenerVehiculo(id);
        if (vehiculo is null)
            return Result<string>.Fail("El vehículo no existe.");

        if (vehiculo.Activo)
            return Result<string>.Fail("El vehículo ya está activo.");
        
        var resultado = await _vehiculoRepository.ActivarVehiculo(vehiculo);

        if (!resultado)
            return Result<string>.Fail("No se pudo activar el vehículo.");

        return Result<string>.Success("Vehículo activar correctamente.");
    }
}