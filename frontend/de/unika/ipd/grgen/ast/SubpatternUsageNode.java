/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

public class SubpatternUsageNode extends DeclNode {
	static {
		setName(SubpatternUsageNode.class, "subpattern node");
	}

	CollectNode<ConstraintDeclNode> connections;
	CollectNode<IdentNode> connectionsUnresolved;

	protected SubpatternDeclNode type = null;


	public SubpatternUsageNode(IdentNode n, BaseNode t, CollectNode<IdentNode> c) {
		super(n, t);
		this.connectionsUnresolved = c;
		becomeParent(this.connectionsUnresolved);
	}

	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return type.getDeclType();
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(connectionsUnresolved, connections));
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
	private static final CollectPairResolver<ConstraintDeclNode> connectionsResolver =
		new CollectPairResolver<ConstraintDeclNode>(new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		fixupDefinition((IdentNode)typeUnresolved, typeUnresolved.getScope());
		type        = actionResolver.resolve(typeUnresolved, this);
		connections = connectionsResolver.resolve(connectionsUnresolved, this);
		return type != null && connections != null;
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

	@Override
	protected boolean checkLocal() {
		return checkSubpatternSignatureAdhered();
	}


	/** Check whether the subpattern usage adheres to the signature of the subpattern declaration */
	protected boolean checkSubpatternSignatureAdhered() {
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
			ConstraintDeclNode actualParameter = connections.children.get(i);
			ConstraintDeclNode formalParameter = (ConstraintDeclNode)formalParameters.get(i);
			InheritanceTypeNode actualParameterType = actualParameter.getDeclType();
			InheritanceTypeNode formalParameterType = formalParameter.getDeclType();

			if(!actualParameterType.isA(formalParameterType)) {
				res = false;
				actualParameter.ident.reportError("Subpattern usage parameter \"" + actualParameter.ident.toString() + "\" has wrong type");
			}
		}

		return res;
	}


	@Override
	protected IR constructIR() {
		List<GraphEntity> subpatternConnections = new LinkedList<GraphEntity>();
		for (ConstraintDeclNode c : connections.getChildren()) {
			subpatternConnections.add(c.checkIR(GraphEntity.class));
		}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(),
				type.checkIR(MatchingAction.class), subpatternConnections);
	}
}

