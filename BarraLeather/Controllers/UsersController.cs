﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarraLeather.Models;
using System.Web.Security;
using Newtonsoft.Json;

namespace BarraLeather.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Register()
        {
            tiendaEntities db = new tiendaEntities();
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            return View();
        }
        [HttpPost]
        public JsonResult Register(models.user userx)
        {
            tiendaEntities db = new tiendaEntities();
            EstatusLog estatus = new EstatusLog();
            var findUser = db.users.FirstOrDefault(x => x.nombre.Equals(userx.nombre, StringComparison.OrdinalIgnoreCase));

            if (findUser == null)
            {
                users newUser = new users
                {
                    nombre = userx.nombre,
                    clave = userx.clave,
                    correo = userx.email,
                    fecha = DateTime.Now
                };

                db.users.Add(newUser);
                db.SaveChanges();
                estatus.success = true;
            }
            else
            {
                estatus.error = true;
                estatus.errorMsg = "El usuario ya Existe...";

            }

            return Json(estatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Login(models.user user)
        {


            /// Verificar logging
            tiendaEntities db = new tiendaEntities();
            EstatusLog estatus = new EstatusLog();

            var findUser = db.users.FirstOrDefault(x => x.nombre.Equals(user.nombre, StringComparison.OrdinalIgnoreCase));

            if (findUser != null)
            {
                if (user.clave.Equals(findUser.clave))
                {

                    FormsAuthentication.SetAuthCookie(user.nombre, true);
                    estatus.success = true;
                }
                else
                {
                    estatus.error = true;
                    estatus.errorMsg = "Clave o Usuario Invalidos";
                }


            }
            else
            {
                estatus.error = true;
                estatus.errorMsg = "Usuario no existe";
            }



            return Json(estatus, JsonRequestBehavior.AllowGet);

        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }

        // POST: Users/Create
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

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Account(string username)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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