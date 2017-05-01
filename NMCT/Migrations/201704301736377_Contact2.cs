namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactForms", "SubmissionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContactForms", "ResolvedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactForms", "ResolvedDate");
            DropColumn("dbo.ContactForms", "SubmissionDate");
        }
    }
}
