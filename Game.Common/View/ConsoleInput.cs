using System;
namespace SavannahGames.Game.Common
{
    public class ConsoleInput : IConsoleInput
    {
        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}
