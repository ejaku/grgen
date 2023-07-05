/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.IO;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the ISubactionAndOutputAdditionEnvironment, to be used with LGSPGraphs.
    /// </summary>
    public class LGSPSubactionAndOutputAdditionEnvironment : LGSPActionExecutionEnvironment, ISubactionAndOutputAdditionEnvironment
    {
        private IRecorder recorder;
        private TextWriter emitWriter = null;


        public LGSPSubactionAndOutputAdditionEnvironment(LGSPGraph graph, LGSPActions actions)
            : base(graph, actions)
        {
            recorder = new Recorder(graph as LGSPNamedGraph, this);
        }

        public virtual void Initialize(LGSPGraph graph, LGSPActions actions)
        {
            this.Graph = graph;
            this.Actions = actions;
            ((Recorder)recorder).Initialize(graph as LGSPNamedGraph, this);
        }


        public virtual void SwitchToSubgraph(IGraph newGraph)
        {
            SwitchingToSubgraph(newGraph);
            usedGraphs.Push((LGSPGraph)newGraph);
            namedGraphOnTop = newGraph as LGSPNamedGraph;
        }

        public virtual IGraph ReturnFromSubgraph()
        {
            IGraph oldGraph = usedGraphs.Pop();
            namedGraphOnTop = usedGraphs.Peek() as LGSPNamedGraph;
            ReturnedFromSubgraph(oldGraph);
            return oldGraph;
        }

        public bool IsInSubgraph
        {
            get { return usedGraphs.Count > 1; }
        }


        public IRecorder Recorder
        {
            get { return recorder; }
            set { recorder = value; }
        }

        public TextWriter EmitWriter
        {
            get { return emitWriter ?? ConsoleUI.outWriter; }
            set { emitWriter = value; }
        }

        public TextWriter EmitWriterDebug
        {
            get { return ConsoleUI.outWriter; }
        }

        #region Events

        public event SwitchToSubgraphHandler OnSwitchingToSubgraph;
        public event ReturnFromSubgraphHandler OnReturnedFromSubgraph;

        private void SwitchingToSubgraph(IGraph graph)
        {
            if(OnSwitchingToSubgraph != null)
                OnSwitchingToSubgraph(graph);
        }

        private void ReturnedFromSubgraph(IGraph graph)
        {
            if(OnReturnedFromSubgraph != null)
                OnReturnedFromSubgraph(graph);
        }

        public event DebugEnterHandler OnDebugEnter;
        public event DebugExitHandler OnDebugExit;
        public event DebugEmitHandler OnDebugEmit;
        public event DebugHaltHandler OnDebugHalt;
        public event DebugHighlightHandler OnDebugHighlight;

        public void DebugEntering(string message, params object[] values)
        {
            if(OnDebugEnter != null)
                OnDebugEnter(message, values);
        }

        public void DebugExiting(string message, params object[] values)
        {
            if(OnDebugExit != null)
                OnDebugExit(message, values);
        }

        public void DebugEmitting(string message, params object[] values)
        {
            if(OnDebugEmit != null)
                OnDebugEmit(message, values);
        }

        public void DebugHalting(string message, params object[] values)
        {
            if(OnDebugHalt != null)
                OnDebugHalt(message, values);
        }

        public void DebugHighlighting(string message, List<object> values, List<string> annotations)
        {
            if(OnDebugHighlight != null)
                OnDebugHighlight(message, values, annotations);
        }

        #endregion Events
    }
}
