namespace PostgreSQLData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstSuccesfullPostgreMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OwnPlanetarySystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OwnPlanetarySystems", "UserID", "dbo.Users");
            DropIndex("dbo.OwnPlanetarySystems", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.OwnPlanetarySystems");
        }
    }
}
