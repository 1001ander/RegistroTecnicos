using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Este campo es requerido")]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Este campo es requerido")]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage = "Este campo es requerido")]
    public string Prioridad { get; set; } = string.Empty;
    [Required(ErrorMessage = "Este campo es requerido")]
    public string Asunto { get; set; } = string.Empty;
    [Required(ErrorMessage = "Este campo es requerido")]
    public string Descripcion { get; set; } = string.Empty;
    [Required(ErrorMessage = "Este campo es requerido")]
    public double TiempoInvertido { get; set; }

}
