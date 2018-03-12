using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess
{
    public interface IRepUOW
    {
        int SaveChanges();
    }
}
