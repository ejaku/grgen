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
            var_r1 = var_v1;
            var_r2 = var_v2;
            var_r3 = var_v3;
            var_r4 = var_v4;
            var_r5 = var_v5;
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

    public partial class Sequence_bla
    {
        public static bool ApplyXGRS_bla(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_v1, GRGEN_MODEL.IE var_v2, ref GRGEN_MODEL.IN var_r1, ref GRGEN_MODEL.IE var_r2)
        {
            var_r1 = var_v1;
            var_r2 = var_v2;
            return true;
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
            object x = procEnv.GetVariableValue("x");
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            object a = procEnv.GetVariableValue("a");
            GRGEN_MODEL.N node_a = (GRGEN_MODEL.N)a;
            int val_x = (int)x;
            // announce change so that debugger can show new value or transaction manager can record it and roll it back
            graph.ChangingNodeAttribute(node_a, GRGEN_MODEL.NodeType_N.AttributeType_i, GRGEN_LIBGR.AttributeChangeType.Assign, val_x, null);
            node_a.i = val_x;
            // add reflexive edge
            GRGEN_MODEL.E someEdge = GRGEN_MODEL.E.CreateEdge(graph, node_a, node_a);
            // here you could do other nifty things like deleting nodes, retyping graph elements, or calling rules
            return false;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////

    public partial class MatchFilters
    {
        public static void Filter_f1(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        {
            // just let pass
        }

        public static void Filter_nomnomnom(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        {
            // eat away the single match of the empty rule
            matches.RemoveMatch(0);
        }

        public static void Filter_f2(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        {
            // inspect matches carefully and manipulate as needed
            IEnumerator<Rule_filterBass.IMatch_filterBass> e = matches.GetEnumeratorExact();
            while(e.MoveNext())
            {
                Rule_filterBass.IMatch_filterBass match = e.Current;
                if(match.node_n.i != 42)
                    break;
            }
        }

        public static void Filter_f3(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        {
            // inspect matches carefully and manipulate as needed, 
            // transforming to a List<IMatch> for easier manipulation and back to an IMatchesExact if needed
            List<Rule_filterBass.IMatch_filterBass> matchesArray = matches.ToList();
            matchesArray.Reverse();
            Rule_filterBass.IMatch_filterBass match = matchesArray[matchesArray.Count-1];
            matchesArray.RemoveAt(matchesArray.Count - 1);
            ++match.node_n.i;
            matchesArray.Add(match);
            matchesArray.Reverse();
            matches.FromList();
        }

        public static void Filter_f4(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches)
        {
            // just let pass
        }
    }
}
