/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Created: Wed Jul  2 14:31:38 2003
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import java.util.HashMap;

/**
 * A symbol table.
 * It maps strings to symbols.
 */
public class SymbolTable {

	private static final SymbolTable INVALID =
		new SymbolTable("<invalid>");

	/** The string - symbol map. */
	private final HashMap<String, Symbol> symbolMap = new HashMap<String, Symbol>();

	/** The name of the symbol table. */
	private final String name;

	public static final SymbolTable getInvalid() {
		return INVALID;
	}

	/**
	 * Make a new symbol table.
	 */
	public SymbolTable(String name) {
		this.name = name;
	}

	/**
	 * Check, if two symbol tables are equal.
	 * Two symbol tables are equal, if they have the same name.
	 * @param obj Another symbol table.
	 * @return true, if both symbol tables denote the same namespace,
	 * false if not.
	 */
	public boolean equals(Object obj) {
		if(obj instanceof SymbolTable)
			return name.equals(((SymbolTable) obj).name);

		return false;
	}

	/**
	 * Get the name of the symbol table.
	 * @return The symbol table's name.
	 */
	public final String getName() {
		return name;
	}

	/**
	 * We also override the hashing scheme
	 * according to the equals method.
	 * @return The hashcode.
	 */
	public int hashCode() {
		return name.hashCode();
	}

	/**
	 * Get the textual representation of a symbol table.
	 * @return The textual representation.
	 */
	public String toString() {
		return symbolMap.toString();
	}

	/**
	 * Enter a keyword into the symbol table.
	 * @param text
	 * @return The keyword symbol.
	 */
	public Symbol enterKeyword(String text) {
		assert !symbolMap.containsKey(text) : "keywords cannot be put twice "
			+ "in the symbol table";

		Symbol sym = new Symbol(text, this) {
			public boolean isKeyword() {
				return true;
			}
		};

		symbolMap.put(text, sym);
		return sym;
	}

	/**
	 * Get a symbol for a string.
	 * @param text The string.
	 * @return The corresponding symbol.
	 */
	public Symbol get(String text) {
		if(!symbolMap.containsKey(text))
			symbolMap.put(text, new Symbol(text, this));

		return symbolMap.get(text);
	}


	/**
	 * Test a symbol for a string.
	 * @param text The string.
	 * @return Whether the symbol is defined.
	 */
	public boolean test(String text) {
		return symbolMap.containsKey(text);
	}
}
