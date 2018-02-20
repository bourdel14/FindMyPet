using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class AnnonceViewModel
    {
        public Annonce annonce { get; set; }
        public int SelectedTypeID { get; set; }
        public ICollection<Type_Animal> Types { get; set; }
        public Type_Animal Type { get; set; }

    }
}