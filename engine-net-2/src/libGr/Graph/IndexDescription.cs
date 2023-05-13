/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The description of a single index, base for all kinds of index descriptions.
    /// (You must typecheck and cast to the concrete description type for more information).
    /// </summary>
    public abstract class IndexDescription
    {
        /// <summary>
        /// The name the index was declared with
        /// </summary>
        public readonly String Name;

        protected IndexDescription(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// The description of a single attribute index.
    /// </summary>
    public class AttributeIndexDescription : IndexDescription
    {
        /// <summary>
        /// The node or edge type the index is defined for.
        /// (May be a subtype of the type the attribute was defined for first.)
        /// </summary>
        public readonly GrGenType GraphElementType;

        /// <summary>
        /// The attribute type the index is declared on.
        /// </summary>
        public readonly AttributeType AttributeType;

        public AttributeIndexDescription(string name,
            GrGenType graphElementType, AttributeType attributeType)
            : base(name)
        {
            GraphElementType = graphElementType;
            AttributeType = attributeType;
        }
    }

    public enum IncidenceDirection
    {
        OUTGOING,
        INCOMING,
        INCIDENT
    }

    /// <summary>
    /// The description of a single incidence count index.
    /// </summary>
    public class IncidenceCountIndexDescription : IndexDescription
    {
        /// <summary>
        /// The direction of incidence followed.
        /// </summary>
        public readonly IncidenceDirection Direction;

        /// <summary>
        /// The type of the start node that is taken into account for the incidence count.
        /// </summary>
        public readonly NodeType StartNodeType;

        /// <summary>
        /// The type of the incident edge that is taken into account for the incidence count.
        /// </summary>
        public readonly EdgeType IncidentEdgeType;

        /// <summary>
        /// The type of the adjacent node that is taken into account for the incidence count.
        /// </summary>
        public readonly NodeType AdjacentNodeType;

        public IncidenceCountIndexDescription(string name, IncidenceDirection direction,
            NodeType startNodeType, EdgeType incidentEdgeType, NodeType adjacentNodeType)
            : base(name)
        {
            Direction = direction;
            StartNodeType = startNodeType;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }
    }
}
