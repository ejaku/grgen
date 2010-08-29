/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
import de.unika.ipd.grgen.ir.MapType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.SetType;
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

	/*
	 * This introduces an ExecVar definition if an identifier is not defined
	 * to support the usage-is-definition policy of the graph global variables in the sequences.
	 * Note: an (x)=r() & (x:A)=r() error will not be found due to the grgen symbol table and the fixupDefinition
	 * not taking care of the position of the definition compared to the uses
	 * (which makes sense for every other construct of the grgen language);
	 * this error will be caught later on when the xgrs is processed by the libgr sequence parser and symbol table.
	 */
	public void addImplicitDefinitions() {
		for(int i=0; i<returnsUnresolved.children.size(); ++i)	
		{
			if(!(returnsUnresolved.children.get(i) instanceof IdentNode)) {
				continue;
			}
			IdentNode id = (IdentNode)returnsUnresolved.children.get(i);

			debug.report(NOTE, "Implicit definition for " + id + " in scope " + getScope());
		
			// Get the definition of the ident's symbol local to the owned scope.
			Symbol.Definition def = getScope().getCurrDef(id.getSymbol());
			debug.report(NOTE, "definition is: " + def);
	
			// If this definition is valid, i.e. it exists, it will be used
			// else, an ExecVarDeclNode of this name is added to the scope
			if(def.isValid()) {
				id.setSymDef(def);
			} else {
				Symbol.Definition vdef = getScope().define(id.getSymbol(), id.getCoords());
				id.setSymDef(vdef);
				vdef.setNode(id);
				getScope().leaveScope();
				ExecVarDeclNode evd = new ExecVarDeclNode(id, new UntypedExecVarTypeNode());
				id.setDecl(evd);
				returnsUnresolved.children.set(i, evd);
			}
		}
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
		addImplicitDefinitions();
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
			error.error(getCoords(), "Formal and actual parameter(s) of action " + actionUnresolved.toString()
					+ " mismatch in number (" + formalParams.size() + " vs. " + actualParams.size() +")");
			res = false;
		} else if(actualParams.size() > 0) {
			Iterator<? extends ExprNode> iterAP = actualParams.iterator();
			int paramCounter = 1;
			for(DeclNode formalParam : formalParams) {
				TypeNode formalParameterType;
				if(formalParam instanceof EdgeInterfaceTypeChangeNode) {
					formalParameterType = ((EdgeInterfaceTypeChangeNode)formalParam).interfaceType.getDeclType();
				} else if(formalParam instanceof NodeInterfaceTypeChangeNode) {
					formalParameterType = ((NodeInterfaceTypeChangeNode)formalParam).interfaceType.getDeclType();
				} else {
					formalParameterType = formalParam.getDecl().getDeclType();
				}
				Type     formalParamType = formalParameterType.getType();

				ExprNode actualParam     = iterAP.next();
				TypeNode actualParameterType = actualParam.getType();
				Type     actualParamType = actualParameterType.getType();

				if(actualParamType.classify()==Type.IS_UNTYPED_EXEC_VAR_TYPE) continue;

				boolean incommensurable = false;

				// Formal param type is a variable?
				if(formalParamType.classify() != Type.IS_UNKNOWN) {
					// Do types match?
					if(actualParamType.classify() != formalParamType.classify())
						incommensurable = true;		// No => illegal
					else {
						if(actualParamType instanceof SetType) {
							SetType apt = (SetType)actualParamType;
							SetType fpt = (SetType)formalParamType;
							if(apt.getValueType().classify()!=fpt.getValueType().classify()) {
								reportError("Set value types are incommensurable. ("+paramCounter+". argument)");
								incommensurable = true;
							}
						}
						if(actualParamType instanceof MapType) {
							MapType apt = (MapType)actualParamType;
							MapType fpt = (MapType)formalParamType;
							if(apt.getValueType().classify()!=fpt.getValueType().classify()) {
								reportError("Map value types are incommensurable. ("+paramCounter+". argument)");
								incommensurable = true;
							}
							if(apt.getKeyType().classify()!=fpt.getKeyType().classify()) {
								reportError("Map key types are incommensurable. ("+paramCounter+". argument)");
								incommensurable = true;
							}
						}
					}
				}
				// No, are formal and actual param types of same kind?
				else if(!(actualParamType instanceof EdgeType && formalParamType instanceof EdgeType ||
						 actualParamType instanceof NodeType && formalParamType instanceof NodeType))
					incommensurable = true;			// No => illegal

				if(incommensurable) {
					reportError("Actual param type \"" + actualParamType
							+ "\" and formal param type \"" + formalParamType
							+ "\" are incommensurable. ("+paramCounter+". argument)");
					res = false;
				}
				else if(actualParamType instanceof InheritanceType) {
					if(!actualParameterType.isCompatibleTo(formalParameterType)) {
						reportError("The "+paramCounter+". argument is not the same or a subtype of the formal parameter");
					} else {
						InheritanceType fpt = (InheritanceType)formalParamType;
						InheritanceType apt = (InheritanceType)actualParamType;
						if(fpt!=apt && !fpt.isRoot() && !apt.isRoot()
								&& Collections.disjoint(fpt.getAllSubTypes(), apt.getAllSubTypes()))
							reportWarning("Formal param type \"" + formalParamType
									+ "\" will never match to actual param type \"" + actualParamType +  "\".");
					}
				}
				++paramCounter;
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
	private boolean checkReturns(CollectNode<TypeNode> formalReturns, CollectNode<ExecVarDeclNode> actualReturns) {
		boolean res = true;
		// It is ok to have no actual returns, but if there are some, then they have to fit.
		if(actualReturns.children.size() > 0 && formalReturns.children.size() != actualReturns.children.size()) {
			error.error(getCoords(), "Formal and actual return-parameter(s) of action " + actionUnresolved.toString()
					+ " mismatch in number (formal:" + formalReturns.children.size()
					+ " vs. actual:" + actualReturns.children.size() +")");
			res = false;
		} else if(actualReturns.children.size() > 0) {
			Iterator<ExecVarDeclNode> iterAR = actualReturns.getChildren().iterator();
			for(TypeNode formalReturn : formalReturns.getChildren()) {
				Type     formalReturnType = formalReturn.getType();

				DeclNode actualReturn     = iterAR.next();
				Type     actualReturnType = actualReturn.getDecl().getDeclType().getType();
				
				if(actualReturnType.classify()==Type.IS_UNTYPED_EXEC_VAR_TYPE) continue;

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
							+ "\" of action " + actionUnresolved.toString() + " are incommensurable.");
					res = false;
				}

				if(actualReturnType instanceof InheritanceType) {
					InheritanceType frt = (InheritanceType)formalReturnType;
					InheritanceType art = (InheritanceType)actualReturnType;
					if(!frt.isCastableTo(art)) {
						reportError("Instances of formal return type \"" + formalReturnType + "\" cannot be assigned to a variable \"" +
										actualReturn + "\" of type \"" + actualReturnType +  "\" (action " + actionUnresolved.toString() + ").");
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
