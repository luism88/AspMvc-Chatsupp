using System.Data.Entity;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;

namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepVisitor : GenericRepository<Visitor>
    {
        public RepVisitor(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
