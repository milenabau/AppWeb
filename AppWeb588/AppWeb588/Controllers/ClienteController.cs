using AppWeb588.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;


namespace AppWeb588.Controllers
{
    public class ClienteController : Controller
    {
        /*
        //metodo listar//
        public IActionResult Index()
        {
            var db = new netsenaContext();
            var Clientes = db.Cliente.ToList();
            return View(Clientes);
        }
        */
        public IActionResult Index(int ? page)
        {
            var db = new netsenaContext();
            var pageNumber = page ?? 1;
            int pageSize = 6;
            var Clientes = db.Cliente.ToPagedList(pageNumber, pageSize);
            return View(Clientes);
        }


        //metodo Crear muestra el formulario//
        [Authorize]
        public IActionResult Crear()
        {
            return View();
        }

        //HttpPost tiene dos metodos: Get cuando se envia por url un dato y post cuando se envia datos por formulario//
        [HttpPost]

        //metodo crear y manda al formulario los datos//

        public IActionResult Crear(Cliente myCliente)
        {
            var db = new netsenaContext();
            var existe = db.Cliente.Find(myCliente.Codigo);
            if (existe==null)
            {
                db.Cliente.Add(myCliente);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["msj"] = $"El codigo {myCliente.Codigo} ya existe";
                return View();
            }

        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            var db = new netsenaContext();
            var myCliente = db.Cliente.Find(id);
            return View(myCliente);
        }

        [HttpPost]  //el boton es un post//
        public IActionResult Editar (Cliente objCliente)
        {
            var db = new netsenaContext();
            var myCliente = db.Cliente.Find(objCliente.Codigo);
            myCliente.Nombre = objCliente.Nombre;
            myCliente.Correo = objCliente.Correo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detalle(int id)
        {
            var db = new netsenaContext();
            var myCliente = db.Cliente.Find(id);
            return View(myCliente);
        }

        [HttpGet]
        public IActionResult Borrar(int id)
        {
            var db = new netsenaContext();
            var myCliente = db.Cliente.Find(id);
            return View(myCliente);
        }

        [HttpPost,ActionName("Borrar")]
        public IActionResult ConfirmarBorrar(int id)
        {
            var db = new netsenaContext();
            var myCliente = db.Cliente.Find(id);
            db.Remove(myCliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
