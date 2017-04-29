namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Events2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Status");
            DropColumn("dbo.Events", "EventDate");
        }
    }
}
