using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AspMvcChatsupp.DataAccess;
using AspMvcChatsupp.DataAccess.Domain;
using AspMvcChatsupp.MVC;
using Microsoft.AspNet.SignalR;

namespace aAspMvcChatsupp.MVC.Areas.Chatsupp.Hubs
{
    //[Authorize]
    public class ChatsuppHub : Hub
    {
        private readonly IRepUOW _rep;

        public ChatsuppHub(IRepUOW rep)
        {
            this._rep = rep;
        }

        #region Visitor
        public void RegisterVisitor(int? visitorId)
        {
            Visitor connectedUser = _rep.RepVisitor.FindBy(visitor => visitor.VisitorId == visitorId).FirstOrDefault();
            
            //to-do verificacion por email
            if (connectedUser == null)
            {
                connectedUser = new Visitor
                {
                    Name = "Anonymous"

                };
                _rep.RepVisitor.Add(connectedUser);

            }
            connectedUser.CurrentConnections.Add(new CurrentConnection { ConnectionId = Context.ConnectionId });
            connectedUser.StateId = EnumState.Connected;
            connectedUser.ConnectionLogs.Add(new ConnectionLog
            {
                Visitor = connectedUser,
                UserConnectionDate = DateTime.Now,
                GroupId = 1

            });
            connectedUser.MessageHistory.Add(new ChatHistory
            {
                Date = DateTime.Now,
                EventTypeId = EnumEventType.VisitorConnected,

            });

            _rep.SaveChanges();

            Clients.Caller.registerResult(connectedUser.VisitorId);

            //actualiza lista de visitantes en el panel de agentes.
            this._RefreshVisitorList();
            
        }

        public void SetVisitorName(string name)
        {
            var visitor = _rep.RepVisitor
                                    .FindBy(vis => vis.CurrentConnections
                                                        .Select(conn => conn.ConnectionId)
                                                        .Contains(Context.ConnectionId))
                                    .FirstOrDefault();
            visitor.Name = name;
            _rep.RepVisitor.Edit(visitor);
            _rep.SaveChanges();

            Clients.Caller.setNameResult(_rep.SaveChanges() > 0);
        }

        public void SendToAgent(string message)
        {
            var visitor = _rep.RepCurrentConnection.GetAll().Where(curr => curr.ConnectionId == Context.ConnectionId).FirstOrDefault().Visitor;
            
            visitor.MessageHistory.Add(new ChatHistory
            {
                AgentId = visitor.AssignedAgentId,
                Visitor = visitor,
                Value = message,
                Date = DateTime.Now,
                EventTypeId = EnumEventType.VisitorMessage,

            });
            _rep.SaveChanges();

            if(visitor.AssignedAgent != null)
            {
                var agentConnections = visitor.AssignedAgent.CurrentConnections.Select(curr => curr.ConnectionId).ToList();
                Clients.Clients(agentConnections).receiveVisitorMessage(visitor.Name, message);
            }                               
           
        }
        
        
        #endregion

        #region Agent
        public void RegisterAgent()
        {
            var agent = _rep.RepAgent.FindBy(agt => agt.Username == Context.User.Identity.Name).FirstOrDefault();

            if (agent != null)
            {
                agent.CurrentConnections.Add(new CurrentConnection
                {
                    ConnectionId = Context.ConnectionId
                });
                _rep.RepAgent.Edit(agent);
                _rep.SaveChanges();
            }
        }

        public void SendToVisitor(int visitorId, string message)
        {
            var visitor = _rep.RepVisitor.FindBy(vis => vis.VisitorId == visitorId).FirstOrDefault();

            var visitorCurrentConnections = visitor.CurrentConnections.Select(cliConn => cliConn.ConnectionId).ToList();

            var agentLogged = _rep.RepAgent.FindBy(agt => agt.Username == Context.User.Identity.Name).FirstOrDefault();

            agentLogged.MessageHistory.Add(new ChatHistory
            {
                AgentId = visitor.AssignedAgentId,
                Visitor = visitor,
                Value = message,
                Date = DateTime.Now,
                EventTypeId = EnumEventType.AgentMessage,

            });
            _rep.SaveChanges();

            Clients.Clients(visitorCurrentConnections).receiveMessage(agentLogged.Name, message);
        }

        private void _RefreshVisitorList()
        {
            Clients.Clients(_rep.RepAgent.GetAll()
                                           .SelectMany(vis => vis.CurrentConnections)
                                           .Select(conn => conn.ConnectionId)
                                           .ToList())
                                           .refreshVisitorList();
        }
        #endregion

        //overrides
        public override Task OnDisconnected(bool stopCalled)
        {
            var currentConn = _rep.RepCurrentConnection.FindBy(conn => conn.ConnectionId == Context.ConnectionId).FirstOrDefault();

            // es un id de visitante?
            if (currentConn.Visitor != null)
            {
                if (currentConn.Visitor.CurrentConnections.Count == 1)
                {
                    currentConn.Visitor.StateId = EnumState.Disconnected;
                    currentConn.Visitor.MessageHistory.Add(new ChatHistory
                    {
                        Visitor = currentConn.Visitor,
                        Date = DateTime.Now,
                        EventTypeId = EnumEventType.VisitorDisconected,

                    });
                }
            }

            _rep.RepCurrentConnection.Delete(currentConn);
            _rep.SaveChanges();
            _RefreshVisitorList();

            return base.OnDisconnected(stopCalled);
        }

    }

}