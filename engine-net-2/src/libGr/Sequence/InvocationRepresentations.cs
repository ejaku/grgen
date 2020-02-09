/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a rule or sequence or procedure or function invocation.
    /// </summary>
    public abstract class Invocation
    {
        /// <summary>
        /// The name of the rule or sequence or procedure or function.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// null if this is a call of a global rule/sequence/procedure/function, otherwise the package the call target is contained in.
        /// </summary>
        public abstract String Package { get; }

        /// <summary>
        /// The name of the rule or sequence or procedure or function, prefixed by the package it is contained in (separated by a double colon), if it is contained in a package.
        /// </summary>
        public abstract String PackagePrefixedName { get; }
    }

    /// <summary>
    /// An object representing a rule invocation.
    /// </summary>
    public abstract class RuleInvocation : Invocation
    {
        /// <summary>
        /// The subgraph to be switched to for rule execution
        /// </summary>
        public readonly SequenceVariable Subgraph;

        protected RuleInvocation(SequenceVariable subgraph)
        {
            Subgraph = subgraph;
        }
    }

    /// <summary>
    /// An object representing a rule invocation at runtime.
    /// </summary>
    public class RuleInvocationInterpreted : RuleInvocation
    {
        public override String Name { get { return Action.Name; } }

        public override String Package { get { return Action.Package; } }

        public override String PackagePrefixedName { get { return Action.Package != null ? Action.Package + "::" + Action.Name : Action.Name; } }

        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public readonly IAction Action;

        public RuleInvocationInterpreted(IAction action,
            SequenceVariable subgraph)
            : base(subgraph)
        {
            Action = action;
        }
    }

    /// <summary>
    /// An object representing a rule invocation at compile time (rule representation object does not exist yet).
    /// </summary>
    public class RuleInvocationCompiled : RuleInvocation
    {
        public override String Name { get { return name; } }

        public override String Package { get { return package; } }

        public override String PackagePrefixedName { get { return packagePrefixedName; } }

        private readonly String name;
        private readonly String package;
        private readonly String packagePrefixedName;

        public RuleInvocationCompiled(String name, String package, String packagePrefixedName,
            SequenceVariable subgraph)
            : base(subgraph)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
        }
    }

    /// <summary>
    /// An object representing a sequence invocation (of a defined thus named thus callable sequence).
    /// </summary>
    public abstract class SequenceInvocation : Invocation
    {
        /// <summary>
        /// The subgraph to be switched to for sequence execution
        /// </summary>
        public readonly SequenceVariable Subgraph;

        protected SequenceInvocation(SequenceVariable subgraph)
        {
            Subgraph = subgraph;
        }
    }

    /// <summary>
    /// An object representing a sequence invocation at runtime.
    /// </summary>
    public class SequenceInvocationInterpreted : SequenceInvocation
    {
        public override String Name { get { return SequenceDef.Name; } }

        public override String Package { get { return SequenceDef is SequenceDefinitionCompiled ? ((SequenceDefinitionCompiled)SequenceDef).SeqInfo.Package : null; } }

        public override String PackagePrefixedName { get { return Package != null ? ((SequenceDefinitionCompiled)SequenceDef).SeqInfo.Package + "::" + SequenceDef.Name : SequenceDef.Name; } }

        /// <summary>
        /// The defined sequence to be used
        /// </summary>
        public readonly ISequenceDefinition SequenceDef;

        public SequenceInvocationInterpreted(ISequenceDefinition sequenceDef,
            SequenceVariable subgraph)
            : base(subgraph)
        {
            SequenceDef = sequenceDef;
        }
    }

    /// <summary>
    /// An object representing a sequence invocation at compile time (sequence representation object does not exist yet).
    /// </summary>
    public class SequenceInvocationCompiled : SequenceInvocation
    {
        public override String Name { get { return name; } }

        public override String Package { get { return package; } }

        public override String PackagePrefixedName { get { return packagePrefixedName; } }

        private readonly String name;
        private readonly String package;
        private readonly String packagePrefixedName;

        public SequenceInvocationCompiled(String name, String package, String packagePrefixedName,
            SequenceVariable subgraph)
            : base(subgraph)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
        }
    }

    /// <summary>
    /// An object representing a procedure invocation.
    /// </summary>
    public abstract class ProcedureInvocation : Invocation
    {
        protected ProcedureInvocation()
        {
        }
    }

    /// <summary>
    /// An object representing a procedure invocation at runtime.
    /// </summary>
    public class ProcedureInvocationInterpreted : ProcedureInvocation
    {
        public override String Name { get { return ProcedureDef.Name; } }

        public override String Package { get { return ProcedureDef.Package; } }

        public override String PackagePrefixedName { get { return ProcedureDef.Package != null ? ProcedureDef.Package + "::" + ProcedureDef.Name : ProcedureDef.Name; } }

        /// <summary>
        /// The procedure to be used
        /// </summary>
        public readonly IProcedureDefinition ProcedureDef;

        public ProcedureInvocationInterpreted(IProcedureDefinition procedureDef)
        {
            ProcedureDef = procedureDef;
        }
    }

    /// <summary>
    /// An object representing a procedure invocation at compile time (procedure representation object does not exist yet).
    /// </summary>
    public class ProcedureInvocationCompiled : ProcedureInvocation
    {
        public override String Name { get { return name; } }

        public override String Package { get { return package; } }

        public override String PackagePrefixedName { get { return packagePrefixedName; } }

        private readonly String name;
        private readonly String package;
        private readonly String packagePrefixedName;

        public ProcedureInvocationCompiled(String name, String package, String packagePrefixedName)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
        }
    }

    /// <summary>
    /// An object representing a method procedure invocation (both runtime and compile time).
    /// </summary>
    public class MethodProcedureInvocation : ProcedureInvocation
    {
        public override String Name { get { return name; } }

        public override String Package { get { return null; } }

        public override String PackagePrefixedName { get { return name; } }

        private readonly String name;

        public MethodProcedureInvocation(String name)
        {
            this.name = name;
        }
    }

    /// <summary>
    /// An object representing a function invocation.
    /// </summary>
    public abstract class FunctionInvocation : Invocation
    {
        protected FunctionInvocation()
        {
        }
    }

    /// <summary>
    /// An object representing a function invocation at runtime.
    /// </summary>
    public class FunctionInvocationInterpreted : FunctionInvocation
    {
        public override String Name { get { return FunctionDef.Name; } }

        public override String Package { get { return FunctionDef.Package; } }

        public override String PackagePrefixedName { get { return FunctionDef.Package != null ? FunctionDef.Package + "::" + FunctionDef.Name : FunctionDef.Name; } }

        /// <summary>
        /// The function to be used
        /// </summary>
        public readonly IFunctionDefinition FunctionDef;

        public FunctionInvocationInterpreted(IFunctionDefinition functionDef)
        {
            FunctionDef = functionDef;
        }
    }

    /// <summary>
    /// An object representing a function invocation at compile time (function representation object does not exist yet).
    /// </summary>
    public class FunctionInvocationCompiled : FunctionInvocation
    {
        public override String Name { get { return name; } }

        public override String Package { get { return package; } }

        public override String PackagePrefixedName { get { return packagePrefixedName; } }

        private readonly String name;
        private readonly String package;
        private readonly String packagePrefixedName;

        public FunctionInvocationCompiled(String name, String package, String packagePrefixedName)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
        }
    }

    /// <summary>
    /// An object representing a method function invocation (both runtime and compile time).
    /// </summary>
    public class MethodFunctionInvocation: FunctionInvocation
    {
        public override String Name { get { return name; } }

        public override String Package { get { return null; } }

        public override String PackagePrefixedName { get { return name; } }

        private readonly String name;

        public MethodFunctionInvocation(String name)
        {
            this.name = name;
        }
    }
}
