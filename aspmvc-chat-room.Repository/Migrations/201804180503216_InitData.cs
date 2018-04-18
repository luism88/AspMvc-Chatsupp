namespace AspMvcChatsupp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageHistory", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.MessageHistory", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageHistory", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.MessageHistory", "Date");
        }
    }
}
