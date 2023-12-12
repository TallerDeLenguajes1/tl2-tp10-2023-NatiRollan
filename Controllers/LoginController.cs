using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tl2_tp10_2023_NatiRollan.Models;

namespace EspacioKanban;

public class LoginController : Controller
{
    private IUsuarioRepository _usuarioRepository;
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository; //inyeccion de dependencia
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    public IActionResult Login(LoginViewModel userLog)
    {
        try{
            if(ModelState.IsValid){
                try{
                    var user = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreDeUsuario == userLog.NombreDeUsuario && u.Contrasenia == userLog.Contrasenia);
                    if(user == null){
                        _logger.LogWarning("Intento de acceso invalido - Usuario: "+userLog.NombreDeUsuario+" Clave ingresada: " + userLog.Contrasenia);
                        return RedirectToAction("Index");
                    }
                    _logger.LogInformation("El usuario: "+userLog.NombreDeUsuario+" ingreso correctamente");
                    LogearUsuario(user);
                    return RedirectToRoute(new{controller = "Home", action = "Index"});
                }catch(Exception ex){
                    _logger.LogError(ex.ToString());
                     return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    private void LogearUsuario(Usuario user){
        HttpContext.Session.SetString("User",user.NombreDeUsuario);
        HttpContext.Session.SetString("Rol",user.Rol);
        HttpContext.Session.SetInt32("id",user.Id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}