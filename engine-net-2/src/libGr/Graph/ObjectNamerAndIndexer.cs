/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
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

    // TODO: reconsider notion namer, as it assigns unique ids not names
    public class TransientObjectNamerAndIndexer
    {
        private readonly Dictionary<ITransientObject, long> transientObjectToUniqueId = new Dictionary<ITransientObject, long>();
        private readonly Dictionary<long, ITransientObject> uniqueIdToTransientObject = new Dictionary<long, ITransientObject>();

        // Source for assigning unique ids to internal transient class objects.
        private long transientObjectUniqueIdSource = 0;

        private long FetchTransientObjectUniqueId()
        {
            return transientObjectUniqueIdSource++;
        }

        public long GetUniqueId(ITransientObject transientObject)
        {
            if(transientObject == null)
                return -1;

            if(!transientObjectToUniqueId.ContainsKey(transientObject))
            {
                long uniqueId = FetchTransientObjectUniqueId();
                transientObjectToUniqueId[transientObject] = uniqueId;
                uniqueIdToTransientObject[uniqueId] = transientObject;
            }
            return transientObjectToUniqueId[transientObject];
        }

        public ITransientObject GetTransientObject(long uniqueId)
        {
            ITransientObject transientObject;
            uniqueIdToTransientObject.TryGetValue(uniqueId, out transientObject);
            return transientObject;
        }
    }
}
