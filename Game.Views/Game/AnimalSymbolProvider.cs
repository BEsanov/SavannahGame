using SavannahGames.Game.Common;
using System;
using System.Collections.Generic;

namespace SavannahGames.Game.Views
{
    public class AnimalSymbolProvider : ISymbolProvider
    {
        Dictionary<Type, char> _animalSymbols = new Dictionary<Type, char>();

        public AnimalSymbolProvider()
        {
            
        }

        public char GetSymbol(Type animalType)
        {
            if (_animalSymbols.ContainsKey(animalType))
            {
                return _animalSymbols[animalType];
            }
            
            return '0';
        }

        public ISymbolProvider AddSymbol(Type animalType, char animalSymbol)
        {
            _animalSymbols.Add(animalType, animalSymbol);
            return this;
        }
    }
}
