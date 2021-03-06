﻿using AspMvcChatsupp.DataAccess.Domain;
using System.Data.Entity;


namespace AspMvcChatsupp.DataAccess
{
    public class ChatsuppContext : DbContext
    {
        public ChatsuppContext() : base("Chatsupp")
        {
            Database.SetInitializer<ChatsuppContext>(new CreateDatabaseIfNotExists<ChatsuppContext>());
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<ConnectionLog> ConnectionsInfo { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Group> Rooms { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Visitor> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
