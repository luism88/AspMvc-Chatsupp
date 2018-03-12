namespace AspMvcChatsupp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        AgenteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgenteId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.ConnectionsInfo",
                c => new
                    {
                        ConnectionInfoId = c.Int(nullable: false, identity: true),
                        VisitorConnectionId = c.String(),
                        VisitorId = c.Int(nullable: false),
                        RoomId = c.Int(),
                        AgentId = c.Int(),
                        AgentConnectionId = c.String(),
                        UserConnectionDate = c.DateTime(nullable: false),
                        AgentConnectionDate = c.DateTime(),
                        StateId = c.Int(),
                    })
                .PrimaryKey(t => t.ConnectionInfoId)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .ForeignKey("dbo.States", t => t.StateId)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.RoomId)
                .Index(t => t.StateId)
                .Index(t => t.VisitorId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        VistorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OperatingSystem = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.VistorId);
            
            CreateTable(
                "dbo.RoomEvents",
                c => new
                    {
                        RoomEventId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        EventTypeId = c.Int(nullable: false),
                        Date = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoomEventId)
                .ForeignKey("dbo.EventsType", t => t.EventTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.EventTypeId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.EventsType",
                c => new
                    {
                        EventTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LegendTemplate = c.String(),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomEvents", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomEvents", "EventTypeId", "dbo.EventsType");
            DropForeignKey("dbo.ConnectionsInfo", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.ConnectionsInfo", "StateId", "dbo.States");
            DropForeignKey("dbo.ConnectionsInfo", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ConnectionsInfo", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.Agents", "RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomEvents", new[] { "RoomId" });
            DropIndex("dbo.RoomEvents", new[] { "EventTypeId" });
            DropIndex("dbo.ConnectionsInfo", new[] { "VisitorId" });
            DropIndex("dbo.ConnectionsInfo", new[] { "StateId" });
            DropIndex("dbo.ConnectionsInfo", new[] { "RoomId" });
            DropIndex("dbo.ConnectionsInfo", new[] { "AgentId" });
            DropIndex("dbo.Agents", new[] { "RoomId" });
            DropTable("dbo.EventsType");
            DropTable("dbo.RoomEvents");
            DropTable("dbo.Visitors");
            DropTable("dbo.States");
            DropTable("dbo.ConnectionsInfo");
            DropTable("dbo.Rooms");
            DropTable("dbo.Agents");
        }
    }
}
