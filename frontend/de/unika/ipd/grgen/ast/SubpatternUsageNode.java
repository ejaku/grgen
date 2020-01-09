/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SubpatternUsage;

public class SubpatternUsageNode extends DeclNode {
	static {
		setName(SubpatternUsageNode.class, "subpattern node");
	}

	private CollectNode<ExprNode> connections;

	protected SubpatternDeclNode type = null;
	protected int context;


	public SubpatternUsageNode(IdentNode n, BaseNode t, int context, CollectNode<ExprNode> c) {
		super(n, t);
		this.context = context;
		this.connections = c;
		becomeParent(this.connections);
	}

	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return type.getDeclType();
	}

	protected SubpatternDeclNode getSubpatternDeclNode() {
		assert isResolved();

		return type;
	}
	
	public int getContext() {
		return context;
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(connections);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
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
	protected boolean resolveLocal() {
		if(!(typeUnresolved instanceof PackageIdentNode)) {
			fixupDefinition((IdentNode)typeUnresolved, typeUnresolved.getScope());
		}
		type        = actionResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	@Override
	protected boolean checkLocal() {
		return checkSubpatternSignatureAdhered();
	}


	/** Check whether the subpattern usage adheres to the signature of the subpattern declaration */
	private boolean checkSubpatternSignatureAdhered() {
		// check if the number of parameters are correct
		int expected = type.pattern.getParamDecls().size();
		int actual = connections.getChildren().size();
		if (expected != actual) {
			String patternName = type.ident.toString();
			ident.reportError("The pattern \"" + patternName + "\" needs "
					+ expected + " parameters, given by subpattern usage " + ident.toString() + " are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		Vector<DeclNode> formalParameters = type.pattern.getParamDecls();
		for (int i = 0; i < connections.children.size(); ++i) {
			ExprNode actualParameter = connections.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			DeclNode formalParameter = formalParameters.get(i);				
			TypeNode formalParameterType = formalParameter.getDeclType();
			if(actualParameter instanceof IdentExprNode && ((IdentExprNode)actualParameter).yieldedTo) {
				if(formalParameter instanceof ConstraintDeclNode) {
					if(!((ConstraintDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						ident.reportError("The " + (i + 1) + ". subpattern usage argument is yielded but the parameter at this position is not declared as def");
					}
				}
				else if(formalParameter instanceof VarDeclNode) {
					if(!((VarDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						ident.reportError("The " + (i + 1) + ". subpattern usage argument is yielded but the parameter at this position is not declared as def");
					}
				}

				if(((IdentExprNode)actualParameter).decl instanceof VarDeclNode) {
					if(!((VarDeclNode)((IdentExprNode)actualParameter).decl).defEntityToBeYieldedTo) {
						res = false;
						ident.reportError("Can't yield to non-def arguments - the " + (i + 1) + ". subpattern usage argument is yielded to but not declared as def");
					}
				} else if(((IdentExprNode)actualParameter).decl instanceof ConstraintDeclNode) {
					if(!((ConstraintDeclNode)((IdentExprNode)actualParameter).decl).defEntityToBeYieldedTo) {
						res = false;
						ident.reportError("Can't yield to non-def arguments - the " + (i + 1) + ". subpattern usage argument is yielded to but not declared as def");
					}
				}
				
				if(!formalParameterType.isCompatibleTo(actualParameterType)) {
					res = false;
					String exprTypeName = getTypeName(actualParameterType);
					String paramTypeName = getTypeName(formalParameterType);
					ident.reportError("The " + (i + 1) + ". subpattern usage argument of type \""
							+ exprTypeName + "\" can't be yielded to from the subpattern def parameter type \"" + paramTypeName + "\"");
				}
			} else {
				if(formalParameter instanceof ConstraintDeclNode) {
					if(((ConstraintDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						ident.reportError("The " + (i + 1) + ". subpattern usage argument is not yielded but the parameter at this position is declared as def");
					}
				}
				else if(formalParameter instanceof VarDeclNode) {
					if(((VarDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						ident.reportError("The " + (i + 1) + ". subpattern usage argument is not yielded but the parameter at this position is declared as def");
					}
				}
				
				if(!actualParameterType.isCompatibleTo(formalParameterType)) {
					res = false;
					String exprTypeName = getTypeName(actualParameterType);
					String paramTypeName = getTypeName(formalParameterType);
					ident.reportError("Cannot convert " + (i + 1) + ". subpattern usage argument from \""
							+ exprTypeName + "\" to \"" + paramTypeName + "\"");
				}
			}
			
			if(actualParameter instanceof IdentExprNode) {
				if(((IdentExprNode)actualParameter).decl instanceof VarDeclNode) {
					if(((VarDeclNode)((IdentExprNode)actualParameter).decl).defEntityToBeYieldedTo) {
						if(formalParameter instanceof VarDeclNode) {
							if(!((VarDeclNode)formalParameter).defEntityToBeYieldedTo) {
								res = false;
								ident.reportError("Can't use def elements as non-def arguments to subpatterns - the " + (i + 1) + ". subpattern usage argument is declared as def, but the parameter at this position is not declared as def");
							}
						}
					}
				} else if(((IdentExprNode)actualParameter).decl instanceof ConstraintDeclNode) {
					if(((ConstraintDeclNode)((IdentExprNode)actualParameter).decl).defEntityToBeYieldedTo) {
						if(formalParameter instanceof ConstraintDeclNode) {
							if(!((ConstraintDeclNode)formalParameter).defEntityToBeYieldedTo) {
								res = false;
								ident.reportError("Can't use def elements as non-def arguments to subpatterns - the " + (i + 1) + ". subpattern usage argument is declared as def, but the parameter at this position is not declared as def");
							}
						}
					}
				}
			}
		}

		return res;
	}

	private String getTypeName(TypeNode type) {
		String typeName;
		if(type instanceof InheritanceTypeNode)
			typeName = ((InheritanceTypeNode) type).getIdentNode().toString();
		else
			typeName = type.toString();
		return typeName;
	}


	@Override
	protected IR constructIR() {
		List<Expression> subpatternConnections = new LinkedList<Expression>();
		List<Expression> subpatternYields = new LinkedList<Expression>();
		for (ExprNode e : connections.getChildren()) {
			if(e instanceof IdentExprNode && ((IdentExprNode)e).yieldedTo)
				subpatternYields.add(e.checkIR(Expression.class));
			else
				subpatternConnections.add(e.checkIR(Expression.class));
		}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(),
				type.checkIR(Rule.class), subpatternConnections, subpatternYields);
	}
}

