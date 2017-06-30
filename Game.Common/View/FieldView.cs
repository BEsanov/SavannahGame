using System.Collections.Generic;
using System.Text;

namespace SavannahGames.Game.Common
{
    public class FieldView : IFieldView
    {
        private readonly Field<IAnimal> _field;
        private readonly IConsoleOutput _consoleOutput;
        private readonly IConsoleWindow _consoleWindow;
        private readonly ISymbolProvider _animalSymbolProvider;
        private readonly IStringDrawer _stringDrawer;
        private readonly StringBuilder _fieldAsStringBuilder;

        public const int INDENT_HORIZONTAL = 1, INDENT_VERTICAL = 1;

        
        public const int MAGIC_1_TO_REMOVE_SCROLLBAR = 1;
        

        public FieldView()
        {

        }

        public FieldView(Field<IAnimal> field)
        {
            _field = field;
        }

        public FieldView(Field<IAnimal> field, IConsoleOutput consoleOutput, IConsoleWindow consoleWindow, ISymbolProvider animalSymbolProvider, IStringDrawer stringDrawer, StringBuilder fieldAsStringBuilder) : this(field)
        {
            _consoleOutput = consoleOutput;
            _consoleWindow = consoleWindow;
            _animalSymbolProvider = animalSymbolProvider;
            _fieldAsStringBuilder = fieldAsStringBuilder;
            _stringDrawer = stringDrawer;
        }

        public void Init()
        {
            int width = _field.Size.X + INDENT_HORIZONTAL * 2,
                height = _field.Size.Y + INDENT_VERTICAL * 2 + MAGIC_1_TO_REMOVE_SCROLLBAR;
            _consoleWindow.Title = "Run or Die";
            _consoleWindow.SetWindowSize(width, height);
            _consoleWindow.SetBufferSize(width, height);
            _fieldAsStringBuilder.Length = width * (height - MAGIC_1_TO_REMOVE_SCROLLBAR);
            _stringDrawer.Setup(_consoleWindow.WindowWidth, _fieldAsStringBuilder);
        }

        public void DrawBorders()
        {

            _stringDrawer.DrawLineOfSymbols(true, INDENT_VERTICAL - 1, 
                INDENT_HORIZONTAL, INDENT_HORIZONTAL + _field.Size.X - 1, '|');
            _stringDrawer.DrawLineOfSymbols(true, _field.Size.Y + INDENT_VERTICAL,
                INDENT_HORIZONTAL, INDENT_HORIZONTAL + _field.Size.X - 1, '|');

            _stringDrawer.DrawLineOfSymbols(false, INDENT_HORIZONTAL - 1,
                INDENT_VERTICAL, INDENT_VERTICAL + _field.Size.Y-1, '|');
            _stringDrawer.DrawLineOfSymbols(false, _field.Size.X + INDENT_HORIZONTAL,
                INDENT_VERTICAL, INDENT_VERTICAL + _field.Size.Y - 1, '|');

            _stringDrawer.PutSymbol(INDENT_HORIZONTAL - 1, INDENT_VERTICAL - 1, '=');
            _stringDrawer.PutSymbol(_field.Size.X + INDENT_HORIZONTAL, INDENT_VERTICAL - 1, '=');
            _stringDrawer.PutSymbol(INDENT_HORIZONTAL - 1, _field.Size.Y + INDENT_VERTICAL, '=');
            _stringDrawer.PutSymbol(_field.Size.X + INDENT_HORIZONTAL, _field.Size.Y + INDENT_VERTICAL, '=');
        }

        public void ShowFieldState(List<IAnimal> animals)
        {
            _stringDrawer.FillRectWithSymbol(INDENT_HORIZONTAL, INDENT_VERTICAL,
                               _field.Size.X, _field.Size.Y, Constants.EMPTY_SYMBOL);

            foreach (var animal in animals)
            {
                _stringDrawer.PutSymbol(INDENT_HORIZONTAL + animal.Position.X, INDENT_VERTICAL + animal.Position.Y, _animalSymbolProvider.GetSymbol(animal.GetType()));
            }

            _consoleOutput.Clear();
            _consoleOutput.Write(_fieldAsStringBuilder.ToString());  
        }
    }
}
