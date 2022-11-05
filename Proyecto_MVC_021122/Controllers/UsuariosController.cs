using Microsoft.AspNetCore.Mvc;

namespace Proyecto_MVC_021122.Controllers
{
    public class UsuariosController : Controller
    {
        // EJEMPLO 1: Envio de dos parametros por URL, envio por 'ViewData'
        /*
          https://localhost:7039/Usuarios/Index?nomb=LuisF&age=32
         * public IActionResult Index(String Nomb,int age)
        {
        	//[HttpGet]
        	//[HttpPost]
        	ViewData["id"] = "Nombre"+Nomb+",edad:"+age;
            return View(); // El objeto se envia por ViewData["id"]
        }*/
        // EJEMPLO 2: Envio de dos parametros por URL, envio por 'View'
        // https://localhost:7039/Usuarios/Index?nomb=LuisF&age=32
        /*public IActionResult Index(String nomb, int age)
        {
            //[HttpGet]
            //[HttpPost]
            String datos = "Nombre: " + nomb + ",Edad: " + age;
            return View("Index",datos); // Se envia el objeto por la vista
        }*/
        // EJEMPLO 3: Cambio de ruta
        //https://localhost:7039/Usuario/MendezG?data=Coki&age=98
        /*[Route("/Usuario/MendezG")] // Codigo para activar ruta
        public IActionResult Index(string data, int age)
        {
            String datos = "data: " + data + " age: " + age;
            return View("Index",datos);
            }
        */
        // EJEMPLO 4:Cambio de ruta + envio de dato por URL
        /*https://localhost:7039/Usuarios/Felipe/age=98
        [Route("/Usuarios/Luis")]
        [Route("/Usuarios/Felipe/{data}")]
        public IActionResult Index(String data, int age)
        {
            String datos =data + " "+ age;
            return View("Index",datos);
        }
        public IActionResult Index()
        {
            return View();
        }*/
        // EJEMPLO 5: Ruta para indicar controlador y vista + parametro
        /*[Route("/Usuarios/LuisF")]
        [Route("[controller]/[action]/{data}")]
        public IActionResult Index(String data,int age)
        {
            String datos = data + "-" + age;
            return View("Index",datos);
        }
        public IActionResult Index()
        {
            return View();  
        }*/

        // EJEMPLO 6: Especificar tipo de variable enviada por URL
        /*://localhost:7039/Usuarios/Index/8.5
        [HttpGet("[controller]/[action]/{data:double}")]
        public IActionResult Index(double data)
        {
            return View("Index",data);  
        }*/

        // EJEMPLO 7: Creacion de URL de metodo personalizada
        /* //localhost:7039/Usuarios
         * public IActionResult Index(double data)
        {
            var url = Url.Action("Metodo");
            return Content(url);   
        }
        // R: Usuarios/Metodo
         */

        // EJEMPLO 8: Ejecutar una vista redireccionando URL
        /* https://localhost:7039/Usuarios
        public IActionResult index()
        {
            // Url.Action(Nombre de Metodo,Nombre de Controlador)
            var url = Url.Action("Metodo","Usuarios");
            // Ejecuta la ruta : Usuarios/Metodo
            return Redirect(url);
        }
        public IActionResult Metodo()
        {
            return View(); // Ejecuta la vista con el mismo nombre
        }  */

        // EJEMPLO 9:Construye y ejecuta URL con envio de parametros  
        /*public IActionResult Index()
        {
            // Construye URL :/Usuarios/Metodo?age=52&name=LuisF
            var url = Url.Action("Metodo", "Usuarios", new { age = 52, name = "LuisF" });

            return Redirect(url);
        }
        public IActionResult Metodo(int age, String name)
        {
            var data = $"Nombre {name}, edad {age}"; // Construye cadena
            return View("Index",data); // Ejecuta Vista 'Index' y manda parametro 'Data'
        }

        */

        // EJEMPLO 10:
        public IActionResult Index()
        {
        // Construe URL : LuisF?age=27&name=LuisFelipe
        var url = Url.RouteUrl("LuisF", new { age = 27, name = "LuisFelipe" });
            return Redirect(url);
        }
        // Para ejecutar Metodo se debe enviar como URL LuisF + parametros
        [HttpGet("[controller]/[action]",Name ="LuisF")]
        public IActionResult Metodo(int age, String name)
        {
            var data = $"Nombre: {name}, age: {age}";
            return View("Index",data); //Ejecuta vista Metodo
        }

    }
}
