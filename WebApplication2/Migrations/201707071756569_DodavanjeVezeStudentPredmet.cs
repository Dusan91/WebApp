namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeVezeStudentPredmet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Predmet_Id", c => c.Int());
            CreateIndex("dbo.Students", "Predmet_Id");
            AddForeignKey("dbo.Students", "Predmet_Id", "dbo.Predmets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Predmet_Id", "dbo.Predmets");
            DropIndex("dbo.Students", new[] { "Predmet_Id" });
            DropColumn("dbo.Students", "Predmet_Id");
        }
    }
}
