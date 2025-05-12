/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Reflection;

namespace de.unika.ipd.grGen.libConsoleAndOS
{
    public class TypeCreator
    {
        public static IDoEventsCaller GetDoEventsCaller()
        {
            Type doEventsCallerType = GetSingleImplementationOfInterfaceFromAssembly("libConsoleAndOSWindowsForms.dll", "IDoEventsCaller");
            IDoEventsCaller doEventsCaller = (IDoEventsCaller)Activator.CreateInstance(doEventsCallerType);
            return doEventsCaller;
        }

        public static Type GetSingleImplementationOfInterfaceFromAssembly(string assemblyName, string interfaceName)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string pathPrefix = System.IO.Path.GetDirectoryName(callingAssembly.Location);
            Assembly assembly = Assembly.LoadFrom(pathPrefix + System.IO.Path.DirectorySeparatorChar + assemblyName); // maybe todo: search path instead of same path as calling graphViewerAndSequenceDebugger.dll?

            Type typeImplementingInterface = null;
            try
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic)
                        continue;
                    if(type.GetInterface(interfaceName) != null)
                    {
                        if(typeImplementingInterface != null)
                        {
                            throw new ArgumentException(
                                "The assembly " + assemblyName + " contains more than one " + interfaceName + " implementation!");
                        }
                        typeImplementingInterface = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                String errorMsg = "";
                foreach(Exception ex in e.LoaderExceptions)
                {
                    errorMsg += "- " + ex.Message + Environment.NewLine;
                }
                if(errorMsg.Length == 0)
                    errorMsg = e.Message;
                throw new ArgumentException(errorMsg);
            }
            if(typeImplementingInterface == null)
                throw new ArgumentException("The assembly " + assemblyName + " does not contain an " + interfaceName + " implementation!");

            return typeImplementingInterface;
        }
    }
}
