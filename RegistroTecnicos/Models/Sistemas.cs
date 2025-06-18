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
}