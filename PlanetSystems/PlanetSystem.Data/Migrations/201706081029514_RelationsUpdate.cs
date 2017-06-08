namespace PlanetSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationsUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtificialObjects",
                c => new
                    {
                        ArtificialObjectId = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        Velocity_X = c.Double(nullable: false),
                        Velocity_Y = c.Double(nullable: false),
                        Velocity_Z = c.Double(nullable: false),
                        Velocity_Length = c.Double(nullable: false),
                        Velocity_Theta = c.Double(nullable: false),
                        Velocity_Phi = c.Double(nullable: false),
                        PointId = c.Int(nullable: false),
                        Center_X = c.Double(nullable: false),
                        Center_Y = c.Double(nullable: false),
                        Center_Z = c.Double(nullable: false),
                        Radius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ArtificialObjectId)
                .ForeignKey("dbo.PlanetarySystems", t => t.PlanetarySystemId)
                .Index(t => t.PlanetarySystemId);
            
            CreateTable(
                "dbo.PlanetarySystems",
                c => new
                    {
                        PlanetarySystemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PlanetarySystemId);
            
            CreateTable(
                "dbo.Asteroids",
                c => new
                    {
                        AsteroidId = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        Velocity_X = c.Double(nullable: false),
                        Velocity_Y = c.Double(nullable: false),
                        Velocity_Z = c.Double(nullable: false),
                        Velocity_Length = c.Double(nullable: false),
                        Velocity_Theta = c.Double(nullable: false),
                        Velocity_Phi = c.Double(nullable: false),
                        PointId = c.Int(nullable: false),
                        Center_X = c.Double(nullable: false),
                        Center_Y = c.Double(nullable: false),
                        Center_Z = c.Double(nullable: false),
                        Radius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AsteroidId)
                .ForeignKey("dbo.PlanetarySystems", t => t.PlanetarySystemId)
                .Index(t => t.PlanetarySystemId);
            
            CreateTable(
                "dbo.Moons",
                c => new
                    {
                        MoonId = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        PlanetId = c.Int(),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        Velocity_X = c.Double(nullable: false),
                        Velocity_Y = c.Double(nullable: false),
                        Velocity_Z = c.Double(nullable: false),
                        Velocity_Length = c.Double(nullable: false),
                        Velocity_Theta = c.Double(nullable: false),
                        Velocity_Phi = c.Double(nullable: false),
                        PointId = c.Int(nullable: false),
                        Center_X = c.Double(nullable: false),
                        Center_Y = c.Double(nullable: false),
                        Center_Z = c.Double(nullable: false),
                        Radius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MoonId)
                .ForeignKey("dbo.PlanetarySystems", t => t.PlanetarySystemId)
                .ForeignKey("dbo.Planets", t => t.PlanetId)
                .Index(t => t.PlanetarySystemId)
                .Index(t => t.PlanetId);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        PlanetId = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        Velocity_X = c.Double(nullable: false),
                        Velocity_Y = c.Double(nullable: false),
                        Velocity_Z = c.Double(nullable: false),
                        Velocity_Length = c.Double(nullable: false),
                        Velocity_Theta = c.Double(nullable: false),
                        Velocity_Phi = c.Double(nullable: false),
                        PointId = c.Int(nullable: false),
                        Center_X = c.Double(nullable: false),
                        Center_Y = c.Double(nullable: false),
                        Center_Z = c.Double(nullable: false),
                        Radius = c.Double(nullable: false),
                        Star_StarId = c.Int(),
                    })
                .PrimaryKey(t => t.PlanetId)
                .ForeignKey("dbo.PlanetarySystems", t => t.PlanetarySystemId)
                .ForeignKey("dbo.Stars", t => t.Star_StarId)
                .Index(t => t.PlanetarySystemId)
                .Index(t => t.Star_StarId);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        StarId = c.Int(nullable: false),
                        Name = c.String(),
                        Mass = c.Double(nullable: false),
                        Velocity_X = c.Double(nullable: false),
                        Velocity_Y = c.Double(nullable: false),
                        Velocity_Z = c.Double(nullable: false),
                        Velocity_Length = c.Double(nullable: false),
                        Velocity_Theta = c.Double(nullable: false),
                        Velocity_Phi = c.Double(nullable: false),
                        PointId = c.Int(nullable: false),
                        Center_X = c.Double(nullable: false),
                        Center_Y = c.Double(nullable: false),
                        Center_Z = c.Double(nullable: false),
                        Radius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StarId)
                .ForeignKey("dbo.PlanetarySystems", t => t.StarId)
                .Index(t => t.StarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Planets", "Star_StarId", "dbo.Stars");
            DropForeignKey("dbo.Stars", "StarId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.ArtificialObjects", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Planets", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Moons", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.Moons", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Asteroids", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropIndex("dbo.Stars", new[] { "StarId" });
            DropIndex("dbo.Planets", new[] { "Star_StarId" });
            DropIndex("dbo.Planets", new[] { "PlanetarySystemId" });
            DropIndex("dbo.Moons", new[] { "PlanetId" });
            DropIndex("dbo.Moons", new[] { "PlanetarySystemId" });
            DropIndex("dbo.Asteroids", new[] { "PlanetarySystemId" });
            DropIndex("dbo.ArtificialObjects", new[] { "PlanetarySystemId" });
            DropTable("dbo.Stars");
            DropTable("dbo.Planets");
            DropTable("dbo.Moons");
            DropTable("dbo.Asteroids");
            DropTable("dbo.PlanetarySystems");
            DropTable("dbo.ArtificialObjects");
        }
    }
}
