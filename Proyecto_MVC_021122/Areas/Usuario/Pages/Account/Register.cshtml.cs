using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proyecto_MVC_021122.Areas.Usuario.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> _userManager; //Creacion de objeto priv que hereda de IdentityUser
        public RegisterModel(UserManager<IdentityUser> userManager) //Constructor
        {
            _userManager = userManager; 
        }
        public void OnGet()
        {
        }
        [BindProperty] // Atrib. Vincula propiedad con vista
        public InputModel Input { get; set; }
        public class InputModel
        {
            // Atributo
            [Required(ErrorMessage ="El campo email es obligatorio.")]      //Obligatorio
            [EmailAddress]  //Formato de correo E
            [Display(Name = "Email")] //Nombre de Interfaz
            public string Email { get; set; } // Propiedad Email

            [Required(ErrorMessage = "El campo Password es obligatorio.")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            [StringLength(100,ErrorMessage ="El numero de caracteres de {0} debes ser al menos {2}.",MinimumLength =6)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password",ErrorMessage ="The password and confirmation password do not martch.")]
            public string ConfirmPassword { get; set; }
            //[Required]
            public string? ErrorMessage { get; set; }
            
        }
        public async Task<IActionResult> OnPostAsync() //Metodo asyncrono
        {
            if (ModelState.IsValid) //Validara el cumplimiento de los atributos asignados a las propiedades
            {
                // Se consultan si el email ya existe en la DBs, se guarda el resulta como lista
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count.Equals(0)) // Si no hay errores
                {
                    var user = new IdentityUser { UserName = Input.Email, Email = Input.Email }; // Guardo la informacion de la interfaz en variable
                    var result = await _userManager.CreateAsync(user, Input.Password); //Registro Asyncrono de usuario y guarda resultado
                    if (result.Succeeded) // Si no hay error en registro
                    {
                        return Page(); //Regresa pagina
                    }
                    else //Error en registro
                    {
                        foreach (var item in result.Errors) // Por cada error en la lista
                        {
                            Input = new InputModel
                            {
                                ErrorMessage = item.Description, //Se guardan mensajes de error
                            };
                        }
                    }
                } 
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = $"El {Input.Email} ya esta registrado",//Mensaje de error
                    };
                }
       
                //ModelState.AddModelError("Input.Email", "Se ha generado un error en el servidor");
            }
           // var data = Input;
            return Page();
        }

    }
}
