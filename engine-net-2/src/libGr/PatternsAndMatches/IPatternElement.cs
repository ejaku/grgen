/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An element of a rule pattern.
    /// </summary>
    public interface IPatternElement
    {
        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The pure name of the pattern element as specified in the .grg without any prefixes.
        /// </summary>
        String UnprefixedName { get; }

        /// <summary>
        /// The pattern where this element is contained the first time / gets matched (null if rule parameter).
        /// </summary>
        IPatternGraph PointOfDefinition { get; }

        /// <summary>
        /// Iff true the element is only defined in its PointOfDefinition pattern,
        /// it gets matched in another, nested or called pattern which yields it to the containing pattern.
        /// </summary>
        bool DefToBeYieldedTo { get; }

        /// <summary>
        /// The annotations of the pattern element.
        /// </summary>
        Annotations Annotations { get; }

        /// <summary>
        /// The base GrGenType of the pattern element (matching may be constrained further, this is only the base type)
        /// </summary>
        GrGenType Type { get; }
    }

    /// <summary>
    /// A pattern node of a rule pattern.
    /// </summary>
    public interface IPatternNode : IPatternElement
    {
        /// <summary>
        /// The base NodeType of the pattern node (matching may be constrained further, this is only the base type)
        /// </summary>
        new NodeType Type { get; }
    }

    /// <summary>
    /// A pattern edge of a rule pattern.
    /// </summary>
    public interface IPatternEdge : IPatternElement
    {
        /// <summary>
        /// The base EdgeType of the pattern edge (matching may be constrained further, this is only the base type)
        /// </summary>
        new EdgeType Type { get; }
    }

    /// <summary>
    /// A pattern variable of a rule pattern.
    /// </summary>
    public interface IPatternVariable : IPatternElement
    {
        /// <summary>
        /// The base VarType of the pattern variable (matching may be constrained further, this is only the base type)
        /// </summary>
        new VarType Type { get; }
    }
}

