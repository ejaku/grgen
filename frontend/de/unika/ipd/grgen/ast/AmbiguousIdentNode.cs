/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast
{
using Symbol = de.unika.ipd.grgen.parser.Symbol;

/// <summary>
/// AST node that represents an Identifier that may be defined in two different symbol tables.
/// </summary>
public class AmbiguousIdentNode : IdentNode
{
	static AmbiguousIdentNode()
	{
		SetClassName(typeof(AmbiguousIdentNode), "ambig identifier");
	}

	/// <summary>
	/// Occurrence of the identifier. </summary>
	protected internal Symbol.Occurrence otherOcc;

	/// <summary>
	/// Make a new identifier node at a symbol's occurrence. </summary>
	/// <param name="occ"> The occurrence of the symbol. </param>
	public AmbiguousIdentNode(Symbol.Occurrence occ, Symbol.Occurrence otherOcc)
		: base(occ)
	{
		this.otherOcc = otherOcc;
	}

	/// <summary>
	/// Get the symbol definition of this identifier </summary>
	/// <seealso cref="Symbol.Definition"/>
	/// <returns> The symbol definition. </returns>
	public override Symbol.Definition SymDef
	{
		get
		{
			if(occ.Definition == null)
			{
				// I don't now why this is needed, it feels like a hack, but it works
				Symbol.Definition def = occ.Scope.GetCurrDef(Symbol);
				if(def.IsValid())
					SymDef = def;
				else
				{
					def = otherOcc.Scope.GetCurrDef(OtherSymbol);
					if(def.IsValid())
						SymDef = def;
				}
			}
			return occ.Definition;
		}
	}

	/// <summary>
	/// Get the symbol of the identifier. </summary>
	/// <returns> The symbol. </returns>
	public virtual Symbol OtherSymbol
	{
		get
		{
			return otherOcc.Symbol;
		}
	}
}

}
