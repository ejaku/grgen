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
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.ast.TestDeclNode;
import de.unika.ipd.grgen.ast.VarDeclNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.Vector;

/**
 * Call of an action with parameters and returns.
 */
public class CallActionNode extends BaseNode {

	static {
		setName(CallActionNode.class, "call action");
	}

	private IdentNode actionUnresolved;
	private CollectNode<IdentNode> paramsUnresolved;
	private CollectNode<BaseNode> returnsUnresolved;

	private TestDeclNode action;
	private VarDeclNode booleVar;
	protected CollectNode<DeclNode> params;
	protected CollectNode<VarDeclNode> returns;

	/**
	 * @param    ruleUnresolved      an IdentNode: thr rule/test name
	 * @param    paramsUnresolved    a  CollectNode<IdentNode>
	 * @param    returnsUnresolved   a  CollectNode<IdentNode>
	 */
	public CallActionNode(Coords coords, IdentNode ruleUnresolved, CollectNode<IdentNode> paramsUnresolved, CollectNode<BaseNode> returnsUnresolved) {
		super(coords);
		this.actionUnresolved = ruleUnresolved;
		this.paramsUnresolved = paramsUnresolved;
		this.returnsUnresolved = returnsUnresolved;
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(actionUnresolved,action,booleVar));
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
	public CollectNode<DeclNode> getParams() {
		assert isResolved();
		return params;
	}

	/**
	 * Returns Returns
	 *
	 * @return    a  CollectNode<IdentNode>
	 */
	public CollectNode<VarDeclNode> getReturns() {
		assert isResolved();
		return returns;
	}

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 */
	public static boolean fixupDefinition(IdentNode id, Scope scope) {
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
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
			error.error(id.getCoords(), "Identifier " + id + " not declared in this scope: " + scope);
		}

		return res;
	}

	private static final DeclarationPairResolver<TestDeclNode, VarDeclNode> actionResolver =
		new DeclarationPairResolver<TestDeclNode, VarDeclNode>(TestDeclNode.class, VarDeclNode.class);

	private static final CollectResolver<DeclNode> paramNodeResolver = new CollectResolver<DeclNode>(new DeclarationResolver<DeclNode>(DeclNode.class));

	private static final CollectResolver<VarDeclNode> varDeclNodeResolver =
		new CollectResolver<VarDeclNode>(new DeclarationResolver<VarDeclNode>(VarDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		Pair<TestDeclNode, VarDeclNode> resolved = actionResolver.resolve(actionUnresolved, this);
		if(resolved!=null)
			if(resolved.fst!=null)
				action = resolved.fst;
			else
				booleVar = resolved.snd;

		successfullyResolved = resolved!=null && (action!=null || booleVar!=null) && successfullyResolved;

		params = paramNodeResolver.resolve(paramsUnresolved, this);
		successfullyResolved = params!=null && successfullyResolved;

		returns = varDeclNodeResolver.resolve(returnsUnresolved, this);
		successfullyResolved = returns!=null && successfullyResolved;

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		boolean res = true;

		/* cannot be checked here, because type info is not yet computed
		 res &= checkParams(action.getParamDecls(), params.getChildren());
		 res &= checkReturns(action.returnFormalParameters, returns);
		 */

		return res;
	}

	/** check after the IR is built */
	protected boolean checkPost() {
		boolean res = true;

		if(action!=null) {
			res &= checkParams(action.pattern.getParamDecls(), params.getChildren());
			res &= checkReturns(action.returnFormalParameters, returns);
		}

		return res;
	}

	/**
	 * Method checkParams
	 *
	 * @param    formalParams        a  Collection<? extends DeclNode>
	 * @param    actualParams        a  Collection<? extends DeclNode>
	 *
	 * @return   a  boolean
	 */
	private boolean checkParams(Collection<? extends DeclNode> formalParams, Collection<? extends DeclNode> actualParams) {
		boolean res = true;
		if(formalParams.size() != actualParams.size()) {
			error.error(getCoords(), "Formal and actual parameter(s) of action " + this.getUseString() + " mismatch in number (" +
							formalParams.size() + " vs. " + actualParams.size() +")");
			res = false;
		} else if(actualParams.size() >0) {
			Iterator<? extends DeclNode> iterAP = actualParams.iterator();
			for(DeclNode formalParam : formalParams) {
				Type     formalParamType = formalParam.getDecl().getDeclType().getType();

				DeclNode actualParam     = iterAP.next();
				Type     actualParamType = actualParam.getDecl().getDeclType().getType();

				if(actualParamType.classify() != formalParamType.classify() ||
				   !(actualParamType instanceof EdgeType && formalParamType instanceof EdgeType ||
						 actualParamType instanceof NodeType && formalParamType instanceof NodeType
					)){
					reportError("Actual param type \"" + actualParamType + "\" and formal param type \"" + formalParamType +
									"\" are incommensurable.");
					res = false;
				}
				if(actualParamType instanceof InheritanceType) {
					InheritanceType fpt = (InheritanceType)formalParamType;
					InheritanceType apt = (InheritanceType)actualParamType;
					if(fpt!=apt && !fpt.isRoot() && !apt.isRoot() &&
					   Collections.disjoint(fpt.getAllSubTypes(), apt.getAllSubTypes()))
						reportWarning("Formal param type \"" + formalParamType + "\" will never match to actual param type \"" + actualParamType +  "\".");
				}
			}
		}
		return res;
	}

	/**
	 * Method checkReturns
	 *
	 * @param    formalReturns a  CollectNode<IdentNode>
	 * @param    actualReturns a  CollectNode<VarDeclNode>
	 *
	 * @return   a  boolean
	 */
	private boolean checkReturns(CollectNode<IdentNode> formalReturns, CollectNode<VarDeclNode> actualReturns) {
		boolean res = true;
		// Its ok to have no actual returns, but if there are some, then they have to fit.
		if(actualReturns.children.size() >0 && formalReturns.children.size() != actualReturns.children.size()) {
			error.error(getCoords(), "Formal and actual return-parameter(s) of action " + this.getUseString() + " mismatch in number (" +
							formalReturns.children.size() + " vs. " + actualReturns.children.size() +")");
			res = false;
		} else if(actualReturns.children.size() >0) {
			Iterator<VarDeclNode> iterAR = actualReturns.getChildren().iterator();
			for(IdentNode formalReturn : formalReturns.getChildren()) {
				Type     formalReturnType = formalReturn.getDecl().getDeclType().getType();

				DeclNode actualReturn     = iterAR.next();
				Type     actualReturnType = actualReturn.getDecl().getDeclType().getType();

				if(actualReturnType.classify()!=formalReturnType.classify() ||
				   !(actualReturnType instanceof EdgeType && formalReturnType instanceof EdgeType ||
						 actualReturnType instanceof NodeType && formalReturnType instanceof NodeType
					)){
					reportError("Actual return type \"" + actualReturnType + "\" and formal return type \"" + formalReturnType +
									"\" are incommensurable.");
					res = false;
				}
				if(actualReturnType instanceof InheritanceType) {
					InheritanceType frt = (InheritanceType)formalReturnType;
					InheritanceType art = (InheritanceType)actualReturnType;
					if(!frt.isCastableTo(art)) {
						reportError("Instances of formal return type \"" + formalReturnType + "\" cannot be assigned to a variable \"" +
										actualReturn + "\" of type \"" + actualReturnType +  "\".");
						res = false;
					}
				}
			}
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		assert false;
		return Bad.getBad(); // TODO fix this
	}
}



