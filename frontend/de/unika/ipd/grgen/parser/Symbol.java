/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

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

	/**
	 * An occurrence of a symbol.
	 */
	public static class Occurrence {

		/** The scope in which the symbol occurred. */
		protected final Scope scope;

		/** The source file coordinates where the symbol occurred. */
		protected final Coords coords;

		/** The symbol that occurred. */
		protected final Symbol symbol;

		/**
		 * The corresponding definition of the symbol.
		 * Points to itself, if this occurrence is a definition,
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
		 * Get the scope of occurrence.
		 * @return The scope.
		 */
		public Scope getScope() {
			return scope;
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
		 * @param def The corresponding definition.
		 */
		public void setDefinition(Definition def) {
			this.def = def;
		}

	}

	/**
	 * The definition of a symbol.
	 * Especially, a definition is an occurrence, that defines an identifier.
	 */
	public static class Definition extends Occurrence {
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
	private static final Symbol INVALID = new Symbol("<invalid>",
													 SymbolTable.getInvalid());

	/** The number of definitions concerning this symbol. */
	private int definitions = 0;

	/** The symbol table the symbol was defined in. */
	private final SymbolTable symbolTable;

	/** An id counter for assigning ids to symbols*/
	// TODO use or remove it
	// private static int currId = 0;

	/** The id for this symbol. */
	// TODO use or remove it
	// private final int id = currId++;

	/** The string of the symbol. */
	private final String text;

	/**
	 * Make a new symbol.
	 * @param text The text of the symbol.
	 */
	public Symbol(String text, SymbolTable symbolTable) {
		this.text = text;
		this.symbolTable = symbolTable;
	}

	/**
	 * Compare two symbols.
	 * Two symbols are equal, if they represent the same string and
	 * are defined in the same symbol table.
	 * @param obj Another symbol.
	 * @return true, if the both symbols represent the same symbol,
	 * false if not.
	 */
	public boolean equals(Object obj) {
		if(obj instanceof Symbol) {
			Symbol sym = (Symbol) obj;
			return text.equals(sym.getText())
				&& symbolTable.equals(sym.getSymbolTable());
		}

		return false;
	}

	/**
	 * Get the symbol table, the symbol was defined in.
	 * @param The symbol table.
	 */
	public SymbolTable getSymbolTable() {
		return symbolTable;
	}

	/**
	 * Get an occurrence of this symbol.
	 * @param sc The current scope.
	 * @param c The coordinates the occurrence happened.
	 * @return An occurrence of the current symbol.
	 */
	public Occurrence occurs(Scope sc, Coords c) {
		return new Occurrence(sc, c, this);
	}

	/**
	 * Get a definition of the symbol.
	 * @param sc The scope the definition occurrs in.
	 * @param c The coordinates of the definition.
	 * @return The definition.
	 */
	public Definition define(Scope sc, Coords c) throws SymbolTableException {
		if(isKeyword() && definitions > 0)
			throw new SymbolTableException(c, "keyword cannot be redefined");
		else {
			definitions++;
			return new Definition(sc, c, this);
		}
	}

	public String getText() {
		return text != null ? text : "<invalid>";
	}

	public String toString() {
		return getText();
	}

	/**
	 * Is this symbol a keyword.
	 * A keyword symbol cannot be defined.
	 * @return true, if the symbol is a keyword, false if not.
	 */
	public boolean isKeyword() {
		return false;
	}

	/**
	 * Get the number of definitions.
	 * @return The number of times the symbol has been defined.
	 */
	public int getDefinitionCount() {
		return definitions;
	}

	/**
	 * Make an anonymous symbol.
	 * This symbol could not have been declared somewhere in the parsed text.
	 * So, it must contain a character, that is not allowed in the language's
	 * identifier rule.
	 * @param name An addition to the name of the symbol.
	 * @param symTab The symbol table the symbol occurs in.
	 * @return An anonymous symbol.
	 */
	public static Symbol makeAnonymous(String name, SymbolTable symTab) {
		return new Symbol("$" + name, symTab);
	}

}
