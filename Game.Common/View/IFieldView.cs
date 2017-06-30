using System.Collections.Generic;


namespace SavannahGames.Game.Common
{
    public interface IFieldView
    {
        void DrawBorders();
        void Init();
        void ShowFieldState(List<IAnimal> animals);
    }
}