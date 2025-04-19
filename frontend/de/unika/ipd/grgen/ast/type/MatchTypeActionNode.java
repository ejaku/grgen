/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type;

import java.util.Collection;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.type.MatchType;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.parser.Symbol.Occurrence;

public class MatchTypeActionNode extends MatchTypeNode
{
	static {
		setName(MatchTypeActionNode.class, "match type action");
	}

	private IdentNode actionUnresolved;
	private ActionDeclNode action;

	private MatchTypeActionNode(IdentNode actionIdent)
	{
		actionUnresolved = becomeParent(actionIdent);
	}

	public static IdentNode defineMatchType(ParserEnvironment env, IdentNode actionIdent)
	{
		String actionString = actionIdent.toString();
		String matchTypeString = "match<" + actionString + ">";
		IdentNode matchTypeIdentNode = new IdentNode(
				env.define(ParserEnvironment.TYPES, matchTypeString, actionIdent.getCoords()));
		MatchTypeActionNode matchTypeNode = new MatchTypeActionNode(actionIdent);
		TypeDeclNode typeDeclNode = new TypeDeclNode(matchTypeIdentNode, matchTypeNode);
		matchTypeIdentNode.setDecl(typeDeclNode);
		return matchTypeIdentNode;
	}

	public static IdentNode getMatchTypeIdentNode(ParserEnvironment env, IdentNode actionIdent)
	{
		Occurrence actionOccurrence = actionIdent.occ;
		Symbol actionSymbol = actionOccurrence.getSymbol();
		String actionString = actionSymbol.getText();
		String matchTypeString = "match<" + actionString + ">";
		if(actionIdent instanceof PackageIdentNode) {
			PackageIdentNode packageActionIdent = (PackageIdentNode)actionIdent;
			Occurrence packageOccurrence = packageActionIdent.owningPackage;
			Symbol packageSymbol = packageOccurrence.getSymbol();
			return new PackageIdentNode(
					env.occurs(ParserEnvironment.PACKAGES, packageSymbol.getText(), packageOccurrence.getCoords()),
					env.occurs(ParserEnvironment.TYPES, matchTypeString, actionOccurrence.getCoords()));
		} else {
			return new IdentNode(env.occurs(ParserEnvironment.TYPES, matchTypeString, actionOccurrence.getCoords()));
		}
	}

	@Override
	public String getTypeName()
	{
		return "match<" + actionUnresolved.toString() + ">";
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(getValidVersion(actionUnresolved, action));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("action");
		return childrenNames;
	}

	private static final DeclarationResolver<ActionDeclNode> actionResolver =
			new DeclarationResolver<ActionDeclNode>(ActionDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(!(actionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		}
		
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.EQ, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.nullEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.NE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.nullEvaluator);

		action = actionResolver.resolve(actionUnresolved, this);
		if(action == null)
			return false;
		return true;
	}

	public ActionDeclNode getAction()
	{
		assert(isResolved());
		return action;
	}

	@Override
	public DeclNode tryGetMember(String name)
	{
		NodeDeclNode node = action.pattern.tryGetNode(name);
		if(node != null)
			return node;
		EdgeDeclNode edge = action.pattern.tryGetEdge(name);
		if(edge != null)
			return edge;
		return action.pattern.tryGetVar(name);
	}

	@Override
	public Set<DeclNode> getEntities()
	{
		return action.pattern.getEntities();
	}

	/** Returns the IR object for this match type node. */
	public MatchType getMatchType()
	{
		return checkIR(MatchType.class);
	}

	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) {
			return (MatchType)getIR();
		}

		MatchType matchType = new MatchType(action.ident.getIdent());

		setIR(matchType);

		Rule matchAction = action.getMatcher();
		matchType.setAction(matchAction);

		return matchType;
	}
}
