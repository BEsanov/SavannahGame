using System.Collections.Generic;
using Conditions;
namespace SavannahGames.Game.Common
{
    public class RandomMoveCalculator : IRandomMoveCalculator
    {

        private static IRandom _random;


        public RandomMoveCalculator()
        {

        }

        public RandomMoveCalculator(IRandom random)
        {
       
            _random = random;
        }


 
        public Vector GetFreePositionsAndCalculate(Vector currentPosition, Field<IAnimal> field)
        {
            currentPosition.Requires()
                .IsNotNull();
            field.Requires()
                .IsNotNull();
            currentPosition.X.Requires()
                .IsInRange(0, field.Size.X - 1);
            currentPosition.Y.Requires()
                .IsInRange(0, field.Size.Y - 1);

            var result = new Vector();
            var availablePositions = new List<Vector>();


            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var checkedPos = new Vector(currentPosition.X + i, currentPosition.Y + j);

                    if (field[checkedPos.X, checkedPos.Y] == null)
                    {
                        availablePositions.Add(new Vector(i,j));
                    }
                }
            }
            if (availablePositions.Count != 0)
            {
                result = availablePositions[_random.Next(0, availablePositions.Count)];
            }

            return result;
        }
    }
}
