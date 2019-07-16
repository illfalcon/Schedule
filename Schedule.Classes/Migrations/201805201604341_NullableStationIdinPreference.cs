namespace Schedule.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableStationIdinPreference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Preferences", "StationId", "dbo.Stations");
            DropIndex("dbo.Preferences", new[] { "StationId" });
            AlterColumn("dbo.Preferences", "StationId", c => c.Int());
            CreateIndex("dbo.Preferences", "StationId");
            AddForeignKey("dbo.Preferences", "StationId", "dbo.Stations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Preferences", "StationId", "dbo.Stations");
            DropIndex("dbo.Preferences", new[] { "StationId" });
            AlterColumn("dbo.Preferences", "StationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Preferences", "StationId");
            AddForeignKey("dbo.Preferences", "StationId", "dbo.Stations", "Id", cascadeDelete: true);
        }
    }
}
