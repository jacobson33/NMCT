namespace NMCT.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NMCT.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NMCT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NMCT.Models.ApplicationDbContext context)
        {
            /*context.Trail.AddOrUpdate(t => t.TrailID,
                new Trail
                {
                    Name = "VASA Single Track",
                    County = "Grand Traverse",
                    URL = "https://www.singletracks.com/bike-trails/vasa-single-track.html",
                    Longitude = -85.4834927,
                    Latitude = 44.7016554
                },
                new Trail
                {
                    Name = "Glacial Hills",
                    County = "Antrim",
                    URL = "http://gtrlc.org",
                    Longitude = -85.2574297,
                    Latitude = 44.9905291
                },
                new Trail
                {
                    Name = "Copper Harbor Trail System",
                    County = "Keweenaw",
                    URL = "http://www.copperharbortrails.org",
                    Longitude = -87.8907967,
                    Latitude = 47.4674156
                },
                new Trail
                {
                    Name = "VASA Single Track",
                    County = "Grand Traverse",
                    URL = "https://www.singletracks.com/bike-trails/vasa-single-track.html",
                    Longitude = -85.4834927,
                    Latitude = 44.7016554
                },
                new Trail
                {
                    Name = "VASA Trail",
                    County = "Grand Traverse",
                    URL = "https://traversetrails.org/trail/vasa-pathway/",
                    Longitude = -85.4928067,
                    Latitude = 44.7502488
                },
                new Trail
                {
                    Name = "Cadillac Pathways",
                    County = "Missaukee",
                    URL = "http://www.cadillacmichigan.com/member-41/cadillac-pathway-trail---mdnr-885.html",
                    Longitude = -85.3385002,
                    Latitude = 44.2829509
                }
            );*/
        }
    }
}
