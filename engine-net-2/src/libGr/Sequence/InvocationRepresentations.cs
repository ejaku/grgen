/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An interface representing a rule or sequence or procedure or function or filter invocation.
    /// </summary>
    public interface Invocation : INamed
    {
        /// <summary>
        /// The name of the rule or sequence or procedure or function.
        /// </summary>
        new string Name { get; }

        /// <summary>
        /// null if this is a call of a global rule/sequence/procedure/function, otherwise the package the call target is contained in.
        /// </summary>
        new string Package { get; }

        /// <summary>
        /// The name of the rule or sequence or procedure or function, prefixed by the package it is contained in (separated by a double colon), if it is contained in a package.
        /// </summary>
        new string PackagePrefixedName { get; }
    }

    /// <summary>
    /// An interface representing a rule invocation.
    /// </summary>
    public interface RuleInvocation : Invocation
    {
        /// <summary>
        /// The subgraph to be switched to for rule execution
        /// </summary>
        SequenceVariable Subgraph { get; }
    }

    /// <summary>
    /// An interface representing a sequence invocation (of a defined thus named thus callable sequence).
    /// </summary>
    public interface SequenceInvocation : Invocation
    {
        /// <summary>
        /// The subgraph to be switched to for sequence execution
        /// </summary>
        SequenceVariable Subgraph { get; }
    }

    /// <summary>
    /// An interface representing a procedure invocation.
    /// </summary>
    public interface ProcedureInvocation : Invocation
    {
        /// <summary>
        /// Tells whether the called procedure is external.
        /// </summary>
        bool IsExternal { get; }
    }

    /// <summary>
    /// An interface representing a method procedure invocation (both runtime and compile time).
    /// </summary>
    public interface MethodProcedureInvocation : ProcedureInvocation
    {
    }

    /// <summary>
    /// An interface representing a function invocation.
    /// </summary>
    public interface FunctionInvocation : Invocation
    {
        /// <summary>
        /// Tells whether the called function is external.
        /// </summary>
        bool IsExternal { get; }
    }

    /// <summary>
    /// An interface representing a method function invocation (both runtime and compile time).
    /// </summary>
    public interface MethodFunctionInvocation: FunctionInvocation
    {
    }

    /// <summary>
    /// An interface representing a filter invocation.
    /// </summary>
    public interface FilterInvocation : Invocation
    {
    }
}
