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
        private readonly IRepUOW _repository;

        public ChatsuppHub(IRepUOW rep)
        {
            this._repository = rep;
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
                _repository.RepVisitor.Add(connectedUser);

            }
            else
                connectedUser = _repository.RepVisitor.FindBy(visitor => visitor.VisitorId == visitorId).FirstOrDefault();

            connectedUser.CurrentConnections.Add(new CurrentConnection { ConnectionId = Context.ConnectionId });

            var connInfo = _repository.RepConnectionInfo.FindBy(conn => conn.VisitorId == visitorId && conn.StateId == 1).FirstOrDefault();

            if(connInfo == null)
            {
                connInfo = new ConnectionInfo
                {
                    Visitor = connectedUser,
                    UserConnectionDate = DateTime.Now,
                    StateId = 1, // connected
                    RoomId = 1

                };
                _repository.RepConnectionInfo.Add(connInfo);
                
            }

            _repository.SaveChanges();
            Clients.Caller.registerResult(connectedUser.VisitorId);

            //actualiza lista de visitantes en el panel de agentes.
            this._RefreshVisitorList();
            
        }

        public void SetVisitorName(string name)
        {
            var visitor = _repository.RepVisitor
                                    .FindBy(vis => vis.CurrentConnections
                                                        .Select(conn => conn.ConnectionId)
                                                        .Contains(Context.ConnectionId))
                                    .FirstOrDefault();
            visitor.Name = name;
            _repository.RepVisitor.Edit(visitor);
            _repository.SaveChanges();

            Clients.Caller.setNameResult(_repository.SaveChanges() > 0);
        }

        public void SendToAgent(string message)
        {
            var connInfo = _repository.RepConnectionInfo
                                            .FindBy(conn => conn.StateId == 1 && conn.Visitor.CurrentConnections.Any(curr => curr.ConnectionId == Context.ConnectionId))
                                            .FirstOrDefault();
                                            
            var agentConnections = connInfo.Agente.CurrentConnections.Select(curr => curr.ConnectionId).ToList();


            Clients.Clients(agentConnections).receiveVisitorMessage(connInfo.Visitor.Name, message);
        }
        
        #endregion

        #region Agent
        public void RegisterAgent()
        {
            var agent = _repository.RepAgent.FindBy(agt => agt.Username == Context.User.Identity.Name).FirstOrDefault();

            if (agent != null)
            {
                agent.CurrentConnections.Add(new CurrentConnection
                {
                    ConnectionId = Context.ConnectionId
                });
                _repository.RepAgent.Edit(agent);
                _repository.SaveChanges();
            }
        }

        public void SendToVisitor(int connInfoId, string message)
        {
            var connInfo = _repository.RepConnectionInfo.FindBy(conn => conn.ConnectionInfoId == connInfoId).FirstOrDefault();

            var visitorCurrentConnections = connInfo.Visitor.CurrentConnections.Select(cliConn => cliConn.ConnectionId).ToList();

            var agent = _repository.RepAgent.FindBy(agt => agt.Username == Context.User.Identity.Name).FirstOrDefault();

            connInfo.AgentId = agent.AgenteId;

            _repository.RepConnectionInfo.Edit(connInfo);
            _repository.SaveChanges();

            Clients.Clients(visitorCurrentConnections).receiveMessage(connInfo.Agente.Name, message);
        }

        private void _RefreshVisitorList()
        {
            Clients.Clients(_repository.RepAgent.GetAll()
                                           .SelectMany(vis => vis.CurrentConnections)
                                           .Select(conn => conn.ConnectionId)
                                           .ToList())
                                           .refreshVisitorList();
        }
        #endregion

        //overrides
        public override Task OnDisconnected(bool stopCalled)
        {
            var currentConn = _repository.RepCurrentConnection.FindBy(conn => conn.ConnectionId == Context.ConnectionId).FirstOrDefault();

            // es un id de visitante?
            if (currentConn.Visitors != null)
            {
                if (currentConn.Visitors.CurrentConnections.Count == 1)
                {
                    var connectionInfo = _repository.RepConnectionInfo
                                            .FindBy(connInfo => connInfo.VisitorId == currentConn.VisitorId)
                                            .OrderByDescending(ord => ord.ConnectionInfoId)
                                            .FirstOrDefault();
                    connectionInfo.StateId = 2;
                }
            }

            _repository.RepCurrentConnection.Delete(currentConn);
            _repository.SaveChanges();
            _RefreshVisitorList();

            return base.OnDisconnected(stopCalled);
        }

    }

}