using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindMyPet.Models;
using System.IO;

namespace FindMyPet.Controllers
{
    [AllowAnonymous]
    public class SignalementsController : Controller
    {
        private FMPContext db = new FMPContext();

        // GET: Signalements
        public ActionResult Index()
        {
            return View(db.signalements.ToList());
        }

        // GET: Signalements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signalement signalement = db.signalements.Find(id);
            if (signalement == null)
            {
                return HttpNotFound();
            }
            return View(signalement);
        }

        // GET: Signalements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Signalements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SignalementViewModel signalementViewModel)
        {
            Signalement signalement = new Signalement();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                signalement.user = db.users.FirstOrDefault(u => u.id.ToString()
                    == HttpContext.User.Identity.Name);
            }
            else
            {
                signalement.nomUser = "Anonymous";
            }

            var ischecked = Request.Form["estRetrouve"];

            signalement.date = DateTime.Now;

            signalement.localisation = signalementViewModel.signalement.localisation;
            signalement.description = signalementViewModel.signalement.description;

            if (ModelState.IsValid)
            {
                db.signalements.Add(signalement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(signalement);
        }

        // GET: Signalements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signalement signalement = db.signalements.Find(id);
            if (signalement == null)
            {
                return HttpNotFound();
            }
            return View(signalement);
        }

        // POST: Signalements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description,date,photo,localisation,estRetrouve")] Signalement signalement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(signalement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(signalement);
        }

        // GET: Signalements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signalement signalement = db.signalements.Find(id);
            if (signalement == null)
            {
                return HttpNotFound();
            }
            return View(signalement);
        }

        // POST: Signalements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Signalement signalement = db.signalements.Find(id);
            db.signalements.Remove(signalement);
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
