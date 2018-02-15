using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyPet.Models
{
    public class AnimalViewModel
    {
        public int Id;
        [Required]
        public String Nom { get; set; }

        [Required]
        [Display(Name = "Type Animal")]
        public int selectedTypeID { get; set; }
        public virtual Type_Animal Type { get; set; }
        public virtual ICollection<Type_Animal> Types { get; set; }
    }
}