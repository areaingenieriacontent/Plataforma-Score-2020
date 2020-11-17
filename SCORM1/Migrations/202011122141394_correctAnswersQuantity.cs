namespace SCORM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctAnswersQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProtectedFailureResults", "correctAnswersQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProtectedFailureResults", "correctAnswersQuantity");
        }
    }
}
