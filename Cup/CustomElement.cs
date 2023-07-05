using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Globalization;
using System.Windows.Input;

namespace Cup
{
    public class MyTextBox : TextBox
    {
        private readonly Stack<string> _undoStack = new Stack<string>();
        private readonly Stack<string> _redoStack = new Stack<string>();

        public MyTextBox()
        {
            PreviewTextInput += OnTextChanged;
            PreviewKeyDown+= OnKeyDown;
        }

        public void Undo()
        {
            if (_undoStack.Count == 0)
                return;

            _redoStack.Push(Text);
            Text = _undoStack.Pop();
            CaretIndex = Text.Length;
        }

        public void Redo()
        {
            if (_redoStack.Count == 0)
                return;

            _undoStack.Push(Text);
            Text = _redoStack.Pop();
            CaretIndex = Text.Length;
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Z:
                    if(Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        Undo();
                        e.Handled = true;
                    }
                break;
                case Key.Y:
                    if(Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        Redo();
                        e.Handled = true;
                    }
                break;
            }
        }
        private void OnTextChanged(object sender, TextCompositionEventArgs e)
        {
            _undoStack.Push(Text);
            _redoStack.Clear();
        }
    }
}
