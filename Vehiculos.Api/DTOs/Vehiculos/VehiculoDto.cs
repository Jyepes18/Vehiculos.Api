using System.ComponentModel.DataAnnotations;

namespace Vehiculos.Api.DTOs;

public class VehiculoDto : IValidatableObject
{
    public required string Placa { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required int Anio { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        int anioActual = DateTime.UtcNow.Year + 1;

        if (Anio < 1980)
        {
            yield return new ValidationResult("El vehiculo es muy viejo");
        }
        else if (Anio > anioActual)
        {
            yield return new ValidationResult("El año del vehiculo no es valido");
        }
    }
}