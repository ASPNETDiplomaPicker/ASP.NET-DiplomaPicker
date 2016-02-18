namespace OptionsWebSite.Migrations.OptionPickerMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiplomaOptionsCreate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            CreateIndex("dbo.Choices", "FirstChoiceOptionId");
            CreateIndex("dbo.Choices", "SecondChoiceOptionId");
            CreateIndex("dbo.Choices", "ThirdChoiceOptionId");
            CreateIndex("dbo.Choices", "FourthChoiceOptionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            CreateIndex("dbo.Choices", "FourthChoiceOptionId", unique: true);
            CreateIndex("dbo.Choices", "ThirdChoiceOptionId", unique: true);
            CreateIndex("dbo.Choices", "SecondChoiceOptionId", unique: true);
            CreateIndex("dbo.Choices", "FirstChoiceOptionId", unique: true);
        }
    }
}
