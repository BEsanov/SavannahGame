

using SavannahGames.Game.Interfaces;
using SavannahGames.Game.Models;

namespace SavannahGames.Game.Common.Logic
{
    public class BorderChecker : IBorderChecker
    {
        public Vector FitVectorIntoBorders(Vector fieldSize, Vector point1, Vector point2)
        {
            var result = point2;
            var pointSum = point1 + point2;

            if (IsOnLeftBorder(fieldSize, pointSum.X))
            {
                point2.X = -point1.X;
            }
            else if (IsOnRightBorder(fieldSize, pointSum.X))
            {
                point2.X = fieldSize.X - point1.X - 1;
            }

            if (IsOnTopBorder(fieldSize, pointSum.Y))
            {
                point2.Y = -point1.Y;
            }
            else if (IsOnBottomBorder(fieldSize, pointSum.Y))
            {
                point2.Y = fieldSize.Y - point1.Y - 1;
            }

            return result;
        }

        public bool IsOnBorder(Vector fieldSize, Vector position)
            => IsOnBorderX(fieldSize, position.X) || IsOnBorderY(fieldSize, position.Y);

        public bool IsOnBorderX(Vector fieldSize, int positionX)
            => IsOnLeftBorder(fieldSize, positionX) || IsOnRightBorder(fieldSize, positionX);

        public bool IsOnBorderY(Vector fieldSize, int positionY)
            => IsOnBottomBorder(fieldSize, positionY) || IsOnTopBorder(fieldSize, positionY);

        public bool IsOnBottomBorder(Vector fieldSize, int positionY)
            => positionY >= fieldSize.Y;

        public bool IsOnTopBorder(Vector fieldSize, int positionY)
            => positionY < 0;

        public bool IsOnRightBorder(Vector fieldSize, int positionX)
            => positionX >= fieldSize.X;
        public bool IsOnLeftBorder(Vector fieldSize, int positionX)
            => positionX < 0;




    }
}
