namespace PlanetSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtificialObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Radius = c.Double(),
                        Mass = c.Double(nullable: false),
                        Density = c.Double(),
                        PointID = c.Int(nullable: false),
                        VectorID = c.Int(nullable: false),
                        PlanetSystemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointID, cascadeDelete: true)
                .ForeignKey("dbo.PlanetSystems", t => t.PlanetSystemID, cascadeDelete: true)
                .ForeignKey("dbo.Vectors", t => t.VectorID, cascadeDelete: true)
                .Index(t => t.PointID)
                .Index(t => t.VectorID)
                .Index(t => t.PlanetSystemID);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Z = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Asteroids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Radius = c.Double(nullable: false),
                        Mass = c.Double(nullable: false),
                        Density = c.Double(nullable: false),
                        PointID = c.Int(nullable: false),
                        VectorID = c.Int(nullable: false),
                        PlanetSystemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointID, cascadeDelete: true)
                .ForeignKey("dbo.PlanetSystems", t => t.PlanetSystemID, cascadeDelete: true)
                .ForeignKey("dbo.Vectors", t => t.VectorID, cascadeDelete: true)
                .Index(t => t.PointID)
                .Index(t => t.VectorID)
                .Index(t => t.PlanetSystemID);
            
            CreateTable(
                "dbo.PlanetSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Mass = c.Double(nullable: false),
                        Density = c.Double(nullable: false),
                        PointID = c.Int(nullable: false),
                        VectorID = c.Int(nullable: false),
                        PlanetSystemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointID, cascadeDelete: true)
                .ForeignKey("dbo.Vectors", t => t.VectorID, cascadeDelete: true)
                .ForeignKey("dbo.PlanetSystems", t => t.PlanetSystemID, cascadeDelete: true)
                .Index(t => t.PointID)
                .Index(t => t.VectorID)
                .Index(t => t.PlanetSystemID);
            
            CreateTable(
                "dbo.Moons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Radius = c.Double(nullable: false),
                        Mass = c.Double(nullable: false),
                        Density = c.Double(nullable: false),
                        PointID = c.Int(nullable: false),
                        VectorID = c.Int(nullable: false),
                        PlanetID = c.Int(nullable: false),
                        PlanetSystemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointID, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.PlanetID, cascadeDelete: false)
                .ForeignKey("dbo.PlanetSystems", t => t.PlanetSystemID, cascadeDelete: true)
                .ForeignKey("dbo.Vectors", t => t.VectorID, cascadeDelete: true)
                .Index(t => t.PointID)
                .Index(t => t.VectorID)
                .Index(t => t.PlanetID)
                .Index(t => t.PlanetSystemID);
            
            CreateTable(
                "dbo.Vectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Z = c.Double(nullable: false),
                        Length = c.Double(nullable: false),
                        Theta = c.Double(nullable: false),
                        Phi = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AstronomicalBodies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Mass = c.Double(nullable: false),
                        Radius = c.Double(nullable: false),
                        PointID = c.Int(nullable: false),
                        VectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Points", t => t.PointID, cascadeDelete: true)
                .ForeignKey("dbo.Vectors", t => t.VectorID, cascadeDelete: true)
                .Index(t => t.PointID)
                .Index(t => t.VectorID);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Radius = c.Double(nullable: false),
                        Mass = c.Double(nullable: false),
                        Density = c.Double(nullable: false),
                        PointID = c.Int(nullable: false),
                        PlanetSystemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Points", t => t.PointID, cascadeDelete: true)
                .ForeignKey("dbo.PlanetSystems", t => t.PlanetSystemID, cascadeDelete: true)
                .Index(t => t.PointID)
                .Index(t => t.PlanetSystemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stars", "PlanetSystemID", "dbo.PlanetSystems");
            DropForeignKey("dbo.Stars", "PointID", "dbo.Points");
            DropForeignKey("dbo.Planets", "PlanetSystemID", "dbo.PlanetSystems");
            DropForeignKey("dbo.Planets", "VectorID", "dbo.Vectors");
            DropForeignKey("dbo.Moons", "VectorID", "dbo.Vectors");
            DropForeignKey("dbo.AstronomicalBodies", "VectorID", "dbo.Vectors");
            DropForeignKey("dbo.AstronomicalBodies", "PointID", "dbo.Points");
            DropForeignKey("dbo.Asteroids", "VectorID", "dbo.Vectors");
            DropForeignKey("dbo.ArtificialObjects", "VectorID", "dbo.Vectors");
            DropForeignKey("dbo.Moons", "PlanetSystemID", "dbo.PlanetSystems");
            DropForeignKey("dbo.Moons", "PlanetID", "dbo.Planets");
            DropForeignKey("dbo.Moons", "PointID", "dbo.Points");
            DropForeignKey("dbo.Planets", "PointID", "dbo.Points");
            DropForeignKey("dbo.Asteroids", "PlanetSystemID", "dbo.PlanetSystems");
            DropForeignKey("dbo.ArtificialObjects", "PlanetSystemID", "dbo.PlanetSystems");
            DropForeignKey("dbo.Asteroids", "PointID", "dbo.Points");
            DropForeignKey("dbo.ArtificialObjects", "PointID", "dbo.Points");
            DropIndex("dbo.Stars", new[] { "PlanetSystemID" });
            DropIndex("dbo.Stars", new[] { "PointID" });
            DropIndex("dbo.AstronomicalBodies", new[] { "VectorID" });
            DropIndex("dbo.AstronomicalBodies", new[] { "PointID" });
            DropIndex("dbo.Moons", new[] { "PlanetSystemID" });
            DropIndex("dbo.Moons", new[] { "PlanetID" });
            DropIndex("dbo.Moons", new[] { "VectorID" });
            DropIndex("dbo.Moons", new[] { "PointID" });
            DropIndex("dbo.Planets", new[] { "PlanetSystemID" });
            DropIndex("dbo.Planets", new[] { "VectorID" });
            DropIndex("dbo.Planets", new[] { "PointID" });
            DropIndex("dbo.Asteroids", new[] { "PlanetSystemID" });
            DropIndex("dbo.Asteroids", new[] { "VectorID" });
            DropIndex("dbo.Asteroids", new[] { "PointID" });
            DropIndex("dbo.ArtificialObjects", new[] { "PlanetSystemID" });
            DropIndex("dbo.ArtificialObjects", new[] { "VectorID" });
            DropIndex("dbo.ArtificialObjects", new[] { "PointID" });
            DropTable("dbo.Stars");
            DropTable("dbo.AstronomicalBodies");
            DropTable("dbo.Vectors");
            DropTable("dbo.Moons");
            DropTable("dbo.Planets");
            DropTable("dbo.PlanetSystems");
            DropTable("dbo.Asteroids");
            DropTable("dbo.Points");
            DropTable("dbo.ArtificialObjects");
        }
    }
}
