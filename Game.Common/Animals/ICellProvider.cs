using System.Collections.Generic;

namespace SavannahGames.Game.Common
{
    public interface IEmptyCellProvider
    {
        List<Vector> GetCellsFromFieldRegion(Field<IAnimal> field, Vector leftTop, Vector bottomRight);
    }
}