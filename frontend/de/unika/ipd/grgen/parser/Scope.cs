/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser
{

	using System;
	using System.Collections.Generic;

	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using Definition = de.unika.ipd.grgen.parser.Symbol.Definition;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	/// <summary>
	/// A namespace.
	/// </summary>
	public class Scope
	{
		/// <summary>
		/// This scope's parent scope. </summary>
		private readonly Scope parent;

		/// <summary>
		/// The name of this scope. </summary>
		private readonly IdentNode ident;

		/// <summary>
		/// An error reporter for error reporting. </summary>
		private readonly ErrorReporter reporter;

		/// <summary>
		/// All definitions of this scope. Map from symbol to Symbol.Definition </summary>
		private readonly IDictionary<Symbol, Symbol.Definition> defs = new Dictionary<Symbol, Symbol.Definition>();

		/// <summary>
		/// A map for numbering of anonymous id's </summary>
		private readonly IDictionary<string, int> anonIds = new Dictionary<string, int>();

		/// <summary>
		/// The children scopes. </summary>
		private readonly IList<Scope> childs = new List<Scope>();

		/// <summary>
		/// A list of all occurrences without a definition in this scope.
		/// Will be used to enter the proper definition in <seealso cref="leaveScope()"/>
		/// </summary>
		private readonly IList<Symbol.Occurrence> occFixup = new List<Symbol.Occurrence>();

		/// <summary>
		/// An invalid scope. </summary>
		private const Scope INVALID = null; //new Scope(null, -1, new IdentNode(new Definition(null, Coords.getBuiltin(), new Symbol("<invalid>", SymbolTable.getInvalid()))));

		/// <summary>
		/// Get an invalid scope. </summary>
		/// <returns> An invalid scope. </returns>
		public static Scope Invalid
		{
			get
			{
				return INVALID;
			}
		}

		/// <summary>
		/// Make a new root scope.
		/// This constructor may only used for initial root scopes. </summary>
		/// <param name="reporter"> An error reporter for error message reporting. </param>
		public Scope(ErrorReporter reporter)
		{
			this.parent = null;
			this.reporter = reporter;
			this.ident = null; //new IdentNode(new Definition(null, Coords.getBuiltin(), new Symbol("ROOT", SymbolTable.getInvalid())));
		}

		/// <summary>
		/// Internal constructor used by <seealso cref="newScope(String)"/>. </summary>
		/// <param name="parent"> The parent scope. </param>
		/// <param name="id"> The numeral id of this scope. </param>
		/// <param name="ident"> The ident node of this scope (commonly this is the ident
		/// that opened the scope). </param>
		private Scope(Scope parent, IdentNode ident)
		{
			this.parent = parent;
			this.ident = ident;
			this.reporter = parent != null ? parent.reporter : null;
		}

		/// <summary>
		/// Checks, if a symbol has been defined in the current scope.
		/// Subscopes are not considered. </summary>
		/// <param name="sym"> The symbol to check for. </param>
		/// <returns> true, if the symbol was defined in <b>this</b> scope, false
		/// otherwise. </returns>
		public virtual bool DefinedHere(Symbol sym)
		{
			return GetLocalDef(sym).IsValid();
		}

		/// <summary>
		/// Checks, if a symbol is legally defined at this position.
		/// First, it is checked, if the symbol has been  defined in this scope, if
		/// not subscopes a visited recursively. </summary>
		/// <param name="sym"> The symbol to check for. </param>
		/// <returns> true, if a definition of this symbol is visible in this scope,
		/// false, if not. </returns>
		public virtual bool Defined(Symbol sym)
		{
			return GetCurrDef(sym).IsValid();
		}

		/// <summary>
		/// Returns the local definition of a symbol. </summary>
		/// <param name="sym"> The symbol whose definition to get. </param>
		/// <returns> The definition of the symbol, or an invalid definition,
		/// if the symbol has not been defined in this scope. </returns>
		public virtual Symbol.Definition GetLocalDef(Symbol sym)
		{
			Symbol.Definition res = Symbol.Definition.Invalid;

			if(defs.ContainsKey(sym))
				res = defs[sym];

			return res;
		}

		/// <summary>
		/// Get the current definition of a symbol. </summary>
		/// <param name="symbol"> The symbol whose definition to get. </param>
		/// <returns> The visible (local or non-local) definition of the symbol,
		/// or an invalid definition, if the symbol's definition is not visible
		/// in this scope. </returns>
		public virtual Definition GetCurrDef(Symbol symbol)
		{
			Symbol.Definition def = GetLocalDef(symbol);

			if(!(def.IsValid() || IsRoot()))
				def = parent.GetCurrDef(symbol);

			return def;
		}

		/// <summary>
		/// Signal the occurrence of a symbol.
		/// The scope remembers the occurrence and enters the correct definition
		/// at the moment the scope is left. This can be a local definition in the
		/// scope, or a visible definition in a subscope, or an invalid definition,
		/// if the symbol was used in this scope, but has never been defined to be
		/// visible in this scope. </summary>
		/// <param name="sym"> The symbol, that occurs. </param>
		/// <param name="coords"> The source code coordinates. </param>
		/// <returns> The symbol's occurrence. </returns>
		public virtual Symbol.Occurrence Occurs(Symbol sym, Coords coords)
		{
			Symbol.Occurrence occ = sym.Occurs(this, coords);
			occFixup.Add(occ);

			return occ;
		}

		/// <summary>
		/// Signal the definition of a symbol. </summary>
		/// <param name="sym"> The symbol that is occurring as a definition. </param>
		/// <returns> The symbol's definition. </returns>
		public virtual Symbol.Definition Define(Symbol sym)
		{
			return Define(sym, new Coords());
		}

		/// <summary>
		/// Signal the definition of a symbol.
		/// This method should be called, if the parser encounters a symbol in
		/// a define situation. </summary>
		/// <param name="sym"> The symbol that is being defined. </param>
		/// <param name="coords"> The source code coordinates for the definition. </param>
		/// <returns> The symbol's definition. </returns>
		public virtual Symbol.Definition Define(Symbol sym, Coords coords)
		{
			Symbol.Definition def = Symbol.Definition.Invalid;

			if(sym.IsKeyword() && sym.DefinitionCount > 0)
			{
				reporter.Error(coords, "Cannot redefine keyword " + sym + ".");
				def = Symbol.Definition.Invalid; // do not redefine a keyword
			}
			else if(DefinedHere(sym))
			{
				def = GetLocalDef(sym); // the previous definition
				reporter.Error(coords, "Symbol " + sym + " has already been defined in this scope"
							+ " [at: " + def.coords + "].");
				def = Symbol.Definition.Invalid; // do not redefine a symbol
			}
			else if(Defined(sym)
					&& sym.SymbolTable.SymbolTableId != ParserEnvironment.ITERATEDS
					&& this.Ident.Symbol.SymbolTable.SymbolTableId != ParserEnvironment.PACKAGES)
			{
				def = GetCurrDef(sym); // the previous definition
				reporter.Error(coords, "Symbol " + sym + " has already been defined in some parent scope"
							+ " [at: " + def.coords + "].");
				def = Symbol.Definition.Invalid; // do not redefine a symbol from a parent scope
			}
			else
			{
				try
				{
					def = sym.Define(this, coords);
					defs[sym] = def;
				}
				catch(SymbolTableException e)
				{
					reporter.Error(coords, e.Message);
				}
			}

			return def;
		}

		/// <summary>
		/// Define an unique anonymous symbol in this scope.
		/// Especially, this can also be done after parsing. </summary>
		/// <param name="name"> An addition to the symbol's name (for easier readability). </param>
		/// <param name="symTab"> The symbol table the symbol is defined in. </param>
		/// <param name="coords"> The source code coordinates, that are associated with this
		/// anonymous symbol. </param>
		/// <returns> A symbol, that could not have been defined in the parsed text,
		/// unique in this scope. </returns>
		public virtual Symbol.Definition DefineAnonymous(string name, SymbolTable symTab,
				Coords coords)
		{
			int currId = 0;
			if(anonIds.ContainsKey(name))
				currId = anonIds[name];

			anonIds[name] = Convert.ToInt32(currId + 1);

			return Define(Symbol.MakeAnonymous(name + currId, symTab), coords);
		}

		/// <summary>
		/// Enter a new subscope. </summary>
		/// <param name="name"> The name of the new subscope. </param>
		/// <returns> The newly entered scope. </returns>
		public virtual Scope NewScope(IdentNode name)
		{
			Scope s = new Scope(this, name);
			childs.Add(s);
			return s;
		}

		/// <summary>
		/// Enter a new or re-enter an already defined subscope. </summary>
		/// <param name="name"> The name of the new subscope. </param>
		/// <returns> The newly entered scope. </returns>
		public virtual Scope NewOrReuseScope(IdentNode name)
		{
			foreach(Scope child in childs)
			{
				if(child.Ident.ToString().Equals(name.ToString()))
					return child;
			}
			Scope s = new Scope(this, name);
			childs.Add(s);
			return s;
		}

		/// <summary>
		/// Leave a scope. </summary>
		/// <returns> The parent scope of the one to leave. </returns>
		public virtual Scope LeaveScope()
		{
			// fixup all occurrences by entering the correct definition.
			foreach(Symbol.Occurrence occ in occFixup)
				occ.def = GetCurrDef(occ.symbol);

			return parent;
		}

		/// <summary>
		/// Check, if a scope is the root scope. </summary>
		/// <returns> true, if the scope is the root scope, false, if not. </returns>
		public virtual bool IsRoot()
		{
			return parent == null;
		}

		/// <summary>
		/// Get the parent of the scope. </summary>
		/// <returns> The parent of the scope, or null, if it is the root scope. </returns>
		public virtual Scope Parent
		{
			get
			{
				return parent;
			}
		}

		public virtual Scope Root
		{
			get
			{
				Scope curScope = this;
				while(!curScope.IsRoot())
					curScope = curScope.Parent;
				return curScope;
			}
		}

		public virtual string Name
		{
			get
			{
				if(ident == null)
					return "<ROOT>";
				return ident.ToString();
			}
		}

		/// <summary>
		/// Returns the defining ident.
		/// </summary>
		public virtual IdentNode Ident
		{
			get
			{
				return ident;
			}
		}

		public virtual string Path
		{
			get
			{
				string res = "";
				if(!IsRoot())
					res = res + parent + ".";
				return res + Name;
			}
		}

		public string ToStringWithOpeningCoords()
		{
			return ToString() + " [opened at " + (ident != null ? ident.Coords : "0,0") + "]";
		}

		/// <seealso cref="java.lang.Object.toString()"/>
		public override string ToString()
		{
			return Name;
		}
	}

}
