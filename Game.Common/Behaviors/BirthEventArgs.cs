using System;


namespace SavannahGames.Game.Common
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
