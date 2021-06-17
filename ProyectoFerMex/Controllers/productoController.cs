using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFerMex.Models;

namespace ProyectoFerMex.Controllers
{
    public class productoController : Controller
    {
        private contextFerMex db = new contextFerMex();

        // GET: producto
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Categoria);
            return View(producto.ToList());
        }

        // GET: producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: producto/Create
        public ActionResult Create()
        {
            ViewBag.ID_CATEGORIA = new SelectList(db.Categoria, "ID_CATEGORIA", "NOMBRE");
            return View();
        }

        // POST: producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRODUCTO,NOMBRE,DESCRIPCION,PRECIO,IMAGEN,STOCK,ID_CATEGORIA")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                int id = producto.ID_PRODUCTO;
                var prod = db.Producto.Find(id);
                DateTime hoy = DateTime.Now;
                prod.ULT_ACTUALIZACION = hoy;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CATEGORIA = new SelectList(db.Categoria, "ID_CATEGORIA", "NOMBRE", producto.ID_CATEGORIA);
            return View(producto);
        }

        // GET: producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CATEGORIA = new SelectList(db.Categoria, "ID_CATEGORIA", "NOMBRE", producto.ID_CATEGORIA);
            return View(producto);
        }

        // POST: producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRODUCTO,NOMBRE,DESCRIPCION,PRECIO,ULT_ACTUALIZACION,IMAGEN,STOCK,ID_CATEGORIA")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                int id = producto.ID_PRODUCTO;
                var prod = db.Producto.Find(id);
                var precio_ant = prod.PRECIO; ////era decimal
                var precio_act = producto.PRECIO; /////era decimal
                prod.NOMBRE = producto.NOMBRE;
                prod.DESCRIPCION = producto.DESCRIPCION;
                prod.PRECIO = producto.PRECIO;
                prod.IMAGEN = producto.IMAGEN;
                prod.STOCK = producto.STOCK;
                prod.ID_CATEGORIA = producto.ID_CATEGORIA;

                if (precio_act != precio_ant)
                {
                    DateTime hoy = DateTime.Now;
                    prod.ULT_ACTUALIZACION = hoy;
                }
                //db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CATEGORIA = new SelectList(db.Categoria, "ID_CATEGORIA", "NOMBRE", producto.ID_CATEGORIA);
            return View(producto);
        }

        // GET: producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
