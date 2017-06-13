namespace SQLiteData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Satellites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2147483647),
                        SattelliteTypeId = c.Int(nullable: false),
                        SatelliteType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SatelliteTypes", t => t.SatelliteType_Id)
                .Index(t => t.SatelliteType_Id);
            
            CreateTable(
                "dbo.SatelliteTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 2147483647),
                        SatteliteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Satellites", "SatelliteType_Id", "dbo.SatelliteTypes");
            DropIndex("dbo.Satellites", new[] { "SatelliteType_Id" });
            DropTable("dbo.SatelliteTypes");
            DropTable("dbo.Satellites");
        }
    }
}
