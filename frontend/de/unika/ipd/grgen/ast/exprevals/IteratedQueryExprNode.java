/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.exprevals.IteratedQueryExpr;
import de.unika.ipd.grgen.parser.Coords;

public class IteratedQueryExprNode extends ExprNode {
	static {
		setName(IteratedQueryExprNode.class, "iterated query");
	}

	private IdentNode iteratedUnresolved;
	private IteratedNode iterated;

	private TypeNode arrayOfMatchTypeUnresolved;
	private TypeNode arrayOfMatchType;

	public IteratedQueryExprNode(Coords coords, IdentNode iterated, TypeNode arrayOfMatchType) {
		super(coords);

		this.iteratedUnresolved = becomeParent(iterated);
		this.arrayOfMatchTypeUnresolved = becomeParent(arrayOfMatchType);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iteratedUnresolved, iterated));
		children.add(getValidVersion(arrayOfMatchTypeUnresolved, arrayOfMatchType));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterated");
		childrenNames.add("arrayOfMatchType");
		return childrenNames;
	}

	private static final DeclarationResolver<IteratedNode> iteratedResolver =
			new DeclarationResolver<IteratedNode>(IteratedNode.class);

	@Override
	protected boolean resolveLocal() {
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		if(iterated==null) {
			return false;
		}
		if(arrayOfMatchTypeUnresolved.resolve()) {
			arrayOfMatchType = arrayOfMatchTypeUnresolved;
		}
		return arrayOfMatchType != null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		return new IteratedQueryExpr(iteratedUnresolved.getIdent(),
				iterated.checkIR(Rule.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return arrayOfMatchType;
	}
}