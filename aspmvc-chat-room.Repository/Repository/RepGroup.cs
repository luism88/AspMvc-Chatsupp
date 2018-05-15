using System.Data.Entity;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;


namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepGroup : GenericRepository<Group>
    {
        public RepGroup(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
