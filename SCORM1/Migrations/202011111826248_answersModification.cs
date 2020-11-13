namespace SCORM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answersModification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProtectedFailureMultiChoiceAnswer", "QuestionId", "dbo.ProtectedFailureMultiChoice");
            DropIndex("dbo.ProtectedFailureMultiChoiceAnswer", new[] { "QuestionId" });
            CreateTable(
                "dbo.ProtectedFailureAnswer",
                c => new
                    {
                        answerId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        AnswerContent = c.String(),
                        isCorrectQuestion = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.answerId)
                .ForeignKey("dbo.ProtectedFailureMultiChoice", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerId");
            AddForeignKey("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerId", "dbo.ProtectedFailureAnswer", "answerId", cascadeDelete: true);
            DropColumn("dbo.ProtectedFailureMultiChoiceAnswer", "QuestionId");
            DropColumn("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerContent");
            DropColumn("dbo.ProtectedFailureMultiChoiceAnswer", "isCorrectQuestion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProtectedFailureMultiChoiceAnswer", "isCorrectQuestion", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerContent", c => c.String());
            AddColumn("dbo.ProtectedFailureMultiChoiceAnswer", "QuestionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerId", "dbo.ProtectedFailureAnswer");
            DropForeignKey("dbo.ProtectedFailureAnswer", "QuestionId", "dbo.ProtectedFailureMultiChoice");
            DropIndex("dbo.ProtectedFailureAnswer", new[] { "QuestionId" });
            DropIndex("dbo.ProtectedFailureMultiChoiceAnswer", new[] { "AnswerId" });
            DropColumn("dbo.ProtectedFailureMultiChoiceAnswer", "AnswerId");
            DropTable("dbo.ProtectedFailureAnswer");
            CreateIndex("dbo.ProtectedFailureMultiChoiceAnswer", "QuestionId");
            AddForeignKey("dbo.ProtectedFailureMultiChoiceAnswer", "QuestionId", "dbo.ProtectedFailureMultiChoice", "QuestionId", cascadeDelete: true);
        }
    }
}
