namespace DiplomaOptions.Migrations.CourseOptionMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        YearTermId = c.Int(),
                        StudentId = c.String(maxLength: 9),
                        StudentFirstName = c.String(nullable: false),
                        StudentLastName = c.String(nullable: false),
                        FirstChoiceOptionId = c.Int(nullable: false),
                        SecondChoiceOptionId = c.Int(nullable: false),
                        ThirdChoiceOptionId = c.Int(nullable: false),
                        FourthChoiceOptionId = c.Int(nullable: false),
                        SelectionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Options", t => t.FirstChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Options", t => t.FourthChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Options", t => t.SecondChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Options", t => t.ThirdChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId)
                .Index(t => t.YearTermId)
                .Index(t => t.FirstChoiceOptionId)
                .Index(t => t.SecondChoiceOptionId)
                .Index(t => t.ThirdChoiceOptionId)
                .Index(t => t.FourthChoiceOptionId);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        Choice_ChoiceId = c.Int(),
                    })
                .PrimaryKey(t => t.OptionId)
                .ForeignKey("dbo.Choices", t => t.Choice_ChoiceId)
                .Index(t => t.Choice_ChoiceId);
            
            CreateTable(
                "dbo.YearTerms",
                c => new
                    {
                        YearTermId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Choice_ChoiceId = c.Int(),
                    })
                .PrimaryKey(t => t.YearTermId)
                .ForeignKey("dbo.Choices", t => t.Choice_ChoiceId)
                .Index(t => t.Choice_ChoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YearTerms", "Choice_ChoiceId", "dbo.Choices");
            DropForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.Choices", "ThirdChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "SecondChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Options", "Choice_ChoiceId", "dbo.Choices");
            DropForeignKey("dbo.Choices", "FourthChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "FirstChoiceOptionId", "dbo.Options");
            DropIndex("dbo.YearTerms", new[] { "Choice_ChoiceId" });
            DropIndex("dbo.Options", new[] { "Choice_ChoiceId" });
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "YearTermId" });
            DropTable("dbo.YearTerms");
            DropTable("dbo.Options");
            DropTable("dbo.Choices");
        }
    }
}
