using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class ModificarTableroViewModel
{
    public int Id {get;set;}

    [Required (ErrorMessage ="este campo es obligatorio.")]
    [Display(Name = "Id Usuario Propietario")]
    public int IdUsuarioPropietario {get;set;}

    [Required (ErrorMessage ="este campo es obligatorio.")]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre {get;set;}

    [StringLength(300)]
    [Display(Name = "Descripcion")]
    public string Descripcion {get;set;}

    public ModificarTableroViewModel()
    {
    }

    public ModificarTableroViewModel(Tablero tablero)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }

}