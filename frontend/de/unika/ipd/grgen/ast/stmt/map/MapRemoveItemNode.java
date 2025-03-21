/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.map;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.map.MapRemoveItem;
import de.unika.ipd.grgen.ir.stmt.map.MapVarRemoveItem;
import de.unika.ipd.grgen.parser.Coords;

public class MapRemoveItemNode extends MapProcedureMethodInvocationBaseNode
{
	static {
		setName(MapRemoveItemNode.class, "map remove item statement");
	}

	private ExprNode keyExpr;

	public MapRemoveItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr)
	{
		super(coords, target);
		this.keyExpr = becomeParent(keyExpr);
	}

	public MapRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode keyExpr)
	{
		super(coords, targetVar);
		this.keyExpr = becomeParent(keyExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target != null ? target : targetVar);
		children.add(keyExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		MapTypeNode targetType = getTargetType();
		if(target != null) {
			TypeNode targetKeyType = targetType.keyType;
			TypeNode keyType = keyExpr.getType();
			if(!keyType.isEqual(targetKeyType)) {
				ExprNode keyExprOld = keyExpr;
				keyExpr = becomeParent(keyExpr.adjustType(targetKeyType, getCoords()));
				if(keyExpr == ConstNode.getInvalid()) {
					keyExprOld.reportError("The map rem item procedure expects as argument (key)"
							+ " a value of type " + targetKeyType.toStringWithDeclarationCoords()
							+ " (but is given a value of type " + keyType.toStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			return true;
		} else {
			TypeNode targetKeyType = targetType.keyType;
			return checkType(keyExpr, targetKeyType, "map rem item procedure", "key");
		}
	}

	@Override
	protected IR constructIR()
	{
		keyExpr = keyExpr.evaluate();
		if(target != null)
			return new MapRemoveItem(target.checkIR(Qualification.class),
					keyExpr.checkIR(Expression.class));
		else
			return new MapVarRemoveItem(targetVar.checkIR(Variable.class),
					keyExpr.checkIR(Expression.class));
	}
}
