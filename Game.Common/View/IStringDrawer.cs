using System.Text;

namespace SavannahGames.Game.Common
{
    public interface IStringDrawer
    {
        void Setup(int windowWidth, StringBuilder stringBuilder);
        void DrawLineOfSymbols(bool isHorizontal, int constantCoord, int beginCoord, int endCoord, char symbol);
        void FillRectWithSymbol(int posX, int posY, int sizeX, int sizeY, char symbol);
        int ParseTwoDimensionsToOneIndex(int x, int y);
        void PutSymbol(int x, int y, char symbol);
    }
}
