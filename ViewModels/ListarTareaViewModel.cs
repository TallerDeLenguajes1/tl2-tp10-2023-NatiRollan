using System.ComponentModel.DataAnnotations;
namespace EspacioKanban;

public class ListarTareaViewModel
{
    List<TareaViewModel> tareasVM;
    public List<TareaViewModel> TareasVM { get => tareasVM; set => tareasVM = value; }

    public ListarTareaViewModel(List<Tarea> tareas)
    {
        tareasVM = new List<TareaViewModel>();
        
        foreach (var t in tareas)
        {
            TareaViewModel tarea = new TareaViewModel(t);
            tareasVM.Add(tarea);
        }
    }
}