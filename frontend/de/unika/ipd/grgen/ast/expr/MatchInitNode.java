/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
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
		setName(MatchInitNode.class, "match init");
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
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
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

	public MatchInit getMatchInit()
	{
		return checkIR(MatchInit.class);
	}

	public static String getKindStr()
	{
		return "match initialization";
	}
}
