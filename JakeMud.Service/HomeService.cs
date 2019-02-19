using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Service.Interface;
using JakeMud.Service.ModelService;
using JakeMud.Domain;

namespace JakeMud.Service
{
    public class HomeService : IHomeService
    {
        public void Init()
        {
            IMapService mapService;
            IPathService pathService;
            mapService = new MapService();
            pathService = new PathService();
            foreach (var CurrMap in mapService.GetAll())
            {
                Global.MapsList.Add(CurrMap.MapID, CurrMap);
            }
        }
        public List<Message> Input(string Input, string ConnectionId)
        {
            Login login = new Login();
            return login.CheckLoginStatus(Input, ConnectionId);
        }
    }
}
