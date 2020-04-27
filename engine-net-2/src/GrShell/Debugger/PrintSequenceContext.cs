/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    public class PrintSequenceContext
    {
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
        public Sequence highlightSeq = null;
        /// <summary> The sequence to be highlighted was already successfully matched? </summary>
        public bool success = false;
        /// <summary> The sequence to be highlighted requires a direction choice? </summary>
        public bool choice = false;

        /// <summary> If not null, gives the sequences to choose amongst </summary>
        public List<Sequence> sequences = null;
        /// <summary> If not null, gives the matches of the sequences to choose amongst </summary>
        public List<IMatches> matches =  null;
    }
}
