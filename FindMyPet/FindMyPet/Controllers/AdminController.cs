using FindMyPet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FindMyPet.Controllers
{
    public class AdminController : Controller
    {
        private FMPContext db = new FMPContext();

        public ActionResult Index()
        {
            return View(db.typesAnimal.ToList());
        }
        public ActionResult CreateTypeAnimal()
        {
            return View();
        }

        public ActionResult EditTypeAnimal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Type_Animal type = db.typesAnimal.Find(id);


            if (type == null)
            {
                return HttpNotFound();
            }

            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTypeAnimal(Type_Animal type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTypeAnimal(Type_Animal type)
        {
            if (ModelState.IsValid)
            {
                db.typesAnimal.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(type);
        }

        public ActionResult DeleteTypeAnimal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type_Animal type = db.typesAnimal.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("DeleteTypeAnimal")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Type_Animal type = db.typesAnimal.Find(id);
            db.typesAnimal.Remove(type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}