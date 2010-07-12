/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

#define MONO_WORKAROUND
//#define MONO_WINDOWS_WORKAROUND

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
        Breakpoint = 16
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
    }

    public class MonoLinuxWorkaroundConsoleIO : MonoWorkaroundConsoleIO
    {
        /// <summary>
        /// Prints the given text in the chosen highlighting mode on the console
        /// </summary>
        public override void PrintHighlighted(String text, HighlightingMode mode)
        {
            // currently limited to bold+underlined; todo: test how well linux works with colours
            if((mode & HighlightingMode.Focus) == HighlightingMode.Focus)
                Console.Write("\x1b[1m" + text + "\x1b[22m"); // bold
            else if((mode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces)
                Console.Write("\x1b[1m" + text + "\x1b[22m"); // bold
            else if((mode & HighlightingMode.LastSuccess) == HighlightingMode.LastSuccess)
                Console.Write("\x1b[4m" + text + "\x1b[24m"); // underlined
            else if((mode & HighlightingMode.LastFail) == HighlightingMode.LastFail)
                Console.Write(text); // normal
            else if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint)
                Console.Write("\x1b[1m" + text + "\x1b[22m"); // bold
            else
                Console.Write(text); // normal
        }

        public override ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
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
            if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint) Console.ForegroundColor = ConsoleColor.Red;
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
            if((mode & HighlightingMode.Breakpoint) == HighlightingMode.Breakpoint) Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = oldForegroundColor;
            Console.BackgroundColor = oldBackgroundColor;
        }

        public TextReader In { get { return Console.In; } }
        public ConsoleKeyInfo ReadKey(bool intercept) { return Console.ReadKey(intercept); }
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
