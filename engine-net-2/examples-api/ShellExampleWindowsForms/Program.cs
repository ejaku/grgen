using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using de.unika.ipd.grGen.grShell;
using de.unika.ipd.grGen.libGr;

namespace ShellExampleWindowsForms
{
    /// <summary>
    /// A TextWriter implemented by a GuiConsoleControl / a GuiConsoleControl seen as a TextWriter, enriched by the IConsoleOutput interface.
    /// Only a subset of the methods is implemented, for one the Write(char) method needed for a minimal implementation,
    /// plus the methods directly supported by the wrapped GuiConsoleControl.
    /// </summary>
    public class GuiConsoleControlAsTextWriter : TextWriter, IConsoleOutput
    {
        public GuiConsoleControlAsTextWriter(de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl wrappedGuiConsoleControl)
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

        public override void WriteLine(string value)
        {
            wrappedGuiConsoleControl.WriteLine(value);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            wrappedGuiConsoleControl.WriteLine(format, arg);
        }

        public override void WriteLine()
        {
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

        private readonly de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl wrappedGuiConsoleControl;
    }

    /// <summary>
    /// A TextReader implemented by a GuiConsoleControl / a GuiConsoleControl seen as a TextReader, enriched by the IConsoleInput interface.
    /// Only a subset of the methods is implemented, for one the methods needed by the CSharpCC lexer,
    /// plus the methods directly supported by the wrapped GuiConsoleControl.
    /// </summary>
    public class GuiConsoleControlAsTextReader : TextReader, IConsoleInput
    {
        public GuiConsoleControlAsTextReader(de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl wrappedGuiConsoleControl)
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

        private readonly de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl wrappedGuiConsoleControl;
    }

    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ShellForm shell = new ShellForm();
            GuiConsoleControlAsTextReader inReader = new GuiConsoleControlAsTextReader(shell.console);
            GuiConsoleControlAsTextWriter outWriter = new GuiConsoleControlAsTextWriter(shell.console);
            ConsoleUI.inReader = inReader;
            ConsoleUI.outWriter = outWriter;
            ConsoleUI.errorOutWriter = outWriter;
            ConsoleUI.consoleIn = inReader;
            ConsoleUI.consoleOut = outWriter;

            int errorCode = GrShellMainHelper.ConstructShell(args, out shell.shellConfig, out shell.shellComponents);
            if(errorCode != 0)
                throw new Exception("Error during construction of shell"); // TODO: show error dialog?

            shell.reader = inReader;
            shell.writer = outWriter;

            Application.Run(shell);
        }
    }
}
