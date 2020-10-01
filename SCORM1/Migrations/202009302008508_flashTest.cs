namespace SCORM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flashTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProtectedFailureTest",
                c => new
                {
                    Modu_Id = c.Int(nullable: false),
                    PF_Name = c.String(),
                    PF_Description = c.String(),
                    GeneralAproveScore = c.Single(nullable: false),
                })
                .PrimaryKey(t => t.Modu_Id)
                .ForeignKey("dbo.Module", t => t.Modu_Id)
                .Index(t => t.Modu_Id);
            CreateTable(
                "dbo.Category",
                c => new
                {
                    Cate_Id = c.Int(nullable: false),
                    Cate_Name = c.String(),
                    Cate_Description = c.String(),
                    ToCo_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Cate_Id)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_Id)
                .Index(t => t.Cate_Id);

            CreateTable(
                "dbo.FlashTest",
                c => new
                {
                    FlashTestId = c.Int(nullable: false),
                    ToCo_Id = c.Int(nullable: false),
                    FlashTestName = c.String(),
                    AprovedPercentage = c.Single(nullable: false),
                })
                .PrimaryKey(t => t.FlashTestId)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_Id)
                .Index(t => t.FlashTestId);

            CreateTable(
                "dbo.FlashQuestion",
                c => new
                {
                    FlashQuestionId = c.Int(nullable: false),
                    FlashTestId = c.Int(nullable: false),
                    Enunciado = c.String(nullable: false),
                })
                .PrimaryKey(t => t.FlashQuestionId)
                .ForeignKey("dbo.FlashTest", t => t.FlashTestId)
                .Index(t => t.FlashQuestionId);

            CreateTable(
                "dbo.FlashQuestionAnswer",
                c => new
                {
                    FlashQueestionAnswerId = c.Int(nullable: false),
                    FlashQuestionId = c.Int(nullable: false),
                    Content = c.String(nullable: false),
                    CorrectAnswer = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.FlashQueestionAnswerId)
                .ForeignKey("dbo.FlashQuestion", t => t.FlashQuestionId)
                .Index(t => t.FlashQueestionAnswerId);

            CreateTable(
                "dbo.UserModuleAdvance",
                c => new
                {
                    Enro_Id = c.Int(nullable: false),
                    ToCo_Id = c.Int(nullable: false),
                    Completed = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Enro_Id, t.ToCo_Id })
                .Index(t => t.Enro_Id)
                .Index(t => t.ToCo_Id);

            CreateTable(
                "dbo.CategoryQuestionBank",
                c => new
                {
                    Cate_Id = c.Int(nullable: false),
                    Modu_Id = c.Int(nullable: false),
                    EvaluateQuestionQuantity = c.Int(nullable: false),
                    AprovedCategoryPercentage = c.Single(nullable: false),
                })
                .PrimaryKey(t => new { t.Cate_Id, t.Modu_Id })
                .ForeignKey("dbo.Module", t => t.Modu_Id)
                .Index(t => t.Cate_Id)
                .Index(t => t.Modu_Id); ;
            AddForeignKey("dbo.CategoryQuestionBank", "Cate_Id", "dbo.Category", "Cate_Id");

            CreateTable(
                "dbo.ProtectedFailureResult",
                c => new
                {
                    Enro_Id = c.Int(nullable: false),
                    Cate_Id = c.Int(nullable: false),
                    Score = c.Single(nullable: false),
                })
                .PrimaryKey(t => new { t.Enro_Id, t.Cate_Id })
                .ForeignKey("dbo.Enrollment", t => t.Enro_Id)
                .Index(t => t.Enro_Id)
                .Index(t => t.Cate_Id); ;
            AddForeignKey("dbo.ProtectedFailureResult", "Cate_Id", "dbo.Category", "Cate_Id");

            AddColumn("dbo.TopicsCourse", "First_Topic", c => c.Int(nullable: true));
            AddColumn("dbo.TopicsCourse", "ConditionedTopic", c => c.Int(nullable: true));
            AddColumn("dbo.TopicsCourse", "Topic_Available", c => c.Boolean(nullable: true));
            AddColumn("dbo.Module", "hasProtectedFailure", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Module", "hasProtectedFailure");
            DropColumn("dbo.TopicsCourse", "Topic_Available");
            DropColumn("dbo.TopicsCourse", "ConditionedTopic");
            DropColumn("dbo.TopicsCourse", "First_Topic");
        }
    }
}
