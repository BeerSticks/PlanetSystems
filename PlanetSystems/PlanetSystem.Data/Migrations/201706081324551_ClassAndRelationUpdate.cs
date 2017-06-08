namespace PlanetSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassAndRelationUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArtificialObjects", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.PlanetarySystems", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Asteroids", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Moons", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Planets", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stars", "Name", c => c.String(nullable: false));
            DropColumn("dbo.ArtificialObjects", "PointId");
            DropColumn("dbo.Asteroids", "PointId");
            DropColumn("dbo.Moons", "PointId");
            DropColumn("dbo.Planets", "PointId");
            DropColumn("dbo.Stars", "PointId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stars", "PointId", c => c.Int(nullable: false));
            AddColumn("dbo.Planets", "PointId", c => c.Int(nullable: false));
            AddColumn("dbo.Moons", "PointId", c => c.Int(nullable: false));
            AddColumn("dbo.Asteroids", "PointId", c => c.Int(nullable: false));
            AddColumn("dbo.ArtificialObjects", "PointId", c => c.Int(nullable: false));
            AlterColumn("dbo.Stars", "Name", c => c.String());
            AlterColumn("dbo.Planets", "Name", c => c.String());
            AlterColumn("dbo.Moons", "Name", c => c.String());
            AlterColumn("dbo.Asteroids", "Name", c => c.String());
            AlterColumn("dbo.PlanetarySystems", "Name", c => c.String());
            AlterColumn("dbo.ArtificialObjects", "Name", c => c.String());
        }
    }
}
