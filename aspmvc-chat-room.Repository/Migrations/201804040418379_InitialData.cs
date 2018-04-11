namespace AspMvcChatsupp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurrentConnectionAgents", "CurrentConnection_CurrenConnectionId", "dbo.CurrentConnections");
            DropForeignKey("dbo.CurrentConnectionAgents", "Agent_AgenteId", "dbo.Agents");
            DropForeignKey("dbo.VisitorCurrentConnections", "Visitor_VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.VisitorCurrentConnections", "CurrentConnection_CurrenConnectionId", "dbo.CurrentConnections");
            DropIndex("dbo.CurrentConnectionAgents", new[] { "CurrentConnection_CurrenConnectionId" });
            DropIndex("dbo.CurrentConnectionAgents", new[] { "Agent_AgenteId" });
            DropIndex("dbo.VisitorCurrentConnections", new[] { "Visitor_VisitorId" });
            DropIndex("dbo.VisitorCurrentConnections", new[] { "CurrentConnection_CurrenConnectionId" });
            AddColumn("dbo.CurrentConnections", "VisitorId", c => c.Int());
            AddColumn("dbo.CurrentConnections", "AgentId", c => c.Int());
            CreateIndex("dbo.CurrentConnections", "AgentId");
            CreateIndex("dbo.CurrentConnections", "VisitorId");
            AddForeignKey("dbo.CurrentConnections", "AgentId", "dbo.Agents", "AgenteId");
            AddForeignKey("dbo.CurrentConnections", "VisitorId", "dbo.Visitors", "VisitorId");
            DropTable("dbo.CurrentConnectionAgents");
            DropTable("dbo.VisitorCurrentConnections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VisitorCurrentConnections",
                c => new
                    {
                        Visitor_VisitorId = c.Int(nullable: false),
                        CurrentConnection_CurrenConnectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Visitor_VisitorId, t.CurrentConnection_CurrenConnectionId });
            
            CreateTable(
                "dbo.CurrentConnectionAgents",
                c => new
                    {
                        CurrentConnection_CurrenConnectionId = c.Int(nullable: false),
                        Agent_AgenteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CurrentConnection_CurrenConnectionId, t.Agent_AgenteId });
            
            DropForeignKey("dbo.CurrentConnections", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.CurrentConnections", "AgentId", "dbo.Agents");
            DropIndex("dbo.CurrentConnections", new[] { "VisitorId" });
            DropIndex("dbo.CurrentConnections", new[] { "AgentId" });
            DropColumn("dbo.CurrentConnections", "AgentId");
            DropColumn("dbo.CurrentConnections", "VisitorId");
            CreateIndex("dbo.VisitorCurrentConnections", "CurrentConnection_CurrenConnectionId");
            CreateIndex("dbo.VisitorCurrentConnections", "Visitor_VisitorId");
            CreateIndex("dbo.CurrentConnectionAgents", "Agent_AgenteId");
            CreateIndex("dbo.CurrentConnectionAgents", "CurrentConnection_CurrenConnectionId");
            AddForeignKey("dbo.VisitorCurrentConnections", "CurrentConnection_CurrenConnectionId", "dbo.CurrentConnections", "CurrenConnectionId", cascadeDelete: true);
            AddForeignKey("dbo.VisitorCurrentConnections", "Visitor_VisitorId", "dbo.Visitors", "VisitorId", cascadeDelete: true);
            AddForeignKey("dbo.CurrentConnectionAgents", "Agent_AgenteId", "dbo.Agents", "AgenteId", cascadeDelete: true);
            AddForeignKey("dbo.CurrentConnectionAgents", "CurrentConnection_CurrenConnectionId", "dbo.CurrentConnections", "CurrenConnectionId", cascadeDelete: true);
        }
    }
}
