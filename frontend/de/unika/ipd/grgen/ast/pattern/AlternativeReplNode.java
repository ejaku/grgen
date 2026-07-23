/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.pattern.Alternative;
import de.unika.ipd.grgen.ir.pattern.AlternativeReplacement;

public class AlternativeReplNode extends OrderedReplacementNode
{
	static {
		setClassName(AlternativeReplNode.class, "alternative repl node");
	}

	private IdentNode alternativeUnresolved;
	private AlternativeDeclNode alternative;

	public AlternativeReplNode(IdentNode n)
	{
		this.alternativeUnresolved = n;
		becomeParent(this.alternativeUnresolved);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(getValidVersion(alternativeUnresolved, alternative));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		childrenNames.add("alternative");
		return childrenNames;
	}

	private static final DeclarationResolver<AlternativeDeclNode> alternativeResolver =
		new DeclarationResolver<AlternativeDeclNode>(AlternativeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		alternative = alternativeResolver.resolve(alternativeUnresolved, this);
		return alternative != null;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		return new AlternativeReplacement("alternative replacement", alternativeUnresolved.getIRIdent(),
				alternative.checkIR(Alternative.class));
	}
}
