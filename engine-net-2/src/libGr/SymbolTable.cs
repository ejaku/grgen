/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
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
        Globals,
        Sequence,
        For,
        If,
        IfThenPart,
        Computation
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
            public int computationCount;
        }

		public SymbolTable()
		{
			scopes = new Stack<Dictionary<String, SequenceVariable>>();
            scopesMeta = new Stack<ScopeMetaInformation>();

            globalsImplicitlyDeclared = new Dictionary<SequenceVariable, bool>();
		}

		public void PushScope(ScopeType scopeType)
		{
            // initial scopes of type Globals and Sequence must be handled by PushFirstScope
            Debug.Assert(scopes.Count > 1); 
            Debug.Assert(scopesMeta.Count > 1);

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
                case ScopeType.Computation:
                    scopeName = "computation" + scopesMeta.Peek().computationCount;
                    ++scopesMeta.Peek().computationCount;
                    break;
                default:
                    Debug.Assert(false); // only first scopes can be of type Globals and Sequence
                    scopeName = "";
                    break;
            }

            scopesMeta.Push(new ScopeMetaInformation(scopeName, scopeType));
		}

        public void PushFirstScope(Dictionary<String, String> predefinedVariables)
		{
            // only for initial scopes of type Globals and Sequence
            Debug.Assert(scopes.Count == 0);
            Debug.Assert(scopesMeta.Count == 0);

            globalsScope = new Dictionary<String, SequenceVariable>();
            scopes.Push(globalsScope);

            Dictionary<String, SequenceVariable> sequenceScope = new Dictionary<String, SequenceVariable>();
            if (predefinedVariables != null)
            {
                foreach (KeyValuePair<String, String> predefinedVariable in predefinedVariables)
                {
                    String name = predefinedVariable.Key;
                    String type = predefinedVariable.Value;
                    SequenceVariable newVar = new SequenceVariable(name, "", type);
                    newVar.Visited = true; // the predefined variables are parameters, don't declare them twice
                    sequenceScope.Add(name, newVar);
                }
            }
            scopes.Push(sequenceScope);

            scopesMeta.Push(new ScopeMetaInformation("", ScopeType.Globals));
            scopesMeta.Push(new ScopeMetaInformation("", ScopeType.Sequence));
		}

		public void PopScope(List<SequenceVariable> seqVarsDefinedInThisScope)
		{
            foreach(SequenceVariable seqVar in scopes.Peek().Values)
                seqVarsDefinedInThisScope.Add(seqVar);

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

        // returns entry for global variable, defines it in global scope if it is not yet defined there
        // a local variable with same name is bypassed (global prefix is not mandatory during the deprecation period)
        public SequenceVariable LookupDefineGlobal(String name)
        {
            if(globalsScope.ContainsKey(name))
            {
                return globalsScope[name];
            }

            SequenceVariable newVar = new SequenceVariable(name, "", "");
            globalsScope.Add(name, newVar);
            return newVar;
        }

		// returns null if variable was already defined
		public SequenceVariable Define(String name, String type)
		{
            // a "redefinition" is allowed if the old definition was from an explicitely prefixed global
			if(Lookup(name)!=null && Lookup(name).Type!="")
				return null;

            if(type == "")
            {
                SequenceVariable var = LookupDefineGlobal(name);
                globalsImplicitlyDeclared.Add(var, true);
                return var;
            }

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

        public bool WasImplicitelyDeclared(SequenceVariable global)
        {
            return globalsImplicitlyDeclared.ContainsKey(global);
        }

        // contains the symbols of the current nesting level and the levels it is contained in
        private Stack<Dictionary<String, SequenceVariable>> scopes;
        // contains some information about the current scope and counters for scope name construction
        // i.e. meta data for the scope whose symbol table is available at the same nesting level in the scopes symbol table stack
        private Stack<ScopeMetaInformation> scopesMeta;

        // quick access reference to globals scope
        private Dictionary<String, SequenceVariable> globalsScope;

        // ::x ;> x:T with its explicit global scope should be allowed, but x ;> x:T forbidden
        // due to compatibility reasons as of now names are implicitely declared in global scope if not yet seen,
        // which would render the second case ok; to prevent this we must remember whether a global was implicitely or explicitely declared
        // this handling can be removed when the deprecated implicit-globals feature is removed and all globals must be accessed with :: prefix
        private Dictionary<SequenceVariable, bool> globalsImplicitlyDeclared;
	}
}
