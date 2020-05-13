/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Symbol;

/**
 * AST node that represents an Identifier that may be defined in two different symbol tables.
 */
public class AmbiguousIdentNode extends IdentNode
{
	static {
		setName(AmbiguousIdentNode.class, "ambig identifier");
	}

	/** Occurrence of the identifier. */
	protected Symbol.Occurrence otherOcc;

	/**
	 * Make a new identifier node at a symbol's occurrence.
	 * @param occ The occurrence of the symbol.
	 */
	public AmbiguousIdentNode(Symbol.Occurrence occ, Symbol.Occurrence otherOcc)
	{
		super(occ);
		this.otherOcc = otherOcc;
	}

	/**
	 * Get the symbol definition of this identifier
	 * @see Symbol#Definition
	 * @return The symbol definition.
	 */
	public Symbol.Definition getSymDef()
	{
		if(occ.getDefinition() == null) {
			// I don't now why this is needed, it feels like a hack, but it works
			Symbol.Definition def = occ.getScope().getCurrDef(getSymbol());
			if(def.isValid())
				setSymDef(def);
			else {
				def = otherOcc.getScope().getCurrDef(getOtherSymbol());
				if(def.isValid())
					setSymDef(def);
			}
		}
		return occ.getDefinition();
	}

	/**
	 * Get the symbol of the identifier.
	 * @return The symbol.
	 */
	public Symbol getOtherSymbol()
	{
		return otherOcc.getSymbol();
	}
}
