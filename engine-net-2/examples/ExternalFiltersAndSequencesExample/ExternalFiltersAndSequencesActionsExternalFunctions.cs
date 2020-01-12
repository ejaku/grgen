// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "ExternalFiltersAndSequences.grg" on 12.01.2020 22:15:09 Mitteleuropäische Zeit
using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalFiltersAndSequences;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_ExternalFiltersAndSequences;

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
        //public static bool ApplyXGRS_blo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IDEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
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

    // You must implement the following filter functions in the same partial class in ./..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequencesActionsExternalFunctionsImpl.cs

    public partial class MatchFilters
    {
        //public static void Filter_f1(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
    }
    public partial class MatchFilters
    {
        //public static void Filter_nomnomnom(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
    }
    public partial class MatchFilters
    {
        //public static void Filter_f2(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
    }
    public partial class MatchFilters
    {
        //public static void Filter_f3(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
    }
    public partial class MatchFilters
    {
        //public static void Filter_f4(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterHass.IMatch_filterHass> matches, System.Int32 i, System.String s)
    }

    // ------------------------------------------------------

    // The following filter functions are automatically generated, you don't need to supply any further implementation

    public partial class MatchFilters
    {
        public static void Filter_filterBase_auto(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBase.IMatch_filterBase> matches)
        {
            if(matches.Count < 2)
            	return;
            List<Rule_filterBase.IMatch_filterBase> matchesArray = matches.ToList();
            if(matches.Count < 5 || Rule_filterBase.Instance.patternGraph.nodes.Length + Rule_filterBase.Instance.patternGraph.edges.Length < 1)
            {
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
            }
            else
            {
                Dictionary<int, Rule_filterBase.Match_filterBase> foundMatchesOfSameMainPatternHash = new Dictionary<int, Rule_filterBase.Match_filterBase>();
                for(int i = 0; i < matchesArray.Count; ++i)
                {
                    Rule_filterBase.Match_filterBase match = (Rule_filterBase.Match_filterBase)matchesArray[i];
                    int duplicateMatchHash = 0;
                    for(int j = 0; j < match.NumberOfNodes; ++j) duplicateMatchHash ^= match.getNodeAt(j).GetHashCode();
                    for(int j = 0; j < match.NumberOfEdges; ++j) duplicateMatchHash ^= match.getEdgeAt(j).GetHashCode();
                    bool contained = foundMatchesOfSameMainPatternHash.ContainsKey(duplicateMatchHash);
                    if(contained)
                    {
                        Rule_filterBase.Match_filterBase duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];
                        do
                        {
                            if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(match, duplicateMatchCandidate, procEnv.graph))
                            {
                                matchesArray[i] = null;
                                goto label_auto_Rule_filterBase;
                            }
                        }
                        while((duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash) != null);
                    }
                    if(!contained)
                    	foundMatchesOfSameMainPatternHash[duplicateMatchHash] = match;
                    else
                    {
                        Rule_filterBase.Match_filterBase duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];
                        while(duplicateMatchCandidate.nextWithSameHash != null) duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash;
                        duplicateMatchCandidate.nextWithSameHash = match;
                    }
label_auto_Rule_filterBase: ;
                }
                foreach(Rule_filterBase.Match_filterBase toClean in foundMatchesOfSameMainPatternHash.Values) toClean.CleanNextWithSameHash();
            }
            matches.FromList();
        }
    }
    public partial class MatchFilters
    {
        public static void Filter_filterBass_auto(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<Rule_filterBass.IMatch_filterBass> matches)
        {
            if(matches.Count < 2)
            	return;
            List<Rule_filterBass.IMatch_filterBass> matchesArray = matches.ToList();
            if(matches.Count < 5 || Rule_filterBass.Instance.patternGraph.nodes.Length + Rule_filterBass.Instance.patternGraph.edges.Length < 1)
            {
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
            }
            else
            {
                Dictionary<int, Rule_filterBass.Match_filterBass> foundMatchesOfSameMainPatternHash = new Dictionary<int, Rule_filterBass.Match_filterBass>();
                for(int i = 0; i < matchesArray.Count; ++i)
                {
                    Rule_filterBass.Match_filterBass match = (Rule_filterBass.Match_filterBass)matchesArray[i];
                    int duplicateMatchHash = 0;
                    for(int j = 0; j < match.NumberOfNodes; ++j) duplicateMatchHash ^= match.getNodeAt(j).GetHashCode();
                    for(int j = 0; j < match.NumberOfEdges; ++j) duplicateMatchHash ^= match.getEdgeAt(j).GetHashCode();
                    bool contained = foundMatchesOfSameMainPatternHash.ContainsKey(duplicateMatchHash);
                    if(contained)
                    {
                        Rule_filterBass.Match_filterBass duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];
                        do
                        {
                            if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(match, duplicateMatchCandidate, procEnv.graph))
                            {
                                matchesArray[i] = null;
                                goto label_auto_Rule_filterBass;
                            }
                        }
                        while((duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash) != null);
                    }
                    if(!contained)
                    	foundMatchesOfSameMainPatternHash[duplicateMatchHash] = match;
                    else
                    {
                        Rule_filterBass.Match_filterBass duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];
                        while(duplicateMatchCandidate.nextWithSameHash != null) duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash;
                        duplicateMatchCandidate.nextWithSameHash = match;
                    }
label_auto_Rule_filterBass: ;
                }
                foreach(Rule_filterBass.Match_filterBass toClean in foundMatchesOfSameMainPatternHash.Values) toClean.CleanNextWithSameHash();
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

        private object[] ReturnValues = new object[5];

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

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            int var_v1 = (int)arguments[0];
            double var_v2 = (double)arguments[1];
            GRGEN_MODEL.ENUM_Enu var_v3 = (GRGEN_MODEL.ENUM_Enu)arguments[2];
            string var_v4 = (string)arguments[3];
            bool var_v5 = (bool)arguments[4];
            int var_r1 = 0;
            double var_r2 = 0.0;
            GRGEN_MODEL.ENUM_Enu var_r3 = (GRGEN_MODEL.ENUM_Enu)0;
            string var_r4 = "";
            bool var_r5 = false;
            bool result = ApplyXGRS_foo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, var_v3, var_v4, var_v5, ref var_r1, ref var_r2, ref var_r3, ref var_r4, ref var_r5);
            returnValues = ReturnValues;
            if(result) {
                returnValues[0] = var_r1;
                returnValues[1] = var_r2;
                returnValues[2] = var_r3;
                returnValues[3] = var_r4;
                returnValues[4] = var_r5;
            }
            return result;
        }
    }

    public partial class Sequence_bar : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_bar instance = null;
        public static Sequence_bar Instance { get { if(instance==null) instance = new Sequence_bar(); return instance; } }
        private Sequence_bar() : base("bar", SequenceInfo_bar.Instance) { }

        private object[] ReturnValues = new object[1];

        public static bool Apply_bar(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object var_v1, object var_v2, ref object var_r1)
        {
            object vari_r1 = null;
            bool result = ApplyXGRS_bar((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1);
            if(result) {
                var_r1 = vari_r1;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            object var_v1 = (object)arguments[0];
            object var_v2 = (object)arguments[1];
            object var_r1 = null;
            bool result = ApplyXGRS_bar((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1);
            returnValues = ReturnValues;
            if(result) {
                returnValues[0] = var_r1;
            }
            return result;
        }
    }

    public partial class Sequence_isnull : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_isnull instance = null;
        public static Sequence_isnull Instance { get { if(instance==null) instance = new Sequence_isnull(); return instance; } }
        private Sequence_isnull() : base("isnull", SequenceInfo_isnull.Instance) { }

        private object[] ReturnValues = new object[0];

        public static bool Apply_isnull(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object var_v1)
        {
            bool result = ApplyXGRS_isnull((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1);
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            object var_v1 = (object)arguments[0];
            bool result = ApplyXGRS_isnull((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1);
            returnValues = ReturnValues;
            return result;
        }
    }

    public partial class Sequence_bla : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_bla instance = null;
        public static Sequence_bla Instance { get { if(instance==null) instance = new Sequence_bla(); return instance; } }
        private Sequence_bla() : base("bla", SequenceInfo_bla.Instance) { }

        private object[] ReturnValues = new object[2];

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

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_MODEL.IN var_v1 = (GRGEN_MODEL.IN)arguments[0];
            GRGEN_MODEL.IE var_v2 = (GRGEN_MODEL.IE)arguments[1];
            GRGEN_MODEL.IN var_r1 = null;
            GRGEN_MODEL.IE var_r2 = null;
            bool result = ApplyXGRS_bla((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            returnValues = ReturnValues;
            if(result) {
                returnValues[0] = var_r1;
                returnValues[1] = var_r2;
            }
            return result;
        }
    }

    public partial class Sequence_blo : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_blo instance = null;
        public static Sequence_blo Instance { get { if(instance==null) instance = new Sequence_blo(); return instance; } }
        private Sequence_blo() : base("blo", SequenceInfo_blo.Instance) { }

        private object[] ReturnValues = new object[2];

        public static bool Apply_blo(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IDEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
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

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_LIBGR.INode var_v1 = (GRGEN_LIBGR.INode)arguments[0];
            GRGEN_LIBGR.IDEdge var_v2 = (GRGEN_LIBGR.IDEdge)arguments[1];
            GRGEN_LIBGR.INode var_r1 = null;
            GRGEN_LIBGR.IEdge var_r2 = null;
            bool result = ApplyXGRS_blo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            returnValues = ReturnValues;
            if(result) {
                returnValues[0] = var_r1;
                returnValues[1] = var_r2;
            }
            return result;
        }
    }

    public partial class Sequence_createEdge : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_createEdge instance = null;
        public static Sequence_createEdge Instance { get { if(instance==null) instance = new Sequence_createEdge(); return instance; } }
        private Sequence_createEdge() : base("createEdge", SequenceInfo_createEdge.Instance) { }

        private object[] ReturnValues = new object[1];

        public static bool Apply_createEdge(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_n1, GRGEN_LIBGR.INode var_n2, ref GRGEN_LIBGR.IEdge var_e)
        {
            GRGEN_LIBGR.IEdge vari_e = null;
            bool result = ApplyXGRS_createEdge((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_n1, var_n2, ref var_e);
            if(result) {
                var_e = vari_e;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_LIBGR.INode var_n1 = (GRGEN_LIBGR.INode)arguments[0];
            GRGEN_LIBGR.INode var_n2 = (GRGEN_LIBGR.INode)arguments[1];
            GRGEN_LIBGR.IEdge var_e = null;
            bool result = ApplyXGRS_createEdge((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_n1, var_n2, ref var_e);
            returnValues = ReturnValues;
            if(result) {
                returnValues[0] = var_e;
            }
            return result;
        }
    }

    public partial class Sequence_huh : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_huh instance = null;
        public static Sequence_huh Instance { get { if(instance==null) instance = new Sequence_huh(); return instance; } }
        private Sequence_huh() : base("huh", SequenceInfo_huh.Instance) { }

        private object[] ReturnValues = new object[0];

        public static bool Apply_huh(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)
        {
            bool result = ApplyXGRS_huh((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            bool result = ApplyXGRS_huh((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            returnValues = ReturnValues;
            return result;
        }
    }
}
