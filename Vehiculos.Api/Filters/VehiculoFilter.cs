using System.ComponentModel.DataAnnotations;

namespace Vehiculos.Api.Filters;

public class VehiculoFilter : AdicionalFilter, IValidatableObject
{
    public string? Placa { get; set; }
    public int? Anio { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Anio.HasValue)
            yield break;
        
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