using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FindMyPet.Models
{
    public class FMPContext : DbContext
    {
        public FMPContext() : base("FMPContext")
        { }
        public DbSet<Utilisateur> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Type_Animal> typesAnimal { get; set; }
        public DbSet<Annonce> annonces { get; set; }
        public DbSet<Commentaire> commentaires { get; set; }
        public DbSet<Signalement> signalements { get; set; }
    }
}