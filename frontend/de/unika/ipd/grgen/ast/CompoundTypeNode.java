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
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;


/**
 * Base class for all AST nodes representing compound types.
 * @note The scope stored in the node
 * (accessible via {@link BaseNode#getScope()}) is the scope,
 * this compound type owns, not the scope it is declared in.
 */
public abstract class CompoundTypeNode extends DeclaredTypeNode
	implements ScopeOwner
{
	/** Checker for the body of the compound type. */
	private final Checker bodyChecker;

	/** Index of the body collect node. */
	private int bodyIndex;

	/** Create compound type AST subnode out of subclass
	 * @param bodyIndex index of the body collect node within children
	 * @param bodyChecker checker for the body compound type */
	protected CompoundTypeNode(int bodyIndex, Checker bodyChecker)
	{
		this.bodyIndex = bodyIndex;
		this.bodyChecker = bodyChecker;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result
		
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		if(successfullyChecked) {
			assert(!isTypeChecked());
			successfullyChecked = typeCheckLocal();
			nodeTypeCheckedSetResult(successfullyChecked);
		}
		
		for(int i=0; i<children(); ++i) {
			successfullyChecked = getChild(i).check() && successfullyChecked;
		}
		return successfullyChecked;
	}
	
	public boolean fixupDefinition(IdentNode id) {
		return fixupDefinition(id, true);
	}

	protected boolean fixupDefinition(IdentNode id, boolean reportErr)
	{
		Scope scope = getScope();

		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getLocalDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// If this definition is valid, i.e. it exists, 
		// the definition of the ident is rewritten to this definition, 
		// else, an error is emitted, 
		// since this ident was supposed to be defined in this scope.
		if(res) {
			id.setSymDef(def);
		} else {
			if(reportErr) {
				reportError("Identifier " + id + " not declared in this scope: "
					+ scope);
			}
		}

		return res;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return checkChild(bodyIndex, bodyChecker);
	}
}
