
namespace SavannahGames.Game.Models
{
    public class Field<T>
    {
        private T[,] _field;
        public Vector Size { get; private set; }

        public Field()
        {

        }

        public Field(int width, int height)
        {
            Size = new Vector(width, height);
            _field = new T[Size.X + 2 * Constants.OFFSET, Size.Y + 2 * Constants.OFFSET];
        }

        public Field(Vector size)
        {
            Size = size;
            _field = new T[Size.X + 2 * Constants.OFFSET, Size.Y + 2 * Constants.OFFSET];
        }

        public T this[int i, int j]
        {
            get
            {
                return _field[i + Constants.OFFSET, j + Constants.OFFSET];
            }
            set
            {
                _field[i + Constants.OFFSET, j + Constants.OFFSET] = value;
            }
        }





    }
}
