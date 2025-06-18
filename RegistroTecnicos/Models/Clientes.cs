using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
  
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "solo se permiten Letras")]
    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? Nombres { get; set; }
   
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "solo se permiten Letras")]
    [Required(ErrorMessage = "Campo Obligatorio")]
    public string? Direccion { get; set; }

    [StringLength(11, ErrorMessage = "El Rnc no debe tener mas de 11 numeros")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten numeros")]
    [Required(ErrorMessage = "Campo Obligatorio")]

    public string? Rnc { get; set; }

    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten numeros")]
    [Required(ErrorMessage = "Campo Obligatorio")]
    public decimal? LimiteCredito { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [ForeignKey("TecnicoId")]
    public int TecnicoId { get; set; }
    public Tecnicos? Tecnico { get; set; }





}
