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


import de.unika.ipd.grgen.ast.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;
import java.util.Collection;
import java.util.Iterator;
import java.util.Vector;

/**
 * An expression that results from a declared identifier.
 */
public class CallActionNode extends BaseNode implements ScopeOwner {

	static {
		setName(CallActionNode.class, "call action");
	}

	private IdentNode actionUnresolved;
	private TestDeclNode action;
	private CollectNode<IdentNode> paramsUnresolved;
	private CollectNode<IdentNode> returnsUnresolved;

	protected CollectNode<ConstraintDeclNode> params;
	protected CollectNode<ConstraintDeclNode> returns;

	/**
	 * @param    ruleUnresolved      an IdentNode: thr rule/test name
	 * @param    paramsUnresolved    a  CollectNode<IdentNode>
	 * @param    returnsUnresolved   a  CollectNode<IdentNode>
	 */
	public CallActionNode(Coords coords, IdentNode ruleUnresolved, CollectNode<IdentNode> paramsUnresolved, CollectNode<IdentNode> returnsUnresolved) {
		super(coords);
		this.actionUnresolved = ruleUnresolved;
		this.paramsUnresolved = paramsUnresolved;
		this.returnsUnresolved = returnsUnresolved;
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(actionUnresolved,action));
		children.add(getValidVersion(paramsUnresolved,params));
		children.add(getValidVersion(returnsUnresolved,returns));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("action");
		childrenNames.add("params");
		childrenNames.add("returns");
		return childrenNames;
	}

	/**
	 * Returns Params
	 *
	 * @return    a  CollectNode<IdentNode>
	 */
	public CollectNode<ConstraintDeclNode> getParams() {
		assert isResolved();
		return params;
	}

	/**
	 * Returns Returns
	 *
	 * @return    a  CollectNode<IdentNode>
	 */
	public CollectNode<ConstraintDeclNode> getReturns() {
		assert isResolved();
		return returns;
	}

	/*
	 * This sets the symbol defintion to the right place, if the defintion is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 */
	public boolean fixupDefinition(IdentNode id) {
		Scope scope = id.getScope().getIdentNode().getScope();

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
			reportError("Identifier " + id + " not declared in this scope: " + scope);
		}

		return res;
	}

	private static DeclarationResolver<TestDeclNode> actionResolver = new DeclarationResolver<TestDeclNode>(TestDeclNode.class);
	private static CollectResolver<ConstraintDeclNode> entityResolver = new CollectResolver<ConstraintDeclNode>(
		new DeclarationResolver<ConstraintDeclNode>(ConstraintDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	/**
	 * Method resolveLocal
	 *
	 * @return   a boolean
	 *
	 */
	/**
	 * Method resolveLocal
	 *
	 * @return   a boolean
	 *
	 */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		fixupDefinition(actionUnresolved);
		action = actionResolver.resolve(actionUnresolved);
		successfullyResolved = action!=null && successfullyResolved;

		//TODO this is wrong!
		params = entityResolver.resolve(paramsUnresolved, this);
		successfullyResolved = params!=null && successfullyResolved;

		returns = entityResolver.resolve(returnsUnresolved, this);
		successfullyResolved = returns!=null && successfullyResolved;

		return successfullyResolved;
	}

	/**
	 * check if actual return entities are conformant
	 * to the formal return parameters.
	 */
	// TODO: check types
	protected boolean checkReturnParams(CollectNode<IdentNode> typeReturns, CollectNode<IdentNode> actualReturns) {
		boolean returnTypes = true;

		/*
		 System.out.println("\n*** this          = " + this.getClass());
		 System.out.println("    this          = " + this.getChildren());
		 System.out.println("*** typeReturns   = "   + typeReturns);
		 System.out.println("    typeReturns   = "   + typeReturns.getChildren());
		 System.out.println("*** actualReturns = " + actualReturns);
		 System.out.println("    actualReturns = " + actualReturns.getChildren());
		 */

		if(actualReturns.children.size() != typeReturns.children.size()) {
			error.error(getCoords(), "Actual and formal return-parameter count mismatch (" +
							actualReturns.children.size() + " vs. " + typeReturns.children.size() +")");
			returnTypes = false;
		} else {
			Iterator<IdentNode> itAR = actualReturns.children.iterator();

			for(BaseNode n : typeReturns.getChildren()) {
				IdentNode       tReturnAST  = (IdentNode)n;
				InheritanceType tReturn     = (InheritanceType)tReturnAST.getDecl().getDeclType().checkIR(InheritanceType.class);

				IdentNode       aReturnAST  = itAR.next();
				InheritanceType aReturnType = (InheritanceType)aReturnAST.getDecl().getDeclType().checkIR(InheritanceType.class);

				if(!aReturnType.isCastableTo(tReturn)) {
					error.error(aReturnAST.getCoords(), "Actual return-parameter is not conformant to formal parameter (" +
									aReturnType + " not castable to " + tReturn + ")");
					returnTypes = false;
				}
			}
		}

		return returnTypes;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		boolean res = true;

		res &= checkParams(action.param, params);
		res &= checkReturns(action.returnFormalParameters, returns);

		return res;
	}

	/**
	 * Method checkParams
	 *
	 * @param    param               a  CollectNode<ConstraintDeclNode>
	 * @param    params              a  CollectNode<ConstraintDeclNode>
	 *
	 * @return   a  boolean
	 */
	private boolean checkParams(CollectNode<ConstraintDeclNode> formalParams, CollectNode<ConstraintDeclNode> actualParams) {
		// TODO
		boolean res = true;
		if(formalParams.children.size() != actualParams.children.size()) {
			error.error(getCoords(), "Formal and actural parameter(s) of action " + this.getUseString() + " mismatch in number (" +
							formalParams.children.size() + " vs. " + actualParams.children.size() +")");
			res = false;
		} else {
		}
		return res;
	}

	/**
	 * Method checkReturns
	 *
	 * @param    returnFormalParameters a  CollectNode<IdentNode>
	 * @param    returns                a  CollectNode<ConstraintDeclNode>
	 *
	 * @return   a  boolean
	 */
	private boolean checkReturns(CollectNode<IdentNode> formalReturns, CollectNode<ConstraintDeclNode> actualReturns) {
		// TODO
		boolean res = true;
		if(formalReturns.children.size() != actualReturns.children.size()) {
			error.error(getCoords(), "Formal and actural return-parameter(s) of action " + this.getUseString() + " mismatch in number (" +
							formalReturns.children.size() + " vs. " + actualReturns.children.size() +")");
			res = false;
		} else {
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		return Bad.getBad(); // TODO fix this
	}
}

