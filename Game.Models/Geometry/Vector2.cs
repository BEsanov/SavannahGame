using System;

namespace SavannahGames.Game.Models
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector()
        {
            X = 0;
            Y = 0;
        }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(VectorDouble vd)
        {
            X = Convert.ToInt32(vd.X);
            Y = Convert.ToInt32(vd.Y);
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Vector v = obj as Vector;
            if ((System.Object)v == null)
            {
                return false;
            }

            return (X == v.X) && (Y == v.Y);
        }

        public bool Equals(Vector v)
        {
            if ((object)v == null)
            {
                return false;
            }

  
            return (X == v.X) && (Y == v.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y );
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}
