/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_WORKAROUND
//#define MONO_WINDOWS_WORKAROUND

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace de.unika.ipd.grGen.grShell
{
    /// <summary>
    /// Workaround fun due to the following bugs:
    ///  - http://bugzilla.ximian.com/show_bug.cgi?id=80176 : Console.In.Read doesn't allow line-oriented input
    ///  - http://bugzilla.ximian.com/show_bug.cgi?id=79711 : Console.ReadKey () appears to not clear key
    ///  - http://bugzilla.ximian.com/show_bug.cgi?id=80330 : Console.ForegroundColor initially does not reflect console color
    /// </summary>

    [Flags]
    public enum HighlightingMode : int
    {
        None = 0,
        Focus = 1,
        FocusSucces = 2,
        LastSuccess = 4,
        LastFail = 8,
        Breakpoint = 16,
        Choicepoint = 32,
        SequenceStart = 64,

        GrsFile = 128,
        GrsiFile = 256,
        GrgFile = 512,
        GriFile = 1024,
        GmFile = 2048,
        Directory = 4096
    }

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        // Legacy flag, should not be used.
        // ES_USER_PRESENT   = 0x00000004,
        ES_CONTINUOUS = 0x80000000,
    }

    public interface IWorkaround
    {
        /// <summary>
        /// A TextReader for stdin.
        /// </summary>
        TextReader In { get; }

        /// <summary>
        /// Reads a key from stdin and optionally displays it in the console.
        /// </summary>
        /// <param name="intercept">If true, the key is NOT displayed in the console.</param>
        /// <returns>A ConsoleKeyInfo object describing the pressed key.</returns>
        ConsoleKeyInfo ReadKey(bool intercept);

        /// <summary>
        /// Prints the given text in a highlighted form.
        /// </summary>
        void PrintHighlighted(String text, HighlightingMode mode);

        /// <summary>
        /// Prevents the computer from going into sleep mode or allows it again.
        /// To be set when you start a long running computation without user interaction or network I/O,
        /// to be reset afterwards (so the computer can fall asleep again in case there's nothing going on).
        /// Not calling this function the computer would fall asleep after a while even at 100% CPU usage and disk usage,
        /// as might happen if you are executing some graph rewrite sequences for an excessive simulation.
        /// TODO: LINUX version. Currently Windows only.
        /// </summary>
        /// <param name="prevent">prevent if true, allow if false</param>
        void PreventComputerGoingIntoSleepMode(bool prevent);
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
                    if(num > count) num = count;
                    Array.ConstrainedCopy(lineBuffer, curLineBufferPos, buffer, index, num);
                    curLineBufferPos += num;
                    if(curLineBufferPos == lineBuffer.Length) lineBuffer = null;
                    index += num;
                    count -= num;
                    if(count == 0) return num;
                }
                String line = Console.ReadLine() + Environment.NewLine;
                //                lineBuffer = line.ToCharArray();
                int realChars = 0;
                for(int i = 0; i < line.Length; i++)
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
                        realChars++;
                    }
                }

                lineBuffer = new char[realChars];
                for(int i = 0, j = 0; i < line.Length; i++)
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
                if(num > count) num = count;
                Array.ConstrainedCopy(lineBuffer, 0, buffer, index, num);
                curLineBufferPos += num;
                if(curLineBufferPos == lineBuffer.Length) lineBuffer = null;
                return num;
            }
        }

        private TextReader pIn = new MonoWorkaroundConsoleTextReader();
        public TextReader In { get { return pIn; } }

        public abstract ConsoleKeyInfo ReadKey(bool intercept);
        public abstract void PrintHighlighted(String text, HighlightingMode mode);
        public abstract void PreventComputerGoingIntoSleepMode(bool prevent);
    }

    public class MonoLinuxWorkaroundConsoleIO : MonoWorkaroundConsoleIO
    {
        /// <summary>
        /// Prints the given text in the chosen highlighting mode on the console
        /// </summary>
        public override void PrintHighlighted(String text, HighlightingMode mode)
        {
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus)
                Console.Write("\x1b[1m\x1b[33m\x1b[40m" + text + "\x1b[0m"); // bold, yellow fg, black bg
            else if((mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces)
                Console.Write("\x1b[1m\x1b[32m\x1b[40m" + text + "\x1b[0m"); // bold, green fg, black bg
            else if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess)
                Console.Write("\x1b[42m" + text + "\x1b[0m"); // green bg
            else if((mode & HighlightingMode.LastFail) == HighlightingMode.LastFail)
                Console.Write("\x1b[41m" + text + "\x1b[0m"); // red bg
            else if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint)
                Console.Write("\x1b[31m" + text + "\x1b[0m"); // red fg
            else if((mode & HighlightingMode.Choicepoint) == HighlightingMode.Choicepoint)
                Console.Write("\x1b[35m" + text + "\x1b[0m"); // magenta fg
            else if((mode & HighlightingMode.SequenceStart) == HighlightingMode.SequenceStart)
                Console.Write("\x1b[34m" + text + "\x1b[0m"); // blue fg
            else if((mode & HighlightingMode.GrsFile) == HighlightingMode.GrsFile)
                Console.Write("\x1b[31m" + text + "\x1b[0m"); // red fg
            else if((mode & HighlightingMode.GrsiFile) == HighlightingMode.GrsiFile)
                Console.Write("\x1b[35m" + text + "\x1b[0m"); // magenta fg
            else if((mode & HighlightingMode.GrgFile) == HighlightingMode.GrgFile)
                Console.Write("\x1b[32m" + text + "\x1b[0m"); // green fg
            else if((mode & HighlightingMode.GriFile) == HighlightingMode.GriFile)
                Console.Write("\x1b[36m" + text + "\x1b[0m"); // cyan fg
            else if((mode & HighlightingMode.GmFile) == HighlightingMode.GmFile)
                Console.Write("\x1b[34m" + text + "\x1b[0m"); // blue fg
            else if((mode & HighlightingMode.Directory) == HighlightingMode.Directory)
                Console.Write("\x1b[43m" + text + "\x1b[0m"); // yellow bg
            else
                Console.Write(text); // normal
        }

        public override ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public override void PreventComputerGoingIntoSleepMode(bool prevent)
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
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus || (mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces) Console.BackgroundColor = ConsoleColor.Black;
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

        public override void PreventComputerGoingIntoSleepMode(bool prevent)
        {
            if(prevent)
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

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
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus || (mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces) Console.BackgroundColor = ConsoleColor.Black;
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

        public TextReader In { get { return Console.In; } }
        public ConsoleKeyInfo ReadKey(bool intercept) { return Console.ReadKey(intercept); }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public void PreventComputerGoingIntoSleepMode(bool prevent)
        {
            if(prevent)
                SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            else
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
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
                    Type t = Type.GetType("System.Int32");
                    if(t.GetType().ToString() == "System.MonoType")
                    {
                        if(Environment.OSVersion.Platform == PlatformID.Unix)
                            workaround = new MonoLinuxWorkaroundConsoleIO();
                        else
                            workaround = new MonoWindowsWorkaroundConsoleIO();
                    }
                    else
                        workaround = new NoWorkaroundConsoleIO();
                }
                return workaround;
            }
        }
    }
}
