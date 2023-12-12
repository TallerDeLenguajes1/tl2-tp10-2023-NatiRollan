using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class ListarUsuarioViewModel
{
    private List<UsuarioViewModel> usuariosVM;
    public List<UsuarioViewModel> UsuariosVM { get => usuariosVM; set => usuariosVM = value; }

    public ListarUsuarioViewModel(List<Usuario> usuarios)
    {
        usuariosVM = new List<UsuarioViewModel>();

        foreach (var usuario in usuarios)
        {
            UsuarioViewModel usuarioNuevo = new UsuarioViewModel(usuario);
            usuariosVM.Add(usuarioNuevo);
        }
    }
}