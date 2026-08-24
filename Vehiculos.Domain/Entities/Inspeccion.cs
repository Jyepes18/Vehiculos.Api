using Vehiculos.Domain.Enums;

namespace Vehiculos.Domain.Entities;

public class Inspeccion
{
    public int Id { get; set; }

    public int VehiculoId { get; set; }

    public DateTime Fecha { get; set; }

    public int Kilometraje { get; set; }

    public string Resultado { get; set; }

    public string? Observaciones { get; set; }

}