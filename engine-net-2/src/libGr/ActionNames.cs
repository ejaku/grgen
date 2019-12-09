/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        }

        public String[] ruleNames;
        public String[] sequenceNames;
        public String[] procedureNames;
        public String[] functionNames;
        public String[] functionOutputTypes;
        public String[] filterFunctionNames;
    }
}
