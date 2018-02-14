using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class Commentaire
    {
        public int id { get; set; }
        public String description { get; set; }
        public virtual Annonce annonce { get; set; }

        public Commentaire()
        {

        }
    }
}