namespace Schedule.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeFromOrigin = c.Int(nullable: false),
                        TimeFromDest = c.Int(nullable: false),
                        Station_Id = c.Int(),
                        Route_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.Station_Id)
                .ForeignKey("dbo.Routes", t => t.Route_Id)
                .Index(t => t.Station_Id)
                .Index(t => t.Route_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Preferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        Description = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.StationId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Preferences", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Preferences", "StationId", "dbo.Stations");
            DropForeignKey("dbo.RouteStations", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.RouteStations", "Station_Id", "dbo.Stations");
            DropIndex("dbo.Preferences", new[] { "User_Id" });
            DropIndex("dbo.Preferences", new[] { "StationId" });
            DropIndex("dbo.RouteStations", new[] { "Route_Id" });
            DropIndex("dbo.RouteStations", new[] { "Station_Id" });
            DropTable("dbo.Preferences");
            DropTable("dbo.Users");
            DropTable("dbo.Stations");
            DropTable("dbo.RouteStations");
            DropTable("dbo.Routes");
        }
    }
}
