using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tl2_tp10_2023_NatiRollan.Models;

namespace EspacioKanban;

public class TareaController : Controller
{
    private ITareaRepository _tareaRepository;
    private ITableroRepository _tableroRepository;
    private readonly ILogger<TareaController> _logger;
    
    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
    }

    [HttpGet]
    public IActionResult ListarTarea() //mismo nombre que la vista
    {
        try
        {
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                ListarTareaViewModel tareas = new ListarTareaViewModel(_tareaRepository.GetAllTareas());
                return View(tareas);
            } else
            {
                ListarTareaViewModel tareas = new ListarTareaViewModel(_tareaRepository.GetTareasPorUsuario(Int32.Parse(HttpContext.Session.GetString("Id")!)));
                return View(tareas);
            }
        } catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        try
        {
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else {
                return View(new CrearTareaViewModel());
            }
        } catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel tareaNuevaVM)
    {
        try{
            if(ModelState.IsValid){
                var tarea = new Tarea(tareaNuevaVM);
                if(isAdmin()){
                    _tareaRepository.AddTarea(tarea, tarea.IdTablero);
                    return RedirectToAction("ListarTarea");
                }
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
            return RedirectToAction("CrearTarea");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        try{
            var tarea = _tareaRepository.GetAllTareas().FirstOrDefault(t => t.Id == id);
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                return View(new ModificarTareaViewModel(tarea));
            }else{
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel tareaVM)
    {
        try{
            if(ModelState.IsValid){
                Tarea tarea = new Tarea(tareaVM);
                if(HttpContext.Session.GetString("Rol")==null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                }else if(isAdmin()){
                    _tareaRepository.UpdateTarea(tarea.Id, tarea);
                }else{
                    if(HttpContext.Session.GetInt32("id")==tarea.Id){
                        _tareaRepository.UpdateTarea(tarea.Id, tarea);
                    }
                }
                return RedirectToAction("ListarTarea");
            }
            return RedirectToAction("ModificarTarea");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult EliminarTarea(int id){
        try{
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                 _tareaRepository.DeleteTarea(id);
            }
            return RedirectToAction("ListarTarea");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    private bool isAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Admin")
        {
            return true;
        }
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}