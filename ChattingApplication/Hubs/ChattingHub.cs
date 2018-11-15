using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChattingApplication.Hubs
{
    public class ChattingHub : Hub
    {
        static List<ConnectionUser> CurrentConnections = new List<ConnectionUser>();
        public override System.Threading.Tasks.Task OnDisconnected(bool any)
        {
            var connection = CurrentConnections.FirstOrDefault(x=>x.ConnectionId ==Context.ConnectionId);
            if (connection != null)
                CurrentConnections.Remove(connection);
            Clients.All.AddToActiveUser(CurrentConnections.GroupBy(x=>x.ConnectionUsername).Select(x=>x.Key));
            return base.OnDisconnected(any);
        }
        public void Send(string name ="Anonymous", string message="Empty")
        {
            // Call the addNewMessageToPage method to update clients.
            
            Clients.All.addNewMessageToPage(name, message);
        }

        public void OnConnected(string name)
        {
            var id = Context.ConnectionId;
            CurrentConnections.Add(new ConnectionUser(){ConnectionId =id,ConnectionUsername = name});
            Clients.All.AddToActiveUser(CurrentConnections.GroupBy(x => x.ConnectionUsername).Select(x => x.Key));

        }


    }

    public class ConnectionUser
    {
        public string ConnectionId { get; set; }
        public string ConnectionUsername { get; set; }
    }
}