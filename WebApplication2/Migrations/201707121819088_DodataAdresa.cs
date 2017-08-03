namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodataAdresa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ulica = c.String(),
                        Broj = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Students", "Adresa_Id", c => c.Int());
            CreateIndex("dbo.Students", "Adresa_Id");
            AddForeignKey("dbo.Students", "Adresa_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Adresa_Id", "dbo.Addresses");
            DropIndex("dbo.Students", new[] { "Adresa_Id" });
            DropColumn("dbo.Students", "Adresa_Id");
            DropTable("dbo.Addresses");
        }
    }
}
