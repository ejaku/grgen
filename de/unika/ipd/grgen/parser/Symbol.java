/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.ast.IdentNode;

/**
 * A lexical symbol.
 */
public class Symbol {
	
	private int definitions = 0;
	
	/** A current id number symbols.*/
	private static int currId = 0;
	
	/** The id for this symbol. */
	private int id = currId++;
	
	/** The string of the symbol. */
	private String text;
	
	/**
	 * An occurrence of a symbol.
	 */
	static public class Occurrence {
		
		/** The scope in which the symbol occurred. */
		protected Scope scope;
		
		/** The source file coordinates where the symbol occurred. */
		protected Coords coords;
		
		/** The symbol that occurred. */
		protected Symbol symbol;
		
		/**
		 * The corresponding definition of the symbol.
		 * Points to itsself, if this occurrence is a definition,
		 */
		protected Definition def;
		
		/**
		 * Make a new occurrence.
		 * @param sc The scope where the symbol occurred,
		 * @param c The source file coordinates.
		 * @param sym The symbol that occurred.
		 */
		public Occurrence(Scope sc, Coords c, Symbol sym) {
			symbol = sym;
			scope = sc;
			coords = c;
		}
		
		/**
		 * @see java.lang.Object#toString()
		 */
		public String toString() {
			return "" + symbol + "(" + coords + "," + scope + ")";
		}
		
		/**
		 * Get the occurring symbol.
		 * @return The symbol.
		 */
		public Symbol getSymbol() {
			return symbol;
		}
		
		/**
		 * Get the source code coordinates.
		 * @return The coordinates.
		 */
		public Coords getCoords() {
			return coords;
		}
		
		/**
		 * Get the symbol's definition.
		 * @return The definition.
		 */
		public Definition getDefinition() {
			return def;
		}
		
		/**
		 * Set the definition for a symbol occurrence.
		 * @param def The coresponding definition.
		 */
		public void setDefinition(Definition def) {
			this.def = def;
		}
		
	}
	
	/**
	 * The definition of a symbol.
	 * Especially, a definition is an occurrence, that defines an identifier.
	 */
	static public class Definition extends Occurrence {
		/**
		 * An AST ident node for this definition.
		 * This is needed, because other ident nodes representing the same
		 * identifier have to resolve the ident node of the definition to
		 * get the defined entity.
		 */
		protected IdentNode node;
		
		private static final Definition INVALID =
			new Definition(Scope.getInvalid(), Coords.INVALID, Symbol.INVALID);
		
		/**
		 * Make an invalid definition.
		 * @return An invalid definition.
		 */
		public static Definition getInvalid() {
			return INVALID;
		}
		
		/**
		 * Make a new symbol definition.
		 * @param sc The scope in which the symbol is defined.
		 * @param c The source code coordinates where the symbol was defined.
		 * @param sym The symbol, that was defined.
		 */
		public Definition(Scope sc, Coords c, Symbol sym) {
			super(sc, c, sym);
			def = this;
		}
		
		/**
		 * Checks the validity of a definition.
		 * @return true, if the definition is valid.
		 */
		public boolean isValid() {
			return symbol != Symbol.INVALID;
		}
		
		/**
		 * Get the AST ident node for this definition.
		 * @return The AST node for this definition.
		 */
		public IdentNode getNode() {
			return node;
		}
		
		/**
		 * Set an AST node for this definition.
		 * @param node An AST ident node.
		 */
		public void setNode(IdentNode node) {
			this.node = node;
		}
		
	}
	
	/** An invalid symbol. */
	protected static final Symbol INVALID = new Symbol(null);
	
	/**
	 * Make a new symbol.
	 * @param text The text of the symbol.
	 */
	public Symbol(String text) {
		this.text = text;
	}
	
	public Occurrence occurs(Scope sc, Coords c) {
		return new Occurrence(sc, c, this);
	}
	
	public Definition define(Scope sc, Coords c) {
		definitions++;
		return new Definition(sc, c, this);
	}
	
	public String getText() {
		return text != null ? text : "<invalid>";
	}
	
	public String toString() {
		return getText();
	}
	
	public boolean isKeyword() {
		return false;
	}
	
	public int getDefinitionCount() {
		return definitions;
	}
	
	/**
	 * Make a anonymous symbol.
	 * This symbol could not have been declared somewhere in the parsed text.
	 * So, it must contain a character, that is not allowed in the language's
	 * identifier rule.
	 * @param name An addition to the name of the symbol.
	 * @return An anonymous symbol.
	 */
	public static Symbol makeAnonymous(String name) {
		return new Symbol("$" + name);
	}
	
}
