
namespace SavannahGames.Game.Common
{
    public interface IRandomMoveCalculator
    {
        Vector GetFreePositionsAndCalculate(Vector currentPosition, Field<IAnimal> field);
    }
}
