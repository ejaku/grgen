/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Class used to render debugger output in graph form to the user, see IDisplayer for more comments.
    /// Sits in between the debugger and the debugger environment.
    /// </summary>
    public class Renderer : IDisplayer
    {
        private IDebuggerEnvironment env;
        private SequenceRenderer sequenceRenderer;

        private int nodeIdSource;

        public Renderer(IDebuggerEnvironment env)
        {
            this.env = env;
            sequenceRenderer = new SequenceRenderer(env);

            // GUI TODO: correct place?
            IDebuggerGUIForDataRendering debuggerGUIForDataRendering = env.guiForDataRendering;
            debuggerGUIForDataRendering.graphViewer.AddNodeRealizer("nrStd", GrColor.Black, GrColor.White, GrColor.Black, GrNodeShape.Box);
            debuggerGUIForDataRendering.graphViewer.AddEdgeRealizer("erStd", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous);
        }

        public void BeginOfDisplay(string header)
        {
            // GUI TODO: handling of header
            env.guiForDataRendering.graphViewer.ClearGraph();
            env.guiForDataRendering.graphViewer.Show();

            nodeIdSource = 0;
        }

        public void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix);
        }

        public void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequence(seq, context, nestingLevel, prefix, postfix);
        }

        public void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix);
        }

        public void DisplayLine(string lineToBeShown)
        {
            string nodeName = "line_" + nodeIdSource.ToString();
            env.guiForDataRendering.graphViewer.AddNode(nodeName, "nrStd", lineToBeShown);
            if(nodeIdSource > 0)
            {
                string previousNodeName = "line_" + (nodeIdSource - 1).ToString();
                env.guiForDataRendering.graphViewer.AddEdge((nodeIdSource - 1).ToString() + "->" + nodeIdSource.ToString(), previousNodeName, nodeName, "erStd", "");
            }
            env.guiForDataRendering.graphViewer.Show();
            ++nodeIdSource;
        }
    }
}
