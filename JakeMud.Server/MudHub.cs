using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using JakeMud.Server.Controllers;
using JakeMud.Domain;

namespace JakeMud.Server
{
    public class MudHub : Hub
    {
        #region 複寫既有事件
        public override Task OnConnected()
        {
            Console.WriteLine("用戶端成功連線 " + Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                // We know that Stop() was called on the client,
                // and the connection shut down gracefully.
                Console.WriteLine("用戶端中斷連線: " + Context.ConnectionId);
            }
            else
            {
                // This server hasn't heard from the client in the last ~35 seconds.
                // If SignalR is behind a load balancer with scaleout configured, 
                // the client may still be connected to another SignalR server.
                Console.WriteLine("用戶端遺失連線: " + Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            Console.WriteLine("用戶端重新連線: " + Context.ConnectionId);
            return base.OnReconnected();
        }
        #endregion
        #region 註冊路由 client to server
        /// <summary>
        /// 接收用戶端訊息
        /// </summary>
        /// <param name="message">接收訊息內容</param>
        public void Receive(string message)
        {
            HomeController hc = new HomeController();
            messagesProc(hc.Input(message, Context.ConnectionId));
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 發送訊息給全體用戶端
        /// </summary>
        /// <param name="message">發送訊息內容</param>
        private void SendToAll(string message)
        {
            Clients.All.AddMessage(message);
        }
        /// <summary>
        /// 發送訊息給目前用戶端
        /// </summary>
        /// <param name="message">發送訊息內容</param>
        private void SendToCaller(string message)
        {
            Clients.Caller.AddMessage(message);
        }
        /// <summary>
        /// 發送訊息給其他用戶端
        /// </summary>
        /// <param name="message">發送訊息內容</param>
        private void SendToOther(string message)
        {
            Clients.Others.AddMessage(message);
        }
        /// <summary>
        /// 發送訊息給特定用戶端
        /// </summary>
        /// <param name="message">發送訊息內容</param>
        /// <param name="connectionId">用戶端識別碼</param>
        private void SendToClient(string message, string connectionId)
        {
            Clients.Client(connectionId).AddMessage(message);
        }
        /// <summary>
        /// 發送訊息給特定群組
        /// </summary>
        /// <param name="message">發送訊息內容</param>
        /// <param name="groupName">群組名稱</param>
        private void SendToGroup(string message, string groupName)
        {
            Clients.Group(groupName).AddMessage(message);
        }
        /// <summary>
        /// 訊息組發送處理程序
        /// </summary>
        /// <param name="messages">訊息組資訊</param>
        private void messagesProc(List<Message> messages)
        {
            foreach (var Curr in messages)
            {
                MessageSwitch(Curr);
            }
        }
        /// <summary>
        /// 訊息發送處理程序
        /// </summary>
        /// <param name="message">訊息資訊</param>
        private void MessageSwitch(Message message)
        {
            switch (message.TargetType)
            {
                case TargetTypeEnum.Caller:
                    SendToCaller(message.Context);
                    break;
                case TargetTypeEnum.All:
                    SendToAll(message.Context);
                    break;
                case TargetTypeEnum.Others:
                    SendToOther(message.Context);
                    break;
                case TargetTypeEnum.Client:
                    SendToClient(message.Context, message.TargetID);
                    break;
                case TargetTypeEnum.Group:
                    SendToGroup(message.Context, message.TargetID);
                    break;
                case TargetTypeEnum.None:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
