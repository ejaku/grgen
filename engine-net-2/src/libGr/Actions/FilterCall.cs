/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a filter call (of an action filter or a match class filter).
    /// To be built by the user and used at API level to carry out filter call.
    /// </summary>
    public class FilterCallBase
    {
        public FilterCallBase(String fullName, int argumentCount)
        {
            FullName = fullName;
            Arguments = new object[argumentCount];
        }

        /// <summary>
        /// Name including entities; with package prefix for filter functions as needed.
        /// (A package prefix for auto-generated filters is implementation-only, does not appear here.)
        /// </summary>
        public readonly String FullName;

        /// <summary>
        /// Buffer to store the argument values for the filter function call (or auto-supplied filter call).
        /// </summary>
        public readonly object[] Arguments;
    }

    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// A descprition of a filter of a rule or match class
    /// Only to be used as base class for now. Intermediate code. TODO: remove again (when all filter objects are created in backend).
    /// </summary>
    public class Filter : IFilter
    {
        public Filter(String name, String package, String packagePrefixedName)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
        }

        public String Name
        {
            get { return name; }
        }

        public String Package
        {
            get { return package; }
        }

        public String PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public virtual bool Matches(String name)
        {
            if(PackagePrefixedName == name)
                return true;
            else
                return false;
        }

        public readonly String name;

        public readonly String package;

        public readonly String packagePrefixedName;
    }

    /// <summary>
    /// A description of an auto-supplied filter of a rule or match class.
    /// For now only used for dummy objects to get more consistent/uniform code (TODO: add to filters reported).
    /// </summary>
    public class FilterAutoSupplied : Filter, IFilterAutoSupplied
    {
        public FilterAutoSupplied(String name, String package, String packagePrefixedName, GrGenType[] inputs, String[] inputNames)
            : base(name, package, packagePrefixedName)
        {
            this.inputs = inputs;
            this.inputNames = inputNames;
        }

        /// <summary>
        /// An array of GrGen types corresponding to filter parameters.
        /// </summary>
        public GrGenType[] Inputs
        {
            get { return inputs; }
        }

        /// <summary>
        /// An array of the names corresponding to filter parameters.
        /// </summary>
        public String[] InputNames
        {
            get { return inputNames; }
        }


        /// <summary>
        /// An array of GrGen types corresponding to filter parameters.
        /// </summary>
        public readonly GrGenType[] inputs;

        /// <summary>
        /// Names of the filter parameter elements
        /// </summary>
        public readonly string[] inputNames;
    }
}
