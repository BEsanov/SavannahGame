using SavannahGames.Game.Models;
using System;

namespace SavannahGames.Game.AnimalBehavior
{
    public class BirthEventArgs : EventArgs
    {
        public Vector Position { get; set; }
        public BirthEventArgs(Vector position)
        {
            Position = position;
        }

        
    }
}
