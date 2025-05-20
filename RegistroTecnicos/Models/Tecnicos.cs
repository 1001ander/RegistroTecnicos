using System.ComponentModel.DataAnnotations;
using System.IO.Compression;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Debe tener al menos un carácter")]
    [RegularExpression(@"\S.*", ErrorMessage = "No se permiten espacios en blanco únicamente")]
    public string Nombres { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Debe ser mayor que cero")]
    public decimal SueldoHora { get; set; }
}


