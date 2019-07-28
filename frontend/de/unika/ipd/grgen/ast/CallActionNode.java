/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.containers.ArrayType;
import de.unika.ipd.grgen.ir.containers.DequeType;
import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.containers.MapType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.exprevals.ObjectType;
import de.unika.ipd.grgen.ir.containers.SetType;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Symbol;

// todo: the entire exec handling in the frontend is nothing but a dirty hack, clean this

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
	private CollectNode<IdentNode> filterFunctionsUnresolved;

	private boolean isAllBracketed;
	
	private TestDeclNode action;
	private SequenceDeclNode sequence;
	private ExecVarDeclNode booleVar;

	protected CollectNode<ExprNode> params;
	protected CollectNode<ExecVarDeclNode> returns;
	protected CollectNode<FilterFunctionDeclNode> filterFunctions;

	/**
	 * @param    ruleUnresolved      an IdentNode: thr rule/test name
	 * @param    paramsUnresolved    a  CollectNode<BaseNode>
	 * @param    returnsUnresolved   a  CollectNode<BaseNode>
	 */
	public CallActionNode(Coords coords, IdentNode ruleUnresolved, CollectNode<BaseNode> paramsUnresolved,
			CollectNode<BaseNode> returnsUnresolved, CollectNode<IdentNode> filterFunctionsUnresolved, boolean isAllBracketed) {
		super(coords);
		this.actionUnresolved = ruleUnresolved;
		this.paramsUnresolved = paramsUnresolved;
		this.returnsUnresolved = returnsUnresolved;
		this.filterFunctionsUnresolved = filterFunctionsUnresolved;
		this.isAllBracketed = isAllBracketed;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(actionUnresolved,action,sequence,booleVar));
		children.add(getValidVersion(paramsUnresolved,params));
		children.add(getValidVersion(returnsUnresolved,returns));
		if(filterFunctionsUnresolved!=null)
			children.add(getValidVersion(filterFunctionsUnresolved,filterFunctions));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("action");
		childrenNames.add("params");
		childrenNames.add("returns");
		if(filterFunctionsUnresolved!=null)
			childrenNames.add("filter");
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

	private static final DeclarationTripleResolver<TestDeclNode, SequenceDeclNode, ExecVarDeclNode> actionResolver =
		new DeclarationTripleResolver<TestDeclNode, SequenceDeclNode, ExecVarDeclNode>(TestDeclNode.class, SequenceDeclNode.class, ExecVarDeclNode.class);

	private static final CollectResolver<ExprNode> paramNodeResolver =
		new CollectResolver<ExprNode>(new DeclarationResolver<ExprNode>(ExprNode.class));

	private static final CollectResolver<ExecVarDeclNode> varDeclNodeResolver =
		new CollectResolver<ExecVarDeclNode>(new DeclarationResolver<ExecVarDeclNode>(ExecVarDeclNode.class));

	private static final CollectResolver<FilterFunctionDeclNode> filterResolver =
		new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(FilterFunctionDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		addImplicitDefinitions();
		if(!(actionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		}
		if(filterFunctionsUnresolved!=null) {
			for(IdentNode filterFunctionUnresolved : filterFunctionsUnresolved.getChildren()) {
				if(!(filterFunctionUnresolved instanceof PackageIdentNode)) {
					fixupDefinition(filterFunctionUnresolved, filterFunctionUnresolved.getScope());
				}
			}
		}
		Triple<TestDeclNode, SequenceDeclNode, ExecVarDeclNode> resolved =
			actionResolver.resolve(actionUnresolved, this);
		if(resolved!=null) {
			if(resolved.first!=null)
				action = resolved.first;
			else if(resolved.second!=null)
				sequence = resolved.second;
			else
				booleVar = resolved.third;
		}

		successfullyResolved = resolved!=null && (action!=null || sequence!=null || booleVar!=null) && successfullyResolved;

		params = paramNodeResolver.resolve(paramsUnresolved, this);
		successfullyResolved = params!=null && successfullyResolved;

		returns = varDeclNodeResolver.resolve(returnsUnresolved, this);
		successfullyResolved = returns!=null && successfullyResolved;

		if(filterFunctionsUnresolved!=null) {
			filterFunctions = filterResolver.resolve(filterFunctionsUnresolved, this);
			successfullyResolved = filterFunctions!=null && successfullyResolved;
		}
		
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
			res &= checkReturns(action.returnFormalParameters.getChildren(), returns);
		} else if(sequence!=null) {
			Vector<TypeNode> outTypes = new Vector<TypeNode>();
			for(ExecVarDeclNode varDecl : sequence.outParams.getChildren())
				outTypes.add(varDecl.getDeclType());
			res &= checkParams(sequence.inParams.getChildren(), params.getChildren());
			res &= checkReturns(outTypes, returns);
		}
		
		if(action!=null) {
			for(FilterFunctionDeclNode filter : filterFunctions.getChildren()) {
				if(filter.action!=action)
					reportError("Filter " + filter.getIdentNode().toString() + " is defined for action " + filter.action.getIdentNode().toString() + ". It can't be applied to action " + action.getIdentNode().toString());				
			}
		} else {
			if(filterFunctionsUnresolved.size()>0)
				reportError("Match filters can only be applied on tests or rules.");
		}

		return res;
	}

	/**
	 * Method checkParams
	 * @param    formalParams        a  Collection<? extends DeclNode>
	 * @param    actualParams        a  Collection<? extends DeclNode>
	 * @return   a  boolean
	 */
	private boolean checkParams(Collection<? extends DeclNode> formalParams, Collection<? extends ExprNode> actualParams) {
		boolean res = true;
		if(formalParams.size() != actualParams.size()) {
			error.error(getCoords(), "Formal and actual parameter(s) of " + actionUnresolved.toString()
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
				if(formalParamType.classify() != Type.IS_UNKNOWN
						&& formalParamType.classify() != Type.IS_NODE
						&& formalParamType.classify() != Type.IS_EDGE) {
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
						else if(actualParamType instanceof MapType) {
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
						else if(actualParamType instanceof ArrayType) {
							ArrayType apt = (ArrayType)actualParamType;
							ArrayType fpt = (ArrayType)formalParamType;
							if(apt.getValueType().classify()!=fpt.getValueType().classify()) {
								reportError("Array value types are incommensurable. ("+paramCounter+". argument)");
								incommensurable = true;
							}
						}
						else if(actualParamType instanceof DequeType) {
							DequeType apt = (DequeType)actualParamType;
							DequeType fpt = (DequeType)formalParamType;
							if(apt.getValueType().classify()!=fpt.getValueType().classify()) {
								reportError("Deque value types are incommensurable. ("+paramCounter+". argument)");
								incommensurable = true;
							}
						}
					}
				}
				// No, are formal and actual param types of same kind?
				else if(!((actualParamType instanceof EdgeType || actualParamType instanceof ObjectType) && formalParamType instanceof EdgeType ||
						 (actualParamType instanceof NodeType || actualParamType instanceof ObjectType) && formalParamType instanceof NodeType))
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
	 * @param    formalReturns a  Collection<? extends TypeNode>
	 * @param    actualReturns a  CollectNode<ExecVarDeclNode>
	 * @return   a  boolean
	 */
	private boolean checkReturns(Collection<? extends TypeNode> formalReturns, CollectNode<ExecVarDeclNode> actualReturns) {
		boolean res = true;
		// It is ok to have no actual returns, but if there are some, then they have to fit.
		if(actualReturns.children.size() > 0 && formalReturns.size() != actualReturns.children.size()) {
			error.error(getCoords(), "Formal and actual return-parameter(s) of " + actionUnresolved.toString()
					+ " mismatch in number (formal:" + formalReturns.size()
					+ " vs. actual:" + actualReturns.children.size() +")");
			res = false;
		} else if(actualReturns.children.size() > 0) {
			Iterator<ExecVarDeclNode> iterAR = actualReturns.getChildren().iterator();
			for(TypeNode formalReturn : formalReturns) {
				Type     formalReturnType = formalReturn.getType();

				DeclNode actualReturn     = iterAR.next();
				Type     actualReturnType = actualReturn.getDecl().getDeclType().getType();

				if(actualReturnType.classify()==Type.IS_UNTYPED_EXEC_VAR_TYPE) continue;

				boolean incommensurable = false;

				// Formal return type is a variable?
				if(formalReturnType.classify() != Type.IS_UNKNOWN) {
					if(isAllBracketed) {
						// Do types match?
						if(actualReturnType.classify() != Type.IS_ARRAY)
							incommensurable = true;		// No => illegal
						else if(((ArrayType)actualReturnType).getValueType().classify() != formalReturnType.classify())
							incommensurable = true;
					}
					// Do types match?
					else if(actualReturnType.classify() != formalReturnType.classify())
						incommensurable = true;		// No => illegal
				}
				// No, are formal and actual return types of same kind?
				else if(!(actualReturnType instanceof EdgeType && formalReturnType instanceof EdgeType ||
						 actualReturnType instanceof NodeType && formalReturnType instanceof NodeType))
					incommensurable = true;			// No => illegal

				if(incommensurable) {
					reportError("Actual return type \"" + actualReturnType
							+ "\" and formal return type \"" + (isAllBracketed ? "array<" + formalReturnType + ">" : formalReturnType)
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
