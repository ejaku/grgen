/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public class TypesHelper
    {
        public static String TypeName(GrGenType type)
        {
            if (type is VarType)
            {
                Type typeOfVar = ((VarType)type).Type;
                if (typeOfVar.IsGenericType)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(typeOfVar.FullName.Substring(0, typeOfVar.FullName.IndexOf('`')));
                    sb.Append('<');
                    bool first = true;
                    foreach (Type typeArg in typeOfVar.GetGenericArguments())
                    {
                        if (first) first = false;
                        else sb.Append(", ");
                        sb.Append(typeArg.FullName);
                    }
                    sb.Append('>');
                    return sb.ToString();
                }

                return typeOfVar.FullName;
            }
            else
            {
                if (type.Name == "Node" || type.Name == "Edge" || type.Name == "UEdge")
                    return "GRGEN_LIBGR.I" + type.Name;
                else
                    return "GRGEN_MODEL.I" + type.Name;
            }
        }
    }
}
