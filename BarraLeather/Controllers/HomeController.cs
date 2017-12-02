using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using BarraLeather.Models;
using System.Net.Mail;
using System.Net;

namespace BarraLeather.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {

            tiendaEntities db = new tiendaEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.offerProd = JsonConvert.SerializeObject(db.productos.Where(x => x.status == 1).Take(8).ToList());
            ViewBag.NewProd = JsonConvert.SerializeObject(db.productos.Where(x => x.status == 2).Take(8).ToList());
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
               return View();
        }

        // GET: Home/Details/5
        public ActionResult About()
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Contact()
         {
            tiendaEntities db = new tiendaEntities();
            var cat = db.category.OrderBy(x => x.id).ToList();
            ViewBag.categorys = JsonConvert.SerializeObject(cat);
            ViewBag.contact = JsonConvert.SerializeObject(new models.Contact());
            return View();
        }
        [HttpPost]
        public ActionResult SendContact(models.Contact contact)
        {
          
            EstatusLog estatus = new EstatusLog();

            try
            {
                var fromAddress = new MailAddress("admin@ilogicsoftwares.com", contact.nombre + " " + contact.email);
                var toAddress = new MailAddress("barralizaraso10@gmail.com", "barraleather");
                const string fromPassword = "MINICO209#";
                string subject = "Mensaje de: " + contact.nombre;
                string body = contact.msg;

                var smtp = new SmtpClient
                { //port 587
                    Host = "smtpout.secureserver.net",
                    Port = 80,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),


                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                estatus.success = true;
                ViewBag.message = "Se ha enviado el mensaje";
             
            }
            catch (Exception)
            {
                estatus.error = true;
                ViewBag.message = "Ocurrio un error al enviar el mensaje, intente mas tarde";
             
            }


            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Msg(string message)
        {

                ViewBag.message=message;
                return View();
            
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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
