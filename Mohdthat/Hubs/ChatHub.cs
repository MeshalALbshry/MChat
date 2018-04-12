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

            Clients.Caller.currentUser(CurrentUser);

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id , UserName = CurrentUser});
                Clients.Caller.onConnected(id, CurrentUser, ConnectedUsers);
            }


            Clients.AllExcept(id).onNewUserConnected(id,CurrentUser);
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
            var currentUserCon = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            var privateChatConver = db.PrivateChatConversation.FirstOrDefault(p => p.SesstionID == currnetUserName + "_" + toUserName || p.SesstionID == toUserName + "_" + currnetUserName);

            if (toUserIdCon != null && currentUserCon != null )
            {
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
                        if (privateChatConver.Id != null)
                        {
                            db.PrivateChatEntries.Add(new PrivateChatEntries
                            {
                                Message = message,
                                Sender = currnetUserName,
                                ConversationID = privateChatConver.Id,
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
                    if (privateChatConver.Id != null)
                    {
                        db.PrivateChatEntries.Add(new PrivateChatEntries
                        {
                             Message = message,
                             Sender = currnetUserName,
                             ConversationID = privateChatConver.Id,
                            CreatedAt = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                }

                Clients.Client(toUserIdCon).recivePrivateMessageOthers(Context.ConnectionId, currentUserCon.UserName, message);
                Clients.Caller.recivePrivateMessageCaller(Context.User.Identity.Name, message);
            }
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

    }
}