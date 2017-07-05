
namespace SavannahGames.Game.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = Builder.GetInstance();
            var gameManager = builder.GetGameManager();
            gameManager.Setup(60, 40);


            gameManager.StartGame();
        }
    }
}
