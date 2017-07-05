using SavannahGames.Game.Interfaces;
using System;


namespace SavannahGames.Game.Views
{
    public class ConsoleOutput : IConsoleOutput
    {
        public ConsoleColor BackgroundColor
        {
            get
            {
                return System.Console.BackgroundColor;
            }

            set
            {
                System.Console.BackgroundColor = value;
            }
        }

        public ConsoleColor ForegroundColor
        {
            get
            {
                return System.Console.ForegroundColor;
            }

            set
            {
                System.Console.ForegroundColor = value;
            }
        }

        public bool CursorVisible
        {
            get
            {
                return System.Console.CursorVisible;
            }

            set
            {
                System.Console.CursorVisible = value;
            }
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void ResetColor()
        {
            System.Console.ResetColor();
        }

        public void Write(string value)
        {
            System.Console.Write(value);
        }

        public void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }
    }
}
