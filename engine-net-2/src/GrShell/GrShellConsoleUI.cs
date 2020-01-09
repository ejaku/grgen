/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.IO;

namespace de.unika.ipd.grGen.grShell
{
    public interface IGrShellUI
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
    public class GrShellConsoleUI : IGrShellUI
    {
        protected TextReader in_;
        protected TextWriter out_;

        public GrShellConsoleUI(TextReader in_, TextWriter out_)
        {
            this.in_ = in_;
            this.out_ = out_;
        }

        public string ReadOrEofErr()
        {
            string result = this.in_.ReadLine();
            if (result == null) {
                throw new EOFException();
            }
            return result;
        }

        public void ShowMsgAskForEnter(string msg)
        {
            this.out_.Write(msg + " [enter] ");
            ReadOrEofErr();
        }

        public bool ShowMsgAskForYesNo(string msg)
        {
            while (true)
            {
                this.out_.Write(msg + " [y(es)/n(o)] ");
                string result = ReadOrEofErr();
                if (result.Equals("y", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("yes", StringComparison.InvariantCultureIgnoreCase)) {
                    return true;
                } else if (result.Equals("n", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("no", StringComparison.InvariantCultureIgnoreCase)) {
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
