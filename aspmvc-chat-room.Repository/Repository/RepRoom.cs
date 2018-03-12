using System.Data.Entity;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;


namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepRoom : GenericRepository<Room>
    {
        public RepRoom(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
