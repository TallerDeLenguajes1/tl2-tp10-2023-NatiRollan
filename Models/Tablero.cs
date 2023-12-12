using System;
using System.Collections.Generic;
using System.Linq;

namespace EspacioKanban;

public class Tablero
{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string? descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero()
    {
    }
    public Tablero(CrearTableroViewModel tableroAdd)
    {
        idUsuarioPropietario = tableroAdd.IdUsuarioPropietario;
        nombre = tableroAdd.Nombre;
        descripcion = tableroAdd.Descripcion;
    }
    public Tablero(ModificarTableroViewModel tableroAdd)
    {
        idUsuarioPropietario = tableroAdd.IdUsuarioPropietario;
        nombre = tableroAdd.Nombre;
        descripcion = tableroAdd.Descripcion;
    }
}