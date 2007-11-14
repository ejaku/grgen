using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A GrGen graph element
    /// </summary>
    public interface IGraphElement
    {
        /// <summary>
        /// Returns the IType of the graph element
        /// </summary>
        IType Type { get; }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type
        /// </summary>
        bool InstanceOf(IType type);

        /// <summary>
        /// Returns the graph element attribute with the given attribute name.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        object GetAttribute(String attrName);

        /// <summary>
        /// Sets the graph element attribute with the given attribute name to the given value.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        void SetAttribute(String attrName, object value);
    }

    /// <summary>
    /// A GrGen node
    /// </summary>
    public interface INode : IGraphElement
    {
        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges
        /// </summary>
        IEnumerable<IEdge> Outgoing { get; }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with the same type or a subtype of the given type
        /// </summary>
        IEnumerable<IEdge> GetCompatibleOutgoing(IType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with exactly the given type
        /// </summary>
        IEnumerable<IEdge> GetExactOutgoing(IType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges
        /// </summary>
        IEnumerable<IEdge> Incoming { get; }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with the same type or a subtype of the given type
        /// </summary>
        IEnumerable<IEdge> GetCompatibleIncoming(IType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with exactly the given type
        /// </summary>
        IEnumerable<IEdge> GetExactIncoming(IType edgeType);
    }

    /// <summary>
    /// A GrGen edge
    /// </summary>
    public interface IEdge : IGraphElement
    {
        /// <summary>
        /// The source node of the edge.
        /// </summary>
        INode Source { get; }

        /// <summary>
        /// The target node of the edge.
        /// </summary>
        INode Target { get; }
    }
}