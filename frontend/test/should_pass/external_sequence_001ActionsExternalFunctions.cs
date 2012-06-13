// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "external_sequence_001.grg" on 13.06.2012 10:54:33 Mitteleuropäische Zeit
using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_external_sequence_001
{
    public partial class Sequence_foo
    {
        // You must implement the following function in the same partial class in ./external_sequence_001ActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_foo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, int var_v1, double var_v2, string var_v3, bool var_v4, ref int var_r1, ref double var_r2, ref string var_r3, ref bool var_r4)
    }

    public partial class Sequence_bar
    {
        // You must implement the following function in the same partial class in ./external_sequence_001ActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_bar(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v1, object var_v2, ref object var_r1)
    }

    public partial class Sequence_isnull
    {
        // You must implement the following function in the same partial class in ./external_sequence_001ActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_isnull(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, object var_v3)
    }

    public partial class Sequence_blo
    {
        // You must implement the following function in the same partial class in ./external_sequence_001ActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_blo(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.INode var_v1, GRGEN_LIBGR.IEdge var_v2, ref GRGEN_LIBGR.INode var_r1, ref GRGEN_LIBGR.IEdge var_r2)
    }

    public partial class Sequence_huh
    {
        // You must implement the following function in the same partial class in ./external_sequence_001ActionsExternalFunctionsImpl.cs
        //public static bool ApplyXGRS_huh(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv)
    }

    // ------------------------------------------------------

    public partial class Sequence_foo : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_foo instance = null;
        public static Sequence_foo Instance { get { if(instance==null) instance = new Sequence_foo(); return instance; } }
        private Sequence_foo() : base("foo", SequenceInfo_foo.Instance) { }

        public static bool Apply_foo(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, int var_v1, double var_v2, string var_v3, bool var_v4, ref int var_r1, ref double var_r2, ref string var_r3, ref bool var_r4)
        {
            int vari_r1 = 0;
            double vari_r2 = 0.0;
            string vari_r3 = "";
            bool vari_r4 = false;
            bool result = ApplyXGRS_foo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, var_v3, var_v4, ref var_r1, ref var_r2, ref var_r3, ref var_r4);
            if(result) {
                var_r1 = vari_r1;
                var_r2 = vari_r2;
                var_r3 = vari_r3;
                var_r4 = vari_r4;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            int var_v1 = (int)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            double var_v2 = (double)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            string var_v3 = (string)sequenceInvocation.ArgumentExpressions[2].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            bool var_v4 = (bool)sequenceInvocation.ArgumentExpressions[3].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            int var_r1 = 0;
            double var_r2 = 0.0;
            string var_r3 = "";
            bool var_r4 = false;
            bool result = ApplyXGRS_foo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, var_v3, var_v4, ref var_r1, ref var_r2, ref var_r3, ref var_r4);
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
                sequenceInvocation.ReturnVars[1].SetVariableValue(var_r2, procEnv);
                sequenceInvocation.ReturnVars[2].SetVariableValue(var_r3, procEnv);
                sequenceInvocation.ReturnVars[3].SetVariableValue(var_r4, procEnv);
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
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            object var_v1 = (object)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            object var_v2 = (object)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            object var_r1 = null;
            bool result = ApplyXGRS_bar((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1);
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

        public static bool Apply_isnull(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object var_v3)
        {
            bool result = ApplyXGRS_isnull((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v3);
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            object var_v3 = (object)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            bool result = ApplyXGRS_isnull((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v3);
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
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            GRGEN_LIBGR.INode var_v1 = (GRGEN_LIBGR.INode)sequenceInvocation.ArgumentExpressions[0].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.IEdge var_v2 = (GRGEN_LIBGR.IEdge)sequenceInvocation.ArgumentExpressions[1].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            GRGEN_LIBGR.INode var_r1 = null;
            GRGEN_LIBGR.IEdge var_r2 = null;
            bool result = ApplyXGRS_blo((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_v1, var_v2, ref var_r1, ref var_r2);
            if(result) {
                sequenceInvocation.ReturnVars[0].SetVariableValue(var_r1, procEnv);
                sequenceInvocation.ReturnVars[1].SetVariableValue(var_r2, procEnv);
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
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;
            bool result = ApplyXGRS_huh((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);
            return result;
        }
    }
}
