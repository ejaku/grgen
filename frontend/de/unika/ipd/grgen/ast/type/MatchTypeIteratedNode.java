/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.executable.TopLevelMatcherDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.type.MatchTypeIterated;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.parser.Symbol.Occurrence;

public class MatchTypeIteratedNode extends MatchTypeNode
{
	static {
		setName(MatchTypeIteratedNode.class, "match type iterated");
	}

	private IdentNode topLevelMatcherUnresolved;
	private TopLevelMatcherDeclNode topLevelMatcher;

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	private MatchTypeIteratedNode(IdentNode topLevelMatcherIdent, IdentNode iteratedIdent)
	{
		topLevelMatcherUnresolved = becomeParent(topLevelMatcherIdent);
		iteratedUnresolved = becomeParent(iteratedIdent);
	}

	public static IdentNode defineMatchType(ParserEnvironment env, IdentNode topLevelMatcherIdent, IdentNode iteratedIdent)
	{
		String topLevelMatcherString = topLevelMatcherIdent.toString();
		String iteratedString = iteratedIdent.toString();
		String matchTypeString = "match<" + topLevelMatcherString + "." + iteratedString + ">";
		IdentNode matchTypeIteratedIdentNode = new IdentNode(
				env.define(ParserEnvironment.TYPES, matchTypeString, iteratedIdent.getCoords()));
		MatchTypeIteratedNode matchTypeIteratedNode = new MatchTypeIteratedNode(topLevelMatcherIdent, iteratedIdent);
		TypeDeclNode typeDeclNode = new TypeDeclNode(matchTypeIteratedIdentNode, matchTypeIteratedNode);
		matchTypeIteratedIdentNode.setDecl(typeDeclNode);
		return matchTypeIteratedIdentNode;
	}

	public static IdentNode getMatchTypeIdentNode(ParserEnvironment env, IdentNode topLevelMatcherIdent, IdentNode iteratedIdent)
	{
		Occurrence topLevelMatcherOccurrence = topLevelMatcherIdent.occ;
		Symbol topLevelMatcherSymbol = topLevelMatcherOccurrence.getSymbol();
		String topLevelMatcherString = topLevelMatcherSymbol.getText();
		Occurrence iteratedOccurrence = iteratedIdent.occ;
		String iteratedString = iteratedIdent.toString();
		String matchTypeString = "match<" + topLevelMatcherString + "." + iteratedString + ">";
		if(topLevelMatcherIdent instanceof PackageIdentNode) {
			PackageIdentNode packageTopLevelMatcherIdent = (PackageIdentNode)topLevelMatcherIdent;
			Occurrence packageOccurrence = packageTopLevelMatcherIdent.owningPackage;
			Symbol packageSymbol = packageOccurrence.getSymbol();
			return new PackageIdentNode(
					env.occurs(ParserEnvironment.PACKAGES, packageSymbol.getText(), packageOccurrence.getCoords()),
					env.occurs(ParserEnvironment.TYPES, matchTypeString, iteratedOccurrence.getCoords()));
		} else {
			return new IdentNode(env.occurs(ParserEnvironment.TYPES, matchTypeString, iteratedOccurrence.getCoords()));
		}
	}

	@Override
	public String getTypeName()
	{
		return "match<" + topLevelMatcherUnresolved.toString() + "." + iteratedUnresolved.toString() + "> type";
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(getValidVersion(topLevelMatcherUnresolved, topLevelMatcher));
		//children.add(getValidVersion(iteratedUnresolved, iterated));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("topLevelMatcher");
		//childrenNames.add("iterated");
		return childrenNames;
	}

	private static final DeclarationResolver<TopLevelMatcherDeclNode> topLevelMatcherResolver =
			new DeclarationResolver<TopLevelMatcherDeclNode>(TopLevelMatcherDeclNode.class);
	private static final DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(IteratedDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(!(topLevelMatcherUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(topLevelMatcherUnresolved, topLevelMatcherUnresolved.getScope());
		}
		topLevelMatcher = topLevelMatcherResolver.resolve(topLevelMatcherUnresolved, this);
		if(topLevelMatcher == null)
			return false;
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		return iterated != null;
	}

	public TopLevelMatcherDeclNode getTopLevelMatcher()
	{
		assert(isResolved());
		return topLevelMatcher;
	}

	public IteratedDeclNode getIterated()
	{
		assert(isResolved());
		return iterated;
	}

	@Override
	public DeclNode tryGetMember(String name)
	{
		NodeDeclNode node = iterated.pattern.tryGetNode(name);
		if(node != null)
			return node;
		EdgeDeclNode edge = iterated.pattern.tryGetEdge(name);
		if(edge != null)
			return edge;
		return iterated.pattern.tryGetVar(name);
	}

	@Override
	public Set<DeclNode> getEntities()
	{
		return iterated.pattern.getEntities();
	}
	
	/** Returns the IR object for this match type node. */
	public MatchTypeIterated getMatchTypeIterated()
	{
		return checkIR(MatchTypeIterated.class);
	}

	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) {
			return (MatchTypeIterated)getIR();
		}

		MatchTypeIterated matchTypeIterated = new MatchTypeIterated(iterated.ident.getIdent());

		setIR(matchTypeIterated);

		Rule matchAction = topLevelMatcher.getMatcher();
		Rule iter = (Rule)iterated.getIR();

		matchTypeIterated.setAction(matchAction);
		matchTypeIterated.setIterated(iter);

		return matchTypeIterated;
	}
}
