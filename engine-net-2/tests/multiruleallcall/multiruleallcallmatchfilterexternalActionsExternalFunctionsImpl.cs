// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "multiruleallcallmatchfilterexternal.grg" on 29.02.2020 21:25:27 Mitteleuropäische Zeit
using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_modelmulti;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_multiruleallcallmatchfilterexternal;

namespace de.unika.ipd.grGen.Action_multiruleallcallmatchfilterexternal
{
    // ------------------------------------------------------

    public partial class MatchClassFilters
    {
        public static void Filter_fext(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IList<GRGEN_LIBGR.IMatch> matches, System.Int32 f)
		{
            GRGEN_LIBGR.IGraph graph = procEnv.Graph;
            List<IMatch_mc> matchesExact = GRGEN_LIBGR.MatchListHelper.ToList<IMatch_mc>(matches);
            foreach(IMatch_mc m in matchesExact)
            {
                GRGEN_MODEL.IN n = (GRGEN_MODEL.IN)(m.node_n);
                m.var_idef = n.@i + f;
                procEnv.EmitWriter.Write("the value of variable \"n.i\" of type int is: ");
                procEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToStringNonNull(n.@i, graph));
                procEnv.EmitWriter.Write("\n");
                procEnv.EmitWriter.Write("the value of variable \"idef\" of type int is: ");
                procEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToStringNonNull(m.var_idef, graph));
                procEnv.EmitWriter.Write("\n");
            }
            matchesExact.RemoveRange(1, matchesExact.Count - 1);
            GRGEN_LIBGR.MatchListHelper.FromList(matches, matchesExact);
            return;
        }
    }

    // ------------------------------------------------------
}
