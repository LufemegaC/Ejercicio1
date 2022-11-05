using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_MVC_021122.Data;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Proyecto_MVC_021122ContextConnection") ?? throw new InvalidOperationException("Connection string 'Proyecto_MVC_021122ContextConnection' not found.");

builder.Services.AddDbContext<Proyecto_MVC_021122Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Proyecto_MVC_021122Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// 
if (app.Environment.IsDevelopment())
{
    // Ejemplo: Obtiene codigo y mensaje de exception creada a drede en el metodo 'Index' de 
    // controlador 'Home'
    /* app.UseExceptionHandler(options => // Funcion flecha / Se envia funcion como parametro
     {
         options.Run(async context => //Ejecuta asyncrono
         {
             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //Convierte error en entero
             context.Response.ContentType = "text/html"; // Texto tipo html
             var ex = context.Features.Get<IExceptionHandlerFeature>(); //Se captura error
             if (ex != null) // Si existe error
             {
                 // Se crea variable para construir html
                 var error = $"<h1> MSJ SError: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                 await context.Response.WriteAsync(error).ConfigureAwait(false); // Im
             }
         });
     });*/
    //app.UseExceptionHandler("/Home/Error"); // Ejecuta metodo Erroe en controlador HomeController
}

else  
{
    app.UseExceptionHandler("/Home/Error"); //Llama metodo 'Error' en controlador 'Home'
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Ejemplo 1: Capturar error de URL no encontrada y cambiar mensaje
//app.UseStatusCodePages("text/plain", "Pagina de codigos de estado, codigo de estado: {0}");

//Ejemplo 2: Capturar error de URL no encontrada y cambiar mensaje
/*app.UseStatusCodePages(async context =>
    {
        await context.HttpContext.Response.WriteAsync(
            "Pagina de codigos de estado, codigo de estado:" + //Mensaje de error
            context.HttpContext.Response.StatusCode // Codigo de error
);
    });*/

//Ejemplo 3: Redireccionar pagina cuando sucede error y envia el codigo de error
//app.UseStatusCodePagesWithRedirects("/Usuarios/Metodo?code={0}");

//app.UseStatusCodePagesWithReExecute("/Shared/Error","?code={0}");
// Duda: Por que si pongo la cartepa "Shared" marca error, al contratio no se observa que la
// pagina Error exista en la carpeta "Home", pero esta si funciona.
// R: En la unidad 'HomeController' (/Home) se encuentra el metodo Error
// este metodo ejecuta a su vez una vista y envia parametros que extrae en un objeto 
// de error. SUPONGO que el nombre de 'RequestId = Activity.Current' es 'Error', por eso se
// ejecuta la vista del mismo nombre
//app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/

//app.UseEndpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute("Usuarios", "Usuario", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute("Usuarios", "Usuario", "{controller=Usuario}/{action=Usuario}/{id?}");
    //endpoints.MapAreaControllerRoute("areas", "areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});
// Entity
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
