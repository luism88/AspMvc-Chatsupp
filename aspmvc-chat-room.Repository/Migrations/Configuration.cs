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
                new Room { RoomId = 1, Name = "Argentina" }
            );
            context.Agents.AddOrUpdate(
             new Agent { AgenteId = 1, Name = "Admin", Username="admin", Password="admin", RoomId = 1 }
            );
            context.States.AddOrUpdate(
                new State { StateId = 1, Name = "Connected"},
                new State { StateId = 2, Name = "Disconnected"}
            );

        }
    }
}
