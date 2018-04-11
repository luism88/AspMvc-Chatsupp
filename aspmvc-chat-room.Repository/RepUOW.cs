using AspMvcChatsupp.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcChatsupp.DataAccess
{
    public class RepUOW : IRepUOW, IDisposable
    {
        private ChatsuppContext _db = new ChatsuppContext();
        private bool _disposed = false;
        private RepVisitor _repVisitor;
        private RepAgent _repAgent;
        private RepConnectionInfo _repConnectionInfo;
        private RepRoom _repRoom;
        private RepRoomEvent _repRoomEvent;
        private RepCurrentConnection _repCurrentConnection;


        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
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
        public RepCurrentConnection RepCurrentConnection
        {
            get
            {
                return _repCurrentConnection ?? (_repCurrentConnection = new RepCurrentConnection(_db));
            }
        }


    }
}
