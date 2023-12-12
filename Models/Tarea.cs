using System;
using System.Collections.Generic;
using System.Linq;

namespace EspacioKanban;

public enum EstadoTarea
{
    Ideas,
    ToDo,
    Doing,
    Review,
    Done
}

public class Tarea
{
    private int id;
    private int idTablero;
    private string nombre;
    private EstadoTarea estado;
    private string? descripcion;
    private string? color;
    private int? idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    public string? Color { get => color; set => color = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    public Tarea()
    {
    }

    public Tarea(CrearTareaViewModel t)
    {
        IdTablero = t.IdTablero;
        Nombre = t.Nombre;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Estado = t.Estado;
        IdUsuarioAsignado = t.IdUsuarioAsignado;
    }

    public Tarea(ModificarTareaViewModel t)
    {
        IdTablero = t.IdTablero;
        Nombre = t.Nombre;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Estado = t.Estado;
        IdUsuarioAsignado = t.IdUsuarioAsignado;
    }
}