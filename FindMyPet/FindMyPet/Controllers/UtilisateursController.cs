using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FindMyPet.Models;

namespace FindMyPet.Controllers
{
    public class UtilisateursController : Controller
    {
        private FMPContext db = new FMPContext();

        public ActionResult Login()
        {
            UserViewModel uservm = new UserViewModel();
            uservm.Authentifie = HttpContext.User.Identity.IsAuthenticated;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                uservm.user = GetUser(HttpContext.User.Identity.Name);
            }
            return View(uservm);
        }

        [HttpPost]
        public ActionResult Login(UserViewModel uservm, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = Authentifier(uservm.user.login, uservm.user.password);
                if (utilisateur != null)
                {
                    FormsAuthentication.SetAuthCookie(utilisateur.id.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                ModelState.AddModelError("Utilisateur.Login", "Prénom et/ou mot de passe incorrect(s)");
            }
            return View(uservm);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        // GET: Utilisateurs
        public ActionResult Index()
        {
            var user = db.users.Include(c => c.role);
            return View(db.users.ToList());
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.users.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur utilisateur)
        {
            var role = db.roles.Find(1);
            if (ModelState.IsValid)
            {
                utilisateur.role = role;
                db.users.Add(utilisateur);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(utilisateur.id.ToString(), false);
                return RedirectToAction("Index");
            }
            
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.users.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,email,login,password")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.users.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.users.Find(id);
            db.users.Remove(utilisateur);
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

        public Utilisateur Authentifier(String login, String mdp)
        {
            return db.users.FirstOrDefault(u => u.login == login && u.password == mdp);
        }

        public Utilisateur GetUser(int id)
        {
            return db.users.FirstOrDefault(u => u.id == id);
        }

        public Utilisateur GetUser(string idString)
        {
            int id;
            if (int.TryParse(idString, out id))
                return GetUser(id);
            return null;
        }
        [Authorize]
        public ActionResult Compte()
        {
            UserViewModel uservm = new UserViewModel();
            Utilisateur user = db.users.FirstOrDefault(u => u.id.ToString() == HttpContext.User.Identity.Name);

            uservm.user = user;

            return View(uservm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compte(UserViewModel uservm)
        {
            Utilisateur user = new Utilisateur();
            var p_user = db.users.FirstOrDefault(u => u.id.ToString() == HttpContext.User.Identity.Name);

            user = p_user;

            if (uservm.newPassword != user.password && uservm.newPassword == uservm.confirmPassword)
            {
                user.password = uservm.newPassword;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Compte");
            }

            return View(uservm);
        }

    }
}
