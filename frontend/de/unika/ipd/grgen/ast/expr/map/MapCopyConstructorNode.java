/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.map;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.map.MapCopyConstructor;
import de.unika.ipd.grgen.ir.type.container.MapType;
import de.unika.ipd.grgen.parser.Coords;

public class MapCopyConstructorNode extends ExprNode
{
	static {
		setName(MapCopyConstructorNode.class, "map copy constructor");
	}

	private MapTypeNode mapType;
	private ExprNode mapToCopy;
	private BaseNode lhsUnresolved;

	public MapCopyConstructorNode(Coords coords, IdentNode member, MapTypeNode mapType, ExprNode mapToCopy)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.mapType = mapType;
		}
		this.mapToCopy = mapToCopy;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(mapToCopy);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("mapToCopy");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		if(mapType != null) {
			return mapType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = true;

		if(lhsUnresolved != null) {
			reportError("Map copy constructor not allowed in map initialization in model");
			success = false;
		} else {
			if(mapToCopy.getType() instanceof MapTypeNode) {
				MapTypeNode sourceMapType = (MapTypeNode)mapToCopy.getType();
				success &= checkCopyConstructorTypes(mapType.keyType, sourceMapType.keyType, "Map", " (key type)");
				success &= checkCopyConstructorTypes(mapType.valueType, sourceMapType.valueType, "Map", " (value type)");
			} else {
				reportError("Map copy constructor expects map type");
				success = false;
			}
		}

		return success;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		return mapType;
	}

	@Override
	protected IR constructIR()
	{
		mapToCopy = mapToCopy.evaluate();
		return new MapCopyConstructor(mapToCopy.checkIR(Expression.class), mapType.checkIR(MapType.class));
	}

	public static String getKindStr()
	{
		return "map copy constructor";
	}
}
