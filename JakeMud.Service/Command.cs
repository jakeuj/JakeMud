using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Models;

namespace JakeMud.Service
{
    class Command
    {
        // 解析指令
        public string CommandProcess(string Input, string ConnectionId)
        {
            Input = Input.ToLower();
            string ResultMessage = string.Empty;
            switch (Input)
            {
                case "l":
                    ResultMessage = LookProcess(Input, ConnectionId);
                    break;
                case "e":
                case "s":
                case "w":
                case "n":
                case "u":
                case "d":
                    ResultMessage = MoveProcess(Input, ConnectionId);
                    break;
                case "":
                    ResultMessage = string.Empty;
                    break;
                default:
                    ResultMessage = "無此指令";
                    break;
            }
            return ResultMessage;
        }
        // 觀看指令
        public string LookProcess(string Input, string ConnectionId)
        {
            Map CurrMap = Global.MapsList[Global.PlayersList[ConnectionId].CurrMapID];
            string ResultMessage;
            ResultMessage = "位置\r\n";
            ResultMessage += CurrMap.Name + "\r\n\r\n";
            ResultMessage += CurrMap.Description + "\r\n\r\n";
            ResultMessage += "出口有：\r\n";
            string ExitStr = string.Empty;
            foreach (Path CurrPath in CurrMap.Paths)
                ExitStr += string.Format("方向：{0} 連結到 {1} .\r\n", CurrPath.Direction, Global.MapsList[CurrPath.NextMapID].Name);
            if (string.IsNullOrEmpty(ExitStr))
                ExitStr = "這個地方沒有任何出口";
            ResultMessage += ExitStr;
            return ResultMessage;
        }
        // 移動指令
        public string MoveProcess(string Input, string ConnectionId)
        {
            string ResultMessage = string.Empty;
            Map CurrMap = Global.MapsList[Global.PlayersList[ConnectionId].CurrMapID];
            var NextPath = (from p in CurrMap.Paths where p.Direction == Input select p).FirstOrDefault();
            if (NextPath == null)
            {
                ResultMessage = "這方向不通唷";
            }
            else
            {
                Global.PlayersList[ConnectionId].CurrMapID = NextPath.NextMapID;
                ResultMessage = LookProcess(Input, ConnectionId);
            }
            return ResultMessage;
        }
    }
}
