/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using System.Text;

namespace de.unika.ipd.grGen.grShell
{
    public class PrintSequenceContext
    {
        /// <summary>
        /// The workaround for printing highlighted
        /// </summary>
        public IWorkaround workaround;

        /// <summary>
        /// A counter increased for every potential breakpoint position and printed next to a potential breakpoint.
        /// If bpPosCounter is smaller than zero, no such counter is used or printed.
        /// If bpPosCounter is greater than or equal zero, the following highlighting values are irrelvant.
        /// </summary>
        public int bpPosCounter = -1;

        /// <summary>
        /// A counter increased for every potential choice position and printed next to a potential choicepoint.
        /// If cpPosCounter is smaller than zero, no such counter is used or printed.
        /// If cpPosCounter is greater than or equal zero, the following highlighting values are irrelvant.
        /// </summary>
        public int cpPosCounter = -1;

        /// <summary> The sequence to be highlighted or null </summary>
        public Sequence highlightSeq;
        /// <summary> The sequence to be highlighted was already successfully matched? </summary>
        public bool success;
        /// <summary> The sequence to be highlighted requires a direction choice? </summary>
        public bool choice;

        /// <summary> If not null, gives the sequences to choose amongst </summary>
        public List<Sequence> sequences;
        /// <summary> If not null, gives the matches of the sequences to choose amongst </summary>
        public List<IMatches> matches;

        public PrintSequenceContext(IWorkaround workaround)
        {
            Init(workaround);
        }

        public void Init(IWorkaround workaround)
        {
            this.workaround = workaround;

            bpPosCounter = -1;

            cpPosCounter = -1;

            highlightSeq = null;
            success = false;
            choice = false;

            sequences = null;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    public enum SubruleComputationType
    {
        Entry,
        Exit,
        Emit,
        Halt,
        Highlight
    }

    public class SubruleComputation
    {
        public SubruleComputation(IGraph graph, SubruleComputationType type, string message, params object[] values)
        {
            this.type = type;
            this.message = message;
            this.parameters = new List<string>();
            for(int i = 0; i < values.Length; ++i)
            {
                parameters.Add(EmitHelper.ToStringAutomatic(values[i], graph));
            }
            this.fakeEntry = false;
        }

        public SubruleComputation(string message)
        {
            this.type = SubruleComputationType.Entry;
            this.message = message;
            this.parameters = new List<string>();
            this.fakeEntry = true;
        }

        public string ToString(bool full)
        {
            StringBuilder sb = new StringBuilder();
            if(fakeEntry)
                sb.Append("(");
            switch(type)
            {
                case SubruleComputationType.Entry: sb.Append("Entry "); break;
                case SubruleComputationType.Exit: sb.Append("Exit "); break;
                case SubruleComputationType.Emit: sb.Append("Emit "); break;
                case SubruleComputationType.Halt: sb.Append("Halt "); break;
                case SubruleComputationType.Highlight: sb.Append("Highlight "); break;
            }
            sb.Append(message);
            for(int i = 0; i < parameters.Count; ++i)
            {
                sb.Append(" ");
                if(full)
                    sb.Append(parameters[i]);
                else
                {
                    if(parameters[i].Length < 120
                        || type == SubruleComputationType.Entry && parameters.Count == 1 && message.Contains("exec")) // print full embedded exec that is entered
                        sb.Append(parameters[i]);
                    else
                    {
                        sb.Append(parameters[i].Substring(0, 117));
                        sb.Append("...");
                    }
                }
            }
            if(fakeEntry)
                sb.Append(")");
            return sb.ToString();
        }

        // specifies the type of this subrule computation/message
        public SubruleComputationType type;

        // the message/name of the computation entered
        public string message;

        // the additional parameters, transfered into string encoding
        public List<string> parameters;

        // true if this is an entry of an action, added by the debugger as a help, 
        // so the full state is contained on the traces stack
        bool fakeEntry;
    }
}
