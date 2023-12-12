using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class UsuarioViewModel
{
    private int id;
    private string? nombreDeUsuario;
    private string rol;
   // private string contrasenia;

    public int Id { get => id; set => id = value; }
    public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Rol { get => rol; set => rol = value; }
   // public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public UsuarioViewModel()
    {
    }

    public UsuarioViewModel(Usuario usuario)
    {
        id = usuario.Id;
        nombreDeUsuario = usuario.NombreDeUsuario;
        rol = usuario.Rol;
       // contrasenia = usuario.Contrasenia;
    }
}