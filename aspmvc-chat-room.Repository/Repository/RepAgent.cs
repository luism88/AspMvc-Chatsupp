using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.DataAccess.Toolkit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess.Repository
{
    public class RepAgent : GenericRepository<Agent>
    {
        public RepAgent(DbContext chatDbContext) : base(chatDbContext)
        {

        }
    }
}
