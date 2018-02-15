using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class Utilisateur
    {
        public int id { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public String email { get; set; }
        public String login { get; set; }
        public String password { get; set; }
        public virtual Role role {get;set; }

        public Utilisateur()
        {

        }
      
    }
}