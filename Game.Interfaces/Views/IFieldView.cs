using SavannahGames.Game.AnimalBehavior;
using System.Collections.Generic;


namespace SavannahGames.Game.Interfaces
{
    public interface IFieldView
    {
        void DrawBorders();
        void Init();
        void ShowFieldState(List<IAnimal> animals);
    }
}