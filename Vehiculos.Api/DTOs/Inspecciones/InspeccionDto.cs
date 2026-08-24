using System.ComponentModel.DataAnnotations;
using Vehiculos.Domain.Enums;

namespace Vehiculos.Api.DTOs.Inspecciones;

public class InspeccionDto : IValidatableObject
{
    public required int Kilometraje { get; set; }

    public required ResultadoInspeccion Resultado { get; set; }
    [StringLength(1000)]
    public string? Observaciones { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Kilometraje <= 0)
            yield return new ValidationResult("El kilometraje no puede ser menor o igual a 0");
    }
}