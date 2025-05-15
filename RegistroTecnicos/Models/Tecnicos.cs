using System.ComponentModel.DataAnnotations;
using System.IO.Compression;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicosId { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "solo se permiten letras ")]
    [Required(ErrorMessage = "Campo requerido")]
    public string? Nombres { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor que cero")]
    [Required(ErrorMessage = "Campo requerido")]    
    public decimal SueldoHora { get; set; }
}
