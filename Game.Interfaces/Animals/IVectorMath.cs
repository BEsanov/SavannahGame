using SavannahGames.Game.Models;

namespace SavannahGames.Game.Interfaces
{
    public interface IVectorMath
    {
        double AngleBetween(VectorDouble v1, VectorDouble v2);

        double SignedAngleBetween(VectorDouble v1, VectorDouble v2);

        double AngleFromMainAxisClockwise(VectorDouble v);

        VectorDouble DivideByNumber(VectorDouble v, double number);

        Vector DivideByNumber(Vector v, int number);

        Vector Normalize(Vector v);

        Vector Normalize(int x, int y);

        VectorDouble NormalizeDouble(VectorDouble v);

        VectorDouble NormalizeDouble(double x, double y);

        VectorDouble RotateClockwise(VectorDouble v, double angleRadians);

        int ToEightWayDistance(Vector v);

        int ToEightWayDistance(int x, int y);

        int ToEuclideanDistanceNoRoot(Vector v);

        double ToEuclideanDistanceNoRootDouble(VectorDouble v);

        double ToEuclideanDistanceNoRootDouble(double x, double y);

        int ToFourWayDistance(Vector v);

        bool Counterclockwise90DegreeRotationIsCloserToVector(VectorDouble vector, VectorDouble vectorToRotate);
    }
}
