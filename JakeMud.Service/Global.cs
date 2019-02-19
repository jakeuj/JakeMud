using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Models;

namespace JakeMud.Service
{
    class Global
    {
        public static Dictionary<int, Map> MapsList = new Dictionary<int, Map>();
        public static Dictionary<string, Player> PlayersList = new Dictionary<string, Player>();
        public static string Welcome = @"


                         三國歪傳之降龍伏虎


                        140.136.196.82  3838

       古云﹕「合久必分 分久必合」 在這戰亂的時代需要一位新霸主﹐
       來統一天下﹐創造新的國度﹐但是首先要平定各大梟雄﹐招攬俠客
       學習一身武藝﹐才有機會劃下此時代的句號........請加油吧﹗
       當然囉﹐三國時代的寶物如此的多﹐相信你一定很希望拿到他﹐
       並且使用他﹐讓寶物幫助你成為三國的梟雄﹐但是﹐寶物可不是那麼
       輕易就拿得到的喔﹗


       本 MUD 的首頁﹕http://mud.ch.fju.edu.tw ﹐歡迎參觀！
                   Upgraded from Merc Diku Mud 2.2";
    }
}
