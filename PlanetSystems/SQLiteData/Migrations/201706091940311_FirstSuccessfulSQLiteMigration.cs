namespace SQLiteData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstSuccessfulSQLiteMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestClasses");
        }
    }
}
