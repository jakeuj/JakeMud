using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Models;
using JakeMud.Domain;
namespace JakeMud.Service
{
    class Login
    {
        public List<Message> CheckLoginStatus(string input, string connectionId)
        {
            Message Curr;
            List<Message> CurrMessage;
            lock (Global.PlayersList)
            {
                Command cmd;
                Player CurrentPlayer;
                string Info;
                string Result;
                if (!Global.PlayersList.ContainsKey(connectionId))
                {
                    CurrentPlayer = new Player();
                    CurrentPlayer.ConnectionId = connectionId;
                    CurrentPlayer.LoginStep = 0;
                    Global.PlayersList.Add(connectionId, CurrentPlayer);
                }
                else
                    CurrentPlayer = Global.PlayersList[connectionId];
                cmd = new Command();
                Result = string.Empty;
                switch (CurrentPlayer.LoginStep)
                {
                    case 0:
                        CurrentPlayer.LoginStep = 1;
                        Global.PlayersList[connectionId] = CurrentPlayer;
                        Result = Global.Welcome + "\r\n請輸入帳號：";
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(input))
                            Result = ("\r\n請輸入正確的帳號：");
                        else
                        {
                            CurrentPlayer.LoginStep = 2;
                            CurrentPlayer.Name = input;
                            Global.PlayersList[connectionId] = CurrentPlayer;
                            Result = ("\r\n請輸入密碼：");
                        }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(input))
                            Result = ("\r\n請輸入正確的帳號：");
                        else
                        {
                            CurrentPlayer.LoginStep = 3;
                            CurrentPlayer.CurrMapID = 1;
                            Global.PlayersList[connectionId] = CurrentPlayer;
                            Info = cmd.CommandProcess("l", connectionId);
                            Result = ("\r\n歡迎進入三國世界," + CurrentPlayer.Name + "\r\n\r\n" + Info + string.Format("\r\n[姓名={0},血量=100,魔力=100,體力=100,飢餓度=0%,飢渴度=0%]　\r\n", CurrentPlayer.Name));
                        }
                        break;
                    case 3:
                        Result += cmd.CommandProcess(input, connectionId);
                        Result += string.Format("\r\n[姓名={0},血量=100,魔力=100,體力=100,飢餓度=0%,飢渴度=0%]　\r\n", CurrentPlayer.Name);
                        break;
                    default:
                        Result = ("\r\n三國系統");
                        break;
                }
                Curr = new Message(TargetTypeEnum.Caller, Result,null);
                CurrMessage = new List<Message>();
                CurrMessage.Add(Curr);
                return CurrMessage;
            }
        }
    }
}
