namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Events : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        TrailID = c.Int(nullable: false),
                        EventName = c.String(nullable: false, maxLength: 100),
                        EventDescription = c.String(nullable: false, maxLength: 512),
                        ContactPhone = c.String(),
                        ContactEmail = c.String(),
                        ContactUrl = c.String(),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.EventPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(nullable: false),
                        PictureURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventPictures");
            DropTable("dbo.Events");
        }
    }
}
