/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll 

using System;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Flags for graph elements.
    /// </summary>
    [Flags]
    public enum LGSPElemFlags : uint
    {
        /// <summary>
        /// Maximum iso space number which can be handled by the flags (i.e. max. number of independent isomorphy spaces).
        /// </summary>
        MAX_ISO_SPACE = 7,

        /// <summary>
        /// Number of visitors which can be handled by the flags.
        /// </summary>
        NUM_SUPPORTED_VISITOR_IDS = 15,

        /// <summary>
        /// This element has already been matched within some enclosing pattern
        /// during the current matching process, needed for patternpath checks.
        /// </summary>
        IS_MATCHED_BY_SOME_ENCLOSING_PATTERN = 1,

        /// <summary>
        /// This element has already been matched within an pattern
        /// of this iso space during the current matching process.
        /// This mask must be shifted left by the current iso space/level.
        /// </summary>
        IS_MATCHED_BY_ENCLOSING_PATTERN = IS_MATCHED_BY_SOME_ENCLOSING_PATTERN << 1,

        /// <summary>
        /// This element has already been matched within the local pattern
        /// during the current matching process.
        /// This mask must be shifted left by the current iso space/level.
        /// </summary>
        IS_MATCHED = IS_MATCHED_BY_ENCLOSING_PATTERN << (int)(MAX_ISO_SPACE),

        /// <summary>
        /// Some variable contains this element.
        /// </summary>
        HAS_VARIABLES = IS_MATCHED << (int)(MAX_ISO_SPACE),

        /// <summary>
        /// This element has already been visited by a visitor.
        /// This mask must be shifted left by the according visitor ID.
        /// </summary>
        IS_VISITED = HAS_VARIABLES << 1,

        /// <summary>
        /// This element has already been visited by the single internal visitor.
        /// </summary>
        IS_VISITED_INTERNALLY = IS_VISITED << (int)(NUM_SUPPORTED_VISITOR_IDS)
    }

    /// <summary>
    /// Flags for graph elements, for parallel matching;
    /// stored outside of the graph elements themselves, in an array per thread.
    /// </summary>
    [Flags]
    public enum LGSPElemFlagsParallel : ushort
    {
        /// <summary>
        /// Maximum iso space number which can be handled by the flags (i.e. max. number of independent isomorphy spaces).
        /// </summary>
        MAX_ISO_SPACE = 7,

        /// <summary>
        /// This element has already been matched within some enclosing pattern
        /// during the current matching process, needed for patternpath checks.
        /// </summary>
        IS_MATCHED_BY_SOME_ENCLOSING_PATTERN = 1,

        /// <summary>
        /// This element has already been matched within an pattern
        /// of this iso space during the current matching process.
        /// This mask must be shifted left by the current iso space/level.
        /// </summary>
        IS_MATCHED_BY_ENCLOSING_PATTERN = IS_MATCHED_BY_SOME_ENCLOSING_PATTERN << 1,

        /// <summary>
        /// This element has already been matched within the local pattern
        /// during the current matching process.
        /// This mask must be shifted left by the current iso space/level.
        /// </summary>
        IS_MATCHED = IS_MATCHED_BY_ENCLOSING_PATTERN << (int)(MAX_ISO_SPACE),

        /// <summary>
        /// This element has already been visited by the single internal visitor.
        /// </summary>
        IS_VISITED_INTERNALLY = IS_MATCHED << (int)(MAX_ISO_SPACE)
    }
}
