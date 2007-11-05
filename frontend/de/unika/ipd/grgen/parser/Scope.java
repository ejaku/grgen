/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.parser.Symbol.Definition;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/**
 * A namespace.
 */
public class Scope {
	
	/**
	 * The id of this scope. It is basically the number of the child of
	 * the super scope.
	 */
	private final int id;
	
	/** This scope's parent scope. */
	private final Scope parent;
	
	/** The name of this scope. */
	private final String name;
	
	/** An error reoprter for error reporting. */
	private final ErrorReporter reporter;
	
	/** All definitions of this scope. Map from symbol to Symbol.Definition */
	private final Map<Symbol, Symbol.Definition> defs = new HashMap<Symbol, Symbol.Definition>();
	
	/** A map for numbering of anonymous id's */
	private final Map<String, Integer> anonIds = new HashMap<String, Integer>();
	
	/** The children scopes. */
	private final List<Scope> childs = new LinkedList<Scope>();
	
	/**
	 * A list for all occurrences, without a definition on this scope.
	 * Will be used to enter the proper definiton in {@link #leaveScope()}
	 */
	private final List<Symbol.Occurrence> occFixup = new LinkedList<Symbol.Occurrence>();
	
	/** An invalid scope. */
	private static final Scope INVALID = new Scope(null, -1, "<invalid>");
	
	/**
	 * Get an invalid scope.
	 * @return An invalid scope.
	 */
	public static Scope getInvalid() {
		return INVALID;
	}
	
	/**
	 * Make a new root scope.
	 * This constructor may only used for initial root scopes.
	 * @param reporter An error reporter for error message reporting.
	 */
	public Scope(ErrorReporter reporter) {
		this.parent = null;
		this.id = 0;
		this.reporter = reporter;
		this.name = "ROOT";
	}
	
	/**
	 * Internal constructor used by {@link #newScope(String)}.
	 * @param parent The parent scope.
	 * @param id The numeral id of this scope.
	 * @param name The name of this scope (commonly this is the symbol's
	 * string that opened the scope).
	 */
	private Scope(Scope parent, int id, String name) {
		this.parent = parent;
		this.id = id;
		this.name = name;
		this.reporter = parent != null ? parent.reporter : null;
	}
	
	/**
	 * Checks, if a symbol has been defined in the current scope.
	 * Subscopes are not considered.
	 * @param sym The symbol to check for.
	 * @return true, if the symbol was definied in <b>this</b> scope, false
	 * otherwise.
	 */
	public boolean definedHere(Symbol sym) {
		return getLocalDef(sym).isValid();
	}
	
	/**
	 * Checks, if a symbol is legally defined at this position.
	 * First, it is checked, if the symbol has been  defined in this scope, if
	 * not subscopes a visited recursively.
	 * @param sym The symbol to check for.
	 * @return true, if a definition of this symbol is visible in this scope,
	 * false, if not.
	 */
	public boolean defined(Symbol sym) {
		return getCurrDef(sym).isValid();
	}
	
	/**
	 * Returns the local definition of a symbol.
	 * @param sym The symbol whose definition to get.
	 * @return The definition of the symbol, or an invalid definition,
	 * if the symbol has not been defined in this scope.
	 */
	public Symbol.Definition getLocalDef(Symbol sym) {
		Symbol.Definition res = Symbol.Definition.getInvalid();
		
		if(defs.containsKey(sym))
			res = defs.get(sym);
		
		return res;
	}
	
	/**
	 * Get the current definition of a symbol.
	 * @param symbol The symbol whose definition to get.
	 * @return The visible (local or non-local) definition of the symbol,
	 * or an invalid definition, if the symbol's definition is not visible
	 * in this scope.
	 */
	private Definition getCurrDef(Symbol symbol) {
		Symbol.Definition def = getLocalDef(symbol);
		
		if(!(def.isValid() || isRoot()))
			def = parent.getCurrDef(symbol);
		
		return def;
	}
	
