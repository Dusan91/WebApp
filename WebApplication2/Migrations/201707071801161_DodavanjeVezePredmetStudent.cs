namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeVezePredmetStudent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Predmet_Id", "dbo.Predmets");
            DropIndex("dbo.Students", new[] { "Predmet_Id" });
            CreateTable(
                "dbo.StudentPredmets",
                c => new
                    {
                        Student_ID = c.Int(nullable: false),
                        Predmet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_ID, t.Predmet_Id })
                .ForeignKey("dbo.Students", t => t.Student_ID, cascadeDelete: true)
                .ForeignKey("dbo.Predmets", t => t.Predmet_Id, cascadeDelete: true)
                .Index(t => t.Student_ID)
                .Index(t => t.Predmet_Id);
            
            DropColumn("dbo.Students", "Predmet_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Predmet_Id", c => c.Int());
            DropForeignKey("dbo.StudentPredmets", "Predmet_Id", "dbo.Predmets");
            DropForeignKey("dbo.StudentPredmets", "Student_ID", "dbo.Students");
            DropIndex("dbo.StudentPredmets", new[] { "Predmet_Id" });
            DropIndex("dbo.StudentPredmets", new[] { "Student_ID" });
            DropTable("dbo.StudentPredmets");
            CreateIndex("dbo.Students", "Predmet_Id");
            AddForeignKey("dbo.Students", "Predmet_Id", "dbo.Predmets", "Id");
        }
    }
}
