/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.IO;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public interface IConsoleUI
    {
        void ShowMsgAskForEnter(string msg);
        bool ShowMsgAskForYesNo(string msg);
        string ShowMsgAskForString(string msg);
    }

    public class EOFException : IOException
    {
    }

    /// <summary>
    /// Console interface for simple user prompts (e.g. for debugging)
    /// </summary>
    public class ConsoleUI : IConsoleUI
    {
        protected readonly TextReader in_;
        protected readonly TextWriter out_;

        public ConsoleUI(TextReader in_, TextWriter out_)
        {
            this.in_ = in_;
            this.out_ = out_;
        }

        public string ReadOrEofErr()
        {
            string result = this.in_.ReadLine();
            if(result == null)
                throw new EOFException();
            return result;
        }

        public void ShowMsgAskForEnter(string msg)
        {
            this.out_.Write(msg + " [enter] ");
            ReadOrEofErr();
        }

        public bool ShowMsgAskForYesNo(string msg)
        {
            while(true)
            {
                this.out_.Write(msg + " [y(es)/n(o)] ");
                string result = ReadOrEofErr();
                if(result.Equals("y", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else if(result.Equals("n", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }
        }

        public string ShowMsgAskForString(string msg)
        {
            this.out_.Write(msg);
            return ReadOrEofErr();
        }
    }
}
