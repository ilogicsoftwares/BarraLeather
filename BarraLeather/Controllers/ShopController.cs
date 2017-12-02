using BarraLeather.Logic;
using BarraLeather.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BarraLeather.Controllers
{
    public class ShopController : Controller
    {
        tiendaEntities db = new tiendaEntities();
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        // GET: Shop/Details/5
        [Authorize]
        public ActionResult Cart()
        {
           
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            return View();
            
        }

        // GET: Shop/Create
        [HttpPost]
        public JsonResult AddItem(productos producto,int cantidad)
        {
            EstatusLog estatus = new EstatusLog();
            cart carrito = new cart();
            if (logic.ActiveUser() == null)
            {
                estatus.success = false;
                estatus.errorMsg = "NotUser";
                return Json(estatus);
            }
            carrito.userid = logic.ActiveUser().id;
            carrito.prodid = producto.id;
            carrito.cantidad = cantidad;
            carrito.fecha = DateTime.Now;
            try {
                db.cart.Add(carrito);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                estatus.success = false;
                estatus.errorMsg = ex.Message;
            }
            return Json(estatus);
        }
        
        [Authorize]
        [HttpPost]
        public JsonResult DeleteItem(int id)
        {
            EstatusLog estatus = new EstatusLog();
            try {
                cart carrito = new cart { id = id };
                db.Entry(carrito).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                estatus.success = false;
                estatus.errorMsg = "Error al eliminar el item del Carrito";
            }
           
            return Json(estatus);
        }

        public JsonResult GetUserCart()
        {
           
            var userid = logic.ActiveUser().id;
            var carrito = db.cart.Where(x => x.userid == userid).Select(x => new
            {
                id = x.id,
                x.prodid,
                nombre = x.productos.nombre,
                foto = x.productos.foto,
                cantidad = x.cantidad,
                precio = x.productos.precio,
                subtotal = x.productos.precio * x.cantidad
            }
                              
                ).ToList();

            return Json(carrito, JsonRequestBehavior.AllowGet);
        }

        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Category()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var allprods = db.productos.ToList();
            ViewBag.shop= JsonConvert.SerializeObject(allprods);
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            ViewBag.category = "Todas";
            return View();
        }
        
        public ActionResult GetCategory(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var allprods = db.productos.Where(x=>x.categoryid==id).ToList();
            ViewBag.shop = JsonConvert.SerializeObject(allprods);
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            ViewBag.category = cat.FirstOrDefault(x=>x.id==id).name;
            return View(@"~\Views\Shop\Category.cshtml");
        }

        // POST: Shop/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
