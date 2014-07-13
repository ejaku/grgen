// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "cast_External.grg" on Sun Jul 06 23:15:38 CEST 2014

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_castModel3;

namespace de.unika.ipd.grGen.Model_castModel3
{
	public partial class N
	{
        public virtual int a(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            return 42;
        }
    }

	public partial class NN : N
	{
        public Dictionary<string, string> dummy = new Dictionary<string, string>();

        public override int a(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            return 42 + 1;
        }

        public int aa(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            return 42 * a(actionEnv, graph);
        }

        public void p(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraphElement elem, GRGEN_MODEL.N n, string s, out GRGEN_MODEL.N no, out string so)
        {
            no = n;
            so = s;
        }

        public void finegrainChange(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraphElement elemCalledOn)
        {
            Console.WriteLine("finegrainChange called");
            ((GRGEN_LIBGR.ISubactionAndOutputAdditionEnvironment)actionEnv).Recorder.External("add foo bar to " + ((GRGEN_LIBGR.INamedGraph)graph).GetElementName(elemCalledOn));
            dummy.Add("foo","bar");
            ((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv).TransactionManager.ExternalTypeChanged(new UndoDummy((GRGEN_MODEL.INB)elemCalledOn, "foo", graph));
        }
    }

    public class UndoDummy : GRGEN_LIBGR.IUndoItem
    {
        GRGEN_MODEL.INB addTarget;
        string elementToRemoveAgain;
        GRGEN_LIBGR.INamedGraph graph;

        public UndoDummy(GRGEN_MODEL.INB addTarget, string elementToRemoveAgain, GRGEN_LIBGR.IGraph graph)
        {
            this.addTarget = addTarget;
            this.elementToRemoveAgain = elementToRemoveAgain;
            this.graph = (GRGEN_LIBGR.INamedGraph)graph;
        }

        public void DoUndo(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            Console.WriteLine("undo " + graph.GetElementName(addTarget) + "." + elementToRemoveAgain);
            addTarget.n.dummy.Remove(elementToRemoveAgain);
        }

        public override string ToString()
        {
            return graph.GetElementName(addTarget) + " remove " + elementToRemoveAgain;
        }
    }

    public partial class AttributeTypeObjectEmitterParser
    {
        public static object ParseImpl(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
        {
            reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l' // default implementation
            return null; // default implementation
        }

        public static string SerializeImpl(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
        {
            Console.WriteLine("Warning: Exporting attribute of object type to null"); // default implementation
            return "null"; // default implementation
        }

        public static string EmitImpl(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
        {
            return "null"; // default implementation
        }

        public static void ExternalImpl(string line, GRGEN_LIBGR.IGraph graph)
        {
            Console.Write("Carrying out: ");
            Console.WriteLine(line);
        }
    }
}

namespace de.unika.ipd.grGen.expression
{
    public partial class ExternalFunctions
    {
        public static GRGEN_MODEL.NN createNN(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
        {
            return new GRGEN_MODEL.NN();
        }
    }
}
