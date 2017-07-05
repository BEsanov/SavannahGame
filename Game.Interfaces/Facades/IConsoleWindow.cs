namespace SavannahGames.Game.Interfaces
{
    public interface IConsoleWindow
    {
        int BufferHeight { get; set; }

        int BufferWidth { get; set; }

        int WindowHeight { get; set; }

        int WindowWidth { get; set; }

        string Title { get; set; }

        int LargestWindowWidth { get; }

        int LargestWindowHeight { get; }

        void SetWindowSize(int width, int height);

        void SetBufferSize(int width, int height);
    }
}