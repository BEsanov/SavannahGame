﻿using StructureMap;
namespace SavannahGames.Game.Common
{
    public class DefaultRegistry : Registry
    {

        public DefaultRegistry()
        {
            For<IConsoleOutput>().Use<ConsoleOutput>();
            For<IConsoleInput>().Use<ConsoleInput>();
            For<IConsoleWindow>().Use<ConsoleWindow>();
            For<IStringDrawer>().Use<StringDrawer>();
            For<IRandomMoveCalculator>().Use<RandomMoveCalculator>();
            For<IBorderChecker>().Use<BorderChecker>();
            For<IVectorMath>().Use<VectorMath>();
            For<IRandom>().Use<RandomFacade>();
            For<IBuilder>().Use<Builder>();
            For<IFieldView>().Use<FieldView>();
            For<ISymbolProvider>().Use<AnimalSymbolProvider>();
            For<IEmptyCellProvider>().Use<EmptyCellProvider>();
        }
    }
}
