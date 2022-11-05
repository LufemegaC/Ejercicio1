using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto_MVC_021122.Models;
using System.Diagnostics;

namespace Proyecto_MVC_021122.Controllers
{
    // Hereda de la clase "Controller"
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Metodo constructor (mismo nombre) (Inyeccion de interface generica(ILogger))
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Metodo(Index) ejecuta la siguiente interface que ejecuta una 
        // accion (IActionResult) y ejecutara el metodo
        // ([Vista=Index],[View=Metodo]) 
        public IActionResult Index()
        {
          //throw new Exception("This is a fcking exception!!"); //Detona expection
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        /*************************************************************************************/
        // Parametro entero,? = Permite recibir nullos | Nombre de parametro | Inicializado en nullo
        public IActionResult Error(int? statusCode = null)
        {
            //Codigo original de trata de error
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            ErrorViewModel error = null; // Inicializa componente
            //String RequestId = "";
            if (statusCode != null) // Valido si es distinto a nulo
            {
                error = new ErrorViewModel // Asigno valor de error a objeto 
                {
                    RequestId = Convert.ToString(statusCode), // Convierto y asigno codigo a string
                    ErrorMessage = "Se produjó un error al procesar su solicitud",
                };
            }
            else
            {
                //Asgina objeto de error
                var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionFeature != null)
                {
                    error = new ErrorViewModel
                    {
                        RequestId = "500",// Codigo de error
                        ErrorMessage = exceptionFeature.Error.Message, //Mensaje de exception
                    };
                }

            }
            // 02/11/22
            // Duda: Por que en el ejemplo anterior el valor de 'RequestId' se tenia 
            // que mandar en la vista , y en este caso no es asi.
            // R: Al asignarse valor a la propiedad 'RequestId', esta viaja ya en el objeto 'error'
            // siendo recibido y leido por 'Error.cshtml'
            return View(error); // Ejecuta vista llamada error
        }
    }
}