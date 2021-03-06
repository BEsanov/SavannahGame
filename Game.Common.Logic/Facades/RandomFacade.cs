﻿using SavannahGames.Game.Interfaces;
using System;

namespace SavannahGames.Game.Common.Logic
{
    public class RandomFacade : IRandom
    {
        private static Random _random;

        public RandomFacade()
        {
            _random = new Random();
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
