using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFerMex.Models;

namespace ProyectoFerMex.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private contextFerMex db = new contextFerMex();
        // GET: Usuario
        public ActionResult Index(String email)
        {
            if (User.Identity.IsAuthenticated)
            {
                String correo = email;
                String rol = "";

                using (db)
                {
                    var query = from st in db.Empleado
                                where st.CORREO == correo
                                select st;
                    var lista = query.ToList();
                    if (lista.Count > 0)
                    {
                        var empleado = query.FirstOrDefault<Empleado>();
                        String[] nombres = empleado.NOMBRE.ToString().Split(' ');
                        Session["name"] = nombres[0];
                        Session["usr"] = empleado.NOMBRE;
                        rol = empleado.ROL.ToString().TrimEnd();
                    }
                    else
                    {
                        var query1 = from st in db.Cliente
                                     where st.CORREO == correo
                                     select st;
                        var lista1 = query.ToList();
                        if (lista1.Count > 0)
                        {
                            var cliente = query1.FirstOrDefault<Cliente>();
                            String[] nombres = cliente.NOMBRE.ToString().Split(' ');
                            Session["name"] = nombres[0];
                            Session["usr"] = cliente.NOMBRE;
                            rol = "Cliente";
                        }
                    }
                }
                if (rol == "Administrador de productos y categorias")
                {
                    return RedirectToAction("Index", "Compras");
                }
                if (rol == "Administrador de envios")
                {
                    return RedirectToAction("Index", "Envios");
                }
                if (rol == "Cliente")
                {
                    return RedirectToAction("Index", "Home");
                }
                if (rol == "Administrador general")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}