using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Models;
public class Sistemas
{
    [Key]
    public int SistemaId { get; set; }

    [Required(ErrorMessage = "Campo Descripción obligatorio")]
    [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Campo Complejidad obligatorio")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public string? Complejidad { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Debe agregar la cantidad en Existencia de este Sistema")]
    [Range(1, 10000)]
    public int Existencia { get; set; }

    [Required(ErrorMessage = "Debe agregar el precio del Sistema")]
    public decimal precio { get; set; }

    public ICollection<VentasDetalle> VentasDetalle { get; set; } = new List<VentasDetalle>();
}