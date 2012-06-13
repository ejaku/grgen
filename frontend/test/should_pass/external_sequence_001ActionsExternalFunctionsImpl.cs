using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_external_sequence_001
{
    public partial class Sequence_foo
    {
        public static bool ApplyXGRS_foo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int var_v1, double var_v2, string var_v3, bool var_v4, ref int var_r1, ref double var_r2, ref string var_r3, ref bool var_r4)
        {
            var_r1 = var_v1;
            var_r2 = var_v2;
            var_r3 = var_v3;
            var_r4 = var_v4;
            return true;
        }
    }

    public partial class Sequence_bar
    {
        public static bool ApplyXGRS_bar(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1, object var_v2, ref object var_r1)
        {
            var_r1 = var_v1 ?? var_v2;
            return true;
        }
    }

    public partial class Sequence_isnull
    {
        public static bool ApplyXGRS_isnull(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1)
        {
            return var_v1 == null;
        }
    }

    public partial class Sequence_blo
    {
        public static bool ApplyXGRS_blo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
        {
            var_r1 = var_v1;
            var_r2 = var_v2;
            return true;
        }
    }

    public partial class Sequence_huh
    {
        public static bool ApplyXGRS_huh(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv)
        {
            return false;
        }
    }
}
