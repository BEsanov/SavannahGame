using System;
using SavannahGames.Game.Interfaces;
namespace SavannahGames.Game.Views
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
