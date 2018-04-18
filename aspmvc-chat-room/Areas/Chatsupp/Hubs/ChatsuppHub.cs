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
            Visitor connectedUser;

            if (visitorId == null)
            {
                connectedUser = new Visitor
                {
                    Name = "Anonymous"

                };
                _rep.RepVisitor.Add(connectedUser);

            }
            else
                connectedUser = _rep.RepVisitor.FindBy(visitor => visitor.VisitorId == visitorId).FirstOrDefault();

            connectedUser.CurrentConnections.Add(new CurrentConnection { ConnectionId = Context.ConnectionId });

            var connInfo = _rep.RepConnectionInfo.FindBy(conn => conn.VisitorId == visitorId && conn.StateId == 1).FirstOrDefault();

            if(connInfo == null)
            {
                connInfo = new ConnectionInfo
                {
                    Visitor = connectedUser,
                    UserConnectionDate = DateTime.Now,
                    StateId = 1, // connected
                    RoomId = 1

                };
                _rep.RepConnectionInfo.Add(connInfo);
                
            }

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
            var connInfo = _rep.RepConnectionInfo
                                            .FindBy(conn => conn.StateId == 1 && conn.Visitor.CurrentConnections.Any(curr => curr.ConnectionId == Context.ConnectionId))
                                            .FirstOrDefault();
            if(connInfo.Agente != null)
            {
                var agentConnections = connInfo.Agente.CurrentConnections.Select(curr => curr.ConnectionId).ToList();
                Clients.Clients(agentConnections).receiveVisitorMessage(connInfo.Visitor.Name, message);
            }                               
           //else solo guardar en historial
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

        public void SendToVisitor(int connInfoId, string message)
        {
            var connInfo = _rep.RepConnectionInfo.FindBy(conn => conn.ConnectionInfoId == connInfoId).FirstOrDefault();

            var visitorCurrentConnections = connInfo.Visitor.CurrentConnections.Select(cliConn => cliConn.ConnectionId).ToList();

            var agent = _rep.RepAgent.FindBy(agt => agt.Username == Context.User.Identity.Name).FirstOrDefault();

            connInfo.AgentId = agent.AgentId;

            _rep.RepConnectionInfo.Edit(connInfo);
            _rep.SaveChanges();

            Clients.Clients(visitorCurrentConnections).receiveMessage(connInfo.Agente.Name, message);
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
            if (currentConn.Visitors != null)
            {
                if (currentConn.Visitors.CurrentConnections.Count == 1)
                {
                    var connectionInfo = _rep.RepConnectionInfo
                                            .FindBy(connInfo => connInfo.VisitorId == currentConn.VisitorId)
                                            .OrderByDescending(ord => ord.ConnectionInfoId)
                                            .FirstOrDefault();
                    connectionInfo.StateId = 2;
                }
            }

            _rep.RepCurrentConnection.Delete(currentConn);
            _rep.SaveChanges();
            _RefreshVisitorList();

            return base.OnDisconnected(stopCalled);
        }

    }

}