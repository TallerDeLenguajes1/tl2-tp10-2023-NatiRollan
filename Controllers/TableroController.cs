using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tl2_tp10_2023_NatiRollan.Models;

namespace EspacioKanban;

public class TableroController : Controller
{
    private ITableroRepository _tableroRepository;
    private readonly ILogger<TableroController> _logger;
    
    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
    }

    [HttpGet]
    public IActionResult ListarTablero() //mismo nombre que la vista
    {
        try{
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                List<Tablero> tableros = new List<Tablero>();
                tableros = _tableroRepository.GetAllTableros();
                return View(new ListarTableroViewModel(tableros));
            }else
            {
                var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos
                List<Tablero> miTableros = _tableroRepository.GetAllTablerosForUsuario(id);
                return View(new ListarTableroViewModel(miTableros));
            } 
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }  
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
        {
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if (isAdmin()) {
                return View(new CrearTableroViewModel());
            } else
            {
                return RedirectToRoute(new { Controller = "Home", Action = "Index"});
            }
        } catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tableroNuevoVM)
    {
        try{
            if(ModelState.IsValid){
                if(isAdmin()){
                    var tablero = new Tablero(tableroNuevoVM);
                    _tableroRepository.AddTablero(tablero);
                    return RedirectToAction("ListarTablero");
                }
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
            return RedirectToAction("CrearTablero");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        } 
    }
    
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        try{
            var tablero = _tableroRepository.GetAllTableros().FirstOrDefault(t => t.Id == id);
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                return View(new ModificarTableroViewModel(tablero));
            }else{
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tableroModificadoVM)
    {
        try{
            if(ModelState.IsValid){
                var t = _tableroRepository.GetTablero(tableroModificadoVM.Id);
                if(t != null){
                    if(HttpContext.Session.GetString("Rol") == null){
                        return RedirectToRoute(new{controller = "Login", action = "Index"});
                    }else if(isAdmin()){
                        Tablero tablero = new Tablero(tableroModificadoVM);
                        _tableroRepository.UpdateTablero(tablero.Id,tablero);
                    }else{
                        if(HttpContext.Session.GetInt32("id")==t.IdUsuarioPropietario){
                            Tablero tablero = new Tablero(tableroModificadoVM);
                            _tableroRepository.UpdateTablero(tableroModificadoVM.Id,tablero);
                        }
                    }
                }
                return RedirectToAction("ListarTablero");
            }
            return RedirectToAction("ModificarTablero");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult EliminarTablero(int id){
        try{
            if(HttpContext.Session.GetString("Rol")==null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(isAdmin()){
                 _tableroRepository.DeleteTablero(id);
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