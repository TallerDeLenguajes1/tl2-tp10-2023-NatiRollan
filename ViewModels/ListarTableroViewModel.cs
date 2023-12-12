using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class ListarTableroViewModel
{
    List<TableroViewModel> tablerosVM;
    public List<TableroViewModel> TablerosVM { get => tablerosVM; set => tablerosVM = value; }

    public ListarTableroViewModel(List<Tablero> tableros)
    {
        tablerosVM = new List<TableroViewModel>(); // inicializo el listado de tableros

        foreach (var tablero in tableros)
        {
            TableroViewModel tableroNuevo = new TableroViewModel(tablero);
            tablerosVM.Add(tableroNuevo); // // lo cargo a la lista 
        }
    }
}