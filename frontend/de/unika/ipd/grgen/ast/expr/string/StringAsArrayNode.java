/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.string;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.string.StringAsArray;
import de.unika.ipd.grgen.parser.Coords;

public class StringAsArrayNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(StringAsArrayNode.class, "string asArray");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSplitAtExpr;
	private ArrayTypeNode arrayTypeNode;

	public StringAsArrayNode(Coords coords, ExprNode stringExpr, ExprNode stringToSplitAtExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.stringToSplitAtExpr = becomeParent(stringToSplitAtExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(stringToSplitAtExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("stringToSplitAt");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		arrayTypeNode = new ArrayTypeNode(((StringTypeNode)stringExpr.getType()).getIdentNode());
		return arrayTypeNode.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("This argument to string explode expression must be of type string");
			return false;
		}
		if(!stringToSplitAtExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringToSplitAtExpr.reportError("Argument (string to "
					+ "split at) to string explode expression must be of type string");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		stringToSplitAtExpr = stringToSplitAtExpr.evaluate();
		return new StringAsArray(stringExpr.checkIR(Expression.class),
				stringToSplitAtExpr.checkIR(Expression.class),
				getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return arrayTypeNode;
	}
}
