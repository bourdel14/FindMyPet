using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class Signalement
    {
        public int id { get; set; }
        public String description { get; set; }
        public DateTime date { get; set; }
        public String photo { get; set; }
        public String localisation { get; set; }
        public Boolean estRetrouve { get; set; }
        public Utilisateur user { get; set; }
        public String nomUser;
    }
}