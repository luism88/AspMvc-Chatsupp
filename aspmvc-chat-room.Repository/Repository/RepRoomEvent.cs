using System.Data.Entity;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;

namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepRoomEvent : GenericRepository<Room>
    {
        public RepRoomEvent(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
