using Microsoft.AspNetCore.Mvc;
using Vehiculos.Api.DTOs;
using Vehiculos.Api.Filters;
using Vehiculos.Api.Services.Interfaces;

namespace Vehiculos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VehiculoController : ControllerBase
{
    private readonly IVehiculoService _vehiculoService;

    public VehiculoController(IVehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }
    
    [HttpPost]
    [Route("crear")]
    public async Task<IActionResult> CrearVehiculo(VehiculoDto vehiculoDto)
    {
        return Ok(await _vehiculoService.CrearVehiculo(vehiculoDto));
    }
    
    [HttpGet]
    [Route("obtener-todos")]
    public async Task<IActionResult> ObtenerVehiculos([FromQuery] VehiculoFilter vehiculoFilter)
    {
        var resultado = await _vehiculoService.ObtenerVehiculos(vehiculoFilter);
        return Ok(new { Data = resultado.Value.Item1, Total = resultado.Value.Total });
    }
    
    [HttpGet]
    [Route("{id::int}")]
    public async Task<IActionResult> ObtenerVehiculo([FromRoute] int id)
    {
        return Ok(await _vehiculoService.ObtenerVehiculo(id));
    }
    
    [HttpDelete]
    [Route("desactivar/{id::int}")]
    public async Task<IActionResult> DesactivarVehiculo([FromRoute] int id)
    {
        return Ok(await _vehiculoService.DesactivarVehiculo(id));
    }
    
    [HttpPut]
    [Route("actualizar/{id::int}")]
    public async Task<IActionResult> DesactivarVehiculo([FromRoute] int id, [FromBody] VehiculoDto vehiculoDto)
    {
        return Ok(await _vehiculoService.ActualizarVehiculo(id, vehiculoDto));
    }
    
    [HttpPut]
    [Route("activar/{id::int}")]
    public async Task<IActionResult> ActivarVehiculo([FromRoute] int id)
    {
        return Ok(await _vehiculoService.ActivarVehiculo(id));
    }
}