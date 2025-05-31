using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }
    [DataType(DataType.DateTime)]
    [Display(Name = "Fecha de Creación")]
   
    public DateTime Fecha { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "El cliente es requerido")]
    [Display(Name = "Cliente")]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "El técnico es requerido")]
    [Display(Name = "Técnico Asignado")]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "La prioridad es requerida")]
    [StringLength(5, ErrorMessage = "La prioridad no puede exceder 5 caracteres")]
    [RegularExpression("^(Alta|Media|Baja)$", ErrorMessage = "Prioridad debe ser Alta, Media o Baja")]
    public string Prioridad { get; set; } = "Media"; 

    [Required(ErrorMessage = "El asunto es requerido")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "El asunto debe tener entre 5 y 100 caracteres")]
    public string Asunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres")]
    [DataType(DataType.MultilineText)]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tiempo invertido es requerido")]
    [Range(0.1, 100.0, ErrorMessage = "El tiempo debe estar entre 0.1 y 100 horas")]
    [Display(Name = "Tiempo Invertido (horas)")]
    public double TiempoInvertido { get; set; }

    // Propiedades de navegación
    [ForeignKey("ClienteId")]
    public virtual Clientes? Cliente { get; set; }

    [ForeignKey("TecnicoId")]
    public virtual Tecnicos? Tecnico { get; set; }
}