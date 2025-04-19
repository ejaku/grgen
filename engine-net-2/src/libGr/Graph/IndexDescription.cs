/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

        /// <summary>
        /// The base type of the graph elements stored in the index
        /// </summary>
        public abstract GrGenType IndexedType { get; }

        /// <summary>
        /// The type used in accessing the index (i.e. the type of the attribute in case of an attribute index, or int in case of an incidence count index)
        /// </summary>
        public abstract AttributeType IndexAccessType { get; }
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

        public override GrGenType IndexedType
        {
            get { return GraphElementType; }
        }

        public override AttributeType IndexAccessType
        {
            get { return AttributeType; }
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

        private static readonly AttributeType IntegerAttributeType = new AttributeType(null, null, AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));

        public IncidenceCountIndexDescription(string name, IncidenceDirection direction,
            NodeType startNodeType, EdgeType incidentEdgeType, NodeType adjacentNodeType)
            : base(name)
        {
            Direction = direction;
            StartNodeType = startNodeType;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override GrGenType IndexedType
        {
            get { return StartNodeType; }
        }

        public override AttributeType IndexAccessType
        {
            get { return IntegerAttributeType; }
        }
    }
}
