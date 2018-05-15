using System.Data.Entity;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;

namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepEventType : GenericRepository<EventType>
    {
        public RepEventType(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
