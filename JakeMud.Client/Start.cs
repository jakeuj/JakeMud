using Microsoft.AspNet.SignalR.Client;
using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace JakeMud.Client
{
    class Start
    {
        public static async Task MainAsync()
        {
            string url = ConfigurationManager.AppSettings["Host"];
            //初始化連線
            var hubConnection = new HubConnection(url);
            //建立路由
            IHubProxy hubProxy = hubConnection.CreateHubProxy("MudHub");
            //註冊路由=AddMessage(msg)
            hubProxy.On("AddMessage", msg => Console.WriteLine(msg));
            //設定連線限制
            ServicePointManager.DefaultConnectionLimit = 10;
            //嘗試連線
            try
            {
                Console.Write("連線開始...");
                await hubConnection.Start();
                Console.WriteLine("完成！");
                string line = string.Empty;
                await hubProxy.Invoke("Receive", line);
                while (true)
                {
                    line = Console.ReadLine();
                    if (line.ToLower() == "quit") // Check string
                    {
                        hubConnection.Stop();
                        break;
                    }
                    else
                        await hubProxy.Invoke("Receive", line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("失敗！");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("連線中斷...");
                Console.ReadKey();
            }
        }
    }
}
