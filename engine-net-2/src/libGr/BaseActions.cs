/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A partial implementation of the interface to the actions. 
    /// </summary>
    public abstract class BaseActions : IActions
    {
        public abstract String Name { get; }

        public abstract IGraph Graph { get; set; }

        public abstract string[] Packages { get; }


        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Enumerates all actions managed by this BaseActions instance.
        /// </summary>
        public abstract IEnumerable<IAction> Actions { get; }

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        public abstract IAction GetAction(String name);


        ////////////////////////////////////////////////////////////////////////////////////////////////////


        private Dictionary<String, SequenceDefinition> namesToSequenceDefinitions = new Dictionary<string, SequenceDefinition>();

        /// <summary>
        /// Register a graph rewrite sequence definition.
        /// An interpreted sequence can be overwritten by a new one of the same name and signature.
        /// A compiled sequence is fixed, an exception is thrown if you try to set a sequence of the same name.
        /// </summary>
        /// <param name="sequenceDef">The sequence definition</param>
        /// <returns>Returns true if an existing definition was overwritten.</returns>
        public bool RegisterGraphRewriteSequenceDefinition(SequenceDefinition sequenceDef)
        {
            if(namesToSequenceDefinitions.ContainsKey(sequenceDef.SequenceName))
            {
                if(namesToSequenceDefinitions[sequenceDef.SequenceName] is SequenceDefinitionCompiled)
                {
                    throw new Exception("A compiled sequence can't be overwritten!");
                }

                SequenceDefinitionInterpreted existingSequenceDef = (SequenceDefinitionInterpreted)namesToSequenceDefinitions[sequenceDef.SequenceName];
                SequenceDefinitionInterpreted interpretedSequenceDef = (SequenceDefinitionInterpreted)sequenceDef;

                if(interpretedSequenceDef.InputVariables.Length != existingSequenceDef.InputVariables.Length)
                    throw new Exception("Old and new sequence definition for " + sequenceDef.SequenceName + " have a different number of parameters");
                for(int i = 0; i < interpretedSequenceDef.InputVariables.Length; ++i)
                    if(interpretedSequenceDef.InputVariables[i].Type != existingSequenceDef.InputVariables[i].Type)
                        throw new Exception("Old and new sequence definition for " + sequenceDef.SequenceName + " differ in parameter #" + i);
                if(interpretedSequenceDef.OutputVariables.Length != existingSequenceDef.OutputVariables.Length)
                    throw new Exception("Old and new sequence definition for " + sequenceDef.SequenceName + " have a different number of output parameters");
                for(int i = 0; i < interpretedSequenceDef.OutputVariables.Length; ++i)
                    if(interpretedSequenceDef.OutputVariables[i].Type != existingSequenceDef.OutputVariables[i].Type)
                        throw new Exception("Old and new sequence definition for " + sequenceDef.SequenceName + " differ in output parameter #" + i);

                namesToSequenceDefinitions[sequenceDef.SequenceName] = sequenceDef; // replace definition in map by name used for new sequences
                foreach(SequenceDefinition seqDef in namesToSequenceDefinitions.Values) // replace all references in old sequences to new one
                {
                    if(!(seqDef is SequenceDefinitionCompiled))
                        seqDef.ReplaceSequenceDefinition(existingSequenceDef, sequenceDef);
                }
                existingSequenceDef.WasReplacedBy(sequenceDef); // flush sequence copy cache for this name

                return true;
            }

            namesToSequenceDefinitions.Add(sequenceDef.SequenceName, sequenceDef);
            return false;
        }

        /// <summary>
        /// Retrieve a graph rewrite sequence definition.
        /// </summary>
        /// <param name="name">The name of the defined sequence to retrieve</param>
        /// <returns>The defined sequence or null if no such sequence exists.</returns>
        public ISequenceDefinition GetSequenceDefinition(String name)
        {
            SequenceDefinition seqDef;
            namesToSequenceDefinitions.TryGetValue(name, out seqDef);
            return seqDef;
        }

        /// <summary>
        /// Enumerates all graph rewrite sequence definitions.
        /// </summary>
        public IEnumerable<ISequenceDefinition> SequenceDefinitions
        {
            get
            {
                foreach(SequenceDefinition seqDef in namesToSequenceDefinitions.Values)
                    yield return seqDef;
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////


        protected Dictionary<String, FunctionInfo> namesToFunctionDefinitions = new Dictionary<string, FunctionInfo>();

        /// <summary>
        /// Retrieve a function definition.
        /// </summary>
        /// <param name="name">The name of the function to retrieve</param>
        /// <returns>The function or null if no such function exists.</returns>
        public IFunctionDefinition GetFunctionDefinition(String name)
        {
            FunctionInfo functionDef;
            namesToFunctionDefinitions.TryGetValue(name, out functionDef);
            return functionDef;
        }

        /// <summary>
        /// Enumerates all function definitions.
        /// </summary>
        public IEnumerable<IFunctionDefinition> FunctionDefinitions
        {
            get
            {
                foreach(FunctionInfo functionDef in namesToFunctionDefinitions.Values)
                    yield return functionDef;
            }
        }

        /// <summary>
        /// returns a comma separated list of the names of the functions known 
        /// </summary>
        public string FunctionNames
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(string name in namesToFunctionDefinitions.Keys)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(name);
                }
                return sb.ToString();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////

        
        protected Dictionary<String, ProcedureInfo> namesToProcedureDefinitions = new Dictionary<string, ProcedureInfo>();

        /// <summary>
        /// Retrieve a procedure definition.
        /// </summary>
        /// <param name="name">The name of the procedure to retrieve</param>
        /// <returns>The procedure or null if no such procedure exists.</returns>
        public IProcedureDefinition GetProcedureDefinition(String name)
        {
            ProcedureInfo procedureDef;
            namesToProcedureDefinitions.TryGetValue(name, out procedureDef);
            return procedureDef;
        }

        /// <summary>
        /// Enumerates all procedure definitions.
        /// </summary>
        public IEnumerable<IProcedureDefinition> ProcedureDefinitions
        {
            get
            {
                foreach(ProcedureInfo procedureDef in namesToProcedureDefinitions.Values)
                    yield return procedureDef;
            }
        }

        /// <summary>
        /// returns a comma separated list of the names of the procedures known 
        /// </summary>
        public string ProcedureNames
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(string name in namesToProcedureDefinitions.Keys)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(name);
                }
                return sb.ToString();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////


        public abstract String ModelMD5Hash { get; }

        public abstract void Custom(params object[] args);

        public abstract bool LazyNIC { get; }

        public abstract bool Profile { get; }
    }
}
