using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalFiltersAndSequences;

namespace de.unika.ipd.grGen.Action_ExternalFiltersAndSequences
{
    public partial class Sequence_foo
    {
        public static bool ApplyXGRS_foo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int var_v1, double var_v2, GRGEN_MODEL.ENUM_Enu var_v3, string var_v4, bool var_v5, ref int var_r1, ref double var_r2, ref GRGEN_MODEL.ENUM_Enu var_r3, ref string var_r4, ref bool var_r5)
        {
            return true;
        }
    }

    public partial class Sequence_bar
    {
        public static bool ApplyXGRS_bar(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1, object var_v2, ref object var_r1)
        {
            return true;
        }
    }

    public partial class Sequence_isnull
    {
        public static bool ApplyXGRS_isnull(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1)
        {
            return true;
        }
    }

    public partial class Sequence_bla
    {
        public static bool ApplyXGRS_bla(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_v1, GRGEN_MODEL.IE var_v2, ref GRGEN_MODEL.IN var_r1, ref GRGEN_MODEL.IE var_r2)
        {
            return true;
        }
    }

    public partial class Sequence_blo
    {
        public static bool ApplyXGRS_blo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
        {
            return true;
        }
    }

    public partial class Sequence_huh
    {
        public static bool ApplyXGRS_huh(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv)
        {
            return true;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////

    public partial class MatchFilters
    {
        public static void Filter_f1(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPMatchesList<Rule_filterBase.Match_filterBase, Rule_filterBase.IMatch_filterBase> matches)
        {
            // inspect matches carefully and manipulate as needed
        }

        public static void Filter_f2(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPMatchesList<Rule_filterBass.Match_filterBass, Rule_filterBass.IMatch_filterBass> matches)
        {
            // inspect matches carefully and manipulate as needed
        }

        public static void Filter_f3(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPMatchesList<Rule_filterBass.Match_filterBass, Rule_filterBass.IMatch_filterBass> matches)
        {
            // inspect matches carefully and manipulate as needed
        }

        public static void Filter_f4(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LGSP.LGSPMatchesList<Rule_filterHass.Match_filterHass, Rule_filterHass.IMatch_filterHass> matches)
        {
            // inspect matches carefully and manipulate as needed
        }
    }
}
