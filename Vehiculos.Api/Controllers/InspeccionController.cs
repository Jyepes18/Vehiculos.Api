using Microsoft.AspNetCore.Mvc;
using Vehiculos.Api.DTOs.Inspecciones;
using Vehiculos.Api.Filters;
using Vehiculos.Api.Services.Interfaces;

namespace Vehiculos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InspeccionController : ControllerBase
{
    private readonly IInspeccionService _inspeccionService;

    public InspeccionController(IInspeccionService inspeccionService)
    {
        _inspeccionService = inspeccionService;
    }

    [HttpPost]
    [Route("crear/{vehiculoId::int}")]
    public async Task<IActionResult> CrearInspeccion([FromBody] InspeccionDto inspeccionDto, [FromRoute] int vehiculoId)
    {
        return Ok(await _inspeccionService.CrearInspeccion(vehiculoId, inspeccionDto));
    }

    [HttpGet]
    [Route("obtener-todos/{vehiculoId::int}")]
    public async Task<IActionResult> CrearInspeccion([FromRoute] int vehiculoId,
        [FromQuery] InspeccionFilter inspeccionFilter)
    {
        var resultado = await _inspeccionService.InspeccioneVehiculo(vehiculoId, inspeccionFilter);
        return Ok(new { Data = resultado.Value.Item1, resultado.Value.Total });
    }
}