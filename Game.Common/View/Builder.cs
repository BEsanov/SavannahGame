
using StructureMap;

namespace SavannahGames.Game.Common
{
    public class Builder : IBuilder
    {
        private readonly IContainer _container;

        public Builder()
        {
        }

        public Builder(IContainer container)
        {
            _container = container;
        }

        private AnimalSymbolProvider SetupAnimalSymbolProvider()
        {
            var animalSymbolProvider = _container.GetInstance<AnimalSymbolProvider>();
            animalSymbolProvider
                .AddSymbol(typeof(Lion), 'L')
                .AddSymbol(typeof(Antelope), 'A');
            return animalSymbolProvider;
        }

        public Field<IAnimal> GetFieldOfAnimals(int width, int height)
        {
            return new Field<IAnimal>(new Vector(width, height));
        }

        public GameManager GetGameManager()
        {
            return _container
                .GetInstance<GameManager>();
        }

        public FieldView GetFieldView(Field<IAnimal> field)
        {
            return _container
                .With(field)
                .With("animalSymbolProvider").EqualTo(SetupAnimalSymbolProvider())
                .GetInstance<FieldView>();
        }

        public T GetAnimal<T>(Vector position) where T: IAnimal
        {
            return _container
                .With(position)
                .GetInstance<T>();
        }

        public static Builder GetInstance()
        {
            return new Builder(new Container(new DefaultRegistry()));
        }
    }
}
