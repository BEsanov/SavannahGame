using System;
using Conditions;
using SavannahGames.Game.Interfaces;
using SavannahGames.Game.Models;
using SavannahGames.Game.Common;

namespace SavannahGames.Game.AnyAnimals
{
    public class VectorMath : IVectorMath
    {
        public Vector Normalize(Vector v)
        {
            (v.X == 0 && v.Y == 0).Requires()
                .IsFalse();

            double absX = Math.Abs(v.X),
                   absY = Math.Abs(v.Y);
            int normalizedX, normalizedY;


            if (absX > absY)
            {
                normalizedX = Convert.ToInt32((double)v.X / absX);
                normalizedY = Convert.ToInt32((double)v.Y / absX);
            }
            else
            {
                normalizedY = Convert.ToInt32((double)v.Y / absY);
                normalizedX = Convert.ToInt32((double)v.X / absY);
            }

            return new Vector(normalizedX, normalizedY);
        }

        public VectorDouble NormalizeDouble(VectorDouble v)
        {
            if (Math.Abs(v.X) < Constants.DELTA && Math.Abs(v.Y) < Constants.DELTA)
            {
                return new VectorDouble(0, 0);
            }

            double absX = Math.Abs(v.X),
                   absY = Math.Abs(v.Y);
            double normalizedX, normalizedY;


            if (absX > absY)
            {
                normalizedX = v.X / absX;
                normalizedY = v.Y / absX;
            }
            else
            {
                normalizedY = v.Y / absY;
                normalizedX = v.X / absY;
            }

            return new VectorDouble(normalizedX, normalizedY);
        }

        public Vector Normalize(int x, int y)
        {
            return Normalize(new Vector(x, y));
        }

        public VectorDouble NormalizeDouble(double x, double y)
        {
            return NormalizeDouble(new VectorDouble(x, y));
        }

        public int ToEuclideanDistanceNoRoot(Vector v)
        {
            return v.X * v.X + v.Y * v.Y;
        }

        public double ToEuclideanDistanceNoRootDouble(VectorDouble v)
        {
            return v.X * v.X + v.Y * v.Y;
        }

        public double ToEuclideanDistanceNoRootDouble(double x, double y)
        {
            return x * x + y * y;
        }

        public int ToFourWayDistance(Vector v)
        {
            return Math.Abs(v.X) + Math.Abs(v.Y);
        }

        public int ToEightWayDistance(Vector v)
        {
            int absX = Math.Abs(v.X), absY = Math.Abs(v.Y);

            return absX > absY ? absX : absY;
        }

        public int ToEightWayDistance(int x, int y)
        {
            int absX = Math.Abs(x), absY = Math.Abs(y);

            return absX > absY ? absX : absY;
        }


        public double SignedAngleBetween(VectorDouble v1, VectorDouble v2)
        {
            if (Math.Abs(v1.X) < Constants.DELTA && Math.Abs(v1.Y) < Constants.DELTA 
                || Math.Abs(v2.X)  < Constants.DELTA && Math.Abs(v2.Y) < Constants.DELTA)
            {
                return 0;
            }
        
            return Math.Atan2(v1.X * v2.Y - v1.Y * v2.X, v1.X * v2.X + v1.Y * v2.Y);
        }

        public double AngleBetween(VectorDouble v1, VectorDouble v2)
        {
            if (Math.Abs(v1.X) < Constants.DELTA && Math.Abs(v1.Y) < Constants.DELTA
                || Math.Abs(v2.X) < Constants.DELTA && Math.Abs(v2.Y) < Constants.DELTA)
            {
                return 0;
            }


            var cos = (v1.X * v2.X + v1.Y * v2.Y) / (Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y) * Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y));
            if ((Math.Abs(cos) > 1.0d))
            {
                cos = Math.Sign(cos) * 1.0;
            }

            var angleRadians = Math.Acos(cos);
            return angleRadians;
        }

        public double AngleFromMainAxisClockwise(VectorDouble v)
        {

            var angleRadians = AngleBetween(v, new VectorDouble(1, 0));
            if (v.Y < 0)
            {
                angleRadians = Math.PI * 2 - angleRadians;
            }
            return angleRadians;
        }

        public VectorDouble DivideByNumber(VectorDouble v, double number)
        {
            return new VectorDouble(v.X / number, v.Y / number);
        }

        public Vector DivideByNumber(Vector v, int number)
        {
            return new Vector(v.X / number, v.Y / number);
        }

        public VectorDouble RotateClockwise(VectorDouble v, double angleRadians)
        {
            var result = new VectorDouble
            {
                X = v.X * Math.Cos(angleRadians) - v.Y * Math.Sin(angleRadians),
                Y = v.X * Math.Sin(angleRadians) + v.Y * Math.Cos(angleRadians)
            };
            return result;
        }

        public bool Counterclockwise90DegreeRotationIsCloserToVector(VectorDouble vector, VectorDouble vectorToRotate)
        {

            var rotatedClockwise = RotateClockwise(vectorToRotate, Math.PI / 2);
            var rotatedCounterclockwise = RotateClockwise(vectorToRotate, -Math.PI / 2);

            var distance1 = ToEuclideanDistanceNoRootDouble(vector.X - rotatedClockwise.X, vector.Y - rotatedClockwise.Y);
            var distance2 = ToEuclideanDistanceNoRootDouble(vector.X - rotatedCounterclockwise.X, vector.Y - rotatedCounterclockwise.Y);

            return distance1 > distance2;
        }

    }
}
