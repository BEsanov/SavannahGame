
using System;

namespace SavannahGames.Game.Common
{
    public class Lion : AbstractAnimal
    {
        protected override int _health { get; set; } = 30;
        protected override int _visionRange { get; set; } = 20;
        protected override int _matingRange { get; set; } = 5;

        private int _healthForAntelope = 20;
        private Antelope _targetAntelope;

        private readonly IRandomMoveCalculator _randomMoveCalculator;
        private readonly IBorderChecker _borderChecker;
        private readonly IVectorMath _vectorMath;

        public Lion()
        {
        }

        public Lion(Vector position, IRandomMoveCalculator randomMoveCalculator, IBorderChecker borderChecker, IVectorMath vectorMath)
        {
            Position = position;
            _randomMoveCalculator = randomMoveCalculator;
            _borderChecker = borderChecker;
            _vectorMath = vectorMath;
        }

        internal Vector CalculateMoveToFollowTarget(Field<IAnimal> field)
        {
            var move = _vectorMath.Normalize(_targetAntelope.Position - Position);
            var nextPos = Position + move;
            var nextCell = field[nextPos.X, nextPos.Y];
            if (nextCell == _targetAntelope || nextCell == null)
            {
                return move;
            }
            return new Vector(0, 0);
        }

        public override bool TryMove(Field<IAnimal> field, out Vector nextMove)
        {
            nextMove = new Vector(0, 0);
            if (!TryDecreaseHealth())
            {
                return false;
            }

            _targetAntelope = FindClosestAntelope(field);

            if (_targetAntelope == null)
            { 
                nextMove = _randomMoveCalculator.GetFreePositionsAndCalculate(Position, field);
            }
            else
            {
                nextMove = CalculateMoveToFollowTarget(field);
                var nextPos = Position + nextMove;
                if (field[nextPos.X, nextPos.Y] == _targetAntelope)
                {
                    _health += _healthForAntelope;
                    _targetAntelope.Die(EventArgs.Empty);
                    _targetAntelope = null;
                }
            }
            return true;
        }

        public Antelope FindClosestAntelope(Field<IAnimal> field)
        {
            Antelope targetAntelope = null;

            for (int i = -_visionRange; i <= _visionRange; i++)
            {
                for (int j = -_visionRange; j <= _visionRange; j++)
                {
                    var currentCellAsAntelope = field[Position.X + i, Position.Y + j] as Antelope;

                    var currentIsCloser = targetAntelope == null ||  
                        _vectorMath.ToEightWayDistance(new Vector(i, j)) < _vectorMath.ToEightWayDistance(Position - targetAntelope.Position);

                    if (currentCellAsAntelope != null && currentIsCloser)
                    {
                        targetAntelope = currentCellAsAntelope;
                    }
                }
            }

            return targetAntelope;
        }

    }
}

