using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_bitbucket2;

namespace de.unika.ipd.grGen.expression
{
	public partial class ExternalFunctions
	{
		public static int TestFunction(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
		{
			return 42;
		}
	}
}

namespace de.unika.ipd.grGen.expression
{
	public partial class ExternalProcedures
	{
		public static void TestProcedure(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph)
		{
		}
	}
}
