/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Collections;

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
        /// Gets the filterer of the match class with the given name.
        /// </summary>
        /// <param name="name">The name of the match class.</param>
        /// <returns>The filterer of the match class with the given name, or null, if no such match class exists.</returns>
        MatchClassFilterer GetMatchClass(String name);

        /// <summary>
        /// Enumerates all match class filterers (and thus also match class info objects) managed by this IActions instance.
        /// </summary>
        IEnumerable<MatchClassFilterer> MatchClasses { get; }


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
        /// The action-backend dependent commands that are available, and a description of each command.
        /// </summary>
        IDictionary<String, String> CustomCommandsAndDescriptions { get; }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do; first parameter has to be the command</param>
        void Custom(params object[] args);


        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Array processing helpers
        
        /// <summary>
        /// Orders the array ascendingly alongside the values in the given member.
        /// The array must be of match type, of a match of one of the actions in this actions object,
        /// or one of the match classes in this actions object.
        /// </summary>
        /// <param name="array">The array to order ascendingly</param>
        /// <param name="member">The member to order alongside</param>
        /// <returns>The array ordered by the member or null if the array was not processed because the array 
        /// did not contain matches of the known rules/tests or match classes,
        /// or the member was not known.</returns>
        IList ArrayOrderAscendingBy(IList array, string member);

        /// <summary>
        /// Orders the array descendingly alongside the values in the given member.
        /// The array must be of match type, of a match of one of the actions in this actions object,
        /// or one of the match classes in this actions object.
        /// </summary>
        /// <param name="array">The array to order descendingly</param>
        /// <param name="member">The member to order alongside</param>
        /// <returns>The array ordered by the member or null if the array was not processed because the array 
        /// did not contain matches of the known rules/tests or match classes,
        /// or the member was not known.</returns>
        IList ArrayOrderDescendingBy(IList array, string member);

        /// <summary>
        /// Groups the array alongside the values in the given member.
        /// The array must be of match type, of a match of one of the actions in this actions object
        /// or one of the match classes in this actions object.
        /// </summary>
        /// <param name="array">The array to group</param>
        /// <param name="member">The member to group by</param>
        /// <returns>The array grouped by the member or null if the array was not processed because the array 
        /// did not contain matches of the known rules/tests or match classes,
        /// or the member was not known.</returns>
        IList ArrayGroupBy(IList array, string member);

        /// <summary>
        /// Keeps the array members with distinct values in the given member / removes duplicates.
        /// The array must be of match type, of a match of one of the actions in this actions object,
        /// or one of the match classes in this actions object.
        /// </summary>
        /// <param name="array">The array to remove duplicates from</param>
        /// <param name="member">The member to check for duplicates</param>
        /// <returns>The array freed from duplicates in the member or null if the array was not processed because the array 
        /// did not contain matches of the known rules/tests or match classes,
        /// or the member was not known.</returns>
        IList ArrayKeepOneForEach(IList array, string member);

        /// <summary>
        /// Searches the array for the given value in the given member.
        /// The array must be of match or match class type of this actions object.
        /// </summary>
        /// <param name="array">The array to search in</param>
        /// <param name="member">The member to be searched for the value</param>
        /// <param name="value">The value to search for</param>
        /// <returns>The first index the value appears at, or -1.</returns>
        int ArrayIndexOfBy(IList array, string member, object value);

        /// <summary>
        /// Searches the array for the given value in the given member.
        /// The array must be of match or match class type of this actions object.
        /// </summary>
        /// <param name="array">The array to search in</param>
        /// <param name="member">The member to be searched for the value</param>
        /// <param name="value">The value to search for</param>
        /// <param name="startIndex">The index to start the search at</param>
        /// <returns>The first index starting at startIndex the value appears at, or -1.</returns>
        int ArrayIndexOfBy(IList array, string member, object value, int startIndex);

        /// <summary>
        /// Searches the array for the given value in the given member from back to front.
        /// The array must be of match or match class type of this actions object.
        /// </summary>
        /// <param name="array">The array to search in</param>
        /// <param name="member">The member to be searched for the value</param>
        /// <param name="value">The value to search for</param>
        /// <returns>The last index the value appears at, or -1.</returns>
        int ArrayLastIndexOfBy(IList array, string member, object value);

        /// <summary>
        /// Searches the array for the given value in the given member from back to front.
        /// The array must be of match or match class type of this actions object.
        /// </summary>
        /// <param name="array">The array to search in</param>
        /// <param name="member">The member to be searched for the value</param>
        /// <param name="value">The value to search for</param>
        /// <param name="startIndex">The index to start the search at</param>
        /// <returns>The last index before or at startIndex the value appears at, or -1.</returns>
        int ArrayLastIndexOfBy(IList array, string member, object value, int startIndex);

        /// <summary>
        /// Searches the array for the given value in the given member.
        /// The array must be of a match or match class type of this actions object.
        /// The array must be ordered alongside the member.
        /// </summary>
        /// <param name="array">The array to search in</param>
        /// <param name="member">The member to be searched for the value</param>
        /// <param name="value">The value to search for</param>
        /// <returns>The first index the value appears at, or -1.</returns>
        int ArrayIndexOfOrderedBy(IList array, string member, object value);

        #endregion Array processing helpers

        ////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Debugging helper. Fails in a debug build with an assertion.
        /// </summary>
        void FailAssertion();

        /// <summary>
        /// An MD5 hash of the used IGraphModel.
        /// </summary>
        String ModelMD5Hash { get; }
    }
}
