namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovaPolja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "IndexNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "DatumRodjenja", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "DatumRodjenja");
            DropColumn("dbo.Students", "IndexNumber");
        }
    }
}
