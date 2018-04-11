using AspMvcChatsupp.DataAccess.Repository;
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

        RepVisitor RepVisitor { get; }
        
        RepAgent RepAgent { get; }
      
        RepRoom RepRoom { get; }
        
        RepRoomEvent RepRoomEvent { get; }
        
        RepConnectionInfo RepConnectionInfo { get; }
       
        RepCurrentConnection RepCurrentConnection { get; }
        
    }
}
