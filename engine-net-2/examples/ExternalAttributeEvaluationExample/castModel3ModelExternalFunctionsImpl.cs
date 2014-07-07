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
    }

}
