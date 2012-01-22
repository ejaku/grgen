/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
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
    }
}
