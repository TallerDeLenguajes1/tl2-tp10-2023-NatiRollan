namespace EspacioKanban;    

public interface ITareaRepository{
    public void AddTarea(Tarea tarea, int idTablero);
    public void UpdateTarea(int idTarea, Tarea tarea);
    public Tarea GetTarea(int idTarea);
    public List<Tarea> GetTareasPorUsuario(int idUsuario);
    public List<Tarea> GetTareasPorTablero(int idTablero);
    public void DeleteTarea(int idTarea);
    public void AssingTarea(int idUsuario, int idTarea);
    public void UpdateEstadoTarea(int idTarea, EstadoTarea estado);
    public List<Tarea> GetAllTareas();
}