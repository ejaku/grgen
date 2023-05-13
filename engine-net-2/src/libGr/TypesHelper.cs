/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The TypesHelper in this file contains code to handle values of diverse supported types at runtime.
    /// </summary>
    public static partial class TypesHelper
    {
        public static bool IsDefaultValue(object value)
        {
            if(value == null)
                return true;

            if(value is SByte)
                return (SByte)value == 0;
            else if(value is Int16)
                return (Int16)value == 0;
            else if(value is Int32)
                return (Int32)value == 0;
            else if(value is Int64)
                return (Int64)value == 0L;
            else if(value is Boolean)
                return (Boolean)value == false;
            else if(value is Single)
                return (Single)value == 0.0f;
            else if(value is Double)
                return (Double)value == 0.0;
            else if(value is String)
                return (String)value == "";
            else if(value is Enum)
                return Convert.ToInt32((Enum)value) == 0;

            return false; // object or node/edge or container type that is not null
        }

        public static object DefaultValue(String typeName, IGraphModel model)
        {
            switch(typeName)
            {
                case "SByte": return 0;
                case "Int16": return 0;
                case "Int32": return 0;
                case "Int64": return 0L;
                case "Boolean": return false;
                case "Single": return 0.0f;
                case "Double": return 0.0;
                case "String": return "";
            }

            switch(typeName)
            {
                case "byte": return 0;
                case "short": return 0;
                case "int": return 0;
                case "long": return 0L;
                case "bool": return false;
                case "float": return 0.0f;
                case "double": return 0.0;
                case "string": return "";
                case "object": return null;
            }

            if(typeName == "boolean")
                return false;

            if(typeName.StartsWith("ENUM_"))
                typeName = typeName.Substring(5);
            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == typeName)
                    return Enum.Parse(enumAttrType.EnumType, Enum.GetName(enumAttrType.EnumType, 0));
            }

            return null; // object or graph or node type or edge type
        }

        public static String DefaultValueString(String typeName, IGraphModel model)
        {
            switch(typeName)
            {
                case "SByte": return "0";
                case "Int16": return "0";
                case "Int32": return "0";
                case "Int64": return "0L";
                case "Boolean": return "false";
                case "Single": return "0.0f";
                case "Double": return "0.0";
                case "String": return "\"\"";
            }

            switch(typeName)
            {
                case "byte": return "0";
                case "short": return "0";
                case "int": return "0";
                case "long": return "0L";
                case "bool": return "false";
                case "float": return "0.0f";
                case "double": return "0.0";
                case "string": return "\"\"";
                case "object": return "null";
            }

            if(typeName == "boolean")
                return "false";

            if(typeName.StartsWith("GRGEN_MODEL."))
                typeName = typeName.Substring(12);
            if(typeName.Contains(".ENUM_"))
                typeName = typeName.Substring(0, typeName.IndexOf(".ENUM_")) + "::" + typeName.Substring(typeName.IndexOf(".ENUM_")+6);
            if(typeName.StartsWith("ENUM_"))
                typeName = typeName.Substring(5);
            foreach(EnumAttributeType enumAttrType in model.EnumAttributeTypes)
            {
                if(enumAttrType.PackagePrefixedName == typeName)
                    return "(GRGEN_MODEL." + (enumAttrType.Package!=null ? enumAttrType.Package+"." : "") + "ENUM_" + enumAttrType.Name + ")0";
            }

            return "null"; // object or graph or node type or edge type
        }

        /// <summary>
        /// Returns a shallow clone of either a graph or a match or a class object or a transient class object or a container
        /// </summary>
        /// <param name="toBeCloned">The graph or match or (transient) class object or container to be cloned</param>
        /// <param name="graph">The graph to fetch the names of new objects from.</param>
        /// <returns>The clone</returns>
        public static object Clone(object toBeCloned, IGraph graph)
        {
            if(toBeCloned is IGraph)
                return GraphHelper.Copy((IGraph)toBeCloned);
            else if(toBeCloned is IMatch)
                return ((IMatch)toBeCloned).Clone();
            else if(toBeCloned is IObject)
                return ((IObject)toBeCloned).Clone(graph);
            else if(toBeCloned is ITransientObject)
                return ((ITransientObject)toBeCloned).Clone();
            else
                return ContainerHelper.Clone(toBeCloned);
        }

        /// <summary>
        /// Returns a deep copy of either a graph or a match or a class object or a transient class object or a container
        /// </summary>
        /// <param name="toBeCopied">The graph or match or (transient) class object or container to be copied</param>
        /// <param name="graph">The graph to fetch the names of new objects from.</param>
        /// <returns>The copy</returns>
        public static object Copy(object toBeCopied, IGraph graph)
        {
            if(toBeCopied is IGraph)
                return GraphHelper.Copy((IGraph)toBeCopied);
            else if(toBeCopied is IMatch)
                return ((IMatch)toBeCopied).Clone();
            else if(toBeCopied is IObject)
                return ((IObject)toBeCopied).Copy(graph, new Dictionary<object, object>());
            else if(toBeCopied is ITransientObject)
                return ((ITransientObject)toBeCopied).Copy(graph, new Dictionary<object, object>());
            else
                return ContainerHelper.Copy(toBeCopied, graph, new Dictionary<object, object>());
        }
    }
}
