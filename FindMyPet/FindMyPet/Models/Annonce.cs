using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class Annonce
    {
        public int id { get; set; }
        public String race { get; set; }
        public String nom { get; set; }
        public String description { get; set; }
        public DateTime date { get; set; }
        public String photo { get; set; }
        public String localisation { get; set; }
        public Boolean estRetrouve { get; set; }
        public virtual Utilisateur user { get; set; }
        public virtual Type_Animal type_animal { get; set; }
        public Annonce()
        {

        }
    }
}