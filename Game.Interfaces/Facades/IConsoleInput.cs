using System;

namespace SavannahGames.Game.Interfaces
{
    public interface IConsoleInput
    {
        ConsoleKeyInfo ReadKey();
        string ReadLine();
    }
}