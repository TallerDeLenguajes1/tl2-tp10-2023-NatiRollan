using System;
using System.Collections.Generic;
using System.Linq;

namespace EspacioKanban;

public class Usuario
{
    private int id;
    private string? nombreDeUsuario;
    private string rol;
    private string contrasenia;

    public int Id { get => id; set => id = value; }
    public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Rol { get => rol; set => rol = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public Usuario()
    {
    }

    public Usuario(CrearUsuarioViewModel viewUsuarioAdd)
    {
        nombreDeUsuario = viewUsuarioAdd.NombreDeUsuario;
        rol = viewUsuarioAdd.Rol;
        Contrasenia = viewUsuarioAdd.Contrasenia;

    }
    public Usuario(ModificarUsuarioViewModel viewUsuarioUpdate)
    {
        Id = viewUsuarioUpdate.Id;
        NombreDeUsuario = viewUsuarioUpdate.NombreDeUsuario;
        Rol = viewUsuarioUpdate.Rol;
        Contrasenia = viewUsuarioUpdate.Contrasenia;

    }
}