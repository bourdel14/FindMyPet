using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyPet.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
    }
}