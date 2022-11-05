using Microsoft.AspNetCore.Mvc;

namespace Proyecto_MVC_021122.Areas.Usuario.Controllers
{
    [Area("Usuario")] //Estable al controlador al area de 'Usuario'
    public class UsuarioController : Controller
    {
        public IActionResult Usuario()
        {
            return View();
        }
    }
}
