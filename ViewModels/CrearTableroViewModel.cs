using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class CrearTableroViewModel
{
    [Required(ErrorMessage = "Complete el campo")]
    [Display(Name = "Id Usuario Propietario")]
    public int IdUsuarioPropietario {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(100)]
    [Display(Name = "Nombre")] 
    public string Nombre {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(300)] //max 300 caracteres
    [Display(Name = "Descripcion")] 
    public string Descripcion {get;set;}

    public CrearTableroViewModel()
    {
    }

    public CrearTableroViewModel(TableroViewModel tableroNuevo)
    {
        IdUsuarioPropietario = tableroNuevo.IdUsuarioPropietario;
        Nombre = tableroNuevo.Nombre;
        Descripcion = tableroNuevo.Descripcion;
    }
}