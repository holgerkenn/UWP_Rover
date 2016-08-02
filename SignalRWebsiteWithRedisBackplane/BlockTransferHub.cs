using System;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace SignalRWebsiteWithRedisBackplane
{
    public class BlockTransferHub : Hub
    {
        public void BlockSend(byte[] block)
        {
            Clients.All.broadcastBlock(block);

        }
        public void BlockGroupSend(string group,  byte[] block)
        {
            Clients.Group(group,Context.ConnectionId).multicastBlock(block); // sent to everybody in the group but me
        }
        public void ControlSend(string message)
        {
            Clients.All.broadcastControl(message);
        }
        public void ControlGroupSend(string group,string message)
        {
            Clients.Group(group, Context.ConnectionId).multicastControl(message); // sent to everybody in the group but me
        }
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
        public void Join(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }
        public void Leave(string group)
        {
            Groups.Remove(Context.ConnectionId, group);
        }


    }
}