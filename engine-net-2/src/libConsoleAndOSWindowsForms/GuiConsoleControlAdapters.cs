/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.IO;
using System.Text;

namespace de.unika.ipd.grGen.libConsoleAndOS
{
    /// <summary>
    /// A TextWriter implemented by a GuiConsoleControl / a GuiConsoleControl seen as a TextWriter, enriched by the IConsoleOutput interface.
    /// Only a subset of the methods is implemented, for one the Write(char) method needed for a minimal implementation,
    /// plus the methods directly supported by the wrapped GuiConsoleControl.
    /// </summary>
    public class GuiConsoleControlAsTextWriter : TextWriter, IConsoleOutput
    {
        public GuiConsoleControlAsTextWriter(GuiConsoleControl wrappedGuiConsoleControl)
        {
            this.wrappedGuiConsoleControl = wrappedGuiConsoleControl;
        }

        public override void Write(char value)
        {
            wrappedGuiConsoleControl.Write(new String(value, 1));
        }

        public override void Write(string value)
        {
            wrappedGuiConsoleControl.Write(value);
        }

        public override void Write(string format, params object[] arg)
        {
            wrappedGuiConsoleControl.Write(format, arg);
        }

        // TODO: code using Invoke also for the other non-WriteLine-methods (as needed - the only needed instance as of now is for the PrintStatistics timer in the shell)
        delegate void WriteLineSingleArgumentDelegate(string value);
        public override void WriteLine(string value)
        {
            WriteLineSingleArgumentDelegate writeLine = wrappedGuiConsoleControl.WriteLine;
            if(wrappedGuiConsoleControl.InvokeRequired)
                wrappedGuiConsoleControl.Invoke(writeLine, value);
            else
                wrappedGuiConsoleControl.WriteLine(value);
        }

        delegate void WriteLineMultipleArgumentsDelegate(string format, object[] arg);
        public override void WriteLine(string format, params object[] arg)
        {
            WriteLineMultipleArgumentsDelegate writeLine = wrappedGuiConsoleControl.WriteLine;
            if(wrappedGuiConsoleControl.InvokeRequired)
                wrappedGuiConsoleControl.Invoke(writeLine, format, arg);
            else
                wrappedGuiConsoleControl.WriteLine(format, arg);
        }

        delegate void WriteLineNoArgumentsDelegate();
        public override void WriteLine()
        {
            WriteLineNoArgumentsDelegate writeLine = wrappedGuiConsoleControl.WriteLine;
            if(wrappedGuiConsoleControl.InvokeRequired)
                wrappedGuiConsoleControl.Invoke(writeLine);
            else
                wrappedGuiConsoleControl.WriteLine();
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        // ------------------------------------------------------------------------

        public void PrintHighlighted(String text, HighlightingMode mode)
        {
            wrappedGuiConsoleControl.PrintHighlighted(text, mode); // TODO: xxxUserDialog?
        }

        public void Clear()
        {
            wrappedGuiConsoleControl.Clear();
        }

        // ------------------------------------------------------------------------

        private readonly GuiConsoleControl wrappedGuiConsoleControl;
    }

    /// <summary>
    /// A TextReader implemented by a GuiConsoleControl / a GuiConsoleControl seen as a TextReader, enriched by the IConsoleInput interface.
    /// Only a subset of the methods is implemented, for one the methods needed by the CSharpCC lexer,
    /// plus the methods directly supported by the wrapped GuiConsoleControl.
    /// </summary>
    public class GuiConsoleControlAsTextReader : TextReader, IConsoleInput
    {
        public GuiConsoleControlAsTextReader(GuiConsoleControl wrappedGuiConsoleControl)
        {
            this.wrappedGuiConsoleControl = wrappedGuiConsoleControl;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            if(buffer == null)
                throw new ArgumentNullException();
            if(index < 0 || count < 0)
                throw new ArgumentOutOfRangeException();
            if(buffer.Length < index + count)
                throw new ArgumentException("buffer.Length < index + count");

            // TODO: handle overly long line by an internal buffer
            string inputLine = wrappedGuiConsoleControl.ReadLine() + '\n';

            int lengthCappedByCount = Math.Min(inputLine.Length, count);
            inputLine.CopyTo(0, buffer, index, lengthCappedByCount);
            return lengthCappedByCount;
        }

        public override int Peek()
        {
            throw new NotImplementedException();
        }
        public override int Read()
        {
            throw new NotImplementedException();
        }
        public override int ReadBlock(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }
        public override string ReadLine()
        {
            return wrappedGuiConsoleControl.ReadLine();
        }
        public override string ReadToEnd()
        {
            throw new NotImplementedException();
        }
        public new static TextReader Synchronized(TextReader reader)
        {
            throw new NotImplementedException();
        }

        // ------------------------------------------------------------------------

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return wrappedGuiConsoleControl.ReadKey(intercept);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return wrappedGuiConsoleControl.ReadKey(false);
        }

        public ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            return wrappedGuiConsoleControl.ReadKeyWithControlCAsInput();
        }

        public bool KeyAvailable
        {
            get { return wrappedGuiConsoleControl.KeyAvailable; }
        }

        public event ConsoleCancelEventHandler CancelKeyPress
        {
            add { ; /*throw new NotImplementedException();*/ } // TODO: send on closing / forward from wrapped? also remove on dispose then...
            remove { ; /*throw new NotImplementedException();*/ }
        }

        // ------------------------------------------------------------------------

        private readonly GuiConsoleControl wrappedGuiConsoleControl;
    }
}
