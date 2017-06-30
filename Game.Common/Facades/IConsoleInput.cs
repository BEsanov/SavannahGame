using System;

namespace SavannahGames.Game.Common
{
    public interface IConsoleInput
    {
        ConsoleKeyInfo ReadKey();
        string ReadLine();
    }
}