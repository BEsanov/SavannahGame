namespace SavannahGames.Game.Models
{
    public class VectorDouble
    {
        public double X { get; set; }
        public double Y { get; set; }

        public VectorDouble()
        {
            X = 0;
            Y = 0;
        }

        public VectorDouble(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            VectorDouble v = obj as VectorDouble;
            if ((System.Object)v == null)
            {
                return false;
            }

            return (X == v.X) && (Y == v.Y);
        }

        public bool Equals(VectorDouble v)
        {
            if ((object)v == null)
            {
                return false;
            }


            return (X == v.X) && (Y == v.Y);
        }

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y;
        }


        public static VectorDouble operator +(VectorDouble v1, VectorDouble v2)
        {
            return new VectorDouble(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static VectorDouble operator -(VectorDouble v1, VectorDouble v2)
        {
            return new VectorDouble(v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}
