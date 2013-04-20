using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_ExternalAttributeEvaluation
{
    public partial class Own
    {
        public bool muh()
        {
            return false;
        }
    }

    public partial class OwnPown : Own
    {
        public string ehe;
    }
	
	public partial class OwnPownHome : OwnPown
    {
        public string aha;
    }
}

namespace de.unika.ipd.grGen.expression
{
    using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

	public partial class ExternalFunctions
	{
        static GRGEN_MODEL.ExternalAttributeEvaluationGraph graph;

        public static void setGraph(GRGEN_MODEL.ExternalAttributeEvaluationGraph graph_)
        {
            graph = graph_;
        }

        ////////////////////////////////////////////////////////////////////

		public static bool foo(int a, double b, GRGEN_MODEL.ENUM_Enu c, string d)
        {
            return true;
        }

        public static object bar(object a, object b)
        {
            return a ?? b ?? null;
        }

        public static bool isnull(object a)
        {
            return a == null;
        }

        public static bool bla(GRGEN_MODEL.IN a, GRGEN_MODEL.IE b)
        {
            return a.b;
        }

        public static GRGEN_MODEL.IN blo(GRGEN_LIBGR.INode a, GRGEN_LIBGR.IEdge b)
        {
            return graph.CreateNodeN();
        }

        public static GRGEN_MODEL.OwnPown har(GRGEN_MODEL.Own a, GRGEN_MODEL.OwnPown b)
        {
            return a!=null ? (a.muh() ? (GRGEN_MODEL.OwnPown)a : b) : null;
        }

        public static bool hur(GRGEN_MODEL.OwnPown a)
        {
            return a!=null ? a.ehe==null : true;
        }
		
		public static bool hurdur(GRGEN_MODEL.OwnPownHome a)
		{
			return a!=null ? a.aha==null : true;
		}
    }
}

namespace de.unika.ipd.grGen.expression
{
    using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

    public partial class ExternalProcedures
    {
        public static void fooProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int a, double b, GRGEN_MODEL.ENUM_Enu c, string d)
        {
        }

        public static void barProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object a, object b, out object res)
        {
            res = a ?? b ?? null;
        }

        public static void isnullProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object a, out bool res)
        {
            res = a == null;
        }

        public static void blaProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.IN a, GRGEN_MODEL.IE b, out bool res1, out bool res2)
        {
            res1 = a.b;
            res2 = !a.b;
            return;
        }

        public static void bloProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.INode a, GRGEN_LIBGR.IEdge b, out GRGEN_MODEL.IN res)
        {
            res = ((GRGEN_MODEL.ExternalAttributeEvaluationGraph)graph).CreateNodeN();
        }

        public static void harProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.Own a, GRGEN_MODEL.OwnPown b, out GRGEN_MODEL.OwnPown res1, out GRGEN_MODEL.Own res2, out GRGEN_MODEL.IN res3)
        {
            res1 = b;
            res2 = b;
            res3 = ((GRGEN_MODEL.ExternalAttributeEvaluationGraph)graph).CreateNodeN();
        }

        public static void hurProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPown a)
        {
        }

        public static void hurdurProc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPownHome a)
        {
        }
    }
}
