using SavannahGames.Game.AnimalBehavior;
using SavannahGames.Game.Common;
using SavannahGames.Game.Models;
using SavannahGames.Game.Views;

namespace SavannahGames.Game.Main
{
    public interface IBuilder
    {
        T GetAnimal<T>(Vector position) where T : IAnimal;
        Field<IAnimal> GetFieldOfAnimals(int width, int height);
        FieldView GetFieldView(Field<IAnimal> field);
        GameManager GetGameManager();
    }
}