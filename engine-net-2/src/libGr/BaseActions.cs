/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
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
    /// A container of rules.
    /// </summary>
    public abstract class BaseActions
    {
        #region Abstract members

        /// <summary>
        /// An associated name.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// An MD5 hash of the used IGraphModel.
        /// Probably useless...
        /// </summary>
        public abstract String ModelMD5Hash { get; }

        /// <summary>
        /// The associated graph.
        /// </summary>
        public abstract IGraph Graph { get; set; }

        /// <summary>
        /// The packages defined in this BaseActions instance.
        /// </summary>
        public abstract string[] Packages { get; }

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

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do</param>
        public abstract void Custom(params object[] args);

        /// <summary>
        /// Tells whether lazy negative/independent/condition evaluation code was generated when the actions were generated.
        /// (So we can regenerated matchers at runtime correctly, using the same setting.)
        /// </summary>
        public abstract bool LazyNIC { get; }

        /// <summary>
        /// Tells whether profiling instrumentation code (search step counting) was generated when the actions were generated.
        /// (So we can regenerated matchers at runtime correctly, using the same setting.)
        /// </summary>
        public abstract bool Profile { get; }

        #endregion Abstract members


        #region Sequence handling

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
        public SequenceDefinition RetrieveGraphRewriteSequenceDefinition(String name)
        {
            SequenceDefinition seqDef;
            namesToSequenceDefinitions.TryGetValue(name, out seqDef);
            return seqDef;
        }

        /// <summary>
        /// Enumerates all graph rewrite sequence definitions.
        /// </summary>
        public IEnumerable<SequenceDefinition> GraphRewriteSequenceDefinitions
        {
            get
            {
                foreach(SequenceDefinition seqDef in namesToSequenceDefinitions.Values)
                    yield return seqDef;
            }
        }

        #endregion Sequence handling

        #region Functions handling

        protected Dictionary<String, FunctionInfo> namesToFunctionDefinitions = new Dictionary<string, FunctionInfo>();

        /// <summary>
        /// Retrieve a function definition.
        /// </summary>
        /// <param name="name">The name of the function to retrieve</param>
        /// <returns>The function or null if no such function exists.</returns>
        public FunctionInfo RetrieveFunctionDefinition(String name)
        {
            FunctionInfo functionDef;
            namesToFunctionDefinitions.TryGetValue(name, out functionDef);
            return functionDef;
        }

        /// <summary>
        /// Enumerates all function definitions.
        /// </summary>
        public IEnumerable<FunctionInfo> FunctionDefinitions
        {
            get
            {
                foreach(FunctionInfo functionDef in namesToFunctionDefinitions.Values)
                    yield return functionDef;
            }
        }

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

        #endregion Functions handling

        #region Procedures handling

        protected Dictionary<String, ProcedureInfo> namesToProcedureDefinitions = new Dictionary<string, ProcedureInfo>();

        /// <summary>
        /// Retrieve a procedure definition.
        /// </summary>
        /// <param name="name">The name of the procedure to retrieve</param>
        /// <returns>The procedure or null if no such procedure exists.</returns>
        public ProcedureInfo RetrieveProcedureDefinition(String name)
        {
            ProcedureInfo procedureDef;
            namesToProcedureDefinitions.TryGetValue(name, out procedureDef);
            return procedureDef;
        }

        /// <summary>
        /// Enumerates all procedure definitions.
        /// </summary>
        public IEnumerable<ProcedureInfo> ProcedureDefinitions
        {
            get
            {
                foreach(ProcedureInfo procedureDef in namesToProcedureDefinitions.Values)
                    yield return procedureDef;
            }
        }

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

        #endregion Procedures handling
    }
}
