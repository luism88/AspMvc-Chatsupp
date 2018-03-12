using System;
using System.Collections.Concurrent;
using System.Linq;
using AspMvcChatsupp.DataAccess;
using AspMvcChatsupp.DataAccess.Domain;
using Microsoft.AspNet.SignalR;

namespace aAspMvcChatsupp.MVC.Areas.Chatsupp.Hubs
{
    public class ChatsuppHub : Hub
    {
       
        private RepUOW _rep = new RepUOW();
        
        public void RegisterUser(string name)
        {
            Visitor connectedUser = new Visitor
            {
                Name = name
            };
            _rep.RepVisitor.Add(connectedUser);
            _rep.RepConnectionInfo.Add(new ConnectionInfo
            {
                VisitorConnectionId = Context.ConnectionId,
                Visitor = connectedUser,
                UserConnectionDate = DateTime.Now,
                StateId = 1,
                RoomId = 1

            });
            _rep.SaveChanges();
            Clients.Caller.registerResult(Context.ConnectionId);
                
        }

        public void SendToAgent(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addNewMessageToPage(name, message);

        }

        public void SendToRoom(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addNewMessageToPage(name, message);

        }

        public void SendToUser(int agentId, int connInfoId, string message)
        {
            var connInfo = _rep.RepConnectionInfo.FindBy(conn => conn.ConnectionInfoId == connInfoId).FirstOrDefault();
            connInfo.AgentId = agentId;
            connInfo.AgentConnectionId = Context.ConnectionId;
            _rep.RepConnectionInfo.Edit(connInfo);
            _rep.SaveChanges();

            Clients.Client(connInfo.VisitorConnectionId).receiveMessage(connInfo.Agente.Name, message);

        }
    }
}