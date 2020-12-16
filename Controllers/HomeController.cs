using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registro.Models;
using Registro.Data;


namespace Registro.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebAppContext _context;

        public HomeController(WebAppContext context)
        {
            _context = context;            
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListaProductos()
        {
            List<Productos> lista= Listar();
            return View(lista);
        }

        public List<Productos> Listar()
        {
            var listaProductos= _context.Productos.ToList();
            List<Productos> lista= new List<Productos>();
            DateTime limitDate= DateTime.Today.AddDays(-7);

            foreach(Productos producto in listaProductos)
            {
                if(DateTime.Compare(producto.date,limitDate)>=0)
                {
                    lista.Add(producto);
                }
            }

            return(lista);
        }

        public IActionResult RegistroProductos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistroProductos(Productos objProducto)
        {
            objProducto.date= DateTime.Today;
            if(ModelState.IsValid){
                _context.Add(objProducto);
                _context.SaveChanges();
                return RedirectToAction("ListaProductos");
            }
            return View(objProducto);
        }

        [HttpPost]
        public IActionResult Eliminar(int id){
            var productos = _context.Productos.FirstOrDefault(x => x.id == id);
            _context.Remove(productos);
            _context.SaveChanges();
            return RedirectToAction("ListaProductos");
        }                
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
