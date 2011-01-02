/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

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
		fixupDefinition((IdentNode)typeUnresolved, typeUnresolved.getScope());
		type        = actionResolver.resolve(typeUnresolved, this);
		return type != null;
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
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName;
				if(actualParameterType instanceof InheritanceTypeNode)
					exprTypeName = ((InheritanceTypeNode) actualParameterType).getIdentNode().toString();
				else
					exprTypeName = actualParameterType.toString();
				String paramTypeName;
				if(formalParameterType instanceof InheritanceTypeNode)
					paramTypeName = ((InheritanceTypeNode) formalParameterType).getIdentNode().toString();
				else
					paramTypeName = formalParameterType.toString();
				ident.reportError("Cannot convert " + (i + 1) + ". subpattern usage argument from \""
						+ exprTypeName + "\" to \"" + paramTypeName + "\"");
			}
		}

		return res;
	}


	@Override
	protected IR constructIR() {
		List<Expression> subpatternConnections = new LinkedList<Expression>();
		for (ExprNode e : connections.getChildren()) {
			subpatternConnections.add(e.checkIR(Expression.class));
		}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(),
				type.checkIR(Rule.class), subpatternConnections);
	}
}

