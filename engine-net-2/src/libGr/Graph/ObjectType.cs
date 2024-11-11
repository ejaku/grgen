/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
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
        /// In case of !ObjectUniquenessIsEnsured, -1 is expected.
        /// </summary>
        /// <returns>The created object.</returns>
        public abstract IObject CreateObject(IGraph graph, long uniqueId);

        /// <summary>
        /// Creates an object according to this type.
        /// Requires the graph to fetch the unique id / the name,
        /// in case the name is given and valid it is taken, or an exception is thrown in case the name/id is already in use or id fetching has passed its value.
        /// The name is expected in format "%" + uniqueId (if null, a name is fetched).
        /// In case of !ObjectUniquenessIsEnsured, null is expected.
        /// </summary>
        /// <returns>The created object.</returns>
        public IObject CreateObject(IGraph graph, String name)
        {
            if(name != null)
                return CreateObject(graph, GetUniqueIdFromName(name));
            else
                return CreateObject(graph, -1);
        }

        public static long GetUniqueIdFromName(string name)
        {
            char prefix = name[0];
            if(prefix != '%')
                throw new Exception("Name must be in format % + uniqueId (prefix is not the percent character)");

            long uniqueId;
            if(long.TryParse(name.Substring(1), out uniqueId))
            {
                return uniqueId;
            }
            else
                throw new Exception("Name must be in format % + uniqueId (uniqueId is not a long number)");
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

    public class ObjectNamerAndIndexer
    {
        // note that the name is strongly based on the uniqueId, and that objects only come with a uniqueId if Model.ObjectUniquenessIsEnsured
        public ObjectNamerAndIndexer(bool createObjectToNameIndex, bool createNameToObjectIndex, bool objectsContainName)
        {
            Debug.Assert(createObjectToNameIndex || createNameToObjectIndex); // class would be pointless without one of the indices
            if(createNameToObjectIndex)
                nameToObject = new Dictionary<string, IObject>();
            if(createObjectToNameIndex)
                objectToName = new Dictionary<IObject, string>();
            this.objectsContainName = objectsContainName;
        }

        // gets the name stored in the objectToName map, or creates a new one (in case of objectsContainName, it just gets the available name from the object), storing it in the available maps
        public string GetOrAssignName(IObject obj)
        {
            string name = null;
            if(objectToName != null)
            {
                if(objectToName.ContainsKey(obj))
                    return objectToName[obj];

                name = objectsContainName ? obj.GetObjectName() : String.Format("%{0,00000000:X}", idSource++);
                objectToName.Add(obj, name);
            }

            if(nameToObject != null)
            {
                if(name == null)
                    name = objectsContainName ? obj.GetObjectName() : String.Format("%{0,00000000:X}", idSource++);

                if(nameToObject.ContainsKey(name))
                {
                    if(nameToObject[name] != obj)
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Object name/id \"{0}\" already in use!", name);
                        throw new Exception("Class object name/id already in use");
                    }
                }
                else
                   nameToObject.Add(name, obj);
            }

            return name;
        }

        // returns the name found in the objectToName-mapping
        // in case the objectToName-mapping was not requested, but the nameToObject-mapping, and alsoSearchReverse is requested, this operation is O(n) instead of O(1)
        public string GetName(IObject obj, bool alsoSearchReverse)
        {
            if(objectToName != null)
            {
                if(objectToName.ContainsKey(obj))
                    return objectToName[obj];
            }
            else
            {
                if(nameToObject != null && alsoSearchReverse)
                {
                    foreach(KeyValuePair<string, IObject> kvp in nameToObject)
                    {
                        if(kvp.Value == obj)
                            return kvp.Key;
                    }
                }
            }

            return null;
        }

        // convenience mapper to GetName(obj, false)
        public string GetName(IObject obj)
        {
            return GetName(obj, false);
        }

        // assigns a name to the object in the internal mappings (usage is with a newly created object, assumes a name in the object is the same in case the model supports object names)
        public void AssignName(IObject obj, string name)
        {
            if(objectsContainName && name != obj.GetObjectName())
            {
                ConsoleUI.errorOutWriter.WriteLine("Requested name \"{0}\" differes from the name \"{1}\" already stored in the object!", name, obj.GetObjectName());
                throw new Exception("Requested name differes from the name already stored in the object");
            }

            if(objectToName != null)
            {
                if(objectToName.ContainsKey(obj))
                {
                    if(objectToName[obj] != name)
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Object \"{0}\" has already another name \"{1}\"!", name, objectToName[obj]);
                        throw new Exception("Object has already another name");
                    }
                }
                else
                    objectToName.Add(obj, name);
            }

            if(nameToObject != null)
            {
                if(nameToObject.ContainsKey(name))
                {
                    if(nameToObject[name] != obj)
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Object name/id \"{0}\" already in use!", obj.GetObjectName());
                        throw new Exception("Class object name/id already in use");
                    }
                }
                else
                    nameToObject.Add(name, obj);
            }

            idSource = ObjectType.GetUniqueIdFromName(name) + 1; // maybe TODO: change into a creation function, an idSource here and one in the global variables (both getting adapted) is a bit ugly
        }

        // returns the object found in the nameToObject-mapping
        // in case the nameToObject-mapping was not requested, but the objectToName-mapping, and alsoSearchReverse is requested, this operation is O(n) instead of O(1)
        public IObject GetObject(string name, bool alsoSearchReverse)
        {
            if(nameToObject != null)
            {
                if(nameToObject.ContainsKey(name))
                    return nameToObject[name];
            }
            else
            {
                if(objectToName != null && alsoSearchReverse)
                {
                    foreach(KeyValuePair<IObject, string> kvp in objectToName)
                    {
                        if(kvp.Value == name)
                            return kvp.Key;
                    }
                }
            }
            return null;
        }

        // convenience mapper to GetObject(name, false)
        public IObject GetObject(string name)
        {
            return GetObject(name, false);
        }

        public void Clear()
        {
            if(nameToObject != null)
                nameToObject.Clear(); // only nameToObject was cleared in old code, but a general clear fits better to this class, its just debatable whether a host graph clear should clear the entire framework
            if(objectToName != null)
                objectToName.Clear();
            idSource = 0;
        }

        long idSource = 0;
        Dictionary<string, IObject> nameToObject = null;
        Dictionary<IObject, string> objectToName = null;
        bool objectsContainName;
    }
}
