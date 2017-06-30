namespace SavannahGames.Game.Common
{
    public interface IBorderChecker
    {
        bool IsOnBorder(Vector fieldSize, Vector position);

        bool IsOnBorderX(Vector fieldSize, int positionX);

        bool IsOnBorderY(Vector fieldSize, int positionY);

        bool IsOnLeftBorder(Vector fieldSize, int positionX);

        bool IsOnRightBorder(Vector fieldSize, int positionX);

        bool IsOnTopBorder(Vector fieldSize, int positionY);

        bool IsOnBottomBorder(Vector fieldSize, int positionY);

        Vector FitVectorIntoBorders(Vector fieldSize, Vector point1, Vector point2);

    }
}
