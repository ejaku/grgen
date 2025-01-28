/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Drawing;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public partial class GuiConsoleControl : UserControl, IDebuggerConsoleUICombined
    {
        internal class TextAttributes
        {
            public TextAttributes(Color foregroundColor, Color backgroundColor, FontStyle fontStyle)
            {
                this.foregroundColor = foregroundColor;
                this.backgroundColor = backgroundColor;
                this.fontStyle = fontStyle;
            }

            public TextAttributes(Color foregroundColor, Color backgroundColor)
            {
                this.foregroundColor = foregroundColor;
                this.backgroundColor = backgroundColor;
                this.fontStyle = FontStyle.Regular;
            }

            public TextAttributes(Color foregroundColor)
            {
                this.foregroundColor = foregroundColor;
                this.backgroundColor = Color.Black;
                this.fontStyle = FontStyle.Regular;
            }

            public Color foregroundColor;
            public Color backgroundColor;
            public FontStyle fontStyle;
        }

        //--------------------------------

        char enteredCharacter = '\0';
        ConsoleKey enteredKey = ConsoleKey.NoName;
        bool escapePressed = false;
        char enteredNextCharacter = '\0'; // mini queue of 2 entries (potential TODO: introduce real queue)
        ConsoleKey enteredNextKey = ConsoleKey.NoName;
        bool cancel = false;

        public void Cancel()
        {
            cancel = true;
        }

        public bool EnableClear
        {
            get { return enableClear; }
            set { enableClear = value; }
        }
        bool enableClear = false;

        //--------------------------------

        public GuiConsoleControl()
        {
            InitializeComponent();

            theRichTextBox.PreviewKeyDown += GuiConsoleRichTextBox_PreviewKeyDown;
            theRichTextBox.KeyPress += GuiConsoleRichTextBox_KeyPress;

            theRichTextBox.ReadOnly = true;
            theRichTextBox.HideSelection = false;

            // we prefer Courier New, but if this one is not available, the system's generic monospace default font
            theRichTextBox.Font = new Font("Courier New", 11);
            if(theRichTextBox.Font.FontFamily != FontFamily.GenericMonospace)
                theRichTextBox.Font = new Font(FontFamily.GenericMonospace, 11);
            theRichTextBox.SelectionFont = new Font("Courier New", 11);
            if(theRichTextBox.SelectionFont.FontFamily != FontFamily.GenericMonospace)
                theRichTextBox.SelectionFont = new Font(FontFamily.GenericMonospace, 11);
        }

        // simulates user input
        public void EnterKey(char character, ConsoleKey key)
        {
            enteredCharacter = character;
            enteredKey = key;
        }

        // simulates user input and following user input
        public void EnterKey(char character, ConsoleKey key, char nextCharacter, ConsoleKey nextKey)
        {
            enteredCharacter = character;
            enteredKey = key;
            enteredNextCharacter = nextCharacter;
            enteredNextKey = nextKey;
        }

        private void GuiConsoleRichTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            escapePressed = e.KeyData == Keys.Escape;
        }

        private void GuiConsoleRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            enteredCharacter = e.KeyChar;
            e.Handled = true;
        }

        //--------------------------------

        public void Write(string value)
        {
            theRichTextBox.AppendText(value);
        }

        public void Write(string format, params object[] arg)
        {
            theRichTextBox.AppendText(String.Format(format, arg));
        }

        public void WriteLine(string value)
        {
            theRichTextBox.AppendText(value + Environment.NewLine);
        }

        public void WriteLine(string format, params object[] arg)
        {
            theRichTextBox.AppendText(String.Format(format, arg) + Environment.NewLine);
        }

        public void WriteLine()
        {
            theRichTextBox.AppendText(Environment.NewLine);
        }

        public void PrintHighlightedUserDialog(String text, de.unika.ipd.grGen.libGr.HighlightingMode mode)
        {
            PrintHighlighted(text, mode);
        }

        public string ReadLine()
        {
            string lineRead = "";
            while(true)
            {
                if(cancel)
                    throw new OperationCanceledException();
                if(enteredCharacter != '\0')
                {
                    Write(new string(enteredCharacter, 1));
                    if(enteredCharacter == '\n' || enteredCharacter == '\r')
                    {
                        ClearEnteredKey();
                        return lineRead;
                    }
                    else if(enteredCharacter == '\u001B')
                    {
                        ClearEnteredKey();
                        WriteLine(); // to be revisited when a real ESC key press is supported as a means of canceling input (also on the real text console), as of now only entering an empty line is interpreted as a means of canceling input, and realized by the GUI by sending fake ESC key presses
                        return "";
                    }
                    lineRead += enteredCharacter;
                    ClearEnteredKey();
                }
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            while(true)
            {
                if(cancel)
                    return new ConsoleKeyInfo('\u0003', ConsoleKey.C, false, false, true); // TODO: maybe throw new OperationCanceledException(); cause this is ReadKey without WithControlCAsInput
                if(enteredCharacter != '\0')
                {
                    if(!intercept)
                        Write(new string(enteredCharacter, 1));
                    // TODO: full ConsoleKey mapping? not needed as ow now, but for the sake of completeness; code available on the net?
                    ConsoleKey key = escapePressed ? ConsoleKey.Escape : (enteredCharacter == '\u0003' ? ConsoleKey.C : ConsoleKey.NoName);
                    if(enteredKey >= ConsoleKey.F1 && enteredKey <= ConsoleKey.F24)
                        key = enteredKey;
                    ConsoleKeyInfo ret = new ConsoleKeyInfo(enteredCharacter, key, false, false, enteredCharacter == '\u0003' ? true : false);
                    ClearEnteredKey();
                    return ret;
                }
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        public ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            return ReadKey(true);
        }

        public bool KeyAvailable
        {
            get
            {
                if(enteredCharacter != '\0')
                {
                    return true;
                }
                else
                {
                    //System.Threading.Thread.Sleep(1);
                    Application.DoEvents();
                    return false;
                }
            }
        }

        private void ClearEnteredKey()
        {
            enteredCharacter = '\0';
            enteredKey = ConsoleKey.NoName;
            if(enteredNextCharacter != '\0')
            {
                enteredCharacter = enteredNextCharacter;
                enteredKey = enteredNextKey;
                enteredNextCharacter = '\0';
                enteredNextKey = ConsoleKey.NoName;
            }
        }

        //--------------------------------

        public void WriteDataRendering(string value)
        {
            Write(value);
        }

        public void WriteDataRendering(string format, params object[] arg)
        {
            Write(format, arg);
        }

        public void WriteLineDataRendering(string value)
        {
            WriteLine(value);
        }

        public void WriteLineDataRendering(string format, params object[] arg)
        {
            WriteLine(format, arg);
        }

        public void WriteLineDataRendering()
        {
            WriteLine();
        }

        public void PrintHighlighted(String text, de.unika.ipd.grGen.libGr.HighlightingMode mode)
        {
            Color oldColor = theRichTextBox.SelectionColor;
            Color oldBackColor = theRichTextBox.SelectionBackColor;
            Font oldFont = theRichTextBox.SelectionFont;

            TextAttributes textAttributes = HighlightingModeToTextAttributes(mode);
            theRichTextBox.SelectionColor = textAttributes.foregroundColor;
            theRichTextBox.SelectionBackColor = textAttributes.backgroundColor;
            theRichTextBox.SelectionFont = new Font(theRichTextBox.SelectionFont, textAttributes.fontStyle);
            WriteDataRendering(text);

            theRichTextBox.SelectionColor = oldColor;
            theRichTextBox.SelectionBackColor = oldBackColor;
            theRichTextBox.SelectionFont = oldFont;
        }

        // TODO: Printing should use low level color codes, there should be a layer translating high level semantic meaning to color codes
        private static TextAttributes HighlightingModeToTextAttributes(libGr.HighlightingMode mode)
        {
            switch(mode)
            {
                case libGr.HighlightingMode.Focus: return new TextAttributes(Color.Yellow, Color.Black, FontStyle.Bold); // with Courier New the width stays the same when using bold face, with generic monospace hopefully too, so re-enabled (side effect of jumpy display due to increased text size is considered worse than the additional visual cue of bolding)
                case libGr.HighlightingMode.FocusSucces: return new TextAttributes(Color.Lime, Color.Black, FontStyle.Bold);
                case libGr.HighlightingMode.LastSuccess: return libGr.WorkaroundManager.IsMono ? new TextAttributes(Color.Green) : new TextAttributes(Color.Black, Color.Green); // workaround fun as it seems that the mono RichTextBox is not able to change the selection background color
                case libGr.HighlightingMode.LastFail: return libGr.WorkaroundManager.IsMono ? new TextAttributes(Color.DarkRed) : new TextAttributes(Color.Black, Color.DarkRed);
                case libGr.HighlightingMode.Breakpoint: return new TextAttributes(Color.Red);
                case libGr.HighlightingMode.Choicepoint: return new TextAttributes(Color.Magenta);
                case libGr.HighlightingMode.SequenceStart: return new TextAttributes(Color.SkyBlue);

                case libGr.HighlightingMode.GrsFile: return new TextAttributes(Color.Red);
                case libGr.HighlightingMode.GrsiFile: return new TextAttributes(Color.Magenta);
                case libGr.HighlightingMode.GrgFile: return new TextAttributes(Color.Green);
                case libGr.HighlightingMode.GriFile: return new TextAttributes(Color.Cyan);
                case libGr.HighlightingMode.GmFile: return new TextAttributes(Color.Blue);
                case libGr.HighlightingMode.Directory: return new TextAttributes(Color.Yellow);

                default: return new TextAttributes(Color.White);
            }
        }

        public void Clear()
        {
            if(enableClear)
                theRichTextBox.Clear();
        }

        public void SuspendImmediateExecution()
        {
            // form TODO: buffer Writes in StringBuilder and write at once
            libGr.WorkaroundManager.Workaround.PreventRedraw(theRichTextBox.Handle);
        }

        public void RestartImmediateExecution()
        {
            libGr.WorkaroundManager.Workaround.AllowRedraw(theRichTextBox.Handle);
            theRichTextBox.Invalidate();
        }
    }
}
