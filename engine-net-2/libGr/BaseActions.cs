/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A container of rules also managing some parts of rule application with sequences.
    /// </summary>
    public abstract class BaseActions
    {
        private Random randomGenerator = new Random();

        #region Abstract members

        /// <summary>
        /// An associated name.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// An MD5 hash of the used IGraphModel.
        /// Probably useless...
        /// </summary>
        public abstract String ModelMD5Hash { get; }

        /// <summary>
        /// The associated graph.
        /// </summary>
        public abstract IGraph Graph { get; set; }

        /// <summary>
        /// Enumerates all actions managed by this BaseActions instance.
        /// </summary>
        public abstract IEnumerable<IAction> Actions { get; }

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        protected abstract IAction GetIAction(String name);

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        public IAction GetAction(String name)
        {
            return GetIAction(name);
        }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        public abstract void Custom(params object[] args);

        #endregion Abstract members

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(Sequence sequence)
        {
			if(Graph.PerformanceInfo != null) Graph.PerformanceInfo.Start();

            bool res = sequence.Apply(this);

			if(Graph.PerformanceInfo != null) Graph.PerformanceInfo.Stop();
            return res;
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="seqStr">The graph rewrite sequence in form of a string</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(String seqStr)
        {
            return ApplyGraphRewriteSequence(SequenceParser.ParseSequence(seqStr, this));
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(Sequence seq)
        {
            IGraph curGraph = Graph;
            Graph = Graph.Clone("clonedGraph");
            bool res = seq.Apply(this);
            Graph = curGraph;
            return res;
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seqStr">The sequence to be executed in form of a string</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(String seqStr)
        {
            return ValidateWithSequence(SequenceParser.ParseSequence(seqStr, this));
        }
    }
}
