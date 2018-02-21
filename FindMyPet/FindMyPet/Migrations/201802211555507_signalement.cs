namespace FindMyPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class signalement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Signalements",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        date = c.DateTime(nullable: false),
                        photo = c.String(),
                        localisation = c.String(),
                        estRetrouve = c.Boolean(nullable: false),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Utilisateurs", t => t.user_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Signalements", "user_id", "dbo.Utilisateurs");
            DropIndex("dbo.Signalements", new[] { "user_id" });
            DropTable("dbo.Signalements");
        }
    }
}
