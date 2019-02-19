using JakeMud.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Service;
using JakeMud.Domain;

namespace JakeMud.Server.Controllers
{
    public class HomeController
    {
        private IHomeService homeService;
        public HomeController()
        {
            this.homeService = new HomeService();
        }
        public void Init()
        {
            this.homeService.Init();
        }
        public List<Message> Input(string Input,string ConnectionId)
        {
            return this.homeService.Input(Input, ConnectionId);
        }
    }
}
