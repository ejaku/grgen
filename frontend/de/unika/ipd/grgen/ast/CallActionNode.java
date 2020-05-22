/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Iterator;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
import de.unika.ipd.grgen.ast.decl.FilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.SequenceDeclNode;
import de.unika.ipd.grgen.ast.decl.TestDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeInterfaceTypeChangeNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeInterfaceTypeChangeNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.TypeTypeNode;
import de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Symbol;

// todo: the entire exec handling in the frontend is nothing but a dirty hack, clean this

/**
 * Call of an action with parameters and returns.
 */
public class CallActionNode extends BaseNode
{
	static {
		setName(CallActionNode.class, "call action");
	}

	private IdentNode actionUnresolved;

	private CollectNode<BaseNode> paramsUnresolved;
	private CollectNode<BaseNode> returnsUnresolved;
	private CollectNode<BaseNode> filterFunctionsUnresolved; // only IdentNode in CallActionNode

	private boolean isAllBracketed;

	private TestDeclNode action;
	private SequenceDeclNode sequence;
	private ExecVarDeclNode boolVar;

	public CollectNode<ExprNode> params;
	protected CollectNode<ExecVarDeclNode> returns;
	protected CollectNode<FilterFunctionDeclNode> filterFunctions;

	/**
	 * @param    ruleUnresolved      an IdentNode: thr rule/test name
	 * @param    paramsUnresolved    a  CollectNode<BaseNode>
	 * @param    returnsUnresolved   a  CollectNode<BaseNode>
	 */
	public CallActionNode(Coords coords, IdentNode ruleUnresolved, CollectNode<BaseNode> paramsUnresolved,
			CollectNode<BaseNode> returnsUnresolved, CollectNode<BaseNode> filterFunctionsUnresolved,
			boolean isAllBracketed)
	{
		super(coords);
		this.actionUnresolved = ruleUnresolved;
		this.paramsUnresolved = paramsUnresolved;
		this.returnsUnresolved = returnsUnresolved;
		this.filterFunctionsUnresolved = filterFunctionsUnresolved;
		this.isAllBracketed = isAllBracketed;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(actionUnresolved, action, sequence, boolVar));
		children.add(getValidVersion(paramsUnresolved, params));
		children.add(getValidVersion(returnsUnresolved, returns));
		children.add(getValidVersion(filterFunctionsUnresolved, filterFunctions));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("action");
		childrenNames.add("params");
		childrenNames.add("returns");
		childrenNames.add("filter");
		return childrenNames;
	}

	/**
	 * Returns Params
	 *
	 * @return    a  CollectNode<IdentNode>
	 */
	protected CollectNode<ExprNode> getParams()
	{
		assert isResolved();
		return params;
	}

	public TestDeclNode getAction()
	{
		return action;
	}

	/*
	 * This introduces an ExecVar definition if an identifier is not defined
	 * to support the usage-is-definition policy of the graph global variables in the sequences.
	 * Note: an (x)=r() & (x:A)=r() error will not be found due to the grgen symbol table and the fixupDefinition
	 * not taking care of the position of the definition compared to the uses
	 * (which makes sense for every other construct of the grgen language);
	 * this error will be caught later on when the xgrs is processed by the libgr sequence parser and symbol table.
	 */
	public void addImplicitDefinitions()
	{
		for(int i = 0; i < returnsUnresolved.size(); ++i) {
			if(!(returnsUnresolved.get(i) instanceof IdentNode)) {
				continue;
			}
			IdentNode id = (IdentNode)returnsUnresolved.get(i);

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
				ExecVarDeclNode evd = new ExecVarDeclNode(id, BasicTypeNode.untypedType);
				id.setDecl(evd);
				returnsUnresolved.set(i, evd);
			}
		}
	}

	private static final DeclarationTripleResolver<TestDeclNode, SequenceDeclNode, ExecVarDeclNode> actionResolver =
		new DeclarationTripleResolver<TestDeclNode, SequenceDeclNode, ExecVarDeclNode>(
				TestDeclNode.class, SequenceDeclNode.class, ExecVarDeclNode.class);

	private static final CollectResolver<ExprNode> paramNodeResolver =
		new CollectResolver<ExprNode>(new DeclarationResolver<ExprNode>(ExprNode.class));

	private static final CollectResolver<ExecVarDeclNode> varDeclNodeResolver =
		new CollectResolver<ExecVarDeclNode>(new DeclarationResolver<ExecVarDeclNode>(ExecVarDeclNode.class));

	private static final CollectResolver<FilterFunctionDeclNode> filterResolver =
		new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(
				FilterFunctionDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		addImplicitDefinitions();
		if(!(actionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		}

		Triple<TestDeclNode, SequenceDeclNode, ExecVarDeclNode> resolved =
				actionResolver.resolve(actionUnresolved, this);
		if(resolved != null) {
			if(resolved.first != null)
				action = resolved.first;
			else if(resolved.second != null)
				sequence = resolved.second;
			else
				boolVar = resolved.third;
		}

		successfullyResolved &= resolved != null && (action != null || sequence != null || boolVar != null);

		if(action != null) {
			for(BaseNode filterFunctionUnresolved : filterFunctionsUnresolved.getChildren()) {
				if(!(filterFunctionUnresolved instanceof PackageIdentNode)) {
					if(!tryFixupDefinition(filterFunctionUnresolved, action.getScope().getParent())) {
						fixupDefinition(filterFunctionUnresolved, filterFunctionUnresolved.getScope());
					}
				}
			}
		}

		params = paramNodeResolver.resolve(paramsUnresolved, this);
		successfullyResolved &= params != null;

		returns = varDeclNodeResolver.resolve(returnsUnresolved, this);
		successfullyResolved &= returns != null;

		filterFunctions = filterResolver.resolve(filterFunctionsUnresolved, this);
		successfullyResolved &= filterFunctions != null;

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = true;

		/* cannot be checked here, because type info is not yet computed
		 res &= checkParams(action.getParamDecls(), params.getChildren());
		 res &= checkReturns(action.returnFormalParameters, returns);
		 */

		return res;
	}

	/** check after the IR is built */
	protected boolean checkPost()
	{
		boolean res = true;

		if(action != null) {
			res &= checkParams(action.pattern.getParamDecls(), params.getChildren());
			res &= checkReturns(action.returnFormalParameters.getChildren(), returns);
		} else if(sequence != null) {
			Vector<TypeNode> outTypes = new Vector<TypeNode>();
			for(ExecVarDeclNode varDecl : sequence.outParams.getChildren())
				outTypes.add(varDecl.getDeclType());
			res &= checkParams(sequence.inParams.getChildren(), params.getChildren());
			res &= checkReturns(outTypes, returns);
		}

		if(action != null) {
			for(FilterFunctionDeclNode filter : filterFunctions.getChildren()) {
				if(filter.action != action) {
					reportError("Filter " + filter.getIdentNode().toString()
							+ " is defined for action " + filter.action.getIdentNode().toString()
							+ ". It can't be applied to action " + action.getIdentNode().toString());
				}
			}
		} else {
			if(filterFunctionsUnresolved.size() > 0)
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
	private boolean checkParams(Collection<? extends DeclNode> formalParams,
			Collection<? extends ExprNode> actualParams)
	{
		if(formalParams.size() != actualParams.size()) {
			error.error(getCoords(), "Formal and actual parameter(s) of " + actionUnresolved.toString()
					+ " mismatch in number (" + formalParams.size() + " vs. " + actualParams.size() + ")");
			return false;
		}
		
		boolean res = true;
		if(actualParams.size() > 0) {
			Iterator<? extends ExprNode> iterAP = actualParams.iterator();
			int paramCounter = 1;
			for(DeclNode formalParam : formalParams) {
				ExprNode actualParam = iterAP.next();

				res &= checkParam(paramCounter, formalParam, actualParam);
				
				++paramCounter;
			}
		}
		return res;
	}

	private boolean checkParam(int paramPos, DeclNode formalParam, ExprNode actualParam)
	{
		TypeNode formalParameterType;
		if(formalParam instanceof EdgeInterfaceTypeChangeNode) {
			EdgeInterfaceTypeChangeNode typeChangeFormalParam = (EdgeInterfaceTypeChangeNode)formalParam;
			formalParameterType = typeChangeFormalParam.interfaceType.getDeclType();
		} else if(formalParam instanceof NodeInterfaceTypeChangeNode) {
			NodeInterfaceTypeChangeNode typeChangeFormalParam = (NodeInterfaceTypeChangeNode)formalParam;
			formalParameterType = typeChangeFormalParam.interfaceType.getDeclType();
		} else {
			formalParameterType = formalParam.getDecl().getDeclType();
		}

		TypeNode actualParameterType = actualParam.getType();

		if(actualParameterType instanceof UntypedExecVarTypeNode)
			return true;

		if(actualParameterType instanceof TypeTypeNode
				&& (formalParameterType instanceof NodeTypeNode || formalParameterType instanceof EdgeTypeNode))
			return true;

		if(!actualParameterType.isCompatibleTo(formalParameterType)) {
			String argumentTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			reportError("Cannot convert " + paramPos + ". argument from \"" + argumentTypeName
					+ "\" to \"" + paramTypeName + "\" (the expected parameter type)");
			return false;
		}
		
		return true;
	}

	/**
	 * Method checkReturns
	 * @param    formalReturns a  Collection<? extends TypeNode>
	 * @param    actualReturns a  CollectNode<ExecVarDeclNode>
	 * @return   a  boolean
	 */
	private boolean checkReturns(Collection<? extends TypeNode> formalReturns,
			CollectNode<ExecVarDeclNode> actualReturns)
	{
		// It is ok to have no actual returns, but if there are some, then they have to fit.
		if(actualReturns.size() > 0 && formalReturns.size() != actualReturns.size()) {
			error.error(getCoords(),
					"Formal and actual return-parameter(s) of " + actionUnresolved.toString()
							+ " mismatch in number (formal:" + formalReturns.size() + " vs. actual:"
							+ actualReturns.size() + ")");
			return false;
		} 
		
		boolean res = true;
		if(actualReturns.size() > 0) {
			Iterator<ExecVarDeclNode> iterAR = actualReturns.getChildren().iterator();
			for(TypeNode formalReturn : formalReturns) {
				ExecVarDeclNode actualReturn = iterAR.next();

				res &= checkReturn(formalReturn, actualReturn);
			}
		}
		return res;
	}

	private boolean checkReturn(TypeNode formalReturn, ExecVarDeclNode actualReturn)
	{
		TypeNode formalReturnType = formalReturn;
		TypeNode actualReturnType = actualReturn.getDecl().getDeclType();

		if(actualReturnType instanceof UntypedExecVarTypeNode)
			return true;

		boolean incommensurable = false;

		if(isAllBracketed) {
			if(!(actualReturnType instanceof ArrayTypeNode))
				incommensurable = true;
			else {
				ArrayTypeNode arrayType = (ArrayTypeNode)actualReturnType;
				if(!formalReturnType.isCompatibleTo(arrayType.valueType))
					incommensurable = true;
			}
		}
		else if(!formalReturnType.isCompatibleTo(actualReturnType))
			incommensurable = true;

		if(incommensurable) {
			reportError("Values of formal return type \"" + formalReturnType.getTypeName() + "\""
					+ (isAllBracketed ? " (array<" + formalReturnType.getTypeName() + ">)" : "")
					+ " cannot be assigned to a variable \"" + actualReturn + "\" of type \""
					+ actualReturnType.getTypeName() + "\" (action " + actionUnresolved.toString() + ").");
			return false;
		}

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		assert false;
		return Bad.getBad(); // TODO fix this
	}
}
