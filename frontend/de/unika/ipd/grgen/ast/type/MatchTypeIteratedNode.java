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
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.TestDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
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

	@Override
	public String getName()
	{
		return "match<" + actionUnresolved.toString() + "." + iteratedUnresolved.toString() + "> type";
	}

	public static IdentNode defineMatchType(ParserEnvironment env, IdentNode actionIdent, IdentNode iteratedIdent)
	{
		String actionString = actionIdent.toString();
		String iteratedString = iteratedIdent.toString();
		String matchTypeString = "match<" + actionString + "." + iteratedString + ">";
		IdentNode matchTypeIteratedIdentNode = new IdentNode(
				env.define(ParserEnvironment.TYPES, matchTypeString, iteratedIdent.getCoords()));
		MatchTypeIteratedNode matchTypeIteratedNode = new MatchTypeIteratedNode(actionIdent, iteratedIdent);
		TypeDeclNode typeDeclNode = new TypeDeclNode(matchTypeIteratedIdentNode, matchTypeIteratedNode);
		matchTypeIteratedIdentNode.setDecl(typeDeclNode);
		return matchTypeIteratedIdentNode;
	}

	public static IdentNode getMatchTypeIdentNode(ParserEnvironment env, IdentNode actionIdent, IdentNode iteratedIdent)
	{
		Occurrence actionOccurrence = actionIdent.occ;
		Symbol actionSymbol = actionOccurrence.getSymbol();
		String actionString = actionSymbol.getText();
		Occurrence iteratedOccurrence = iteratedIdent.occ;
		String iteratedString = iteratedIdent.toString();
		String matchTypeString = "match<" + actionString + "." + iteratedString + ">";
		if(actionIdent instanceof PackageIdentNode) {
			PackageIdentNode packageActionIdent = (PackageIdentNode)actionIdent;
			Occurrence packageOccurrence = packageActionIdent.owningPackage;
			Symbol packageSymbol = packageOccurrence.getSymbol();
			return new PackageIdentNode(
					env.occurs(ParserEnvironment.PACKAGES, packageSymbol.getText(), packageOccurrence.getCoords()),
					env.occurs(ParserEnvironment.TYPES, matchTypeString, iteratedOccurrence.getCoords()));
		} else {
			return new IdentNode(env.occurs(ParserEnvironment.TYPES, matchTypeString, iteratedOccurrence.getCoords()));
		}
	}

	private SubpatternDeclNode subpattern;

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	// the match type node instances are created in ParserEnvironment as needed
	public MatchTypeIteratedNode(IdentNode actionIdent, IdentNode iteratedIdent)
	{
		super(actionIdent);
		iteratedUnresolved = becomeParent(iteratedIdent);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		//children.add(getValidVersion(actionUnresolved, action, subpattern));
		//children.add(getValidVersion(iteratedUnresolved, iterated));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		//childrenNames.add("actionOrSubpattern");
		//childrenNames.add("iterated");
		return childrenNames;
	}

	private static final DeclarationPairResolver<TestDeclNode, SubpatternDeclNode> actionOrSubpatternResolver =
			new DeclarationPairResolver<TestDeclNode, SubpatternDeclNode>(TestDeclNode.class, SubpatternDeclNode.class);
	private static final DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(IteratedDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(!(actionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		}
		Pair<TestDeclNode, SubpatternDeclNode> actionOrSubpattern = actionOrSubpatternResolver.resolve(actionUnresolved, this);
		if(actionOrSubpattern == null || actionOrSubpattern.fst == null && actionOrSubpattern.snd == null)
			return false;
		if(actionOrSubpattern.fst != null)
			action = actionOrSubpattern.fst;
		if(actionOrSubpattern.snd != null)
			subpattern = actionOrSubpattern.snd;
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		return iterated != null;
	}

	public IteratedDeclNode getIterated()
	{
		assert(isResolved());
		return iterated;
	}

	@Override
	public DeclNode tryGetMember(String name)
	{
		NodeDeclNode node = iterated.getLeft().tryGetNode(name);
		if(node != null)
			return node;
		EdgeDeclNode edge = iterated.getLeft().tryGetEdge(name);
		if(edge != null)
			return edge;
		return iterated.getLeft().tryGetVar(name);
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

		Rule matchAction = action != null ? action.getAction() : subpattern.getAction();

		Rule iter = (Rule)iterated.getIR();

		matchTypeIterated.setAction(matchAction);
		matchTypeIterated.setIterated(iter);

		return matchTypeIterated;
	}
}