	/**
	 * Signal the occurrence of a symbol.
	 * If a symbol occurrs before it is defined, the scope remembers the
	 * occurrence and enters the correct definition for each occurrence
	 * at the moment the scope is left. This can be a local definition in the
	 * scope, or a visible definition in a subscope, or an invalid definition,
	 * if the symbol was used in this scope, but has never been defined to be
	 * visible in this scope.
	 * @param sym The symbol, that occurrs.
	 * @param coords The source code coordinates.
	 * @return The symbol's occurrence.
	 */
	public Symbol.Occurrence occurs(Symbol sym, Coords coords) {
		Symbol.Occurrence occ = sym.occurs(this, coords);
		occFixup.add(occ);
		
		return occ;
	}
	
	/**
	 * Signal the definition of a symbol.
	 * @param sym The symbol that is occurring as a definition.
	 * @return The symbol's definition.
	 */
	public Symbol.Definition define(Symbol sym) {
		return define(sym, new Coords());
	}
	
	/**
	 * Signal the definition of a symbol.
	 * This method should be called, if the parser encounteres a symbol in
	 * a define situatation.
	 * @param sym The symbol that is being defined.
	 * @param coords The source code coordinates for the definition.
	 * @return The symbol's definition.
	 */
	public Symbol.Definition define(Symbol sym, Coords coords) {
		Symbol.Definition def = Symbol.Definition.getInvalid();
		
		if(sym.isKeyword() && sym.getDefinitionCount() > 0) {
			reporter.error(coords, "Cannot redefine keyword \"" + sym + "\"");
			def = Symbol.Definition.getInvalid();
		} else {
			
			if(definedHere(sym)) {
				def = getLocalDef(sym);
				reporter.error(coords, "Symbol \"" + sym + "\" has already been "
								   + "defined in this scope (at: " + def.coords + ")");
			} else {
				try {
					def = sym.define(this, coords);
					defs.put(sym, def);
				} catch(SymbolTableException e) {
					reporter.error(e.getMessage());
				}
			}
		}
		
		return def;
	}
	
	/**
	 * Define an unique anonymous symbol in this scope.
	 * Especially, this can also be done after parsing.
	 * @param name An addition to the symbol's name (for easier readability).
	 * @param symTab The symbol table the symbol is defined in.
	 * @param coords The source code coordinates, that are associated with this
	 * anonymous symbol.
	 * @return A symbol, that could not have been defined in the parsed text,
	 * unique in this scope.
	 */
	public Symbol.Definition defineAnonymous(String name, SymbolTable symTab,
																					 Coords coords) {
		int currId = 0;
		if(anonIds.containsKey(name))
			currId = anonIds.get(name).intValue();
		
		anonIds.put(name, new Integer(currId + 1));
		
		return define(Symbol.makeAnonymous(name + currId, symTab), coords);
	}
	
	/**
	 * Enter a new subscope.
	 * @param name The name of the new subscope.
	 * @return The newly entered scope.
	 */
	public Scope newScope(String name) {
		Scope s = new Scope(this, childs.size(), name);
		childs.add(s);
		return s;
	}
	
	/**
	 * Leave a scope.
	 * @return The parent scope of the one to leave.
	 */
	public Scope leaveScope() {
		
		// fixup all occurrences by entering the correct definition.
		for(Iterator<Symbol.Occurrence> it = occFixup.iterator(); it.hasNext();) {
			Symbol.Occurrence occ = it.next();
			occ.def = getCurrDef(occ.symbol);
		}
		
		return parent;
	}
	
	/**
	 * Check, if a scope is the root scope.
	 * @return true, if the scope is the root scope, false, if not.
	 */
	public boolean isRoot() {
		return parent == null;
	}
	
	/**
	 * Get the parent of the scope.
	 * @return The parent of the scope, or null, if it is the root scope.
	 */
	public Scope getParent() {
		return parent;
	}
	
	public String getName() {
		return name;
	}
	
	public String getPath() {
		String res = "";
		if(!isRoot())
			res = res + parent + ".";
		return res + name;
	}
	
	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return getName();
	}
    
}
