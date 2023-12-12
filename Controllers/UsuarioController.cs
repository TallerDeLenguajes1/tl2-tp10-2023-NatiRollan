using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tl2_tp10_2023_NatiRollan.Models;

namespace EspacioKanban;

public class UsuarioController : Controller
{
    private IUsuarioRepository _usuarioRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult ListarUsuario() //mismo nombre que la vista
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else if (isAdmin())
            {
                ListarUsuarioViewModel usuarios = new ListarUsuarioViewModel(_usuarioRepository.GetAllUsuarios());
                return View(usuarios);
            }
            else
            {
                ListarUsuarioViewModel usuarios = new ListarUsuarioViewModel(_usuarioRepository.GetAllUsuarios().FindAll(u => u.Id == HttpContext.Session.GetInt32("id")));
                return View(usuarios);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        try
        {
            if (isAdmin())
            {
                return View(new CrearUsuarioViewModel());
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel usuarioNuevoVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (isAdmin())
                {
                    var usuario = new Usuario(usuarioNuevoVM);
                    _usuarioRepository.AddUsuario(usuario);
                    return RedirectToAction("Index");
                }
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return RedirectToAction("CrearUsuario");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        try
        {
            if (isAdmin())
            {
                ModificarUsuarioViewModel usuario = new ModificarUsuarioViewModel(_usuarioRepository.GetUsuario(id));
                return View(usuario);
            }
            else
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarUsuario(int id, ModificarUsuarioViewModel usuarioModificadoVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("Rol") == null)
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                else if (isAdmin())
                {
                    var usuario = new Usuario(usuarioModificadoVM);
                    _usuarioRepository.UpdateUsuario(id, usuario);
                }
                else
                {
                    if (HttpContext.Session.GetInt32("id") == id)
                    {
                        var usuario = new Usuario(usuarioModificadoVM);
                        _usuarioRepository.UpdateUsuario(id, usuario);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("ModificarUsuario");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); ;
        }
    }

    public IActionResult EliminarUsuario(int id)
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else if (isAdmin())
            {
                _usuarioRepository.DeleteUsuario(id);
                return RedirectToAction("ListarUsuario");
            }
            else
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }
        catch (Exception ex)
        {
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