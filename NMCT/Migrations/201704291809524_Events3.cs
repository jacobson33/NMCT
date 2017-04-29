namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Events3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.EventPictures", "EventID");
            AddForeignKey("dbo.EventPictures", "EventID", "dbo.Events", "EventID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventPictures", "EventID", "dbo.Events");
            DropIndex("dbo.EventPictures", new[] { "EventID" });
        }
    }
}
