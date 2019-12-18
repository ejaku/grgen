/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The common base of sequence, sequence computation, and sequence expression objects,
    /// with some common infrastructure.
    /// </summary>
    public abstract class SequenceBase
    {
        /// <summary>
        /// Checks the sequence /expression for errors utilizing the given checking environment
        /// reports them by exception
        /// </summary>s
        public abstract void Check(SequenceCheckingEnvironment env);

        /// <summary>
        /// Returns the type of the sequence /expression (for sequences always "boolean")
        /// </summary>
        public abstract string Type(SequenceCheckingEnvironment env);
        
        /// <summary>
        /// A common random number generator for all sequence /expression objects.
        /// It uses a time-dependent seed.
        /// </summary>
        public static Random randomGenerator = new Random();

        /// <summary>
        /// The precedence of this operator. Zero is the highest priority, int.MaxValue the lowest.
        /// Used to add needed parentheses for printing sequences /expressions
        /// TODO: WTF? das ist im Parser genau umgekehrt implementiert!
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// A string symbol representing this sequence /expression kind.
        /// </summary>
        public abstract String Symbol { get; }

        /// <summary>
        /// returns the sequence /expresion id - every sequence /expression is assigned a unique id used in xgrs code generation
        /// for copies the old id is just taken over, does not cause problems as code is only generated once per defined sequence
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// stores the sequence /expression unique id
        /// </summary>
        protected int id;

        /// <summary>
        /// the static member used to assign the unique ids to the sequence /expression instances
        /// </summary>
        protected static int idSource = 0;

        /// <summary>
        /// Enumerates all child sequence computation objects
        /// </summary>
        public abstract IEnumerable<SequenceBase> ChildrenBase { get; }

        /// <summary>
        /// sets for the very node the profiling flag (does not recurse)
        /// </summary>
        public virtual void SetNeedForProfiling(bool profiling)
        {
            // NOP, sufficient for most sequences / sequence computations / sequence expressions,
            // only the node/edge/incident/adjacent/reachable/isX-constructs need to call a special version
            // counting up the search steps with each visited element/graph element accessed (but not the implicit operations)
        }

        /// <summary>
        /// sets for the node and all children, i.e. the entire tree the profiling flag
        /// </summary>
        public void SetNeedForProfilingRecursive(bool profiling)
        {
            SetNeedForProfiling(true);
            foreach(SequenceBase child in ChildrenBase)
            {
                child.SetNeedForProfilingRecursive(profiling);
            }
        }
    }
}
