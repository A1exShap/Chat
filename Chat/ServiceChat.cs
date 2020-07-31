using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {

        public List<ServerUser> _users = new List<ServerUser>();
        private int nextID = 0;

        public int NextID
        {
            get => ++nextID;
        }

        public int Connect(string name)
        {
            var user = new ServerUser()
            {
                ID = NextID,
                Name = name,
                Context = OperationContext.Current
            };

            _users.Add(user);

            SendMessage($"Welcome to our chat, {user.Name}!");
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = _users.FirstOrDefault(x => x.ID == id);
            if (user == null) return;
            SendMessage($"Good bye, {user.Name}!");
            _users.Remove(user);
        }

        public void SendMessage(string message, int senderID = 0)
        {
            var senderName = senderID == 0 ?
                "SERVER" : _users.FirstOrDefault(x => x.ID == senderID).Name;

            var msg = $"({DateTime.Now.ToShortTimeString()})[{senderName}]: {message}";

            foreach (var user in _users)
                user.Context.GetCallbackChannel<IServerChatCallback>().MessageCallback(msg);
        }
    }
}
