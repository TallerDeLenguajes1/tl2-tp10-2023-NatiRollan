using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class CrearUsuarioViewModel
{
    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe tener entre 2 y 50 caracteres")]    
    [Display(Name = "Nombre de Usuario")] 
    public string NombreDeUsuario {get;set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    public string Contrasenia {get;set;}

    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(50)]
    [Display(Name = "Tipo Usuario")]
    public string Rol {get; set;}

    public CrearUsuarioViewModel()
    {
    }

    public CrearUsuarioViewModel(Usuario usuarioNuevo)
    {
        NombreDeUsuario = usuarioNuevo.NombreDeUsuario;
        Contrasenia = usuarioNuevo.Contrasenia;
        Rol = usuarioNuevo.Rol;
    }
}