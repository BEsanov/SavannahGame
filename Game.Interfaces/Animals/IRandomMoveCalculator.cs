using SavannahGames.Game.Models;
using SavannahGames.Game.Common;
using SavannahGames.Game.AnimalBehavior;

namespace SavannahGames.Game.Interfaces
{
    public interface IRandomMoveCalculator
    {
        Vector GetFreePositionsAndCalculate(Vector currentPosition, Field<IAnimal> field);
    }
}
