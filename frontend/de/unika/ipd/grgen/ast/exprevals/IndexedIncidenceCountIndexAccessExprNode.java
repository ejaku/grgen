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
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.IndexedIncidenceCountIndexAccessExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IncidenceCountIndex;
import de.unika.ipd.grgen.parser.Coords;

public class IndexedIncidenceCountIndexAccessExprNode extends ExprNode
{
	static {
		setName(IndexedIncidenceCountIndexAccessExprNode.class, "indexed incidence count index access expression");
	}

	private IdentNode targetUnresolved;
	private IncidenceCountIndexDeclNode target;
	private ExprNode keyExpr;

	public IndexedIncidenceCountIndexAccessExprNode(Coords coords, IdentNode target, ExprNode keyExpr)
	{
		super(coords);
		this.targetUnresolved = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(targetUnresolved, target));
		children.add(keyExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	private static DeclarationResolver<IncidenceCountIndexDeclNode> indexResolver =
		new DeclarationResolver<IncidenceCountIndexDeclNode>(IncidenceCountIndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();
		target = indexResolver.resolve(targetUnresolved, this);
		successfullyResolved &= target!=null;
		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode keyType = target.getType();
		TypeNode keyExprType = keyExpr.getType();

		if (keyExprType instanceof InheritanceTypeNode) {
			if(keyExprType.isCompatibleTo(keyType))
				return true;
			
			String givenTypeName;
			if(keyExprType instanceof InheritanceTypeNode)
				givenTypeName = ((InheritanceTypeNode) keyExprType).getIdentNode().toString();
			else
				givenTypeName = keyExprType.toString();
			String expectedTypeName;
			if(keyType instanceof InheritanceTypeNode)
				expectedTypeName = ((InheritanceTypeNode) keyType).getIdentNode().toString();
			else
				expectedTypeName = keyType.toString();
			reportError("Cannot convert indexed incidence index access argument from \""
					+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
			return false;
		} else {
			if (keyExprType.isEqual(keyType))
				return true;

			keyExpr = becomeParent(keyExpr.adjustType(keyType, getCoords()));
			return keyExpr != ConstNode.getInvalid();
		}
	}

	@Override
	public TypeNode getType() {
		return IntTypeNode.intType;
	}

	@Override
	protected IR constructIR() {
		return new IndexedIncidenceCountIndexAccessExpr(
				target.checkIR(IncidenceCountIndex.class),
				keyExpr.checkIR(Expression.class));
	}
}
