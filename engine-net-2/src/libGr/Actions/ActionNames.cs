/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// For sequence handling, contains the names of the different kinds of actions (plus a bit further information) 
    /// </summary>
    public class ActionNames
    {
        public ActionNames(ActionsTypeInformation ati)
        {
            // extract rule names from domain of rule names to input types map
            ruleNames = new String[ati.rulesToInputTypes.Count];
            int i = 0;
            foreach(KeyValuePair<String, List<String>> ruleToInputTypes in ati.rulesToInputTypes)
            {
                ruleNames[i] = ruleToInputTypes.Key;
                ++i;
            }
            // extract match class names from domain of match class names to filter functions map
            matchClassNames = new String[ati.matchClassesToFilters.Count];
            i = 0;
            foreach(KeyValuePair<String, List<IFilter>> matchClassToFilters in ati.matchClassesToFilters)
            {
                matchClassNames[i] = matchClassToFilters.Key;
                ++i;
            }
            // extract sequence names from domain of sequence names to input types map
            sequenceNames = new String[ati.sequencesToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> sequenceToInputTypes in ati.sequencesToInputTypes)
            {
                sequenceNames[i] = sequenceToInputTypes.Key;
                ++i;
            }
            // extract procedure names from domain of procedure names to input types map
            procedureNames = new String[ati.proceduresToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> procedureToInputTypes in ati.proceduresToInputTypes)
            {
                procedureNames[i] = procedureToInputTypes.Key;
                ++i;
            }
            // extract function names from domain of function names to input types map
            functionNames = new String[ati.functionsToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> functionToInputTypes in ati.functionsToInputTypes)
            {
                functionNames[i] = functionToInputTypes.Key;
                ++i;
            }
            // extract function output types from range of function names to output types map
            functionOutputTypes = new String[ati.functionsToOutputType.Count];
            i = 0;
            foreach(KeyValuePair<String, String> functionToOutputType in ati.functionsToOutputType)
            {
                functionOutputTypes[i] = functionToOutputType.Value;
                ++i;
            }
            // extract filter function names from domain of filter functions to input types
            filterFunctionNames = new String[ati.filterFunctionsToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> filterFunctionToInputType in ati.filterFunctionsToInputTypes)
            {
                filterFunctionNames[i] = filterFunctionToInputType.Key;
                ++i;
            }

            // assign rules to filters and match classes to filters
            rulesToFilters = ati.rulesToFilters;
            matchClassesToFilters = ati.matchClassesToFilters;
            // assign is external information
            proceduresToIsExternal = ati.proceduresToIsExternal;
            functionsToIsExternal = ati.functionsToIsExternal;
        }

        public bool RuleContainsFilter(string ruleName, string filterName)
        {
            foreach(IFilter filter in rulesToFilters[ruleName])
            {
                if(filter.PackagePrefixedName == filterName)
                    return true;
            }
            return false;
        }

        public bool MatchClassContainsFilter(string matchClassName, string filterName)
        {
            foreach(IFilter filter in matchClassesToFilters[matchClassName])
            {
                if(filter.PackagePrefixedName == filterName)
                    return true;
            }
            return false;
        }

        public bool IsExternal(string functionOrProcedureName)
        {
            if(functionsToIsExternal.ContainsKey(functionOrProcedureName))
                return functionsToIsExternal[functionOrProcedureName];
            if(proceduresToIsExternal.ContainsKey(functionOrProcedureName))
                return proceduresToIsExternal[functionOrProcedureName];
            return false;
        }

        public bool ContainsRule(string ruleName)
        {
            return Array.IndexOf(ruleNames, ruleName) != -1;
        }

        public bool ContainsMatchClass(string matchClassName)
        {
            return Array.IndexOf(matchClassNames, matchClassName) != -1;
        }

        public bool ContainsSequence(string sequenceName)
        {
            return Array.IndexOf(sequenceNames, sequenceName) != -1;
        }

        public bool ContainsProcedure(string procedureName)
        {
            return Array.IndexOf(procedureNames, procedureName) != -1;
        }

        public bool ContainsFunction(string functionName)
        {
            return Array.IndexOf(functionNames, functionName) != -1;
        }

        public bool ContainsFilterFunction(string filterFunctionName)
        {
            return Array.IndexOf(filterFunctionNames, filterFunctionName) != -1;
        }

        // the package prefixed names of the entities available
        public readonly String[] ruleNames;
        public readonly String[] matchClassNames;
        public readonly String[] sequenceNames;
        public readonly String[] procedureNames;
        public readonly String[] functionNames;
        public readonly String[] functionOutputTypes;
        public readonly String[] filterFunctionNames;

        public readonly Dictionary<String, List<IFilter>> rulesToFilters;
        public readonly Dictionary<String, List<IFilter>> matchClassesToFilters;
        public readonly Dictionary<String, bool> proceduresToIsExternal;
        public readonly Dictionary<String, bool> functionsToIsExternal;
    }
}
