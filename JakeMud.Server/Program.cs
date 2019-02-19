using JakeMud.Server.Controllers;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace JakeMud.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeController hc = new HomeController();
            hc.Init();
            string url = ConfigurationManager.AppSettings["Host"];
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                while (true)
                {
                    Console.WriteLine("輸入 quit 關閉伺服器：");
                    if (Console.ReadLine().ToLower() == "quit") // Check string
                        break;
                }
            }
            Console.WriteLine("伺服器已離線，請按任意鍵結束：");
            Console.ReadKey();
        }
    }
}
