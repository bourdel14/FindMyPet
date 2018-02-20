namespace FindMyPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annonces",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        race = c.String(),
                        nom = c.String(),
                        description = c.String(),
                        date = c.DateTime(nullable: false),
                        photo = c.String(),
                        localisation = c.String(),
                        estRetrouve = c.Boolean(nullable: false),
                        type_animal_id = c.Int(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Type_Animal", t => t.type_animal_id)
                .ForeignKey("dbo.Utilisateurs", t => t.user_id)
                .Index(t => t.type_animal_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.Type_Animal",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libelle = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                        prenom = c.String(),
                        email = c.String(),
                        login = c.String(),
                        password = c.String(),
                        role_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Roles", t => t.role_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libelle = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Commentaires",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        annonce_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Annonces", t => t.annonce_id)
                .Index(t => t.annonce_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commentaires", "annonce_id", "dbo.Annonces");
            DropForeignKey("dbo.Annonces", "user_id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Utilisateurs", "role_id", "dbo.Roles");
            DropForeignKey("dbo.Annonces", "type_animal_id", "dbo.Type_Animal");
            DropIndex("dbo.Commentaires", new[] { "annonce_id" });
            DropIndex("dbo.Utilisateurs", new[] { "role_id" });
            DropIndex("dbo.Annonces", new[] { "user_id" });
            DropIndex("dbo.Annonces", new[] { "type_animal_id" });
            DropTable("dbo.Commentaires");
            DropTable("dbo.Roles");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Type_Animal");
            DropTable("dbo.Annonces");
        }
    }
}
