/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.MapRemoveItem;
import de.unika.ipd.grgen.ir.containers.MapVarRemoveItem;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

public class MapRemoveItemNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(MapRemoveItemNode.class, "map remove item statement");
	}

	private QualIdentNode target;
	private VarDeclNode targetVar;
	private ExprNode keyExpr;

	public MapRemoveItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
	}

	public MapRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode keyExpr)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
		this.keyExpr = becomeParent(keyExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target!=null ? target : targetVar);
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

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(target!=null) {
			TypeNode targetType = target.getDecl().getDeclType();
			TypeNode targetKeyType = ((MapTypeNode)targetType).keyType;
			TypeNode keyType = keyExpr.getType();
			if (!keyType.isEqual(targetKeyType))
			{
				keyExpr = becomeParent(keyExpr.adjustType(targetKeyType, getCoords()));
				if(keyExpr == ConstNode.getInvalid()) {
					keyExpr.reportError("Argument (key) to "
							+ "map remove item statement must be of type " + targetKeyType.toString());
					return false;
				}
			}
			return true;
		} else {
			TypeNode targetType = targetVar.getDeclType();
			TypeNode targetKeyType = ((MapTypeNode)targetType).keyType;
			return checkType(keyExpr, targetKeyType, "map remove item statement", "key");
		}
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(target!=null)
			return new MapRemoveItem(target.checkIR(Qualification.class),
					keyExpr.checkIR(Expression.class));
		else
			return new MapVarRemoveItem(targetVar.checkIR(Variable.class),
					keyExpr.checkIR(Expression.class));
	}
}
