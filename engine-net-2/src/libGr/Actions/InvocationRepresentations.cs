/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a rule or sequence or procedure or function invocation.
    /// </summary>
    public class Invocation
    {
        /// <summary>
        /// The name of the rule or sequence or procedure or function.
        /// Used for compilation, where the rule or sequence or procedure or function representation objects do not exist yet.
        /// </summary>
        public String Name;

        /// <summary>
        /// null if this is a call of a global rule/sequence/procedure/function, otherwise the package the call target is contained in.
        /// Used for compilation, where the rule or sequence or procedure or function representation objects do not exist yet.
        /// </summary>
        public String Package;

        /// <summary>
        /// The name of the rule or sequence or procedure or function, prefixed by the package it is contained in (separated by a double colon), if it is contained in a package.
        /// Used for compilation, where the rule or sequence or procedure or function representation objects do not exist yet.
        /// </summary>
        public String PackagePrefixedName;

        /// <summary>
        /// Instantiates a new Invocation object
        /// </summary>
        public Invocation()
        {
            Name = "<Unknown>";
        }
    }

    /// <summary>
    /// An object representing a rule invocation.
    /// </summary>
    public class RuleInvocation : Invocation
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public IAction Action;

        /// <summary>
        /// The subgraph to be switched to for rule execution
        /// </summary>
        public SequenceVariable Subgraph;

        /// <summary>
        /// Instantiates a new RuleInvocation object
        /// </summary>
        /// <param name="action">The IAction instance to be used</param>
        public RuleInvocation(IAction action,
            SequenceVariable subgraph)
            : base()
        {
            Action = action;
            if(action != null)
            {
                Name = action.Name;
                Package = action.Package;
                PackagePrefixedName = Package != null ? Package + "::" + Name : Name;
            }
            Subgraph = subgraph;
        }
    }

    /// <summary>
    /// An object representing a sequence invocation.
    /// </summary>
    public class SequenceInvocation : Invocation
    {
        /// <summary>
        /// The defined sequence to be used
        /// </summary>
        public ISequenceDefinition SequenceDef;

        /// <summary>
        /// The subgraph to be switched to for sequence execution
        /// </summary>
        public SequenceVariable Subgraph;

        /// <summary>
        /// Instantiates a new SequenceInvocation object
        /// </summary>
        /// <param name="sequenceDef">The defined sequence to be used</param>
        public SequenceInvocation(ISequenceDefinition sequenceDef,
            SequenceVariable subgraph)
            : base()
        {
            SequenceDef = sequenceDef;
            if(sequenceDef != null)
            {
                Name = sequenceDef.Name;
                Package = sequenceDef is SequenceDefinitionCompiled ? ((SequenceDefinitionCompiled)sequenceDef).SeqInfo.Package : null;
                PackagePrefixedName = Package != null ? Package + "::" + Name : Name;
            }
            Subgraph = subgraph;
        }
    }

    /// <summary>
    /// An object representing a procedure.
    /// </summary>
    public class ProcedureInvocation : Invocation
    {
        /// <summary>
        /// The procedure to be used
        /// </summary>
        public IProcedureDefinition ProcedureDef;

        /// <summary>
        /// Instantiates a new ProcedureInvocation object
        /// </summary>
        /// <param name="procedureDef">The defined procedure to be used</param>
        public ProcedureInvocation(IProcedureDefinition procedureDef)
            : base()
        {
            ProcedureDef = procedureDef;
            if(procedureDef != null)
            {
                Name = procedureDef.Name;
                Package = procedureDef.Package;
                PackagePrefixedName = Package != null ? Package + "::" + Name : Name;
            }
        }
    }

    /// <summary>
    /// An object representing a function invocation.
    /// </summary>
    public class FunctionInvocation : Invocation
    {
        /// <summary>
        /// The function to be used
        /// </summary>
        public IFunctionDefinition FunctionDef;

        /// <summary>
        /// The type returned
        /// </summary>
        public string ReturnType;

        /// <summary>
        /// Instantiates a new FunctionInvocation object
        /// </summary>
        /// <param name="functionDef">The defined function to be used</param>
        public FunctionInvocation(IFunctionDefinition functionDef)
            : base()
        {
            FunctionDef = functionDef;
            if(functionDef != null)
            {
                Name = functionDef.Name;
                Package = functionDef.Package;
                PackagePrefixedName = Package != null ? Package + "::" + Name : Name;
            }
        }
    }
}
