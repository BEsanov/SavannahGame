
namespace SavannahGames.Game.Common
{
    public class ConsoleWindow : IConsoleWindow
    {
        public int LargestWindowHeight => System.Console.LargestWindowHeight;

        public int LargestWindowWidth => System.Console.LargestWindowWidth;

        public int BufferHeight
        {
            get
            {
                return System.Console.BufferHeight;
            }

            set
            {
                System.Console.BufferHeight = value;
            }
        }

        public int BufferWidth
        {
            get
            {
                return System.Console.BufferWidth;
            }

            set
            {
                System.Console.BufferWidth = value;
            }
        }

        public string Title
        {
            get
            {
                return System.Console.Title;
            }

            set
            {
                System.Console.Title = value;
            }
        }

        public int WindowHeight
        {
            get
            {
                return System.Console.WindowHeight;
            }

            set
            {
                System.Console.WindowHeight = value;
            }
        }

        public int WindowWidth
        {
            get
            {
                return System.Console.WindowWidth;
            }

            set
            {
                System.Console.WindowWidth = value;
            }
        }

        public void SetBufferSize(int width, int height)
        {
            System.Console.SetBufferSize(width, height);
        }

        public void SetWindowSize(int width, int height)
        {
            System.Console.SetWindowSize(width, height);
        }
    }
}
