/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;


/**
 * The base class for a compound type.
 * @note The scope stored in the node 
 * (accessible via {@link BaseNode#getScope()}) is the scope, 
 * this compound type owns, not the scope it is declared in.
 */
public abstract class CompoundTypeNode extends DeclaredTypeNode
	implements ScopeOwner {
	
	/** Checker for the body of the compound type. */		
	private final Checker bodyChecker;
		
	/** Index of the body collect node. */
	private int bodyIndex;
		
	protected CompoundTypeNode(int bodyIndex, 
														 Checker bodyChecker,
														 Resolver bodyResolver) {
		this.bodyIndex = bodyIndex;
		this.bodyChecker = bodyChecker;
		addResolver(bodyIndex, bodyResolver);
	}
		
	public boolean fixupDefinition(IdentNode id) {
		return fixupDefinition(id, true);
	}
	
	protected boolean fixupDefinition(IdentNode id, boolean reportErr) {
		Scope scope = getScope();
		
		debug.entering();
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);
		
		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getLocalDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);
		
		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		/*
		 * If this definition is valid, i.e. it exists, the definition 		
		 * of the ident is rewritten to this definition, else, an error
		 * is emitted, since this ident was supposed to be defined in this
		 * scope.
		 */
		if(res)
			id.setSymDef(def);
		else
			if(reportErr)
				reportError("Identifier " + id + " not declared in this scope: "
					+ scope);
				
		debug.leaving();
		return res;
	}

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
    return checkChild(bodyIndex, bodyChecker);
  }

}
