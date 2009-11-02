/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * Call of an action with parameters and returns.
 */
public class CallActionNode extends BaseNode {

	static {
		setName(CallActionNode.class, "call action");
	}

	private IdentNode actionUnresolved;
	private CollectNode<BaseNode> paramsUnresolved;
	private CollectNode<BaseNode> returnsUnresolved;

	private TestDeclNode action;
	private ExecVarDeclNode booleVar;
	protected CollectNode<ExprNode> params;
	protected CollectNode<ExecVarDeclNode> returns;

	/**
	 * @param    ruleUnresolved      an IdentNode: thr rule/test name
	 * @param    paramsUnresolved    a  CollectNode<BaseNode>
	 * @param    returnsUnresolved   a  CollectNode<BaseNode>
	 */
	public CallActionNode(Coords coords, IdentNode ruleUnresolved, CollectNode<BaseNode> paramsUnresolved,
			CollectNode<BaseNode> returnsUnresolved) {
		super(coords);
		this.actionUnresolved = ruleUnresolved;
		this.paramsUnresolved = paramsUnresolved;
		this.returnsUnresolved = returnsUnresolved;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(actionUnresolved,action,booleVar));
		children.add(getValidVersion(paramsUnresolved,params));
		children.add(getValidVersion(returnsUnresolved,returns));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
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
	protected CollectNode<ExprNode> getParams() {
		assert isResolved();
		return params;
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
			id.reportError("Identifier \"" + id + "\" not declared in this scope: " + scope);
		}

		return res;
	}

	private static final DeclarationPairResolver<TestDeclNode, ExecVarDeclNode> actionResolver =
		new DeclarationPairResolver<TestDeclNode, ExecVarDeclNode>(TestDeclNode.class, ExecVarDeclNode.class);

	private static final CollectResolver<ExprNode> paramNodeResolver =
		new CollectResolver<ExprNode>(new DeclarationResolver<ExprNode>(ExprNode.class));

	private static final CollectResolver<ExecVarDeclNode> varDeclNodeResolver =
		new CollectResolver<ExecVarDeclNode>(new DeclarationResolver<ExecVarDeclNode>(ExecVarDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		Pair<TestDeclNode, ExecVarDeclNode> resolved = actionResolver.resolve(actionUnresolved, this);
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
	@Override
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
	private boolean checkParams(Collection<? extends DeclNode> formalParams, Collection<? extends ExprNode> actualParams) {
		boolean res = true;
		if(formalParams.size() != actualParams.size()) {
			error.error(getCoords(), "Formal and actual parameter(s) of action " + this.getUseString()
					+ " mismatch in number (" + formalParams.size() + " vs. " + actualParams.size() +")");
			res = false;
		} else if(actualParams.size() > 0) {
			Iterator<? extends ExprNode> iterAP = actualParams.iterator();
			for(DeclNode formalParam : formalParams) {
				Type     formalParamType = formalParam.getDecl().getDeclType().getType();

				ExprNode actualParam     = iterAP.next();
				Type     actualParamType = actualParam.getType().getType();

				boolean incommensurable = false;

				// Formal param type is a variable?
				if(formalParamType.classify() != Type.IS_UNKNOWN) {
					// Do types match?
					if(actualParamType.classify() != formalParamType.classify())
						incommensurable = true;		// No => illegal
				}
				// No, are formal and actual param types of same kind?
				else if(!(actualParamType instanceof EdgeType && formalParamType instanceof EdgeType ||
						 actualParamType instanceof NodeType && formalParamType instanceof NodeType))
					incommensurable = true;			// No => illegal

				if(incommensurable) {
					reportError("Actual param type \"" + actualParamType
							+ "\" and formal param type \"" + formalParamType
							+ "\" are incommensurable.");
					res = false;
				}
				else if(actualParamType instanceof InheritanceType) {
					InheritanceType fpt = (InheritanceType)formalParamType;
					InheritanceType apt = (InheritanceType)actualParamType;
					if(fpt!=apt && !fpt.isRoot() && !apt.isRoot()
							&& Collections.disjoint(fpt.getAllSubTypes(), apt.getAllSubTypes()))
						reportWarning("Formal param type \"" + formalParamType
								+ "\" will never match to actual param type \"" + actualParamType +  "\".");
				}
			}
		}
		return res;
	}

	/**
	 * Method checkReturns
	 *
	 * @param    formalReturns a  CollectNode<IdentNode>
	 * @param    actualReturns a  CollectNode<ExecVarDeclNode>
	 *
	 * @return   a  boolean
	 */
	private boolean checkReturns(CollectNode<IdentNode> formalReturns, CollectNode<ExecVarDeclNode> actualReturns) {
		boolean res = true;
		// It is ok to have no actual returns, but if there are some, then they have to fit.
		if(actualReturns.children.size() > 0 && formalReturns.children.size() != actualReturns.children.size()) {
			error.error(getCoords(), "Formal and actual return-parameter(s) of action " + this.getUseString()
					+ " mismatch in number (" + formalReturns.children.size()
					+ " vs. " + actualReturns.children.size() +")");
			res = false;
		} else if(actualReturns.children.size() > 0) {
			Iterator<ExecVarDeclNode> iterAR = actualReturns.getChildren().iterator();
			for(IdentNode formalReturn : formalReturns.getChildren()) {
				Type     formalReturnType = formalReturn.getDecl().getDeclType().getType();

				DeclNode actualReturn     = iterAR.next();
				Type     actualReturnType = actualReturn.getDecl().getDeclType().getType();

				boolean incommensurable = false;

				// Formal return type is a variable?
				if(formalReturnType.classify() != Type.IS_UNKNOWN) {
					// Do types match?
					if(actualReturnType.classify() != formalReturnType.classify())
						incommensurable = true;		// No => illegal
				}
				// No, are formal and actual return types of same kind?
				else if(!(actualReturnType instanceof EdgeType && formalReturnType instanceof EdgeType ||
						 actualReturnType instanceof NodeType && formalReturnType instanceof NodeType))
					incommensurable = true;			// No => illegal

				if(incommensurable) {
					reportError("Actual return type \"" + actualReturnType
							+ "\" and formal return type \"" + formalReturnType
							+ "\" are incommensurable.");
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
	@Override
	protected IR constructIR() {
		assert false;
		return Bad.getBad(); // TODO fix this
	}
}
