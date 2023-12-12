using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class TableroViewModel
{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string? descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }

    public TableroViewModel()
    {
    }

    public TableroViewModel(Tablero tablero)
    {
        id = tablero.Id;
        idUsuarioPropietario = tablero.IdUsuarioPropietario;
        nombre = tablero.Nombre;
        descripcion = tablero.Descripcion;
    }
}