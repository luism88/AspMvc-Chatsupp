namespace AspMvcChatsupp.DataAccess.Migrations
{
    using AspMvcChatsupp.DataAccess.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspMvcChatsupp.DataAccess.ChatsuppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AspMvcChatsupp.DataAccess.ChatsuppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Rooms.AddOrUpdate(
                new Group { GroupId = 1, Name = "Argentina" }
            );
            context.Agents.AddOrUpdate(
             new Agent { AgentId = 1, Name = "Admin", Username="admin", Password="admin", RoomId = 1 }
            );
            context.States.AddOrUpdate(
                new State { StateId = EnumState.Connected, Name = EnumState.Connected.ToString()},
                new State { StateId = EnumState.Disconnected, Name = EnumState.Disconnected.ToString() },
                new State { StateId = EnumState.WaitingAnswer, Name = EnumState.WaitingAnswer.ToString() }
            );
            context.EventTypes.AddOrUpdate(
                new EventType { EventTypeId = EnumEventType.AgentConnected, Name = EnumEventType.AgentConnected.ToString() },
                new EventType { EventTypeId = EnumEventType.AgentDisconnected, Name = EnumEventType.AgentDisconnected.ToString() },
                new EventType { EventTypeId = EnumEventType.AgentMessage, Name = EnumEventType.AgentMessage.ToString() },
                new EventType { EventTypeId = EnumEventType.VisitorConnected, Name = EnumEventType.VisitorConnected.ToString() },
                new EventType { EventTypeId = EnumEventType.VisitorDisconected, Name = EnumEventType.VisitorDisconected.ToString() },
                new EventType { EventTypeId = EnumEventType.VisitorMessage, Name = EnumEventType.VisitorMessage.ToString() }
            );

        }
    }
}
