using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class Animal
    {
        public int id { get; set; }
        public String nom { get; set; }
        public virtual Utilisateur user { get; set; }
        public virtual Type_Animal type_animal { get; set; }

        public Animal()
        {

        }
    }
}