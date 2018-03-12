using AspMvcChatsupp.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess
{
    public class RepUOW : IRepUOW
    {
        private ChatDBContext _db = new ChatDBContext();
        private RepVisitor _repVisitor = null;
        private RepAgent _repAgent = null;
        private RepConnectionInfo _repConnectionInfo = null;
        private RepRoom _repRoom = null;
        private RepRoomEvent _repRoomEvent = null;

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public RepVisitor RepVisitor
        {
            get
            {
                return _repVisitor ?? (_repVisitor = new RepVisitor(_db));
            }
        }
        public RepAgent RepAgent
        {
            get
            {
                return _repAgent ?? (_repAgent = new RepAgent(_db));
            }
        }
        public RepRoom RepRoom
        {
            get
            {
                return _repRoom ?? (_repRoom = new RepRoom(_db));
            }
        }
        public RepRoomEvent RepRoomEvent
        {
            get
            {
                return _repRoomEvent ?? (_repRoomEvent = new RepRoomEvent(_db));
            }
        }
        public RepConnectionInfo RepConnectionInfo
        {
            get
            {
                return _repConnectionInfo ?? (_repConnectionInfo = new RepConnectionInfo(_db));
            }
        }
    }
}
