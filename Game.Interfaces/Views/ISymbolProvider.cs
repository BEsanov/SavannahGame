using System;

namespace SavannahGames.Game.Common
{
    public interface ISymbolProvider
    {
        ISymbolProvider AddSymbol(Type animalType, char animalSymbol);
        char GetSymbol(Type animalType);
    }
}
