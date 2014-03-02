/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The interface to the actions (the "generic" interface, using strings and objects).
    /// </summary>
    public interface IActions
    {
        /// <summary>
        /// The name of the actions.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The associated graph.
        /// </summary>
        IGraph Graph { get; }

        /// <summary>
        /// The packages defined in this IActions instance.
        /// </summary>
        string[] Packages { get; }


        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        IAction GetAction(String name);

        /// <summary>
        /// Enumerates all actions managed by this IActions instance.
        /// </summary>
        IEnumerable<IAction> Actions { get; }

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Retrieve a graph rewrite sequence definition.
        /// </summary>
        /// <param name="name">The name of the defined sequence to retrieve</param>
        /// <returns>The defined sequence or null if no such sequence exists.</returns>
        ISequenceDefinition GetSequenceDefinition(String name);

        /// <summary>
        /// Enumerates all graph rewrite sequence definitions.
        /// </summary>
        IEnumerable<ISequenceDefinition> SequenceDefinitions { get; }

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Retrieve a function definition.
        /// </summary>
        /// <param name="name">The name of the function to retrieve</param>
        /// <returns>The function or null if no such function exists.</returns>
        IFunctionDefinition GetFunctionDefinition(String name);

        /// <summary>
        /// Enumerates all function definitions.
        /// </summary>
        IEnumerable<IFunctionDefinition> FunctionDefinitions { get; }

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Retrieve a procedure definition.
        /// </summary>
        /// <param name="name">The name of the procedure to retrieve</param>
        /// <returns>The procedure or null if no such procedure exists.</returns>
        IProcedureDefinition GetProcedureDefinition(String name);

        /// <summary>
        /// Enumerates all procedure definitions.
        /// </summary>
        IEnumerable<IProcedureDefinition> ProcedureDefinitions { get; }


        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// An MD5 hash of the used IGraphModel.
        /// </summary>
        String ModelMD5Hash { get; }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do</param>
        void Custom(params object[] args);
    }
}
