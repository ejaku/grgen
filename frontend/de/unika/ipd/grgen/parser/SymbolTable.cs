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

using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// A symbol table.
/// It maps strings to symbols.
/// </summary>
public class SymbolTable
{
	public const int IRRELEVANT = 0;

	private static readonly SymbolTable INVALID = new SymbolTable("<invalid>", IRRELEVANT);

	/// <summary>
	/// The string - symbol map. </summary>
	private readonly Dictionary<string, Symbol> symbolMap = new Dictionary<string, Symbol>();

	/// <summary>
	/// The name of the symbol table. </summary>
	private readonly string name;

	/// <summary>
	/// Id/Classification of the symbol table </summary>
	private readonly int id;

	public static SymbolTable Invalid
	{
		get
		{
			return INVALID;
		}
	}

	/// <summary>
	/// Make a new symbol table.
	/// </summary>
	public SymbolTable(string name, int id)
	{
		this.name = name;
		this.id = id;
	}

	/// <summary>
	/// Check, if two symbol tables are equal.
	/// Two symbol tables are equal, if they have the same name. </summary>
	/// <param name="obj"> Another symbol table. </param>
	/// <returns> true, if both symbol tables denote the same namespace,
	/// false if not. </returns>
	public override bool Equals(object obj)
	{
		if(obj is SymbolTable)
			return name.Equals(((SymbolTable)obj).name);

		return false;
	}

	/// <summary>
	/// Get the name of the symbol table. </summary>
	/// <returns> The symbol table's name. </returns>
	public string Name
	{
		get
		{
			return name;
		}
	}

	/// <summary>
	/// We also override the hashing scheme
	/// according to the equals method. </summary>
	/// <returns> The hashcode. </returns>
	public override int GetHashCode()
	{
		return name.GetHashCode();
	}

	/// <summary>
	/// Get the textual representation of a symbol table. </summary>
	/// <returns> The textual representation. </returns>
	public override string ToString()
	{
		return symbolMap.ToString();
	}

	/// <summary>
	/// Enter a keyword into the symbol table. </summary>
	/// <param name="text"> </param>
	/// <returns> The keyword symbol. </returns>
	public virtual Symbol EnterKeyword(string text)
	{
		Debug.Assert(!symbolMap.ContainsKey(text), "keywords cannot be put twice "
				+ "in the symbol table");

		Symbol sym = new SymbolAnonymousInnerClass(this, text);

		symbolMap[text] = sym;
		return sym;
	}

	private class SymbolAnonymousInnerClass : Symbol
	{
		private readonly SymbolTable outerInstance;

		public SymbolAnonymousInnerClass(SymbolTable outerInstance, string text) : base(text, outerInstance)
		{
			this.outerInstance = outerInstance;
		}

		public override bool isKeyword()
		{
			return true;
		}
	}

	/// <summary>
	/// Get a symbol for a string. </summary>
	/// <param name="text"> The string. </param>
	/// <returns> The corresponding symbol. </returns>
	public virtual Symbol Get(string text)
	{
		if(!symbolMap.ContainsKey(text))
			symbolMap[text] = new Symbol(text, this);

		return symbolMap[text];
	}

	/// <summary>
	/// Test a symbol for a string. </summary>
	/// <param name="text"> The string. </param>
	/// <returns> Whether the symbol is defined. </returns>
	public virtual bool Test(string text)
	{
		return symbolMap.ContainsKey(text);
	}

	/// <summary>
	/// returns the id/classification of this symbol table </summary>
	internal virtual int SymbolTableId
	{
		get
		{
			return id;
		}
	}
}

}
