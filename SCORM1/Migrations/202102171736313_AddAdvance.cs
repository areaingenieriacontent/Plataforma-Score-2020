namespace SCORM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdvance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advance", "Usuario_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advance", "Usuario_Id");
        }
    }
}
