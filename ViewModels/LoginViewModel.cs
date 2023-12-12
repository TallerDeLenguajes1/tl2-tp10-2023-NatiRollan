using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo es obligatorio.")]
    [Display(Name = "Nombre de usuario")]
    public string NombreDeUsuario {get; set;}

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [Display(Name = "Contraseña")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres.")]
    public string Contrasenia {get; set;}
}