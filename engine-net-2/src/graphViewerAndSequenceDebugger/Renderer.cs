/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// Class used to render debugger output in graph form to the user, see IDisplayer for more comments.
    /// Sits in between the debugger and the debugger environment.
    /// </summary>
    public class Renderer : Displayer
    {
        private IDebuggerEnvironment env;
        private SequenceRenderer sequenceRenderer;

        private int lineNodeIdSource;
        private int idSource;

        public Renderer(IDebuggerEnvironment env)
        {
            this.env = env;
            sequenceRenderer = new SequenceRenderer(env);

            // GUI TODO: correct place?
            ITwinConsoleUIDataRenderingGUI debuggerGUIForDataRendering = env.guiForDataRendering;
            debuggerGUIForDataRendering.graphViewer.AddNodeRealizer("nrStd", GrColor.Black, GrColor.White, GrColor.Black, GrNodeShape.Box);
            debuggerGUIForDataRendering.graphViewer.AddEdgeRealizer("erStd", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous);

            debuggerGUIForDataRendering.graphViewer.AddNodeRealizer("nr" + GroupNodeTypes.CallStackFrame, GrColor.Black, GrColor.White, GrColor.Black, GrNodeShape.Box);
            debuggerGUIForDataRendering.graphViewer.AddNodeRealizer("nr" + GroupNodeTypes.LocalVariables, GrColor.Blue, GrColor.White, GrColor.Black, GrNodeShape.Box);
            debuggerGUIForDataRendering.graphViewer.AddNodeRealizer("nr" + GroupNodeTypes.GlobalVariables, GrColor.Blue, GrColor.White, GrColor.Black, GrNodeShape.Box);
            debuggerGUIForDataRendering.graphViewer.AddNodeRealizer("nr" + GroupNodeTypes.VisitedFlags, GrColor.Orchid, GrColor.White, GrColor.Black, GrNodeShape.Box);
        }

        public override void BeginOfDisplay(string header)
        {
            // GUI TODO: handling of header
            env.guiForDataRendering.graphViewer.ClearGraph();
            env.guiForDataRendering.graphViewer.Show();

            lineNodeIdSource = 0;
            idSource = 0;
        }

        public override void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix);
        }

        public override void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequence(seq, context, nestingLevel, prefix, postfix);
        }

        public override void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            sequenceRenderer.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix);
        }

        public override string DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            return sequenceRenderer.DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        public override string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            return sequenceRenderer.DisplaySequence(seq, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        public override string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            return sequenceRenderer.DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        //public void DisplayCallStacks(SequenceBase[] callStack, SubruleComputation[] subruleStack, bool fullSubruleTracesEntries); inherited from Displayer
        //public void DisplayVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv); inherited from Displayer
        //public void DisplayFullState(SequenceBase[] callStack, SubruleComputation[] subruleStack, DebuggerGraphProcessingEnvironment debuggerProcEnv); inherited from Displayer

        public override void DisplayObject(object obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            string str = EmitHelper.ToStringAutomatic(obj, procEnv.NamedGraph, false, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, procEnv);
            env.guiForDataRendering.graphViewer.AddNode(FetchNodeName(), "nrStd", str);
            Show();
        }

        public override void DisplayClassObject(IObject obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            DisplayBaseObject(obj, /*"%:" +*/debuggerProcEnv.objectNamerAndIndexer.GetOrAssignName(obj) + " : " + obj.Type.PackagePrefixedName, procEnv, debuggerProcEnv);
        }

        public override void DisplayTransientClassObject(ITransientObject obj, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            DisplayBaseObject(obj, /*"&:" +*/"&" + debuggerProcEnv.transientObjectNamerAndIndexer.GetUniqueId(obj) + " : " + obj.Type.PackagePrefixedName, procEnv, debuggerProcEnv);
        }

        public override void DisplayLine(string lineToBeShown)
        {
            string nodeName = "line_" + lineNodeIdSource.ToString();
            env.guiForDataRendering.graphViewer.AddNode(nodeName, "nrStd", lineToBeShown);
            if(lineNodeIdSource > 0)
            {
                string previousNodeName = "line_" + (lineNodeIdSource - 1).ToString();
                env.guiForDataRendering.graphViewer.AddEdge((lineNodeIdSource - 1).ToString() + "->" + lineNodeIdSource.ToString(), previousNodeName, nodeName, "erStd", "");
            }
            env.guiForDataRendering.graphViewer.Show();
            ++lineNodeIdSource;
        }

        // ----------------------------------------------------------------

        protected override void PrintGlobalVariables(DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            //DisplayLine("Available global (non null) variables:");
            string groupNodeName = CreateGroupNode(GroupNodeTypes.GlobalVariables);
            foreach(Variable var in debuggerProcEnv.ProcEnv.Variables)
            {
                string type;
                string content;
                VariableToString(var.Value, out type, out content, debuggerProcEnv);

                //DisplayLine("  " + var.Name + " = " + content + " : " + type);
                string nodeName = FetchNodeName(); // TODO: when rendering a real ASG containing data flow we likely need a name based on variable name (and stack frame)
                env.guiForDataRendering.graphViewer.AddNode(nodeName, "nr" + GroupNodeTypes.GlobalVariables.ToString(), var.Name + " = " + content + " : " + type);
                env.guiForDataRendering.graphViewer.MoveNode(nodeName, groupNodeName);
            }
        }

        protected override void PrintVisited(DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            List<int> allocatedVisitedFlags = debuggerProcEnv.ProcEnv.NamedGraph.GetAllocatedVisitedFlags();
            StringBuilder sb = new StringBuilder();
            sb.Append("Allocated visited flags are: ");
            bool first = true;
            foreach(int allocatedVisitedFlag in allocatedVisitedFlags)
            {
                if(!first)
                    sb.Append(", ");
                sb.Append(allocatedVisitedFlag.ToString());
                first = false;
            }
            //DisplayLine(sb.ToString() + ".");
            string nodeName = FetchNodeName();
            env.guiForDataRendering.graphViewer.AddNode(nodeName, "nr" + GroupNodeTypes.VisitedFlags.ToString(), sb.ToString() + ".");
        }

        protected override string PrintVariables(SequenceBase seqStart, SequenceBase seq, DebuggerGraphProcessingEnvironment debuggerProcEnv, string groupNodeName)
        {
            //DisplayLine("Available local variables:");
            string variablesGroupNodeName = CreateGroupNode(GroupNodeTypes.LocalVariables);
            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqStart.GetLocalVariables(seqVars, constructors, seq);
            List<SequenceVariable> arrayBuilder = new List<SequenceVariable>();
            foreach(SequenceVariable seqVar in seqVars.Keys)
            {
                string type;
                string content;
                VariableToString(seqVar.LocalVariableValue, out type, out content, debuggerProcEnv);

                //DisplayLine("  " + seqVar.Name + " = " + content + " : " + type);
                string nodeName = FetchNodeName(); // TODO: when rendering a real ASG containing data flow we likely need a name based on variable name (and stack frame)
                env.guiForDataRendering.graphViewer.AddNode(nodeName, "nr" + GroupNodeTypes.LocalVariables.ToString(), seqVar.Name + " = " + content + " : " + type);
                env.guiForDataRendering.graphViewer.MoveNode(nodeName, variablesGroupNodeName);
            }
            if(groupNodeName != null)
                env.guiForDataRendering.graphViewer.MoveNode(variablesGroupNodeName, groupNodeName);
            return variablesGroupNodeName;
        }

        protected override string PrintDebugTracesStack(SubruleComputation[] computationsEnteredStack, bool full)
        {
            //DisplayLine("Subrule traces stack is:");
            string firstNodeName = null;
            string nodeName = null;
            for(int i = 0; i < computationsEnteredStack.Length; ++i)
            {
                if(!full && computationsEnteredStack[i].type != SubruleComputationType.Entry)
                    continue;
                //DisplayLine(computationsEnteredStack[i].ToString(full));
                string previousNodeName = nodeName;
                nodeName = FetchNodeName(); // TODO: when rendering a real ASG containing data flow we likely need a name based on variable name (and stack frame)
                env.guiForDataRendering.graphViewer.AddNode(nodeName, "nrStd", computationsEnteredStack[i].ToString(full));
                if(firstNodeName == null)
                    firstNodeName = nodeName;
                if(previousNodeName != null)
                    LinkTo(previousNodeName, nodeName, "entered");
            }
            return firstNodeName;
        }

        protected override string CreateGroupNode(GroupNodeTypes groupNodeType)
        {
            string groupNodeName = FetchNodeName();
            env.guiForDataRendering.graphViewer.AddSubgraphNode(groupNodeName, "nr" + groupNodeType.ToString(), groupNodeType.ToString());
            return groupNodeName;
        }

        protected override string LinkTo(string sourceNodeName, string targetNodeName, string label)
        {
            string edgeName = sourceNodeName + "->" + targetNodeName;
            env.guiForDataRendering.graphViewer.AddEdge(edgeName, sourceNodeName, targetNodeName, "erStd", label);
            return edgeName;
        }

        private string FetchNodeName()
        {
            string nodeName = idSource.ToString();
            ++idSource;
            return "rn" + nodeName; // renderer node name based on the idSource could coincide with node names from the sequence renderer that are based on the sequence id, disambiguate by name prefix
        }

        protected override void Show()
        {
            env.guiForDataRendering.graphViewer.Show();
        }

        private void DisplayBaseObject(IBaseObject obj, string nameAndType, IGraphProcessingEnvironment procEnv, DebuggerGraphProcessingEnvironment debuggerProcEnv)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(nameAndType);
            foreach(AttributeType attrType in obj.Type.AttributeTypes)
            {
                string attrTypeString;
                string attrValueString;
                GetAttributeString(attrType, obj, procEnv.NamedGraph, debuggerProcEnv.objectNamerAndIndexer, debuggerProcEnv.transientObjectNamerAndIndexer, out attrTypeString, out attrValueString);
                sb.AppendLine(attrType.OwnerType.Name + "::" + attrType.Name + " : " + attrTypeString + " = " + attrValueString);
            }
            env.guiForDataRendering.graphViewer.AddNode(FetchNodeName(), "nrStd", sb.ToString());
            Show();
        }

        private void GetAttributeString(AttributeType attrType, IAttributeBearer obj, IGraph graph, ObjectNamerAndIndexer objectNamerAndIndexer, TransientObjectNamerAndIndexer transientObjectNamerAndIndexer, out string attrTypeString, out string attrValueString)
        {
            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
                EmitHelper.ToString((IDictionary)obj.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
            else if(attrType.Kind == AttributeKind.ArrayAttr)
                EmitHelper.ToString((IList)obj.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
            else if(attrType.Kind == AttributeKind.DequeAttr)
                EmitHelper.ToString((IDeque)obj.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
            else
                EmitHelper.ToString(obj.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
        }
    }
}
