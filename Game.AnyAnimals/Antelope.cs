using System.Collections.Generic;
using System;
using SavannahGames.Game.Common;
using SavannahGames.Game.Interfaces;
using SavannahGames.Game.Models;
namespace SavannahGames.Game.AnyAnimals
{
    public class Antelope : AbstractAnimal
    {
        private readonly IRandomMoveCalculator _randomMoveCalculator;
        private readonly IBorderChecker _borderChecker;
        private readonly IVectorMath _vectorMath;

        protected override int _health { get; set; } = 50;
        protected override int _visionRange { get; set; } = 10;
        protected override int _matingRange { get; set; } = 2;

       

        private Antelope _matingTarget = null;
        private const int ROUNDS_TO_REPRODUCE = 3;
        private int _roundsLeftUntilBirth = ROUNDS_TO_REPRODUCE;


        public Antelope()
        {
        }

        public Antelope(Vector position, IRandomMoveCalculator randomMoveCalculator, IBorderChecker borderChecker, IVectorMath vectorMath)
        {
            Position = position;
            _randomMoveCalculator = randomMoveCalculator;
            _borderChecker = borderChecker;
            _vectorMath = vectorMath;
        }

        public bool MateStillAround(Field<IAnimal> field)
        {
            for (int i = -_matingRange; i <= _matingRange; i++)
            {
                for (int j = -_matingRange; j <= _matingRange; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    var checkedCell = field[Position.X + i, Position.Y + j];
                    if (checkedCell != null && checkedCell == _matingTarget)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Antelope FindNewMate(Field<IAnimal> field)
        {
            var closestMateDistance = int.MaxValue;
            Antelope newMate = null;

            for (int i = -_matingRange; i <= _matingRange; i++)
            {
                for (int j = -_matingRange; j <= _matingRange; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    var checkedCellAsAntelope = field[Position.X + i, Position.Y + j] as Antelope;
                    if (checkedCellAsAntelope != null && _vectorMath.ToEightWayDistance(i, j) < closestMateDistance)
                    {
                        newMate = checkedCellAsAntelope;
                        closestMateDistance = _vectorMath.ToEightWayDistance(Position - newMate.Position);
                    }
                }
            }
            return newMate;
        }

        private void PerformMating(Field<IAnimal> field)
        {
            if (_roundsLeftUntilBirth <= 0)
            {
                var cellBetweenParents = _vectorMath.DivideByNumber(Position + _matingTarget.Position, 2);
                GibBirth(new BirthEventArgs(cellBetweenParents));
                _roundsLeftUntilBirth = ROUNDS_TO_REPRODUCE;
            }

            if (_matingTarget != null && MateStillAround(field))
            {
                _roundsLeftUntilBirth--;
            }
            else
            {
                _matingTarget = FindNewMate(field);
                _roundsLeftUntilBirth = ROUNDS_TO_REPRODUCE;
            }
        }

        public override bool TryMove(Field<IAnimal> field, out Vector nextMove)
        {
            if (!TryDecreaseHealth())
            {
                nextMove = new Vector();
                return false;
            }

            PerformMating(field);

            var vectorsLionToAntelope = CollectVectorsFromNearbyLions(field);

            if (vectorsLionToAntelope.Count > 0)
            {
                nextMove = _borderChecker.FitVectorIntoBorders(field.Size, Position, CalculateMoveByNearbyLions(vectorsLionToAntelope));
                
            }
            else
            {
                nextMove = _randomMoveCalculator.GetFreePositionsAndCalculate(Position, field);
            }

            var p = Position + nextMove;
            return field[p.X, p.Y] == null;
        }

        internal List<Vector> CollectVectorsFromNearbyLions(Field<IAnimal> field)
        {
            var vectorsLionToAntelope = new List<Vector>();

            for (int i = -_visionRange; i <= _visionRange; i++)
            {
                for (int j = -_visionRange; j <= _visionRange; j++)
                {
                    if (field[Position.X + i, Position.Y + j]?.GetType() == typeof(Lion))
                    {
                        vectorsLionToAntelope.Add(new Vector(-i, -j));
                    }
                }
            }

            return vectorsLionToAntelope;
        }

        internal List<VectorDouble> WeighByDistance(List<Vector> vectors)
        {
            var vectorsWeighedByDistance = new List<VectorDouble>();

            foreach (var v in vectors)
            {
                vectorsWeighedByDistance.Add(
                    _vectorMath.DivideByNumber(
                        _vectorMath.NormalizeDouble(new VectorDouble(v.X, v.Y)),
                        _vectorMath.ToEightWayDistance(v)
                    )
                );
            }
            return vectorsWeighedByDistance;
        }

        internal Vector CalculateMoveByNearbyLions(List<Vector> vectorsLionToAntelope)
        {
            var vectorsWeighedByDistance = WeighByDistance(vectorsLionToAntelope);
            var resultingVector = new VectorDouble(0, 0);

            foreach (var processedVector in vectorsWeighedByDistance)
            {
                var signedAngleResultToCurrentRadians = _vectorMath.SignedAngleBetween(resultingVector, processedVector);

                var currentAngleAbs = Math.Abs(signedAngleResultToCurrentRadians);
                if (currentAngleAbs < Constants.DELTA)
                {
                    resultingVector += processedVector;
                    continue;
                }

                if (currentAngleAbs >= Math.PI - Constants.DELTA && currentAngleAbs <= Math.PI + Constants.DELTA)
                {
                    Perform180DegreeLogics(ref signedAngleResultToCurrentRadians, vectorsWeighedByDistance,
                        processedVector);
                }
       
                // ToDo: weigh angle by distance. 
                signedAngleResultToCurrentRadians /= 2;

                resultingVector = _vectorMath.RotateClockwise(resultingVector, signedAngleResultToCurrentRadians) +
                                  _vectorMath.RotateClockwise(processedVector, -signedAngleResultToCurrentRadians);

            }
            resultingVector = _vectorMath.NormalizeDouble(resultingVector);
          
            return new Vector(resultingVector); 
        }

        public void Perform180DegreeLogics(ref double signedAngleResultToCurrentRadians, List<VectorDouble> vectorsWeighedByDistance, VectorDouble processedVector)
        {
            signedAngleResultToCurrentRadians = Math.PI;

            var currentIndex = vectorsWeighedByDistance.IndexOf(processedVector);

            if (currentIndex == (vectorsWeighedByDistance.Count - 1)) return;

            if (_vectorMath.Counterclockwise90DegreeRotationIsCloserToVector(vectorsWeighedByDistance[currentIndex + 1], processedVector))
            {
                signedAngleResultToCurrentRadians *= -1;
            }
        }
    }
}
