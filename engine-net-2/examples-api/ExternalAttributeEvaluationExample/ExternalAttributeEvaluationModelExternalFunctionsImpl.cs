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
            return null;
        }

        public static bool isnull(object a)
        {
            return true;
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
	}
}
