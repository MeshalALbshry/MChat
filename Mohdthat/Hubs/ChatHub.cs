using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.Data.Entity;
using Mohdthat.Models;
using System.Threading.Tasks;
using Mohdthat.ModelView;
namespace Mohdthat.Hubs
{
    public class ChatHub : Hub
    {

        static List<UserDetail> ConnectedUsers = new System.Collections.Generic.List<UserDetail>();

        private ApplicationDbContext db;


        public ChatHub()
        {
            db = new ApplicationDbContext();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //}

        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            var CurrentUser = Context.User.Identity.Name;
            var userContact = db.UserContacts.Where(u => u.CurrnetUser == CurrentUser);
            //Group
            var userRoom = db.UserRoom.Include(u => u.Room).Where(u => u.UserSelected == CurrentUser);
            var numberOfUsersInGroup = db.UserRoom.Include(n => n.User).ToList();

            Clients.Caller.currentUser(CurrentUser);
            //Clients.Caller.userContact(userContact);
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id , UserName = CurrentUser});
                Clients.Caller.onConnected(id, CurrentUser, ConnectedUsers);
                Clients.Caller.userContact(userContact, ConnectedUsers);
                Clients.Caller.groups(userRoom , numberOfUsersInGroup);
            }
            Clients.AllExcept(id).onNewUserConnected(id, CurrentUser);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);
                Clients.All.onUserDisconnected(id, item.UserName);
            }
            return base.OnDisconnected(stopCalled);
        }

        #region private
        public void SendPrivateMessage(string toUserName,string toUserIdCon,string message)
        {
            var currnetUserName = Context.User.Identity.Name;
            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserIdCon);

            var privateChatConver = db.PrivateChatConversation.FirstOrDefault(p => p.SesstionID == currnetUserName + "_" + toUserName || p.SesstionID == toUserName + "_" + currnetUserName);

                if (privateChatConver != null)
                {
                    if (privateChatConver.SesstionID == currnetUserName + "_" + toUserName || privateChatConver.SesstionID == toUserName + "_" + currnetUserName)
                    {
                        db.PrivateChatEntries.Add(new PrivateChatEntries { 
                            Message = message,
                            Sender = currnetUserName,
                            ConversationID = privateChatConver.Id,
                            CreatedAt = DateTime.Now
                        });
                         db.SaveChanges();
                    }
                    else
                    {
                        db.PrivateChatConversation.Add(new PrivateChatConversation
                        {
                            SesstionID = currnetUserName + "_" + toUserName,
                            UserID1 = currnetUserName,
                            UserID2 = toUserName,
                            CreatedAt = DateTime.Now
                        });
                        db.SaveChanges();
                        var pId = db.PrivateChatConversation.FirstOrDefault(p => p.SesstionID == currnetUserName + "_" + toUserName);
                        if (pId != null)
                        {
                            db.PrivateChatEntries.Add(new PrivateChatEntries
                            {
                                Message = message,
                                Sender = currnetUserName,
                                ConversationID = pId.Id,
                                CreatedAt = DateTime.Now
                            });
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    db.PrivateChatConversation.Add(new PrivateChatConversation
                    {
                        SesstionID = currnetUserName + "_" + toUserName,
                        UserID1 = currnetUserName,
                        UserID2 = toUserName,
                        CreatedAt = DateTime.Now
                    });
                    db.SaveChanges();
                    var pId = db.PrivateChatConversation.FirstOrDefault(p => p.SesstionID == currnetUserName + "_" + toUserName);
                    if (pId != null)
                    {
                        db.PrivateChatEntries.Add(new PrivateChatEntries
                        {
                             Message = message,
                             Sender = currnetUserName,
                             ConversationID = pId.Id,
                            CreatedAt = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                }

                if (toUser != null)
                {
                    Clients.Client(toUserIdCon).recivePrivateMessageOthers(Context.ConnectionId, currnetUserName, message);
                    Clients.Client(toUserIdCon).notification(currnetUserName);
                }
                Clients.Caller.recivePrivateMessageCaller(currnetUserName, message);
        }

        public void GetPrivateMessage(string toUserName)
        {
            var currnetUserName = Context.User.Identity.Name;
            var privateChatConver = db.PrivateChatConversation.FirstOrDefault(p => p.SesstionID == currnetUserName + "_" + toUserName || p.SesstionID == toUserName + "_" + currnetUserName);
            if (privateChatConver != null)
            {
                var privateChatEnt = db.PrivateChatEntries.ToList().Where(p => p.ConversationID == privateChatConver.Id);
                
                if (privateChatEnt != null)
                {
                    Clients.Caller.recivePrivateMessageWhenClick(privateChatEnt,currnetUserName,toUserName);
                }
            }
            
        }
        #endregion

        #region Group
        public void SendToGroup(string message ,int groupID)
        {
            var currnetUserName = Context.User.Identity.Name;
            var userId = db.Users.FirstOrDefault(u => u.UserName == currnetUserName);
            var room = db.Room.FirstOrDefault(r => r.Id == groupID);
            db.RoomEntries.Add(new RoomEntries { 
                Message = message,
                Room = room,
                Sender = userId,
                CreatedAt = DateTime.Now
            });
            db.SaveChanges();
            Clients.Group(room.Name).recieveMessageGroup(Context.User.Identity.Name, message);
            Clients.Caller.reciveGroupMessageCaller(currnetUserName, message);
        }

        public void GetGroupMessage(int roomid)
        {
            var currnetUserName = Context.User.Identity.Name;
            var getMessage = db.RoomEntries.Include(u => u.Sender).Include(r => r.Room).Where(r => r.Room.Id == roomid);
            

            Clients.Caller.reciveGroupMessageWhenClick(getMessage, currnetUserName, roomid, ConnectedUsers);
        }

        public void JoinGroup(string roomName)
        {
            Groups.Add(this.Context.ConnectionId, roomName);
        }


        public void LeaveGroup(string roomName)
        {
            Groups.Remove(this.Context.ConnectionId, roomName);
        }
        #endregion

    }
}