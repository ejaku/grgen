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
    /// For sequence handling, contains type information (plus a bit further information) about the different kinds of actions
    /// </summary>
    public class ActionsTypeInformation
    {
        public ActionsTypeInformation()
        {
            rulesToFilters = new Dictionary<String, List<IFilter>>();
            matchClassesToFilters = new Dictionary<String, List<IFilter>>();

            filterFunctionsToInputTypes = new Dictionary<String, List<String>>();

            rulesToInputTypes = new Dictionary<String, List<String>>();
            rulesToOutputTypes = new Dictionary<String, List<String>>();

            rulesToTopLevelEntities = new Dictionary<String, List<String>>();
            rulesToTopLevelEntityTypes = new Dictionary<String, List<String>>();

            matchClasses = new Dictionary<String, MatchClassInfo>();
            rulesToImplementedMatchClasses = new Dictionary<String, List<MatchClassInfo>>();

            sequencesToInputTypes = new Dictionary<String, List<String>>();
            sequencesToOutputTypes = new Dictionary<String, List<String>>();

            proceduresToInputTypes = new Dictionary<String, List<String>>();
            proceduresToOutputTypes = new Dictionary<String, List<String>>();
            proceduresToIsExternal = new Dictionary<String, bool>();

            functionsToInputTypes = new Dictionary<String, List<String>>();
            functionsToOutputType = new Dictionary<String, String>();
            functionsToIsExternal = new Dictionary<String, bool>();
        }

        public List<String> InputTypes(string actionName)
        {
            if(rulesToInputTypes.ContainsKey(actionName))
            {
                return rulesToInputTypes[actionName];
            }
            else if(sequencesToInputTypes.ContainsKey(actionName))
            {
                return sequencesToInputTypes[actionName];
            }
            else if(proceduresToInputTypes.ContainsKey(actionName))
            {
                return proceduresToInputTypes[actionName];
            }
            else if(functionsToInputTypes.ContainsKey(actionName))
            {
                return functionsToInputTypes[actionName];
            }
            return null;
        }

        public List<String> OutputTypes(string actionName)
        {
            if(rulesToOutputTypes.ContainsKey(actionName))
            {
                return rulesToOutputTypes[actionName];
            }
            else if(sequencesToOutputTypes.ContainsKey(actionName))
            {
                return sequencesToOutputTypes[actionName];
            }
            else if(proceduresToOutputTypes.ContainsKey(actionName))
            {
                return proceduresToOutputTypes[actionName];
            }
            else if(functionsToOutputType.ContainsKey(actionName))
            {
                List<String> ret = new List<String>();
                ret.Add(functionsToOutputType[actionName]);
                return ret;
            }
            return null;
        }

        public IFilter GetFilterOfRule(string ruleName, string filterName)
        {
            foreach(IFilter filter in rulesToFilters[ruleName])
            {
                if(filter.PackagePrefixedName == filterName)
                    return filter;
            }
            return null;
        }

        public IFilter GetFilterOfMatchClass(string matchClassName, string filterName)
        {
            foreach(IFilter filter in matchClassesToFilters[matchClassName])
            {
                if(filter.PackagePrefixedName == filterName)
                    return filter;
            }
            return null;
        }

        public MatchClassInfo GetImplementedMatchClassOfRule(string ruleName, string matchClassName)
        {
            foreach(MatchClassInfo implementedMatchClass in rulesToImplementedMatchClasses[ruleName])
            {
                if(implementedMatchClass.PackagePrefixedName == matchClassName)
                    return implementedMatchClass;
            }
            return null;
        }

        public readonly Dictionary<String, List<IFilter>> rulesToFilters;
        public readonly Dictionary<String, List<IFilter>> matchClassesToFilters;
        public readonly Dictionary<String, List<String>> filterFunctionsToInputTypes;
        public readonly Dictionary<String, List<String>> rulesToInputTypes;
        public readonly Dictionary<String, List<String>> rulesToOutputTypes;
        public readonly Dictionary<String, List<String>> sequencesToInputTypes;
        public readonly Dictionary<String, List<String>> sequencesToOutputTypes;
        public readonly Dictionary<String, List<String>> proceduresToInputTypes;
        public readonly Dictionary<String, List<String>> proceduresToOutputTypes;
        public readonly Dictionary<String, bool> proceduresToIsExternal;
        public readonly Dictionary<String, List<String>> functionsToInputTypes;
        public readonly Dictionary<String, String> functionsToOutputType;
        public readonly Dictionary<String, bool> functionsToIsExternal;
        public readonly Dictionary<String, List<String>> rulesToTopLevelEntities;
        public readonly Dictionary<String, List<String>> rulesToTopLevelEntityTypes;
        public readonly Dictionary<String, MatchClassInfo> matchClasses;
        public readonly Dictionary<String, List<MatchClassInfo>> rulesToImplementedMatchClasses;
    }
}
