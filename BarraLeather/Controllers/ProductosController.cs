using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarraLeather.Models;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BarraLeather.Controllers
{
    
    public class ProductosController : Controller
    {
        // GET: Productos
        tiendaEntities db = new tiendaEntities();
        public ActionResult Index()
        {
            return View();
        }



        // GET: Productos/Details/5
        public JsonResult Get(models.objectId prodId)
        {
           
            var prod = db.productos.FirstOrDefault(x =>x.id==prodId.id);
            return Json(prod);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
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

        // GET: Productos/Edit/5
        public ActionResult Details(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            var producto = db.productos.FirstOrDefault(x => x.id == id);
            ViewBag.producto = producto;
            ViewBag.NewProd = JsonConvert.SerializeObject(db.productos.Where(x => x.categoryid == producto.categoryid).Take(8).ToList());
            return View();
        }

        // POST: Productos/Edit/5
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
        [HttpPost]
        public JsonResult GetAll()
        {
         
            var prods = db.productos.ToList();
            return Json(prods,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRandom()
        {
            db.Configuration.ProxyCreationEnabled = false;
            Random rand = new Random();
            int toSkip = rand.Next(1, db.productos.Count());

            var prod=   db.productos.OrderBy(x=>x.id).Skip(toSkip).Take(1).First();
          
            return Json(prod, JsonRequestBehavior.AllowGet);
        }
        // GET: Productos/Delete/5

        public JsonResult Categorys()
        {
             
      
            var cat = db.category.OrderBy(x=>x.id).ToList();

            return Json(cat,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCategory(models.objectId categoryId)
        {
            tiendaEntities db = new tiendaEntities();
            var prodsByCat = db.productos.Where(x => x.categoryid==categoryId.id).ToList();

            return Json(prodsByCat);
        }
        [HttpPost]
        public JsonResult GetStatus(models.objectId statusId)
        {
            tiendaEntities db = new tiendaEntities();
            var prodsBySta = db.productos.Where(x => x.status == statusId.id).ToList();

            return Json(prodsBySta);
        }
        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Productos/Delete/5
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
