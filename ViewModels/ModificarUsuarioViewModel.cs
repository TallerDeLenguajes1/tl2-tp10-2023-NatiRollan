using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class ModificarUsuarioViewModel
{
    public int Id;

    [Required (ErrorMessage ="Este campo es requerido")]
    [StringLength(100)]
    [Display(Name = "Nombre de Usuario")]
    public string NombreDeUsuario;

    [Required (ErrorMessage ="Este campo es requerido")]
    [StringLength(50)]
    [Display(Name = "Tipo Usuario")]
    public string Rol;

    [Required (ErrorMessage ="Este campo es requerido")]
    [StringLength(20)]
    [Display(Name = "Contraseña")]
    public string Contrasenia;

    public ModificarUsuarioViewModel()
    {
    }

    public ModificarUsuarioViewModel(Usuario usuario)
    {
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
        Rol = usuario.Rol;
    }
}