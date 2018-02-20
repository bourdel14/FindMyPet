using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindMyPet.Models;

namespace FindMyPet.Controllers
{
    [Authorize]
    public class AnnoncesController : Controller
    {
        private FMPContext db = new FMPContext();

        [AllowAnonymous]
        // GET: Annonces
        public ActionResult Index()
        {
            return View(db.annonces.ToList());
        }

        // GET: Annonces/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        // GET: Annonces/Create
        public ActionResult Create()
        {
            AnnonceViewModel annoncevm = new AnnonceViewModel();
            var types = db.typesAnimal.ToList();
            annoncevm.Types = types;
            return View(annoncevm);
        }

        // POST: Annonces/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnnonceViewModel annoncevm)
        {
            var types = db.typesAnimal.ToList();
            annoncevm.Types = types;

            Annonce a = new Annonce();

            a.nom = annoncevm.annonce.nom;

            var typeSelected = db.typesAnimal.Find(annoncevm.SelectedTypeID);
            a.type_animal = typeSelected;
            a.description = annoncevm.annonce.description;
            a.date = DateTime.Now;
            a.estRetrouve = false;
            a.localisation = annoncevm.annonce.localisation;
            a.user = db.users.FirstOrDefault(u => u.id.ToString() == HttpContext.User.Identity.Name);

            if (ModelState.IsValid)
            {
                db.annonces.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(annoncevm);
        }

        // GET: Annonces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnonceViewModel annoncevm = new AnnonceViewModel();

            var p_annonce = db.annonces.Find(id);

            var types = db.typesAnimal.ToList();

            if (p_annonce == null)
            {
                return HttpNotFound();
            }

            annoncevm.Types = types;
            annoncevm.annonce = p_annonce;
            annoncevm.SelectedTypeID = p_annonce.type_animal.id;
            return View(annoncevm);
        }

        // POST: Annonces/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnnonceViewModel annoncevm)
        {
            Annonce a = new Annonce();
            var annonce = db.annonces.Find(annoncevm.annonce.id);
            a = annonce;

            var types = db.typesAnimal.ToList();
            annoncevm.Types = types;
            var typeSelected = db.typesAnimal.Find(annoncevm.SelectedTypeID);

            a.nom = annoncevm.annonce.nom;
            a.race = annoncevm.annonce.race;
            a.description = annoncevm.annonce.description;
            a.localisation = annoncevm.annonce.localisation;

            a.type_animal = typeSelected;

            if (ModelState.IsValid)
            {
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(annoncevm);
        }

        // GET: Annonces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        // POST: Annonces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annonce annonce = db.annonces.Find(id);
            db.annonces.Remove(annonce);
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
