/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.ScanExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an object of the specified type derived from scanning the string input parameter.
 */
public class ScanExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(ScanExprNode.class, "scan expr");
	}

	private BaseNode typeUnresolved;
	private TypeNode type;
	private ExprNode stringExpr;

	public ScanExprNode(Coords coords, BaseNode type, ExprNode stringExpr)
	{
		super(coords);
		if(type != null) {
			this.typeUnresolved = type;
			becomeParent(this.typeUnresolved);
		}
		this.stringExpr = stringExpr;
		becomeParent(this.stringExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		if(typeUnresolved != null) {
			children.add(getValidVersion(typeUnresolved, type));
		}
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string expr");
		if(typeUnresolved != null) {
			childrenNames.add("type");
		}
		return childrenNames;
	}

	protected static final DeclarationTypeResolver<TypeNode> typeResolver =
			new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(typeUnresolved == null) {
			type = BasicTypeNode.objectType;
		} else {
			type = typeResolver.resolve(typeUnresolved, this);
		}
		
		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(stringExpr.getType() instanceof StringTypeNode)) {
			if(type != null) {
				reportError("argument of scan<T>(.) must be a string");
			} else {
				reportError("argument of scan(.) must be a string");
			}
			return false;
		}

		if(type != null) {
			if(type instanceof InternalObjectTypeNode) {
				reportError("type argument of scan<T>(.) is not allowed to be a class object type");
			} else if(type instanceof InternalTransientObjectTypeNode) {
				reportError("type argument of scan<T>(.) is not allowed to be a transient class object type");
			}
			if(type instanceof ContainerTypeNode) {
				ContainerTypeNode containerType = (ContainerTypeNode)type;
				if(containerType.getElementType() instanceof InternalObjectTypeNode) {
					reportError("type argument of scan<T>(.) is not allowed to contain a class object type");
				} else if(containerType.getElementType() instanceof InternalTransientObjectTypeNode) {
					reportError("type argument of scan<T>(.) is not allowed to contain a transient class object type");
				}
				if(type instanceof MapTypeNode) {
					MapTypeNode mapType = (MapTypeNode)type;
					if(mapType.keyType instanceof InternalObjectTypeNode) {
						reportError("type argument of scan<T>(.) is not allowed to contain a class object type");
					} else if(mapType.keyType instanceof InternalTransientObjectTypeNode) {
						reportError("type argument of scan<T>(.) is not allowed to contain a transient class object type");
					}
				}
			}
		}
		
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		return new ScanExpr(stringExpr.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		if(type != null)
			return type;
		else
			return BasicTypeNode.objectType;
	}
}
