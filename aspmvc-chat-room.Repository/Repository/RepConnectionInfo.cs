using System.Data.Entity;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;



namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepConnectionInfo : GenericRepository<ConnectionLog>
    {
        public RepConnectionInfo(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
