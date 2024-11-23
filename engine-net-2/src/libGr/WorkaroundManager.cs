/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

#define MONO_WORKAROUND
//#define MONO_WINDOWS_WORKAROUND

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Workaround fun due to the following bugs:
    ///  - http://bugzilla.ximian.com/show_bug.cgi?id=80176 : Console.In.Read doesn't allow line-oriented input
    ///  - http://bugzilla.ximian.com/show_bug.cgi?id=79711 : Console.ReadKey () appears to not clear key
    ///  - http://bugzilla.ximian.com/show_bug.cgi?id=80330 : Console.ForegroundColor initially does not reflect console color
    /// </summary>

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        // Legacy flag, should not be used.
        // ES_USER_PRESENT   = 0x00000004,
        ES_CONTINUOUS = 0x80000000,
    }

    public interface IWorkaround : IConsoleOutput, IConsoleInput
    {
        /// <summary>
        /// A TextReader for stdin.
        /// </summary>
        TextReader In { get; }

        /// <summary>
        /// Prevents the computer from going into sleep mode or allows it again.
        /// To be set when you start a long running computation without user interaction or network I/O,
        /// to be reset afterwards (so the computer can fall asleep again in case there's nothing going on).
        /// Not calling this function the computer would fall asleep after a while even at 100% CPU usage and disk usage,
        /// as might happen if you are executing some graph rewrite sequences for an excessive simulation.
        /// TODO: LINUX version. Currently Windows only. Silent nop.
        /// </summary>
        /// <param name="prevent">prevent if true, allow if false</param>
        void PreventComputerFromGoingIntoSleepMode(bool prevent);

        /// <summary>
        /// Allocates the console.
        /// TODO: LINUX version. Currently Windows only. Silent nop.
        /// </summary>
        bool AllocTheConsole();

        /// <summary>
        /// Attaches the console.
        /// TODO: LINUX version. Currently Windows only. Silent nop.
        /// </summary>
        bool AttachTheConsole(int pid);

        /// <summary>
        /// Frees the console.
        /// TODO: LINUX version. Currently Windows only. Silent nop.
        /// </summary>
        bool FreeTheConsole();

        /// <summary>
        /// Prevents redrawing of the given window.
        /// TODO: LINUX version. Currently Windows only. Silent nop.
        /// </summary>
        void PreventRedraw(IntPtr hWnd);

        /// <summary>
        /// Allows redrawing of the given window.
        /// TODO: LINUX version. Currently Windows only. Silent nop.
        /// </summary>
        void AllowRedraw(IntPtr hWnd);
    }

    public abstract class MonoWorkaroundConsoleIO : IWorkaround
    {
        private class MonoWorkaroundConsoleTextReader : TextReader
        {
            char[] lineBuffer = null;
            int curLineBufferPos = -1;

            public override int Read(char[] buffer, int index, int count)
            {
                int num;
                if(lineBuffer != null)
                {
                    num = lineBuffer.Length - curLineBufferPos;
                    if(num > count)
                        num = count;
                    Array.ConstrainedCopy(lineBuffer, curLineBufferPos, buffer, index, num);
                    curLineBufferPos += num;
                    if(curLineBufferPos == lineBuffer.Length)
                        lineBuffer = null;
                    index += num;
                    count -= num;
                    if(count == 0)
                        return num;
                }
                String line = Console.ReadLine() + Environment.NewLine;
                //lineBuffer = line.ToCharArray();
                int realChars = 0;
                for(int i = 0; i < line.Length; ++i)
                {
                    if(line[i] != 0)                                // not special key code? (e.g. for arrow keys)
                    {
                        if(line[i] == 27 && i + 3 < line.Length)    // del escape sequence?
                        {
                            if(line[i + 1] == 91 && line[i + 2] == 51 && line[i + 3] == 126)
                            {
                                i += 3;
                                continue;   // -> i++ => i += 4
                            }
                        }
                        ++realChars;
                    }
                }

                lineBuffer = new char[realChars];
                for(int i = 0, j = 0; i < line.Length; ++i)
                {
                    if(line[i] != 0)                                // not special key code? (e.g. for arrow keys)
                    {
                        if(line[i] == 27 && i + 3 < line.Length)    // del escape sequence?
                        {
                            if(line[i + 1] == 91 && line[i + 2] == 51 && line[i + 3] == 126)
                            {
                                i += 3;
                                continue;   // -> i++ => i += 4
                            }
                        }
                        lineBuffer[j++] = line[i];
                    }
                }

                curLineBufferPos = 0;
                num = lineBuffer.Length;
                if(num > count)
                    num = count;
                Array.ConstrainedCopy(lineBuffer, 0, buffer, index, num);
                curLineBufferPos += num;
                if(curLineBufferPos == lineBuffer.Length)
                    lineBuffer = null;
                return num;
            }
        }

        private TextReader pIn = new MonoWorkaroundConsoleTextReader();
        public TextReader In
        {
            get { return pIn; }
        }

        public abstract ConsoleKeyInfo ReadKey(bool intercept);
        public virtual ConsoleKeyInfo ReadKey()
        {
            return ReadKey(false);
        }
        public virtual ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            Console.TreatControlCAsInput = true;
            ConsoleKeyInfo consoleKeyInfo = ReadKey(true);
            Console.TreatControlCAsInput = false;
            return consoleKeyInfo;
        }
        public bool KeyAvailable { get { return Console.KeyAvailable; } }
        public event ConsoleCancelEventHandler CancelKeyPress { add { Console.CancelKeyPress += value; } remove { Console.CancelKeyPress -= value;  } }

        public abstract void PrintHighlighted(String text, HighlightingMode mode);
        public void Clear()
        {
            Console.Clear();
        }
        public abstract void PreventComputerFromGoingIntoSleepMode(bool prevent);

        public abstract bool AllocTheConsole();
        public abstract bool AttachTheConsole(int pid);
        public abstract bool FreeTheConsole();

        public abstract void PreventRedraw(IntPtr hWnd);
        public abstract void AllowRedraw(IntPtr hWnd);
    }

    public class MonoLinuxWorkaroundConsoleIO : MonoWorkaroundConsoleIO
    {
        const string RESET = "\x1b[0m";
        const string BOLD = "\x1b[1m";

        const string BLACK = "\x1b[30m";
        const string RED = "\x1b[31m";
        const string GREEN = "\x1b[32m";
        const string YELLOW = "\x1b[33m";
        const string BLUE = "\x1b[34m";
        const string MAGENTA = "\x1b[35m";
        const string CYAN = "\x1b[36m";
        const string WHITE = "\x1b[37m";

        const string BLACK_BACKGROUND = "\x1b[40m";
        const string RED_BACKGROUND = "\x1b[41m";
        const string GREEN_BACKGROUND = "\x1b[42m";
        const string YELLOW_BACKGROUND = "\x1b[43m";
        const string BLUE_BACKGROUND = "\x1b[44m";
        const string MAGENTA_BACKGROUND = "\x1b[45m";
        const string CYAN_BACKGROUND = "\x1b[46m";
        const string WHITE_BACKGROUND = "\x1b[47m";

        /// <summary>
        /// Prints the given text in the chosen highlighting mode on the console
        /// </summary>
        public override void PrintHighlighted(String text, HighlightingMode mode)
        {
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus)
                Console.Write(BOLD + YELLOW + text + RESET);
            else if((mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces)
                Console.Write(BOLD + GREEN + text + RESET);
            else if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess)
                Console.Write(GREEN_BACKGROUND + BLACK + text + RESET);
            else if((mode & HighlightingMode.LastFail) == HighlightingMode.LastFail)
                Console.Write(RED_BACKGROUND + BLACK + text + RESET);
            else if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint)
                Console.Write(RED + text + RESET);
            else if((mode & HighlightingMode.Choicepoint) == HighlightingMode.Choicepoint)
                Console.Write(MAGENTA + text + RESET);
            else if((mode & HighlightingMode.SequenceStart) == HighlightingMode.SequenceStart)
                Console.Write(BLUE + text + RESET);
            else if((mode & HighlightingMode.GrsFile) == HighlightingMode.GrsFile)
                Console.Write(RED + text + RESET);
            else if((mode & HighlightingMode.GrsiFile) == HighlightingMode.GrsiFile)
                Console.Write(MAGENTA + text + RESET);
            else if((mode & HighlightingMode.GrgFile) == HighlightingMode.GrgFile)
                Console.Write(GREEN + text + RESET);
            else if((mode & HighlightingMode.GriFile) == HighlightingMode.GriFile)
                Console.Write(CYAN + text + RESET);
            else if((mode & HighlightingMode.GmFile) == HighlightingMode.GmFile)
                Console.Write(BLUE + text + RESET);
            else if((mode & HighlightingMode.Directory) == HighlightingMode.Directory)
                Console.Write(YELLOW_BACKGROUND + text + RESET);
            else
                Console.Write(text); // normal
        }

        public override ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public override void PreventComputerFromGoingIntoSleepMode(bool prevent)
        {
            // TODO - NIY
        }

        public override bool AllocTheConsole()
        {
            // TODO - NIY
            return false;
        }

        public override bool AttachTheConsole(int pid)
        {
            // TODO - NIY
            return false;
        }

        public override bool FreeTheConsole()
        {
            // TODO - NIY
            return false;
        }

        public override void PreventRedraw(IntPtr hWnd)
        {
            // TODO - NIY
        }

        public override void AllowRedraw(IntPtr hWnd)
        {
            // TODO - NIY
        }
    }

    public class MonoWindowsWorkaroundConsoleIO : MonoWorkaroundConsoleIO
    {
        /// <summary>
        /// Prints the given text in the chosen highlighting mode on the console
        /// </summary>
        public override void PrintHighlighted(String text, HighlightingMode mode)
        {
            if(mode == HighlightingMode.None)
            {
                Console.Write(text);
                return;
            }

            ConsoleColor oldForegroundColor = Console.ForegroundColor;
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus) Console.ForegroundColor = ConsoleColor.Yellow;
            if((mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces) Console.ForegroundColor = ConsoleColor.Green;
            if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess) Console.BackgroundColor = ConsoleColor.DarkGreen;
            if((mode & HighlightingMode.LastFail) == HighlightingMode.LastFail) Console.BackgroundColor = ConsoleColor.DarkRed;
            if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess || (mode & HighlightingMode.LastFail) == HighlightingMode.LastFail) Console.ForegroundColor = ConsoleColor.Black;
            if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint) Console.ForegroundColor = ConsoleColor.Red;
            if((mode & HighlightingMode.Choicepoint) == HighlightingMode.Choicepoint) Console.ForegroundColor = ConsoleColor.Magenta;
            if((mode & HighlightingMode.SequenceStart) == HighlightingMode.SequenceStart) Console.ForegroundColor = ConsoleColor.Blue;
            if((mode & HighlightingMode.GrsFile) == HighlightingMode.GrsFile) Console.ForegroundColor = ConsoleColor.Red;
            if((mode & HighlightingMode.GrsiFile) == HighlightingMode.GrsiFile) Console.ForegroundColor = ConsoleColor.Magenta;
            if((mode & HighlightingMode.GrgFile) == HighlightingMode.GrgFile) Console.ForegroundColor = ConsoleColor.Green;
            if((mode & HighlightingMode.GriFile) == HighlightingMode.GriFile) Console.ForegroundColor = ConsoleColor.Cyan;
            if((mode & HighlightingMode.GmFile) == HighlightingMode.GmFile) Console.ForegroundColor = ConsoleColor.Blue;
            if((mode & HighlightingMode.Directory) == HighlightingMode.Directory) Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(text);
            Console.ForegroundColor = oldForegroundColor;
            Console.BackgroundColor = oldBackgroundColor;
        }

        /// <summary>
        /// Reads a key from stdin and optionally displays it in the console.
        /// Additionally it ignores several incorrect keys returned by Mono on Windows.
        /// </summary>
        /// <param name="intercept">If true, the key is NOT displayed in the console.</param>
        /// <returns>A ConsoleKeyInfo object describing the pressed key.</returns>
        public override ConsoleKeyInfo ReadKey(bool intercept)
        {
            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept);
                switch(key.Key)
                {
                case (ConsoleKey) 16:           // shift
                case (ConsoleKey) 17:           // control
                case (ConsoleKey) 18:           // alt
                case (ConsoleKey) 20:           // caps lock
                case (ConsoleKey) 30676:        // context menu
                    Console.ReadKey(true);      // catch second wrong key event
                    break;

                case (ConsoleKey) 22:           // get(?) focus
                case (ConsoleKey) 23:           // loose(?) focus
                    break;
                default:
                    Console.ReadKey(true);      // catch wrong key event
                    return key;
                }
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public override void PreventComputerFromGoingIntoSleepMode(bool prevent)
        {
            if(prevent)
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        // import Win32 API console functionality
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        public override bool AllocTheConsole()
        {
            return AllocConsole();
        }

        public override bool AttachTheConsole(int pid)
        {
            return AttachConsole(pid);
        }

        public override bool FreeTheConsole()
        {
            return FreeConsole();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int WM_SETREDRAW = 0x0b;

        public override void PreventRedraw(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        public override void AllowRedraw(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
        }
    }

    // vanilla Windows version
    public class NoWorkaroundConsoleIO : IWorkaround
    {
        /// <summary>
        /// Prints the given text in the chosen highlighting mode on the console
        /// </summary>
        public void PrintHighlighted(String text, HighlightingMode mode)
        {
            if(mode == HighlightingMode.None)
            {
                Console.Write(text);
                return;
            }

            ConsoleColor oldForegroundColor = Console.ForegroundColor;
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus) Console.ForegroundColor = ConsoleColor.Yellow;
            if((mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces) Console.ForegroundColor = ConsoleColor.Green;
            if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess) Console.BackgroundColor = ConsoleColor.DarkGreen;
            if((mode & HighlightingMode.LastFail) == HighlightingMode.LastFail) Console.BackgroundColor = ConsoleColor.DarkRed;
            if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess || (mode & HighlightingMode.LastFail) == HighlightingMode.LastFail) Console.ForegroundColor = ConsoleColor.Black;
            if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint) Console.ForegroundColor = ConsoleColor.Red;
            if((mode & HighlightingMode.Choicepoint) == HighlightingMode.Choicepoint) Console.ForegroundColor = ConsoleColor.Magenta;
            if((mode & HighlightingMode.SequenceStart) == HighlightingMode.SequenceStart) Console.ForegroundColor = ConsoleColor.Blue;
            if((mode & HighlightingMode.GrsFile) == HighlightingMode.GrsFile) Console.ForegroundColor = ConsoleColor.Red;
            if((mode & HighlightingMode.GrsiFile) == HighlightingMode.GrsiFile) Console.ForegroundColor = ConsoleColor.Magenta;
            if((mode & HighlightingMode.GrgFile) == HighlightingMode.GrgFile) Console.ForegroundColor = ConsoleColor.Green;
            if((mode & HighlightingMode.GriFile) == HighlightingMode.GriFile) Console.ForegroundColor = ConsoleColor.Cyan;
            if((mode & HighlightingMode.GmFile) == HighlightingMode.GmFile) Console.ForegroundColor = ConsoleColor.Blue;
            if((mode & HighlightingMode.Directory) == HighlightingMode.Directory) Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(text);
            Console.ForegroundColor = oldForegroundColor;
            Console.BackgroundColor = oldBackgroundColor;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public TextReader In
        {
            get { return Console.In; }
        }

        public virtual ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public virtual ConsoleKeyInfo ReadKey()
        {
            return ReadKey(false);
        }

        public virtual ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            Console.TreatControlCAsInput = true;
            ConsoleKeyInfo consoleKeyInfo = ReadKey(true);
            Console.TreatControlCAsInput = false;
            return consoleKeyInfo;
        }

        public bool KeyAvailable { get { return Console.KeyAvailable; } }
        public event ConsoleCancelEventHandler CancelKeyPress { add { Console.CancelKeyPress += value; } remove { Console.CancelKeyPress -= value; } }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public void PreventComputerFromGoingIntoSleepMode(bool prevent)
        {
            if(prevent)
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        // import Win32 API console functionality
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        public bool AllocTheConsole()
        {
            return AllocConsole();
        }

        public bool AttachTheConsole(int pid)
        {
            return AttachConsole(pid);
        }

        public bool FreeTheConsole()
        {
            return FreeConsole();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int WM_SETREDRAW = 0x0b;

        public void PreventRedraw(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        public void AllowRedraw(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
        }
    }

    public class CygwinBashWorkaroundConsoleIO : NoWorkaroundConsoleIO
    {
        public override ConsoleKeyInfo ReadKey(bool intercept)
        {
            Console.Write("cygwin bash compatibility mode - please enter a key and press return:");
            int key = Console.Read();
            int returnKey = Console.Read();
            return new ConsoleKeyInfo(Convert.ToChar(key), new ConsoleKey(), false, false, false); // todo: build better key info
        }

        public override ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            Console.Write("cygwin bash compatibility mode - please enter a key and press return (Ctrl-C not working):");
            int key = Console.Read();
            int returnKey = Console.Read();
            return new ConsoleKeyInfo(Convert.ToChar(key), new ConsoleKey(), false, false, false); // todo: build better key info
        }
    }

    public class WorkaroundManager
    {
        private static IWorkaround workaround;

        /// <summary>
        /// An appropriate IWorkaround instance for the used CLR and operating system.
        /// </summary>
        public static IWorkaround Workaround
        {
            get
            {
                if(workaround == null)
                {
                    if(IsMono)
                    {
                        if(IsLinux)
                            workaround = new MonoLinuxWorkaroundConsoleIO();
                        else
                            workaround = new MonoWindowsWorkaroundConsoleIO();
                    }
                    else
                    {
                        if(IsCygwinBash())
                            workaround = new CygwinBashWorkaroundConsoleIO();
                        else
                            workaround = new NoWorkaroundConsoleIO();
                    }
                }
                return workaround;
            }
        }

        public static bool IsMono
        {
            get
            {
                return Type.GetType("Mono.Runtime") != null;
            }
        }

        public static bool IsLinux
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Unix;
            }
        }

        private static bool IsCygwinBash()
        {
            try
            {
                Console.TreatControlCAsInput = true;
                Console.TreatControlCAsInput = false;
            }
            catch(IOException)
            {
                return true;
            }

            return false;
        }
    }
}
