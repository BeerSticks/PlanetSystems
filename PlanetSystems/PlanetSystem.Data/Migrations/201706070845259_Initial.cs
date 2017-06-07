namespace PlanetSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanetarySystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Asteroids",
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
                "dbo.Moons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanetarySystemId = c.Int(),
                        PlanetId = c.Int(),
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
                .ForeignKey("dbo.Planets", t => t.PlanetId)
                .Index(t => t.PlanetarySystemId)
                .Index(t => t.PlanetId)
                .Index(t => t.VelocityId)
                .Index(t => t.PointId);
            
            CreateTable(
                "dbo.Planets",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Planets", "VelocityId", "dbo.Vectors");
            DropForeignKey("dbo.Planets", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Moons", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.Planets", "PointId", "dbo.Points");
            DropForeignKey("dbo.Moons", "VelocityId", "dbo.Vectors");
            DropForeignKey("dbo.Moons", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Moons", "PointId", "dbo.Points");
            DropForeignKey("dbo.Asteroids", "VelocityId", "dbo.Vectors");
            DropForeignKey("dbo.Asteroids", "PlanetarySystemId", "dbo.PlanetarySystems");
            DropForeignKey("dbo.Asteroids", "PointId", "dbo.Points");
            DropIndex("dbo.Planets", new[] { "PointId" });
            DropIndex("dbo.Planets", new[] { "VelocityId" });
            DropIndex("dbo.Planets", new[] { "PlanetarySystemId" });
            DropIndex("dbo.Moons", new[] { "PointId" });
            DropIndex("dbo.Moons", new[] { "VelocityId" });
            DropIndex("dbo.Moons", new[] { "PlanetId" });
            DropIndex("dbo.Moons", new[] { "PlanetarySystemId" });
            DropIndex("dbo.Asteroids", new[] { "PointId" });
            DropIndex("dbo.Asteroids", new[] { "VelocityId" });
            DropIndex("dbo.Asteroids", new[] { "PlanetarySystemId" });
            DropTable("dbo.Planets");
            DropTable("dbo.Moons");
            DropTable("dbo.Vectors");
            DropTable("dbo.Points");
            DropTable("dbo.Asteroids");
            DropTable("dbo.PlanetarySystems");
        }
    }
}
