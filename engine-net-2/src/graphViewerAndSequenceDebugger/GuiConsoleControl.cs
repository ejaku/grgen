/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Drawing;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public partial class GuiConsoleControl : UserControl
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

        char enteredKey = '\0';
        bool escapePressed = false;

        //--------------------------------

        public GuiConsoleControl()
        {
            InitializeComponent();

            theRichTextBox.PreviewKeyDown += GuiConsoleRichTextBox_PreviewKeyDown;
            theRichTextBox.KeyPress += GuiConsoleRichTextBox_KeyPress;

            theRichTextBox.ReadOnly = true;
        }

        private void GuiConsoleRichTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            escapePressed = e.KeyData == Keys.Escape;
        }

        private void GuiConsoleRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            enteredKey = e.KeyChar;
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
            theRichTextBox.AppendText(value + "\r\n");
        }

        public void WriteLine(string format, params object[] arg)
        {
            theRichTextBox.AppendText(String.Format(format, arg) + "\r\n");
        }

        public void WriteLine()
        {
            theRichTextBox.AppendText("\r\n");
        }

        public void ErrorWrite(string value)
        {
            Write(value);
        }

        public void ErrorWrite(string format, params object[] arg)
        {
            Write(format, arg);
        }

        public void ErrorWriteLine(string value)
        {
            WriteLine(value);
        }

        public void ErrorWriteLine(string format, params object[] arg)
        {
            WriteLine(format, arg);
        }

        public void ErrorWriteLine()
        {
            WriteLine();
        }

        public void PrintHighlighted(String text, de.unika.ipd.grGen.libGr.HighlightingMode mode)
        {
            Color oldColor = theRichTextBox.SelectionColor;
            Color oldBackColor = theRichTextBox.BackColor;
            Font oldFont = theRichTextBox.Font;

            TextAttributes textAttributes = HighlightingModeToTextAttributes(mode);
            theRichTextBox.SelectionColor = textAttributes.foregroundColor;
            theRichTextBox.SelectionBackColor = textAttributes.backgroundColor;
            theRichTextBox.SelectionFont = new Font(theRichTextBox.Font, textAttributes.fontStyle);
            Write(text);

            theRichTextBox.SelectionColor = oldColor;
            theRichTextBox.BackColor = oldBackColor;
            theRichTextBox.Font = oldFont;
        }

        // TODO: Printing should use low level color codes, there should be a layer translating high level semantic meaning to color codes
        private static TextAttributes HighlightingModeToTextAttributes(libGr.HighlightingMode mode)
        {
            switch(mode)
            {
                case libGr.HighlightingMode.Focus: return new TextAttributes(Color.Yellow, Color.Black, FontStyle.Bold);
                case libGr.HighlightingMode.FocusSucces: return new TextAttributes(Color.Green, Color.Black, FontStyle.Bold);
                case libGr.HighlightingMode.LastSuccess: return new TextAttributes(Color.Black, Color.Green);
                case libGr.HighlightingMode.LastFail: return new TextAttributes(Color.Black, Color.Red);
                case libGr.HighlightingMode.Breakpoint: return new TextAttributes(Color.Red);
                case libGr.HighlightingMode.Choicepoint: return new TextAttributes(Color.Magenta);
                case libGr.HighlightingMode.SequenceStart: return new TextAttributes(Color.Blue);

                case libGr.HighlightingMode.GrsFile: return new TextAttributes(Color.Red);
                case libGr.HighlightingMode.GrsiFile: return new TextAttributes(Color.Magenta);
                case libGr.HighlightingMode.GrgFile: return new TextAttributes(Color.Green);
                case libGr.HighlightingMode.GriFile: return new TextAttributes(Color.Cyan);
                case libGr.HighlightingMode.GmFile: return new TextAttributes(Color.Blue);
                case libGr.HighlightingMode.Directory: return new TextAttributes(Color.Yellow);

                default: return new TextAttributes(Color.White);
            }
        }

        public string ReadLine()
        {
            string lineRead = "";
            while(true)
            {
                if(enteredKey != '\0')
                {
                    Write(new string(enteredKey, 1));
                    if(enteredKey == '\n' || enteredKey == '\r')
                    {
                        enteredKey = '\0';
                        return lineRead;
                    }
                    lineRead += enteredKey;
                    enteredKey = '\0';
                }
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            while(true)
            {
                if(enteredKey != '\0')
                {
                    if(!intercept)
                        Write(new string(enteredKey, 1));
                    // TODO: full ConsoleKey mapping? not needed as ow now, but for the sake of completeness; code available on the net?
                    ConsoleKeyInfo ret = new ConsoleKeyInfo(enteredKey, escapePressed ? ConsoleKey.Escape : (enteredKey == '\u0003' ? ConsoleKey.C : ConsoleKey.NoName), false, false, enteredKey == '\u0003' ? true : false);
                    enteredKey = '\0';
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
                if(enteredKey != '\0')
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
    }
}
