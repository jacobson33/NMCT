namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contact21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactForms", "ResolvedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactForms", "ResolvedDate", c => c.DateTime(nullable: false));
        }
    }
}
