namespace AspMvcChatsupp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageHistory", "AgentId", "dbo.Agents");
            DropIndex("dbo.MessageHistory", new[] { "AgentId" });
            AddColumn("dbo.MessageHistory", "FromAgent", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MessageHistory", "AgentId", c => c.Int());
            CreateIndex("dbo.MessageHistory", "AgentId");
            AddForeignKey("dbo.MessageHistory", "AgentId", "dbo.Agents", "AgentId");
            DropColumn("dbo.MessageHistory", "IsAgentMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageHistory", "IsAgentMessage", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.MessageHistory", "AgentId", "dbo.Agents");
            DropIndex("dbo.MessageHistory", new[] { "AgentId" });
            AlterColumn("dbo.MessageHistory", "AgentId", c => c.Int(nullable: false));
            DropColumn("dbo.MessageHistory", "FromAgent");
            CreateIndex("dbo.MessageHistory", "AgentId");
            AddForeignKey("dbo.MessageHistory", "AgentId", "dbo.Agents", "AgentId", cascadeDelete: true);
        }
    }
}
