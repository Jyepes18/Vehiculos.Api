using Moq;
using Vehiculos.Api.DTOs.Inspecciones;
using Vehiculos.Api.Services;
using Vehiculos.Domain.Entities;
using Vehiculos.Domain.Enums;
using Vehiculos.Infrastructure.Repositories.Interfaces;

namespace Vehiculos.Tets;

public class InspeccionServiceTest
{
    private readonly Mock<IInspeccionRepository> _inspeccionarRepositoryMock;
    private readonly Mock<IVehiculoRepository> _vehiculoRepositoryMock;
    private readonly InspeccionService _service;

    public InspeccionServiceTest()
    {
        _inspeccionarRepositoryMock = new Mock<IInspeccionRepository>();
        _vehiculoRepositoryMock = new Mock<IVehiculoRepository>();
        _service = new InspeccionService(_inspeccionarRepositoryMock.Object, _vehiculoRepositoryMock.Object);
        
    }

    [Fact]
    public async Task Test_Kilometraje_Invalido()
    {
        // Arrange
        int vehiculoId = 58;
        
        var dto = new InspeccionDto()
        {
            Kilometraje = 20000,
            Resultado = ResultadoInspeccion.APROBADO,
            Observaciones = "paso"
        };
        var vehiculo = new Vehiculo
        {
            Id = vehiculoId,
            Placa = "ABC123",
            Activo = true
        };

        _vehiculoRepositoryMock.Setup(x => x.ObtenerVehiculo(vehiculoId)).ReturnsAsync(vehiculo);
        _inspeccionarRepositoryMock.Setup(x => x.ObtenerKilometrajeVehiculo(vehiculoId)).ReturnsAsync(20000);
        
        // Act

        var resultado = await _service.CrearInspeccion(vehiculoId, dto);
        
        Assert.False(resultado.IsSuccess);

    }
}