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
	
	/** The string - symbol map. */
	HashMap symbolMap;
	
	/**
	 * Make a new symbol table.
	 */
	public SymbolTable() {
		symbolMap = new HashMap();
	}
	
	/**
	 * Enter a keyword into the symbol table.
	 * @param text
	 * @return The keyword symbol.
	 */
	public Symbol enterKeyword(String text) {
		assert !symbolMap.containsKey(text) : "keywords cannot be put twice "
			+ "in the symbol table";
		
		Symbol sym = new Symbol(text) {
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
			symbolMap.put(text, new Symbol(text));
		
		return (Symbol) symbolMap.get(text);
	}
	
	public String toString() {
		return symbolMap.toString();
	}
}
