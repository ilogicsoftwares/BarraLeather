﻿using BarraLeather.Logic;
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
        public ActionResult Offers()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var allprods = db.productos.Where(x => x.status ==1).ToList();
            ViewBag.shop = JsonConvert.SerializeObject(allprods);
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            ViewBag.category = "OFERTAS";
            return View(@"~\Views\Shop\Category.cshtml");
        }
        public ActionResult Search(string nombre)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var allprods = db.productos.Where(x => x.nombre.Contains (nombre)).ToList();
            ViewBag.shop = JsonConvert.SerializeObject(allprods);
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            ViewBag.category = "Busqueda";
            return View(@"~\Views\Shop\Category.cshtml");
        }
        [Authorize]
        [HttpPost]
        public JsonResult Process()
        {
            EstatusLog estatus = new EstatusLog();
          try { 
            var actualUser = logic.ActiveUser();
              
                var selectCart = db.cart.Where(x => x.userid == actualUser.id).ToList();
               
                pedidosex pedido = new pedidosex();
            pedido.fecha = DateTime.Now;
            pedido.estatus = 0;
            pedido.userid = actualUser.id;
            pedido.total = (decimal)(selectCart.Sum(x => x.cantidad * x.productos.precio));
            pedido.subtotal =(decimal)(pedido.total);
            pedido.impuesto = 0;
            pedido.montoEntrega = 0;
            pedido.mododepago = 0;
            pedido.direccionEntrega = actualUser.direccionEntrega;

            db.pedidosex.Add(pedido);
              
                db.SaveChanges();

                var detallePedido = selectCart.Select(x => new pedidos
                {
                    userid = actualUser.id,
                    prodid = x.prodid,
                    cantidad = x.cantidad,
                    pedidoid = pedido.id,
                    subtotal=(decimal)(x.cantidad * x.productos.precio)
                });
                db.pedidos.AddRange(detallePedido);
                db.cart.RemoveRange(db.cart.Where(x => x.userid == actualUser.id));
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                estatus.success = false;
                estatus.errorMsg = "Error al Crear el pedido";
            }
            return Json(estatus);
        }

       

        // GET: Shop/Delete/5
        public ActionResult Pedidos()
        {
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            var userid = logic.ActiveUser().id;
            var pedidos = db.pedidosex.Where(x => x.userid ==userid).ToList();
            ViewBag.pedidos = JsonConvert.SerializeObject(pedidos);
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
