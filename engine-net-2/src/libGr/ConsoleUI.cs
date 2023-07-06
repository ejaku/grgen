/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
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

    public interface IConsoleOutput
    {
        /// <summary>
        /// Prints the given text in a highlighted form.
        /// </summary>
        void PrintHighlighted(String text, HighlightingMode mode);
    }

    public interface IConsoleInput
    {
        /// <summary>
        /// Reads a key from stdin and optionally displays it in the console.
        /// </summary>
        /// <param name="intercept">If true, the key is NOT displayed in the console.</param>
        /// <returns>A ConsoleKeyInfo object describing the pressed key.</returns>
        ConsoleKeyInfo ReadKey(bool intercept);

        /// <summary>
        /// Reads a key from stdin and displays it in the console.
        /// </summary>
        ConsoleKeyInfo ReadKey();

        /// <summary>
        /// Reads a key from stdin. Does not display it. Also allows Control-C as input.
        /// </summary>
        /// <returns>A ConsoleKeyInfo object describing the pressed key.</returns>
        ConsoleKeyInfo ReadKeyWithControlCAsInput();

        /// <summary>
        /// Gets a value indicating whether a key press is available in the input stream.
        /// </summary>
        bool KeyAvailable { get; }

        event ConsoleCancelEventHandler CancelKeyPress;
    }

    /// <summary>
    /// Class that allows for console input/output, normally stdin, stdout, stderr, extended by dedicated stuff,
    /// but also in a GUI environment offering mechanisms for console input/output.
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// The output writer normally mapping to stdout
        /// </summary>
        public static TextWriter outWriter
        {
            get
            {
                if(_outWriter == null)
                    _outWriter = System.Console.Out;
                return _outWriter;
            }
            set { _outWriter = value; }
        }
        private static TextWriter _outWriter;

        /// <summary>
        /// The error output write normally mapping to stderr
        /// </summary>
        public static TextWriter errorOutWriter
        {
            get
            {
                if(_errorOutWriter == null)
                    _errorOutWriter = System.Console.Error;
                return _errorOutWriter;
            }
            set { _errorOutWriter = value; }
        }
        private static TextWriter _errorOutWriter;

        /// <summary>
        /// An interface that allows for highlighted output, typically null unless a debugger session is started
        /// </summary>
        public static IConsoleOutput consoleOut
        {
            get
            {
                if(_consoleOut == null)
                    _consoleOut = WorkaroundManager.Workaround;
                return _consoleOut;
            }
            set { _consoleOut = value; }
        }
        private static IConsoleOutput _consoleOut;

        /// <summary>
        /// The input reader normally mapping to stdin
        /// </summary>
        public static TextReader inReader
        {
            get
            {
                if(_inReader == null)
                    _inReader = WorkaroundManager.Workaround.In;
                return _inReader;
            }
            set { _inReader = value; }
        }
        private static TextReader _inReader;


        /// <summary>
        /// An interface that allows to read keys from the console, typically null unless a debugger session is started
        /// </summary>
        public static IConsoleInput consoleIn
        {
            get
            {
                if(_consoleIn == null)
                    _consoleIn = WorkaroundManager.Workaround;
                return _consoleIn;
            }
            set { _consoleIn = value; }
        }
        private static IConsoleInput _consoleIn;
    }
}
