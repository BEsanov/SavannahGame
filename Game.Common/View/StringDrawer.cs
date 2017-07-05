using System.Text;
using SavannahGames.Game.Interfaces;
namespace SavannahGames.Game.Common
{
    public class StringDrawer : IStringDrawer
    {
        private StringBuilder _stringBuilder;
        private int _windowWidth;

        public StringDrawer()
        {

        }


        public void Setup(int windowWidth, StringBuilder stringBuilder)
        {
            _windowWidth = windowWidth;
            _stringBuilder = stringBuilder;
        }


        public int ParseTwoDimensionsToOneIndex(int x, int y)
        {
            return _windowWidth * y + x;
        }

        public void PutSymbol(int x, int y, char symbol)
        {
            var position = ParseTwoDimensionsToOneIndex(x, y);
            _stringBuilder[position] = symbol;
        }

        public void FillRectWithSymbol(int posX, int posY, int sizeX, int sizeY, char symbol)
        {
            for (int h = 0; h < sizeY; h++)
            {
                for (int w = 0; w < sizeX; w++)
                {
                    PutSymbol(posX + w, posY + h, symbol);
                }
            }
        }

        public void DrawLineOfSymbols(bool isHorizontal, int constantCoord, int beginCoord, int endCoord, char symbol)
        {
            if (isHorizontal)
            {
                for (int x = beginCoord; x <= endCoord; x++)
                {
                    PutSymbol(x, constantCoord, symbol);
                }
            }
            else
            {
                for (int y = beginCoord; y <= endCoord; y++)
                {
                    PutSymbol(constantCoord, y, symbol);
                }
            }
        }
    }
}
