namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactForms",
                c => new
                    {
                        FormID = c.Int(nullable: false, identity: true),
                        FormCategory = c.Int(nullable: false),
                        FormStatus = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        AssignedUserName = c.String(),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.FormID);
            
            AlterColumn("dbo.Events", "TrailID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "TrailID", c => c.Int(nullable: false));
            DropTable("dbo.ContactForms");
        }
    }
}
