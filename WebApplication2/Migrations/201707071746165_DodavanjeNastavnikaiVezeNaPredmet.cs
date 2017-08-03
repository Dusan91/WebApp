namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeNastavnikaiVezeNaPredmet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nastavniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        DatumRodjenja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Predmets", "Nastavnik_Id", c => c.Int());
            CreateIndex("dbo.Predmets", "Nastavnik_Id");
            AddForeignKey("dbo.Predmets", "Nastavnik_Id", "dbo.Nastavniks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predmets", "Nastavnik_Id", "dbo.Nastavniks");
            DropIndex("dbo.Predmets", new[] { "Nastavnik_Id" });
            DropColumn("dbo.Predmets", "Nastavnik_Id");
            DropTable("dbo.Nastavniks");
        }
    }
}
