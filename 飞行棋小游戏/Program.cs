using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋小游戏
{
    class Program
    {
        //map中所有单元格对应的数组，下标代表位置，值代表对应的图形
        private static int[] Maps = new int[100];

        //玩家的位置
        private static int[] PlayerPos = new int[2];

        //玩家的姓名
        private static string[] PlayerNames = new string[2];

        //玩家是否被暂停了
        private static bool[] IsPaused = new bool[2];

        static void Main(string[] args)
        {
            DrawHead();
            InitPlayerInfo();
            Console.Clear();
            DrawHead();
            InitMaps();
            DrawMaps();

            while (PlayerPos[0] < 100 && PlayerPos[1] < 100)
            {
                if (!IsPaused[0])
                {
                    Play(0);
                }
                else
                {
                    IsPaused[0] = false;
                }
                if (!IsPaused[1])
                {
                    Play(1);
                }
                else
                {
                    IsPaused[1] = false;
                }
            }

            DrawWinningTip();
            Console.Read();
        }

        //画游戏头
        private static void DrawHead()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*****飞行棋小游戏*****");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**********************");
        }

        //初始化Maps数组
        private static void InitMaps()
        {
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘◎
            for (int i = 0; i < luckyTurn.Length; i++)
            {
                Maps[luckyTurn[i]] = 1;
            }
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            int[] pause = { 9, 27, 60, 93, 2, 3, 4, 7, 8 };//暂停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道卐
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }

        //根据当前单元格的下标画单元格的图像
        private static string GetElementSymbolByIndex(int index)
        {
            string symbol = "";

            //玩家AB的位置相同，且在当前要画的单元格
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == index)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                symbol = "<>";
            }
            //玩家A在当前的单元格的位置
            else if (PlayerPos[0] == index)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //全角
                symbol = "Ａ";
            }
            //玩家B在当前的单元格的位置
            else if (PlayerPos[1] == index)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //全角
                symbol = "Ｂ";
            }
            else
            {
                switch (Maps[index])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        symbol = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        symbol = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        symbol = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        symbol = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Green;
                        symbol = "卐";
                        break;
                }
            }
            return symbol;
        }

        //画地图
        private static void DrawMaps()
        {
            //画图标提示
            Console.WriteLine("图例:幸运轮盘:◎   地雷:☆   暂停:▲   时空隧道:卐");

            //画第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(GetElementSymbolByIndex(i));
            }

            //换行
            Console.WriteLine();

            //画第一竖行
            string spaceLine = "                                                          ";
            for (int i = 30; i < 35; i++)
            {
                Console.WriteLine(spaceLine + GetElementSymbolByIndex(i));
            }

            //画第2横行
            for (int i = 64; i > 34; i--)
            {
                Console.Write(GetElementSymbolByIndex(i));
            }

            //换行
            Console.WriteLine();

            //画第2竖行
            for (int i = 65; i < 70; i++)
            {
                Console.WriteLine(GetElementSymbolByIndex(i));
            }

            //画第3横行
            for (int i = 70; i < 100; i++)
            {
                Console.Write(GetElementSymbolByIndex(i));
            }

            //换行
            Console.WriteLine();
        }

        //输入和检查玩家姓名
        private static void InitPlayerInfo()
        {
            Console.WriteLine("请输入玩家A的姓名：");
            PlayerNames[0] = Console.ReadLine();
            while (string.IsNullOrEmpty(PlayerNames[0]))
            {
                Console.WriteLine("姓名不能为空，请重新输入：");
                PlayerNames[0] = Console.ReadLine();
            }

            Console.WriteLine("请输入玩家B的姓名：");
            PlayerNames[1] = Console.ReadLine();
            while (string.IsNullOrEmpty(PlayerNames[1]) || PlayerNames[1] == PlayerNames[0])
            {
                if (string.IsNullOrEmpty(PlayerNames[1]))
                {
                    Console.WriteLine("姓名不能为空，请重新输入：");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("输入的玩家B的姓名与玩家A的姓名重复，请重新输入：");
                    PlayerNames[1] = Console.ReadLine();
                }
            }
        }

        //玩游戏
        private static void Play(int playerNum)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            int opponentPlayerNum = 1 - playerNum;
            string currentPlayerName = PlayerNames[playerNum];
            string opponentPlayerName = PlayerNames[opponentPlayerNum];

            Console.WriteLine("玩家{0}请按任意键回车掷骰子", currentPlayerName);
            Console.ReadKey(true);
            Random random = new Random();
            int steps = random.Next(1, 7);
            Console.WriteLine("玩家{0}掷出了{1}", currentPlayerName, steps);
            PlayerPos[playerNum] += steps;

            if (PlayerPos[playerNum] > 99)
            {
                return;
            }

            //当前玩家踩到了另一个玩家
            if (PlayerPos[playerNum] == PlayerPos[opponentPlayerNum])
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}退6格", currentPlayerName, opponentPlayerName, opponentPlayerName);
                PlayerPos[opponentPlayerNum] -= 6;
            }
            //当前玩家踩到了关卡：方块 幸运轮盘 地雷 暂停 时空隧道
            else
            {
                switch (Maps[PlayerPos[playerNum]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，安全。", currentPlayerName);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择 1--交换位置 2--轰炸对方", currentPlayerName);
                        string luckyTurnChoice = Console.ReadLine();
                        LuckyTurn(luckyTurnChoice, opponentPlayerNum);
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷,退6格", currentPlayerName);
                        PlayerPos[playerNum] -= 6;
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", currentPlayerName);
                        IsPaused[playerNum] = true;
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", currentPlayerName);
                        PlayerPos[playerNum] += 10;
                        break;
                }
            }
            Console.WriteLine("玩家{0}本轮游戏结束，按任意键刷新地图并继续", currentPlayerName);
            Console.ReadKey(true);
            CheckPosRange();
            Console.Clear();
            DrawMaps();
        }

        //玩家踩到幸运轮盘，1--交换位置 2--轰炸对方
        private static void LuckyTurn(string luckyTurnChoice, int opponentPlayerNum)
        {
            while (true)
            {
                if (luckyTurnChoice == "1")
                {
                    int temp = PlayerPos[0];
                    PlayerPos[0] = PlayerPos[1];
                    PlayerPos[1] = temp;
                    Console.WriteLine("交换位置成功");
                    break;
                }
                else if (luckyTurnChoice == "2")
                {
                    PlayerPos[opponentPlayerNum] -= 6;
                    Console.WriteLine("轰炸成功，玩家{0}退6格", PlayerNames[opponentPlayerNum]);
                    break;
                }
                else
                {
                    Console.WriteLine("只能输入1或者2  1--交换位置 2--轰炸对方");
                    luckyTurnChoice = Console.ReadLine();
                }
            }
        }

        //检查玩家位置是否越界
        private static void CheckPosRange()
        {
            for (int i = 0; i < PlayerPos.Length; i++)
            {
                if (PlayerPos[i] < 0)
                {
                    PlayerPos[i] = 0;
                }
                else if (PlayerPos[i] > 100)
                {
                    PlayerPos[i] = 100;
                }
            }
        }

        //画胜利提示
        private static void DrawWinningTip()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                          ◆                      ");
            Console.WriteLine("                    ■                  ◆               ■        ■");
            Console.WriteLine("      ■■■■  ■  ■                ◆■         ■    ■        ■");
            Console.WriteLine("      ■    ■  ■  ■              ◆  ■         ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■       ■■■■■■■   ■    ■        ■");
            Console.WriteLine("      ■■■■ ■   ■                ●■●       ■    ■        ■");
            Console.WriteLine("      ■    ■      ■               ● ■ ●      ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■         ●  ■  ●     ■    ■        ■");
            Console.WriteLine("      ■■■■      ■             ●   ■   ■    ■    ■        ■");
            Console.WriteLine("      ■    ■      ■            ■    ■         ■    ■        ■");
            Console.WriteLine("      ■    ■      ■                  ■               ■        ■ ");
            Console.WriteLine("     ■     ■      ■                  ■           ●  ■          ");
            Console.WriteLine("    ■    ■■ ■■■■■■             ■              ●         ●");
            Console.ResetColor();
        }
    }
}
