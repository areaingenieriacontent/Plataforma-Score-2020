namespace SCORM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answersModification2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProtectedFailureMultiChoiceAnswer");
            AddPrimaryKey("dbo.ProtectedFailureMultiChoiceAnswer", new[] { "UserId", "AnswerId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProtectedFailureMultiChoiceAnswer");
            AddPrimaryKey("dbo.ProtectedFailureMultiChoiceAnswer", "UserId");
        }
    }
}
