/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class DisplaySequenceContext
    {
        /// <summary>
        /// If not null, gives a mapping of the sequences by id to the breakpoint positions to be displayed.
        /// Used for highlighting breakpoints during printing and rendering.
        /// </summary>
        public Dictionary<int, int> sequenceIdToBreakpointPosMap = null;

        /// <summary>
        /// If not null, gives a mapping of the sequences by id to the choicepoint positions to be displayed.
        /// Used for highlighting choicepoints during printing and rendering.
        /// </summary>
        public Dictionary<int, int> sequenceIdToChoicepointPosMap = null;

        /// <summary> The sequence to be highlighted or null </summary>
        public SequenceBase highlightSeq = null;
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
