using Moq;
using Vehiculos.Api.DTOs;
using Vehiculos.Api.Services;
using Vehiculos.Domain.Entities;
using Vehiculos.Infrastructure.Repositories.Interfaces;

namespace Vehiculos.Tets;

public class VehiculoServiceTests
{
    private readonly Mock<IVehiculoRepository> _repositoryMock;
    private readonly VehiculoService _service;

    public VehiculoServiceTests()
    {
        _repositoryMock = new Mock<IVehiculoRepository>();
        _service = new VehiculoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task Test_palca_existente()
    {
        // Arrange
        var vehiculoDto = new VehiculoDto()
        {
            Placa = "ABC123",
            Marca = "Toyota",
            Modelo = "Corolla",
            Anio = 2024
        };
        _repositoryMock.Setup(x => x.ExistePlaca(vehiculoDto.Placa)).ReturnsAsync(true);
        
        // Act
        var resultado = await _service.CrearVehiculo(vehiculoDto);
        
        // Assert
        Assert.False(resultado.IsSuccess);
    }
    
    
    [Fact]
    public async Task Tets_Nuevo_Vehiculo()
    {
        // Arrange
        var vehiculoDto = new VehiculoDto()
        {
            Placa = "ABC123",
            Marca = "Toyota",
            Modelo = "Corolla",
            Anio = 2024
        };
        
        _repositoryMock.Setup(x => x.ExistePlaca(vehiculoDto.Placa)).ReturnsAsync(false);
        _repositoryMock.Setup(x => x.Crear(It.IsAny<Vehiculo>())).ReturnsAsync(1);
        // Act
        var resultado = await _service.CrearVehiculo(vehiculoDto);
        
        // Assert
        Assert.True(resultado.IsSuccess);
    }
}