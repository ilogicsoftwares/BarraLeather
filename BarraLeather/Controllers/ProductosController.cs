using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarraLeather.Models;
using System.Runtime.Serialization;

namespace BarraLeather.Controllers
{
    
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }



        // GET: Productos/Details/5
        public JsonResult Get(models.objectId prodId)
        {
            tiendaEntities db = new tiendaEntities();
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
        public ActionResult Edit(int id)
        {
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
            tiendaEntities db = new tiendaEntities();
            var prods = db.productos.ToList();
            return Json(prods,JsonRequestBehavior.AllowGet);
        }

        // GET: Productos/Delete/5
      
        public JsonResult Categorys()
        {
             
        tiendaEntities db = new tiendaEntities();
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
