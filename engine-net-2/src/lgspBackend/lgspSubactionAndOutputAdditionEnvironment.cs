/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the ISubactionAndOutputAdditionEnvironment, to be used with LGSPGraphs.
    /// </summary>
    public class LGSPSubactionAndOutputAdditionEnvironment : LGSPActionExecutionEnvironment, ISubactionAndOutputAdditionEnvironment
    {
        private IRecorder recorder;
        private TextWriter emitWriter = Console.Out;


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


        public void SwitchToSubgraph(IGraph newGraph)
        {
            SwitchingToSubgraph(newGraph);
            usedGraphs.Push((LGSPGraph)newGraph);
            namedGraphOnTop = newGraph as LGSPNamedGraph;
        }

        public IGraph ReturnFromSubgraph()
        {
            IGraph oldGraph = usedGraphs.Pop();
            namedGraphOnTop = usedGraphs.Peek() as LGSPNamedGraph;
            ReturnedFromSubgraph(oldGraph);
            return oldGraph;
        }

        public bool IsInSubgraph { get { return usedGraphs.Count > 1; } }


        public IRecorder Recorder
        {
            get { return recorder; }
            set { recorder = value; }
        }

        public TextWriter EmitWriter
        {
            get { return emitWriter; }
            set { emitWriter = value; }
        }
                
        #region Events

        public event SwitchToSubgraphHandler OnSwitchingToSubgraph;
        public event ReturnFromSubgraphHandler OnReturnedFromSubgraph;

        private void SwitchingToSubgraph(IGraph graph)
        {
            SwitchToSubgraphHandler handler = OnSwitchingToSubgraph;
            if(handler != null) handler(graph);
        }

        private void ReturnedFromSubgraph(IGraph graph)
        {
            ReturnFromSubgraphHandler handler = OnReturnedFromSubgraph;
            if(handler != null) handler(graph);
        }

        public event DebugEnterHandler OnDebugEnter;
        public event DebugExitHandler OnDebugExit;
        public event DebugEmitHandler OnDebugEmit;
        public event DebugHaltHandler OnDebugHalt;
        public event DebugHighlightHandler OnDebugHighlight;

        public void DebugEntering(string message, params object[] values)
        {
            DebugEnterHandler handler = OnDebugEnter;
            if(handler != null) handler(message, values);
        }

        public void DebugExiting(string message, params object[] values)
        {
            DebugExitHandler handler = OnDebugExit;
            if(handler != null) handler(message, values);
        }

        public void DebugEmitting(string message, params object[] values)
        {
            DebugEmitHandler handler = OnDebugEmit;
            if(handler != null) handler(message, values);
        }

        public void DebugHalting(string message, params object[] values)
        {
            DebugHaltHandler handler = OnDebugHalt;
            if(handler != null) handler(message, values);
        }

        public void DebugHighlighting(string message, List<object> values, List<string> annotations)
        {
            DebugHighlightHandler handler = OnDebugHighlight;
            if(handler != null) handler(message, values, annotations);
        }

        #endregion Events
    }
}
