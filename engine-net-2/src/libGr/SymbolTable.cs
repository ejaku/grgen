/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The possible scope opening types
    /// </summary>
    public enum ScopeType
    {
        Sequence,
        For,
        If,
        IfThenPart
    }

    /// <summary>
    /// class of symbol table containing the variables declared in the sequence 
    /// variables might be implicitely(graph-global) or explicitely(sequence-local) declared
    /// </summary>
    public class SymbolTable
	{
        private class ScopeMetaInformation
        {
            public ScopeMetaInformation(String name, ScopeType scopeType)
            {
                this.name = name;
                this.scopeType = scopeType;
                forCount = 0;
                ifCount = 0;
            }

            public String name;
            public ScopeType scopeType;
            public int forCount;
            public int ifCount;
        }

		public SymbolTable()
		{
			scopes = new Stack<Dictionary<String, SequenceVariable>>();
            scopesMeta = new Stack<ScopeMetaInformation>();
		}
		
		public void PushScope(ScopeType scopeType)
		{
            Debug.Assert(scopes.Count > 0); // initial scope of type Sequence must be handled by PushFirstScope
            Debug.Assert(scopesMeta.Count > 0); // initial scope of type Sequence must be handled by PushFirstScope
            
            scopes.Push(new Dictionary<String, SequenceVariable>());
            
            String scopeName;
            switch (scopeType)
            {
                case ScopeType.For: 
                    scopeName = "for" + scopesMeta.Peek().forCount;
                    ++scopesMeta.Peek().forCount;
                    break;
                case ScopeType.If:
                    scopeName = "if" + scopesMeta.Peek().ifCount;
                    ++scopesMeta.Peek().ifCount;
                    break;
                case ScopeType.IfThenPart: 
                    scopeName = "thenpart";
                    break;
                default: Debug.Assert(false); // only first scope can be of type sequence
                    scopeName = "";
                    break;
            }
            scopesMeta.Push(new ScopeMetaInformation(scopeName, scopeType));
		}

        public void PushFirstScope(Dictionary<String, String> predefinedVariables)
		{
            Debug.Assert(scopes.Count == 0); // only for initial scope of type Sequence
            Debug.Assert(scopesMeta.Count == 0); // only for initial scope of type Sequence

            Dictionary<String, SequenceVariable> initialScope = new Dictionary<String, SequenceVariable>();
            if (predefinedVariables != null)
            {
                foreach (KeyValuePair<String, String> predefinedVariable in predefinedVariables)
                {
                    String name = predefinedVariable.Key;
                    String type = predefinedVariable.Value;
                    SequenceVariable newVar = new SequenceVariable(name, "", type);
                    newVar.Visited = true; // the predefined variables are parameters, don't declare them twice
                    initialScope.Add(name, newVar);
                }
            }
            scopes.Push(initialScope);

            scopesMeta.Push(new ScopeMetaInformation("", ScopeType.Sequence));
		}
		
		public void PopScope()
		{
			scopes.Pop();
            scopesMeta.Pop();
		}

		// returns entry for variable, null if not defined
        // entry.type will give the type for a sequence-local variable and will be "" for a graph-global variable
        public SequenceVariable Lookup(String name)
		{
			foreach(Dictionary<String, SequenceVariable> scope in scopes)
			{
				if(scope.ContainsKey(name))
				{
					return scope[name];
				}
			}
			
			return null;
		}
		
		// returns null if variable was already defined
		public SequenceVariable Define(String name, String type)
		{
			if(Lookup(name)!=null)
				return null;

            string prefix = "";
            ScopeMetaInformation[] scopesMeta = this.scopesMeta.ToArray(); // holy shit! no sets, no backward iterators, no direct access to stack, 
                                                                        // size sometimes called length, sometimes count ... c# data structures suck
            for (int i = scopesMeta.Length - 1; i >= 0; --i) // stack dumped in reverse ^^
            {
                prefix += scopesMeta[i].name;
            }

            SequenceVariable newVar = new SequenceVariable(name, prefix, type);
			scopes.Peek().Add(name, newVar);
			return newVar;
		}
		
        // contains the symbols of the current nesting level and the levels it is contained in
        private Stack<Dictionary<String, SequenceVariable>> scopes;
        // contains some information about the current scope and counters for scope name construction
        // i.e. meta data for the scope whose symbol table is available at the same nesting level in the scopes symbol table stack
        private Stack<ScopeMetaInformation> scopesMeta;
	}
}
