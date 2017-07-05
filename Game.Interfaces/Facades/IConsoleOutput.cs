using System;

namespace SavannahGames.Game.Interfaces
{
    public interface IConsoleOutput
    {
        ConsoleColor BackgroundColor { get; set; }
        ConsoleColor ForegroundColor { get; set; }
        bool CursorVisible { get; set; }
        void Clear();
        void Write(string value);
        void SetCursorPosition(int left, int top);
    }
}