using System.Collections.Generic;
using SavannahGames.Game.Models;
using SavannahGames.Game.Common;
using SavannahGames.Game.AnimalBehavior;

namespace SavannahGames.Game.Interfaces
{
    public interface IEmptyCellProvider
    {
        List<Vector> GetCellsFromFieldRegion(Field<IAnimal> field, Vector leftTop, Vector bottomRight);
    }
}