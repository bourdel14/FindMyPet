namespace FindMyPet.Migrations.FMPContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Utilisateurs", name: "role_id_id", newName: "role_id");
            RenameIndex(table: "dbo.Utilisateurs", name: "IX_role_id_id", newName: "IX_role_id");
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                        type_animal_id = c.Int(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Type_Animal", t => t.type_animal_id)
                .ForeignKey("dbo.Utilisateurs", t => t.user_id)
                .Index(t => t.type_animal_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.Annonces",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        date = c.DateTime(nullable: false),
                        photo = c.String(),
                        localisation = c.String(),
                        estRetrouve = c.Boolean(nullable: false),
                        animal_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Animals", t => t.animal_id)
                .Index(t => t.animal_id);
            
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
            DropForeignKey("dbo.Annonces", "animal_id", "dbo.Animals");
            DropForeignKey("dbo.Animals", "user_id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Animals", "type_animal_id", "dbo.Type_Animal");
            DropIndex("dbo.Commentaires", new[] { "annonce_id" });
            DropIndex("dbo.Annonces", new[] { "animal_id" });
            DropIndex("dbo.Animals", new[] { "user_id" });
            DropIndex("dbo.Animals", new[] { "type_animal_id" });
            DropTable("dbo.Commentaires");
            DropTable("dbo.Annonces");
            DropTable("dbo.Animals");
            RenameIndex(table: "dbo.Utilisateurs", name: "IX_role_id", newName: "IX_role_id_id");
            RenameColumn(table: "dbo.Utilisateurs", name: "role_id", newName: "role_id_id");
        }
    }
}
