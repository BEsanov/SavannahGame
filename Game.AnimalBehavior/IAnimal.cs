﻿
using SavannahGames.Game.Models;
using System;

namespace SavannahGames.Game.AnimalBehavior
{
    public interface IAnimal
    {
        event EventHandler Death;

        event EventHandler<BirthEventArgs> Birth;

        Vector Position { get; set; }

        bool TryMove(Field<IAnimal> field, out Vector nextMove);
    }
}
