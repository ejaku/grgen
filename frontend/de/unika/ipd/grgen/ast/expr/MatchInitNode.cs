/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.MatchInit;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.parser.Coords;

public class MatchInitNode extends ExprNode
{
	static {
		setClassName(MatchInitNode.class, "match init");
	}

	private IdentNode matchTypeUnresolved;
	private DefinedMatchTypeNode matchType;
	
	public MatchInitNode(Coords coords, IdentNode matchType)
	{
		super(coords);
		this.matchTypeUnresolved = matchType;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		return childrenNames;
	}

	private static final DeclarationTypeResolver<DefinedMatchTypeNode> matchTypeResolver =
			new DeclarationTypeResolver<DefinedMatchTypeNode>(DefinedMatchTypeNode.class);

	@Override
	protected boolean resolveLocal()
	{
		matchType = matchTypeResolver.resolve(matchTypeUnresolved, this);
		return matchType != null && matchType.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return getMatchType();
	}

	public DefinedMatchTypeNode getMatchType()
	{
		assert(isResolved());
		return matchType;
	}

	@Override
	protected IR constructIR()
	{
		DefinedMatchType type = matchType.checkIR(DefinedMatchType.class);
		return new MatchInit(type);
	}

	public MatchInit getIRMatchInit()
	{
		return checkIR(MatchInit.class);
	}

	public static String getKindStr()
	{
		return "match initialization";
	}
}
