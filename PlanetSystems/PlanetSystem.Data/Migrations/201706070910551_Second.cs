namespace PlanetSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtificialObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        VelocityId = c.Int(nullable: false),
                        PointId = c.Int(nullable: false),
                        Radius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointId, cascadeDelete: true)
                .ForeignKey("dbo.PlanetarySystems", t => t.PlanetarySystemId)
                .ForeignKey("dbo.Vectors", t => t.VelocityId, cascadeDelete: true)
                .Index(t => t.PlanetarySystemId)
                .Index(t => t.VelocityId)
                .Index(t => t.PointId);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        VelocityId = c.Int(nullable: false),
                        PointId = c.Int(nullable: false),
                        Radius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointId, cascadeDelete: true)
                .ForeignKey("dbo.PlanetarySystems", t => t.PlanetarySystemId)
                .ForeignKey("dbo.Vectors", t => t.VelocityId, cascadeDelete: true)
                .Index(t => t.PlanetarySystemId)
                .Index(t => t.VelocityId)
                .Index(t => t.PointId);
            
            AddColumn("dbo.Planets", "Star_Id", c => c.Int());
            CreateIndex("dbo.Planets", "Star_Id");
            AddForeignKey("dbo.Planets", "Star_Id", "dbo.Stars", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stars", "VelocityId", "dbo.Vectors");
            DropForeignKey("dbo.Planets", "Star_Id", "dbo.Stars");
            DropForeignKey("dbo.Stars", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Stars", "PointId", "dbo.Points");
            DropForeignKey("dbo.ArtificialObjects", "VelocityId", "dbo.Vectors");
            DropForeignKey("dbo.ArtificialObjects", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.ArtificialObjects", "PointId", "dbo.Points");
            DropIndex("dbo.Stars", new[] { "PointId" });
            DropIndex("dbo.Stars", new[] { "VelocityId" });
            DropIndex("dbo.Stars", new[] { "PlanetarySystemId" });
            DropIndex("dbo.Planets", new[] { "Star_Id" });
            DropIndex("dbo.ArtificialObjects", new[] { "PointId" });
            DropIndex("dbo.ArtificialObjects", new[] { "VelocityId" });
            DropIndex("dbo.ArtificialObjects", new[] { "PlanetarySystemId" });
            DropColumn("dbo.Planets", "Star_Id");
            DropTable("dbo.Stars");
            DropTable("dbo.ArtificialObjects");
        }
    }
}
