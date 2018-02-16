using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class UserViewModel
    {
        public Boolean Authentifie { get; set; }
        public Utilisateur user { get; set; }
    }
}