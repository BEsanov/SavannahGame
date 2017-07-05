
using SavannahGames.Game.AnyAnimals;
using SavannahGames.Game.Common;
using SavannahGames.Game.Interfaces;
using SavannahGames.Game.Models;
using System;
using System.Collections.Generic;

namespace SavannahGames.Game.Main
{
    public class GameManager
    {
        private readonly List<IAnimal> _aliveAnimals = new List<IAnimal>();
        private readonly List<IAnimal> _animalsToBeBorn = new List<IAnimal>();
        private readonly HashSet<IAnimal> _animalsToDie = new HashSet<IAnimal>();
        private Dictionary<ConsoleKey, Action> _keyPressStrategy = new Dictionary<ConsoleKey, Action>();

        private readonly IBuilder _builder;
        private readonly IConsoleInput _consoleInput;
        private readonly IRandom _random;
        private readonly IBorderChecker _borderChecker;
        private readonly IEmptyCellProvider _emptyCellProvider;
        private Field<IAnimal> _field;
        private IFieldView _fieldView;
        
        public GameManager(IFieldView fieldView, IConsoleInput consoleInput, IBuilder builder, IRandom random, IBorderChecker borderChecker, IEmptyCellProvider emptyCellProvider)
        {
            _consoleInput = consoleInput;
            _builder = builder;
            _fieldView = fieldView;
            _random = random;
            _borderChecker = borderChecker;
            _emptyCellProvider = emptyCellProvider;
        }

        public void Setup(int fieldWidth, int fieldHeight)
        {   
            _field = _builder.GetFieldOfAnimals(fieldWidth, fieldHeight);
            _fieldView = _builder.GetFieldView(_field);
            
            _keyPressStrategy.Add(
                ConsoleKey.A, AddAnimalToRandomPosition<Antelope>);
            _keyPressStrategy.Add(
                ConsoleKey.L, AddAnimalToRandomPosition<Lion>);
        }   


        public void StartGame()
        {
            _fieldView.Init();
            _fieldView.DrawBorders();

            var input = new ConsoleKeyInfo();

            while (input.Key != ConsoleKey.Spacebar)
            {
                if (_keyPressStrategy.ContainsKey(input.Key))
                {
                    _keyPressStrategy[input.Key]();
                }

                _fieldView.ShowFieldState(_aliveAnimals);
                CalculateGameIteration(_aliveAnimals, _animalsToDie);
                input = _consoleInput.ReadKey();
            }
        }

        public void CalculateGameIteration(List<IAnimal> aliveAnimals, HashSet<IAnimal> animalsToDie)
        {
            foreach (var animal in aliveAnimals)
            {
                if (animalsToDie.Contains(animal)) continue;

                Vector nextMove;
                if (animal.TryMove(_field, out nextMove))
                {
                    AdjustPosition(_field, animal, nextMove);
                }
            }

            foreach (var animal in animalsToDie)
            {
                RemoveAnimal(animal);
            }

            _aliveAnimals.AddRange(_animalsToBeBorn);
            _animalsToBeBorn.Clear();
        }

        public void AddAnimal<T>(Vector position, List<IAnimal> list) where T: IAnimal
        {
            var animal = _builder.GetAnimal<T>(position);
            _field[position.X, position.Y] = animal;
            list.Add(animal);
            animal.Death += DeathHandler;
            animal.Birth += BirthHandler<T>;
        }

        public void AddAnimalToRandomPosition<T>() where T : IAnimal
        {
            int x, y;
            while (true)
            {
                x = _random.Next(0, _field.Size.X);
                y = _random.Next(0, _field.Size.Y);
                if (_field[x, y] == null)
                {
                    break;
                }
            }
            AddAnimal<T>(new Vector(x, y), _aliveAnimals);

        }

        public void RemoveAnimal(IAnimal a)
        {
            _aliveAnimals.Remove(a);
        }

        public void DeathHandler(object sender, EventArgs args)
        {
            var animal = sender as IAnimal;
            if (animal == null) return;

            _field[animal.Position.X, animal.Position.Y] = null;
            _animalsToDie.Add(animal);
        }

        public void BirthHandler<T>(object sender, BirthEventArgs args) where T: IAnimal
        {
            var randomRange = new Vector(1,1);
            var availablePositions = _emptyCellProvider.GetCellsFromFieldRegion(_field, args.Position - randomRange,
                args.Position + randomRange);
            

            if (availablePositions.Count > 0)
            {
                var chosenPosition = availablePositions[_random.Next(0, availablePositions.Count)];
                AddAnimal<T>(chosenPosition, _animalsToBeBorn);
            }
        }

        public void AdjustPosition(Field<IAnimal> field, IAnimal animal, Vector nextMove)
        {
            nextMove = _borderChecker.FitVectorIntoBorders(_field.Size, animal.Position, nextMove);
            var nextPosition = animal.Position + nextMove;
            if (field[nextPosition.X, nextPosition.Y] != null)
            {
                return;
            }

            field[animal.Position.X, animal.Position.Y] = null;
            field[nextPosition.X, nextPosition.Y] = animal;
            animal.Position = nextPosition;
        }
    }
}
