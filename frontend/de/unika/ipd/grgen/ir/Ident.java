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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.parser.SymbolTable;
import de.unika.ipd.grgen.util.Annotated;
import de.unika.ipd.grgen.util.Annotations;
import java.util.HashMap;

/**
 * A class representing an identifier.
 */
public class Ident extends IR implements Comparable<Ident>, Annotated {
	
	/** Symbol table recording all identifiers. */
	private static HashMap<String, Ident> identifiers = new HashMap<String, Ident>();
	
	/** Text of the identifier */
	private final String text;
	
	private final SymbolTable symTab;
	
	private final Scope scope;
	
	/** The scope/namespace the identifier was defined in. */
	//private final String scope;
	
	/** location of the definition of the identifier */
	private final Coords def;
	
	/** The annotations for the identifier. */
	private final Annotations annots;
	
	/** A precomputed hash code. */
	private final int precomputedHashCode;
	
	/**
	 * New Identifier.
	 * @param text The text of the identifier.
	 * @param scope The scope/namespace of the identifier.
	 * @param def The location of the definition of the identifier.
	 * @param annots The annotations of this identifier 
	 * (Each identifier can carry several annotations which serve as meta information usable by backend components).
	 */
	private Ident(String text, SymbolTable symTab, Scope scope, Coords def, Annotations annots) {
		super("ident");
		this.text = text;
		this.scope = scope;
		this.symTab = symTab;
		this.def = def;
		this.annots = annots;
		this.precomputedHashCode = (symTab.getName() + ":" + text).hashCode();
	}
	
	/**
	 * New Identifier.
	 * @param text The text of the identifier.
	 * @param def The location of the definition of the identifier.
	 */
	private Ident(String text, Coords def, Annotations annots) {
		this(text, SymbolTable.getInvalid(), Scope.getInvalid(), def, annots);
	}
  
	/** The string of an identifier is its text.
	 *  @see java.lang.Object#toString() */
	public String toString() {
		return text;
	}
	
	/** @return The location where the identifier was defined. */
	public Coords getCoords() {
		return def;
	}
  
	/**
	 * @see java.lang.Object#equals(java.lang.Object)
	 * Two identifiers are equal, if they have the same names and the same location of definition.
	 */
	public boolean equals(Object obj) {
		boolean res = false;
		if(obj instanceof Ident) {
			Ident id = (Ident) obj;
			res = text.equals(id.text) && scope.equals(id.scope);
		}
		return res;
	}
  
	/**
	 * Identifier factory.
	 * Use this to get a new Identifier using a string and a location
	 * @param text The text of the identifier.
	 * @param scope The scope/namespace the identifier was defined in.
	 * @param loc The location of the identifier.
	 * @param annots The annotations of this identifier.
	 * @return The IR identifier object for the desired identifier.
	 */
	public static Ident get(String text, Symbol.Definition def, Annotations annots) {
		Coords loc = def.getCoords();
		String key = text + "#" + loc.toString();
		Ident res;
		
		if(identifiers.containsKey(key)) {
			res = identifiers.get(key);
		} else {
			res = new Ident(text, def.getSymbol().getSymbolTable(), def.getScope(), loc, annots);
			identifiers.put(key, res);
		}
		return res;
	}
	
	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo() */
	public String getNodeInfo() {
		return super.getNodeInfo() + "\nCoords: " + def + "\nScope: " + scope.getPath();
	}
	
	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return getName() + " " + text;
	}
	
	/**
	 * Compare an identifier to another.
	 * @param obj The other identifier.
	 * @return -1, 0, 1, respectively.
	 */
	public int compareTo(Ident id) {
		return toString().compareTo(id.toString());
	}
	
	public int hashCode() {
		return precomputedHashCode;
	}
	
	public Scope getScope() {
		return scope;
	}
	
	public SymbolTable getSymbolTable() {
		return symTab;
	}
	
	/** @return The annotations. */
	public Annotations getAnnotations() {
		return annots;
	}
}
