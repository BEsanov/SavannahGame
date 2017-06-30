
namespace SavannahGames.Game.Common
{
    public interface IBuilder
    {
        T GetAnimal<T>(Vector position) where T : IAnimal;
        Field<IAnimal> GetFieldOfAnimals(int width, int height);
        FieldView GetFieldView(Field<IAnimal> field);
        GameManager GetGameManager();
    }
}