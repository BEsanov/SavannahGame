using System;

namespace SavannahGames.Game.Common
{
    public abstract class AbstractAnimal : IAnimal
    {
        #region abstract members
        protected abstract int _health { get; set; }
        protected abstract int _visionRange { get; set; }
        protected abstract int _matingRange { get; set; }
        public abstract bool TryMove(Field<IAnimal> field, out Vector nextMove);
        #endregion

        public event EventHandler Death;
        public event EventHandler<BirthEventArgs> Birth;
        public Vector Position { get; set; }



        public bool TryDecreaseHealth()
        {
            if (_health == 0)
            {
                Die(new EventArgs());
                return false;
            }
            _health--;
            return true;
        }

        public void Die(EventArgs e)
        {
            Death?.Invoke(this, e);
        }

        public void GibBirth(BirthEventArgs e)
        {
            Birth?.Invoke(this, e);
        }
    }
}
