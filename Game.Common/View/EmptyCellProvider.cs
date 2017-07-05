
using SavannahGames.Game.AnimalBehavior;
using SavannahGames.Game.Interfaces;
using SavannahGames.Game.Models;
using System.Collections.Generic;

namespace SavannahGames.Game.Common
{
    public class EmptyCellProvider : IEmptyCellProvider
    {

        private readonly IBorderChecker _borderChecker;

        public EmptyCellProvider(IBorderChecker borderChecker)
        {
            _borderChecker = borderChecker;
        }

        public List<Vector> GetCellsFromFieldRegion(Field<IAnimal> field, Vector leftTop, Vector bottomRight)
        {
            var result = new List<Vector>();

            for (int i = leftTop.X; i <= bottomRight.X; i++)
            {
                for (int j = leftTop.Y; j <= bottomRight.Y; j++)
                {
                    var currentPos = new Vector(i, j);

                    if (field[i, j] == null && !_borderChecker.IsOnBorder(field.Size, currentPos))
                    {
                        result.Add(currentPos);
                    }
                }
            }
            return result;
        }
    }
}