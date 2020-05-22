/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.DeclaredCharacter;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.expr.Expression;

public class SubpatternUsageNode extends DeclNode
{
	static {
		setName(SubpatternUsageNode.class, "subpattern node");
	}

	private CollectNode<ExprNode> connections;

	public SubpatternDeclNode type = null;
	public int context;

	public SubpatternUsageNode(IdentNode n, BaseNode t, int context, CollectNode<ExprNode> c)
	{
		super(n, t);
		this.context = context;
		this.connections = c;
		becomeParent(this.connections);
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return type.getDeclType();
	}

	public SubpatternDeclNode getSubpatternDeclNode()
	{
		assert isResolved();

		return type;
	}

	public int getContext()
	{
		return context;
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(connections);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("connections");
		return childrenNames;
	}

	private static final DeclarationResolver<SubpatternDeclNode> actionResolver =
			new DeclarationResolver<SubpatternDeclNode>(SubpatternDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(!(typeUnresolved instanceof PackageIdentNode)) {
			fixupDefinition((IdentNode)typeUnresolved, typeUnresolved.getScope());
		}
		type = actionResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	@Override
	protected boolean checkLocal()
	{
		return checkSubpatternSignatureAdhered();
	}

	/** Check whether the subpattern usage adheres to the signature of the subpattern declaration */
	private boolean checkSubpatternSignatureAdhered()
	{
		// check if the number of parameters are correct
		int expected = type.pattern.getParamDecls().size();
		int actual = connections.getChildren().size();
		if(expected != actual) {
			String patternName = type.ident.toString();
			ident.reportError("The pattern \"" + patternName + "\" needs " + expected
					+ " parameters, given by subpattern usage " + ident.toString() + " are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		Vector<DeclNode> formalParameters = type.pattern.getParamDecls();
		for(int i = 0; i < connections.size(); ++i) {
			ExprNode actualParameter = connections.get(i);
			DeclNode formalParameter = formalParameters.get(i);
			if(actualParameter instanceof IdentExprNode && ((IdentExprNode)actualParameter).yieldedTo) {
				res &= checkYieldedToParameter(i, actualParameter, formalParameter);
			} else {
				res &= checkParameter(i, actualParameter, formalParameter);
			}
			res &= checkDefArgument(i, actualParameter, formalParameter);
		}
		return res;
	}

	private boolean checkYieldedToParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		boolean res = true;
		
		TypeNode actualParameterType = actualParameter.getType();
		TypeNode formalParameterType = formalParameter.getDeclType();

		if(formalParameter instanceof ConstraintDeclNode) {
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(!parameterElement.defEntityToBeYieldedTo) {
				res = false;
				ident.reportError("The " + (i + 1) + ". subpattern usage argument is yielded to, "
						+ "but the parameter at this position is not declared as def"
						+ "(" + parameterElement.getIdentNode() + ")");
			}
		} else { //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(!parameterVar.defEntityToBeYieldedTo) {
				res = false;
				ident.reportError("The " + (i + 1) + ". subpattern usage argument is yielded to, "
						+ "but the parameter at this position is not declared as def"
						+ "(" + parameterVar.getIdentNode() + ")");
			}
		}

		DeclaredCharacter argument = ((IdentExprNode)actualParameter).decl;
		if(argument instanceof VarDeclNode) {
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if(!argumentVar.defEntityToBeYieldedTo) {
				res = false;
				ident.reportError("Can't yield to non-def arguments - the " + (i + 1)
						+ ". subpattern usage argument is yielded to but not declared as def"
						+ "(" + argumentVar.getIdentNode() + ")");
			}
		} else { //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if(!argumentElement.defEntityToBeYieldedTo) {
				res = false;
				ident.reportError("Can't yield to non-def arguments - the " + (i + 1)
						+ ". subpattern usage argument is yielded to but not declared as def"
						+ "(" + argumentElement.getIdentNode() + ")");
			}
		}

		if(!formalParameterType.isCompatibleTo(actualParameterType)) {
			res = false;
			String exprTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			ident.reportError("The " + (i + 1) + ". subpattern usage argument of type \"" + exprTypeName
					+ "\" can't be yielded to from the subpattern def parameter type \"" + paramTypeName
					+ "\"");
		}
		
		return res;
	}

	private boolean checkParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		boolean res = true;
		
		TypeNode actualParameterType = actualParameter.getType();
		TypeNode formalParameterType = formalParameter.getDeclType();

		if(formalParameter instanceof ConstraintDeclNode) {
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(parameterElement.defEntityToBeYieldedTo) {
				res = false;
				ident.reportError("The " + (i + 1) + ". subpattern usage argument is not yielded to, "
						+ "but the parameter at this position is declared as def"
						+ "(" + parameterElement.getIdentNode() + ")");
			}
		} else { //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(parameterVar.defEntityToBeYieldedTo) {
				res = false;
				ident.reportError("The " + (i + 1) + ". subpattern usage argument is not yielded to, "
						+ "but the parameter at this position is declared as def"
						+ "(" + parameterVar.getIdentNode() + ")");
			}
		}

		if(!actualParameterType.isCompatibleTo(formalParameterType)) {
			res = false;
			String exprTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			ident.reportError("Cannot convert " + (i + 1) + ". subpattern usage argument from \"" + exprTypeName
					+ "\" to \"" + paramTypeName + "\"");
		}

		return res;
	}

	private boolean checkDefArgument(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		if(!(actualParameter instanceof IdentExprNode)) {
			return true;
		}
		
		DeclaredCharacter argument = ((IdentExprNode)actualParameter).decl;
		if(argument instanceof VarDeclNode) {
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if(argumentVar.defEntityToBeYieldedTo) {
				if(formalParameter instanceof VarDeclNode) {
					VarDeclNode parameterVar = (VarDeclNode)formalParameter;
					if(!parameterVar.defEntityToBeYieldedTo) {
						ident.reportError("Can't use def elements as non-def arguments to subpatterns"
								+" - the " + (i + 1) + ". subpattern usage argument is declared as def, "
								+ "but the parameter at this position is not declared as def");
						return false;
					}
				}
			}
		} else { //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if(argumentElement.defEntityToBeYieldedTo) {
				if(formalParameter instanceof ConstraintDeclNode) {
					ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
					if(!parameterElement.defEntityToBeYieldedTo) {
						ident.reportError("Can't use def elements as non-def arguments to subpatterns"+
								" - the " + (i + 1) + ". subpattern usage argument is declared as def, "
								+ "but the parameter at this position is not declared as def");
						return false;
					}
				}
			}
		}
		
		return true;
	}

	@Override
	protected IR constructIR()
	{
		List<Expression> subpatternConnections = new LinkedList<Expression>();
		List<Expression> subpatternYields = new LinkedList<Expression>();
		for(ExprNode e : connections.getChildren()) {
			if(e instanceof IdentExprNode && ((IdentExprNode)e).yieldedTo)
				subpatternYields.add(e.checkIR(Expression.class));
			else
				subpatternConnections.add(e.checkIR(Expression.class));
		}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(), type.checkIR(Rule.class),
				subpatternConnections, subpatternYields);
	}
}
