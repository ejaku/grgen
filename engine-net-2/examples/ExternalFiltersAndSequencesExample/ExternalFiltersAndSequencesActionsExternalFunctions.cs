// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "ExternalFiltersAndSequences.grg" on 01.12.2013 13:17:05 Mitteleuropäische Zeit
using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalFiltersAndSequences;

namespace de.unika.ipd.grGen.Action_ExternalFiltersAndSequences
{
    public partial class Sequence_foo
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_foo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int var_v1, double var_v2, GRGEN_MODEL.ENUM_Enu var_v3, string var_v4, bool var_v5, ref int var_r1, ref double var_r2, ref GRGEN_MODEL.ENUM_Enu var_r3, ref string var_r4, ref bool var_r5)
    }

    public partial class Sequence_bar
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_bar(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1, object var_v2, ref object var_r1)
    }

    public partial class Sequence_isnull
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_isnull(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1)
    }

    public partial class Sequence_bla
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_bla(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_v1, GRGEN_MODEL.IE var_v2, ref GRGEN_MODEL.IN var_r1, ref GRGEN_MODEL.IE var_r2)
    }

    public partial class Sequence_blo
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_blo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
    }

    public partial class Sequence_createEdge
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_createEdge(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_n1, GRGEN_LIBGR.INode var_n2, ref GRGEN_LIBGR.IEdge var_e)
    }

    public partial class Sequence_huh
    {
        // You must implement the following function in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_huh(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv)
    }

    public partial class MatchFilters
    {
        // You must implement the following filter functions in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs
        //public static void Filter_f1(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        //public static void Filter_nomnomnom(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        //public static void Filter_f2(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        //public static void Filter_f3(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        //public static void Filter_f4(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches)

        // ------------------------------------------------------

        // The following filter functions are automatically generated, you don't need to supply any further implementation
        public static void Filter_filterBase_auto(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        {
            if(matches.Count<2)
            	return;
            List<Rule_filterBase.IMatch_filterBase> matchesArray = matches.ToList();
            for(int i = 0; i < matchesArray.Count; ++i)
            {
                if(matchesArray[i] == null)
                	continue;
                for(int j = i + 1; j < matchesArray.Count; ++j)
                {
                    if(matchesArray[j] == null)
                    	continue;
                    if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(matchesArray[i], matchesArray[j], procEnv.graph))
                    	matchesArray[j] = null;
                }
            }
            matches.FromList();
        }
        public static void Filter_filterBass_auto(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        {
            if(matches.Count<2)
            	return;
            List<Rule_filterBass.IMatch_filterBass> matchesArray = matches.ToList();
            for(int i = 0; i < matchesArray.Count; ++i)
            {
                if(matchesArray[i] == null)
                	continue;
                for(int j = i + 1; j < matchesArray.Count; ++j)
                {
                    if(matchesArray[j] == null)
                    	continue;
                    if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(matchesArray[i], matchesArray[j], procEnv.graph))
                    	matchesArray[j] = null;
                }
            }
            matches.FromList();
        }
    }

    // ------------------------------------------------------

    public partial class Sequence_foo : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_foo instance = null;
        public static Sequence_foo Instance { get { if(instance==null) instance = new Sequence_foo(); return instance; } }
        private Sequence_foo() : base("foo", SequenceInfo_foo.Instance) { }

        public static bool Apply_foo(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int var_v1, double var_v2, GRGEN_MODEL.ENUM_Enu var_v3, string var_v4, bool var_v5, ref int var_r1, ref double var_r2, ref GRGEN_MODEL.ENUM_Enu var_r3, ref string var_r4, ref bool var_r5)
        {
            int vari_r1 = 0;
            double vari_r2 = 0.0;
            GRGEN_MODEL.ENUM_Enu vari_r3 = (GRGEN_MODEL.ENUM_Enu)0;
            string vari_r4 = "";
            bool vari_r5 = false;
            bool result = ApplyXGRS_foo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, var_v3, var_v4, var_v5, ref var_r1, ref var_r2, ref var_r3, ref var_r4, ref var_r5);
            if(result) {
                var_r1 = vari_r1;
                var_r2 = vari_r2;
                var_r3 = vari_r3;
                var_r4 = vari_r4;
                var_r5 = vari_r5;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            int var_v1 = (int)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            double var_v2 = (double)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_MODEL.ENUM_Enu var_v3 = (GRGEN_MODEL.ENUM_Enu)sequenceInvocation.ArgumentExpressions[2].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            string var_v4 = (string)sequenceInvocation.ArgumentExpressions[3].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            bool var_v5 = (bool)sequenceInvocation.ArgumentExpressions[4].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            int var_r1 = 0;
            double var_r2 = 0.0;
            GRGEN_MODEL.ENUM_Enu var_r3 = (GRGEN_MODEL.ENUM_Enu)0;
            string var_r4 = "";
            bool var_r5 = false;
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_foo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, var_v3, var_v4, var_v5, ref var_r1, ref var_r2, ref var_r3, ref var_r4, ref var_r5);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
                sequenceInvocation.ReturnVars[1].SetVariableValue(var_r2, procEnv);
                sequenceInvocation.ReturnVars[2].SetVariableValue(var_r3, procEnv);
                sequenceInvocation.ReturnVars[3].SetVariableValue(var_r4, procEnv);
                sequenceInvocation.ReturnVars[4].SetVariableValue(var_r5, procEnv);
            }
            return result;
        }
    }

    public partial class Sequence_bar : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_bar instance = null;
        public static Sequence_bar Instance { get { if(instance==null) instance = new Sequence_bar(); return instance; } }
        private Sequence_bar() : base("bar", SequenceInfo_bar.Instance) { }

        public static bool Apply_bar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object var_v1, object var_v2, ref object var_r1)
        {
            object vari_r1 = null;
            bool result = ApplyXGRS_bar((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1);
            if(result) {
                var_r1 = vari_r1;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            object var_v1 = (object)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            object var_v2 = (object)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            object var_r1 = null;
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_bar((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
            }
            return result;
        }
    }

    public partial class Sequence_isnull : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_isnull instance = null;
        public static Sequence_isnull Instance { get { if(instance==null) instance = new Sequence_isnull(); return instance; } }
        private Sequence_isnull() : base("isnull", SequenceInfo_isnull.Instance) { }

        public static bool Apply_isnull(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object var_v1)
        {
            bool result = ApplyXGRS_isnull((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1);
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            object var_v1 = (object)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_isnull((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            return result;
        }
    }

    public partial class Sequence_bla : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_bla instance = null;
        public static Sequence_bla Instance { get { if(instance==null) instance = new Sequence_bla(); return instance; } }
        private Sequence_bla() : base("bla", SequenceInfo_bla.Instance) { }

        public static bool Apply_bla(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_v1, GRGEN_MODEL.IE var_v2, ref GRGEN_MODEL.IN var_r1, ref GRGEN_MODEL.IE var_r2)
        {
            GRGEN_MODEL.IN vari_r1 = null;
            GRGEN_MODEL.IE vari_r2 = null;
            bool result = ApplyXGRS_bla((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(result) {
                var_r1 = vari_r1;
                var_r2 = vari_r2;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_MODEL.IN var_v1 = (GRGEN_MODEL.IN)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_MODEL.IE var_v2 = (GRGEN_MODEL.IE)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_MODEL.IN var_r1 = null;
            GRGEN_MODEL.IE var_r2 = null;
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_bla((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
                sequenceInvocation.ReturnVars[1].SetVariableValue(var_r2, procEnv);
            }
            return result;
        }
    }

    public partial class Sequence_blo : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_blo instance = null;
        public static Sequence_blo Instance { get { if(instance==null) instance = new Sequence_blo(); return instance; } }
        private Sequence_blo() : base("blo", SequenceInfo_blo.Instance) { }

        public static bool Apply_blo(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
        {
            GRGEN_LIBGR.INode vari_r1 = null;
            GRGEN_LIBGR.IEdge vari_r2 = null;
            bool result = ApplyXGRS_blo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(result) {
                var_r1 = vari_r1;
                var_r2 = vari_r2;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_LIBGR.INode var_v1 = (GRGEN_LIBGR.INode)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.IEdge var_v2 = (GRGEN_LIBGR.IEdge)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.INode var_r1 = null;
            GRGEN_LIBGR.IEdge var_r2 = null;
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_blo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
                sequenceInvocation.ReturnVars[1].SetVariableValue(var_r2, procEnv);
            }
            return result;
        }
    }

    public partial class Sequence_createEdge : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_createEdge instance = null;
        public static Sequence_createEdge Instance { get { if(instance==null) instance = new Sequence_createEdge(); return instance; } }
        private Sequence_createEdge() : base("createEdge", SequenceInfo_createEdge.Instance) { }

        public static bool Apply_createEdge(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_n1, GRGEN_LIBGR.INode var_n2, ref GRGEN_LIBGR.IEdge var_e)
        {
            GRGEN_LIBGR.IEdge vari_e = null;
            bool result = ApplyXGRS_createEdge((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_n1, var_n2, ref var_e);
            if(result) {
                var_e = vari_e;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_LIBGR.INode var_n1 = (GRGEN_LIBGR.INode)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.INode var_n2 = (GRGEN_LIBGR.INode)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.IEdge var_e = null;
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_createEdge((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_n1, var_n2, ref var_e);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_e, procEnv);
            }
            return result;
        }
    }

    public partial class Sequence_huh : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_huh instance = null;
        public static Sequence_huh Instance { get { if(instance==null) instance = new Sequence_huh(); return instance; } }
        private Sequence_huh() : base("huh", SequenceInfo_huh.Instance) { }

        public static bool Apply_huh(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            bool result = ApplyXGRS_huh((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            bool result = ApplyXGRS_huh((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            if(sequenceInvocation.Subgraph!=null)
            	{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }
            return result;
        }
    }
}
