/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A representation of a GrGen object type, i.e. class of internal non-node/edge values.
    /// </summary>
    public abstract class ObjectType : BaseObjectType
    {
        /// <summary>
        /// Constructs an ObjectType (non-graph-element internal class) instance with the given type ID.
        /// </summary>
        /// <param name="typeID">The unique type ID.</param>
        protected ObjectType(int typeID)
            : base(typeID)
        {
        }

        /// <summary>
        /// This ObjectType describes classes whose real .NET interface type is named as returned (fully qualified).
        /// </summary>
        public abstract String ObjectInterfaceName { get; }

        public override String BaseObjectInterfaceName
        {
            get { return ObjectInterfaceName; }
        }

        /// <summary>
        /// This ObjectType describes classes whose real .NET class type is named as returned (fully qualified).
        /// It might be null in case this type IsAbstract.
        /// </summary>
        public abstract String ObjectClassName { get; }

        public override String BaseObjectClassName
        {
            get { return ObjectClassName; }
        }

        /// <summary>
        /// Creates an object according to this type.
        /// Requires the graph to fetch the unique id / the name,
        /// in case the unique id is given (!= -1) it is taken, or an exception is thrown in case the id is already in use or id fetching has passed its value.
        /// </summary>
        /// <returns>The created object.</returns>
        public abstract IObject CreateObject(IGraph graph, long uniqueId);

        /// <summary>
        /// Creates an object according to this type.
        /// Requires the graph to fetch the unique id / the name,
        /// in case the name is given and valid it is taken, or an exception is thrown in case the name/id is already in use or id fetching has passed its value.
        /// The name is expected in format "%" + uniqueId (if null, a name is fetched).
        /// </summary>
        /// <returns>The created object.</returns>
        public IObject CreateObject(IGraph graph, String name)
        {
            if(name != null)
            {
                char prefix = name[0];
                if(prefix != '%')
                    throw new Exception("Name must be in format % + uniqueId (prefix is not the percent character)");

                long uniqueId;
                if(long.TryParse(name.Substring(1), out uniqueId))
                {
                    return CreateObject(graph, uniqueId);
                }
                else
                    throw new Exception("Name must be in format % + uniqueId (uniqueId is not a long number)");
            }
            else
                return CreateObject(graph, -1);
        }

        /// <summary>
        /// Creates an object according to this type.
        /// </summary>
        /// <returns>The created (base) object.</returns>
        public override IBaseObject CreateBaseObject()
        {
            throw new Exception("use the ObjectType.CreateObject method");
        }

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public ObjectType[] subOrSameTypes;
        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public ObjectType[] directSubTypes;
        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public ObjectType[] superOrSameTypes;
        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public ObjectType[] directSuperTypes;

        /// <summary>
        /// Array containing this type first and following all sub types
        /// </summary>
        public new ObjectType[] SubOrSameTypes
        {
            [DebuggerStepThrough]
            get { return subOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct sub types of this type.
        /// </summary>
        public new ObjectType[] DirectSubTypes
        {
            [DebuggerStepThrough]
            get { return directSubTypes; }
        }

        /// <summary>
        /// Array containing this type first and following all super types
        /// </summary>
        public new ObjectType[] SuperOrSameTypes
        {
            [DebuggerStepThrough]
            get { return superOrSameTypes; }
        }

        /// <summary>
        /// Array containing all direct super types of this type.
        /// </summary>
        public new ObjectType[] DirectSuperTypes
        {
            [DebuggerStepThrough]
            get { return directSuperTypes; }
        }

        /// <summary>
        /// Tells whether the given type is the same or a subtype of this type
        /// </summary>
        public override abstract bool IsMyType(int typeID);

        /// <summary>
        /// Tells whether this type is the same or a subtype of the given type
        /// </summary>
        public override abstract bool IsA(int typeID);

        /// <summary>
        /// The annotations of the class type
        /// </summary>
        public override abstract Annotations Annotations { get; }
    }
}
