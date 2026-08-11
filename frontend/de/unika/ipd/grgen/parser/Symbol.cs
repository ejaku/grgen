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
using System.Diagnostics;

using IdentNode = de.unika.ipd.grgen.ast.IdentNode;

/// <summary>
/// A lexical symbol.
/// </summary>
public class Symbol
{
	/// <summary>
	/// An occurrence of a symbol.
	/// </summary>
	public class Occurrence
	{
		/// <summary>
		/// The scope in which the symbol occurred. </summary>
		protected internal readonly Scope scope;

		/// <summary>
		/// The source file coordinates where the symbol occurred. </summary>
		protected internal readonly Coords coords;

		/// <summary>
		/// The symbol that occurred. </summary>
		protected internal readonly Symbol symbol;

		/// <summary>
		/// The corresponding definition of the symbol.
		/// Points to itself, if this occurrence is a definition,
		/// </summary>
		protected internal Definition def;

		/// <summary>
		/// Make a new occurrence. </summary>
		/// <param name="sc"> The scope where the symbol occurred, </param>
		/// <param name="c"> The source file coordinates. </param>
		/// <param name="sym"> The symbol that occurred. </param>
		public Occurrence(Scope sc, Coords c, Symbol sym)
		{
			symbol = sym;
			scope = sc;
			coords = c;
		}

		/// <seealso cref="java.lang.Object.toString()"/>
		public override string ToString()
		{
			return "" + symbol + "(" + coords + "," + scope + ")";
		}

		/// <summary>
		/// Get the occurring symbol. </summary>
		/// <returns> The symbol. </returns>
		public virtual Symbol Symbol
		{
			get
			{
				return symbol;
			}
		}

		/// <summary>
		/// Get the source code coordinates. </summary>
		/// <returns> The coordinates. </returns>
		public virtual Coords Coords
		{
			get
			{
				return coords;
			}
		}

		/// <summary>
		/// Get the scope of occurrence. </summary>
		/// <returns> The scope. </returns>
		public virtual Scope Scope
		{
			get
			{
				return scope;
			}
		}

		/// <summary>
		/// Get the symbol's definition. </summary>
		/// <returns> The definition. </returns>
		public virtual Definition Definition
		{
			get
			{
				return def;
			}
			set
			{
				this.def = value;
			}
		}


		public virtual bool IsAnonymous()
		{
			return symbol.text.StartsWith("$", StringComparison.Ordinal) || def.anonymous;
		}
	}

	/// <summary>
	/// The definition of a symbol.
	/// Especially, a definition is an occurrence, that defines an identifier.
	/// </summary>
	public class Definition : Occurrence
	{
		/// <summary>
		/// An AST ident node for this definition.
		/// This is needed, because other ident nodes representing the same
		/// identifier have to resolve the ident node of the definition to
		/// get the defined entity.
		/// </summary>
		protected internal IdentNode node;

		protected internal bool anonymous = false;

		internal static readonly Definition INVALID = new Definition(Scope.Invalid, Coords.INVALID, Symbol.INVALID);

		/// <summary>
		/// Make an invalid definition. </summary>
		/// <returns> An invalid definition. </returns>
		public static Definition Invalid
		{
			get
			{
				return INVALID;
			}
		}

		/// <summary>
		/// Make a new symbol definition. </summary>
		/// <param name="sc"> The scope in which the symbol is defined. </param>
		/// <param name="c"> The source code coordinates where the symbol was defined. </param>
		/// <param name="sym"> The symbol, that was defined. </param>
		public Definition(Scope sc, Coords c, Symbol sym)
			 : base(sc, c, sym)
		{
			def = this;
		}

		public virtual Definition DeclareAnonymous()
		{
			anonymous = true;
			return this;
		}

		/// <summary>
		/// Checks the validity of a definition. </summary>
		/// <returns> true, if the definition is valid. </returns>
		public virtual bool IsValid()
		{
			return symbol != Symbol.INVALID;
		}

		/// <summary>
		/// Get the AST ident node for this definition. </summary>
		/// <returns> The AST node for this definition. </returns>
		public virtual IdentNode Node
		{
			get
			{
				return node;
			}
			set // Set an AST node for this definition. An AST ident node.
			{
			this.node = value;
			}
		}

	}

	/// <summary>
	/// An invalid symbol. </summary>
	private static readonly Symbol INVALID = new Symbol("<invalid>",
			SymbolTable.Invalid);

	/// <summary>
	/// The number of definitions concerning this symbol. </summary>
	private int definitions = 0;

	/// <summary>
	/// The symbol table the symbol was defined in. </summary>
	private readonly SymbolTable symbolTable;

	/// <summary>
	/// The string of the symbol. </summary>
	private readonly string text;

	/// <summary>
	/// Make a new symbol. </summary>
	/// <param name="text"> The text of the symbol. </param>
	public Symbol(string text, SymbolTable symbolTable)
	{
		Debug.Assert((!string.ReferenceEquals(text, null)));
		this.text = text;
		this.symbolTable = symbolTable;
	}

	// Two symbols are equal, if they represent the same string and
	// are defined in the same symbol table.
	// This holds if they are compared for object/reference identity.
	// Removed the equals implementation (previously available at this place).

	/// <summary>
	/// Get the symbol table, the symbol was defined in. </summary>
	/// <param name="The"> symbol table. </param>
	public virtual SymbolTable SymbolTable
	{
		get
		{
			return symbolTable;
		}
	}

	/// <summary>
	/// Get an occurrence of this symbol. </summary>
	/// <param name="sc"> The current scope. </param>
	/// <param name="c"> The coordinates the occurrence happened. </param>
	/// <returns> An occurrence of the current symbol. </returns>
	public virtual Occurrence Occurs(Scope sc, Coords c)
	{
		return new Occurrence(sc, c, this);
	}

	/// <summary>
	/// Get a definition of the symbol. </summary>
	/// <param name="sc"> The scope the definition occurrs in. </param>
	/// <param name="c"> The coordinates of the definition. </param>
	/// <returns> The definition. </returns>
	public virtual Definition Define(Scope sc, Coords c)
	{
		if(IsKeyword() && definitions > 0)
			throw new SymbolTableException(c, "keyword cannot be redefined");
		else
		{
			definitions++;
			return new Definition(sc, c, this);
		}
	}

	public virtual string Text
	{
		get
		{
			return /*text != null ? */text/* : "<invalid>"*/;
		}
	}

	public override string ToString()
	{
		return Text;
	}

	/// <summary>
	/// Is this symbol a keyword.
	/// A keyword symbol cannot be defined. </summary>
	/// <returns> true, if the symbol is a keyword, false if not. </returns>
	public virtual bool IsKeyword()
	{
		return false; // overridden in anonymous class created in SymbolTable.enterKeyword
	}

	/// <summary>
	/// Get the number of definitions. </summary>
	/// <returns> The number of times the symbol has been defined. </returns>
	public virtual int DefinitionCount
	{
		get
		{
			return definitions;
		}
	}

	/// <summary>
	/// Make an anonymous symbol.
	/// This symbol could not have been declared somewhere in the parsed text.
	/// So, it must contain a character, that is not allowed in the language's
	/// identifier rule. </summary>
	/// <param name="name"> An addition to the name of the symbol. </param>
	/// <param name="symTab"> The symbol table the symbol occurs in. </param>
	/// <returns> An anonymous symbol. </returns>
	public static Symbol MakeAnonymous(string name, SymbolTable symTab)
	{
		return new Symbol("$" + name, symTab);
	}
}

}
