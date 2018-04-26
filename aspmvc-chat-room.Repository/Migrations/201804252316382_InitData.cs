namespace AspMvcChatsupp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        AgentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgentId)
                .ForeignKey("dbo.Groups", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.CurrentConnections",
                c => new
                    {
                        CurrenConnectionId = c.Int(nullable: false, identity: true),
                        ConnectionId = c.String(),
                        VisitorId = c.Int(),
                        AgentId = c.Int(),
                    })
                .PrimaryKey(t => t.CurrenConnectionId)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.Visitors", t => t.VisitorId)
                .Index(t => t.AgentId)
                .Index(t => t.VisitorId);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        VisitorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        StateId = c.Int(nullable: false),
                        AssignedAgentId = c.Int(),
                    })
                .PrimaryKey(t => t.VisitorId)
                .ForeignKey("dbo.Agents", t => t.AssignedAgentId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.AssignedAgentId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.ConnectionsInfo",
                c => new
                    {
                        ConnectionInfoId = c.Int(nullable: false, identity: true),
                        VisitorId = c.Int(nullable: false),
                        GroupId = c.Int(),
                        UserConnectionDate = c.DateTime(nullable: false),
                        OperatingSystem = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ConnectionInfoId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.VisitorId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.ChatHistory",
                c => new
                    {
                        MessageHistoryId = c.Int(nullable: false, identity: true),
                        VisitorId = c.Int(nullable: false),
                        AgentId = c.Int(),
                        EventTypeId = c.Int(nullable: false),
                        Value = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageHistoryId)
                .ForeignKey("dbo.Agents", t => t.AgentId)
                .ForeignKey("dbo.EventsType", t => t.EventTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.EventTypeId)
                .Index(t => t.VisitorId);
            
            CreateTable(
                "dbo.EventsType",
                c => new
                    {
                        EventTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        LegendTemplate = c.String(),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitors", "StateId", "dbo.States");
            DropForeignKey("dbo.ChatHistory", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.ChatHistory", "EventTypeId", "dbo.EventsType");
            DropForeignKey("dbo.ChatHistory", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.CurrentConnections", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.ConnectionsInfo", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.ConnectionsInfo", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Agents", "RoomId", "dbo.Groups");
            DropForeignKey("dbo.Visitors", "AssignedAgentId", "dbo.Agents");
            DropForeignKey("dbo.CurrentConnections", "AgentId", "dbo.Agents");
            DropIndex("dbo.Visitors", new[] { "StateId" });
            DropIndex("dbo.ChatHistory", new[] { "VisitorId" });
            DropIndex("dbo.ChatHistory", new[] { "EventTypeId" });
            DropIndex("dbo.ChatHistory", new[] { "AgentId" });
            DropIndex("dbo.CurrentConnections", new[] { "VisitorId" });
            DropIndex("dbo.ConnectionsInfo", new[] { "VisitorId" });
            DropIndex("dbo.ConnectionsInfo", new[] { "GroupId" });
            DropIndex("dbo.Agents", new[] { "RoomId" });
            DropIndex("dbo.Visitors", new[] { "AssignedAgentId" });
            DropIndex("dbo.CurrentConnections", new[] { "AgentId" });
            DropTable("dbo.States");
            DropTable("dbo.EventsType");
            DropTable("dbo.ChatHistory");
            DropTable("dbo.Groups");
            DropTable("dbo.ConnectionsInfo");
            DropTable("dbo.Visitors");
            DropTable("dbo.CurrentConnections");
            DropTable("dbo.Agents");
        }
    }
}
