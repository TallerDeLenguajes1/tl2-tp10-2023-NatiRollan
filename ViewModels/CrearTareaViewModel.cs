using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class CrearTareaViewModel
{
    [Required (ErrorMessage ="este campo es requerido")]
    [Display(Name = "Id Tablero")]
    public int IdTablero {get;set;}

    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre {get;set;}

    [StringLength(1000)]
    [Display(Name = "Descripcion")]
    public string Descripcion {get;set;}

    [Required (ErrorMessage ="este campo es requerido")]
    [Display(Name = "Color")]
    public string Color {get;set;}

    [Required (ErrorMessage ="este campo es requerido")]
    [Display(Name = "Estado Tarea")]
    public EstadoTarea Estado {get;set;}

    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioAsignado {get;set;}

    public CrearTareaViewModel()
    {
    }

    public CrearTareaViewModel(Tarea tarea)
    {
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Color = tarea.Color;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}