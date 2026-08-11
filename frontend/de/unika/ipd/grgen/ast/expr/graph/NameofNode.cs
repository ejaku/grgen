/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Nameof = de.unika.ipd.grgen.ir.expr.graph.Nameof;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the name of some node/edge or the graph.
/// </summary>
public class NameofNode : ExprNode
{
	static NameofNode()
	{
		SetClassName(typeof(NameofNode), "nameof");
	}

	private ExprNode namedEntity; // null if name of main graph is requested

	public NameofNode(Coords coords, ExprNode namedEntity)
		: base(coords)
	{
		this.namedEntity = namedEntity;
		BecomeParent(this.namedEntity);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			if(namedEntity != null)
				children.Add(namedEntity);
			return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			if(namedEntity != null)
				childrenNames.Add("named entity");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		if(namedEntity != null)
		{
			if(namedEntity.Type.IsEqual(BasicTypeNode.graphType))
				return true;
			if(namedEntity.Type is EdgeTypeNode)
				return true;
			if(namedEntity.Type is NodeTypeNode)
				return true;

			ReportError("The function nameof expects as argument (entityToFetchNameOf) a value of type node or edge or graph"
					+ " (but is given a value of type " + namedEntity.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		if(namedEntity == null)
			return new Nameof(null, Type.IRType);
		namedEntity = namedEntity.Evaluate();
		return new Nameof(namedEntity.CheckIR(typeof(Expression)), Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return BasicTypeNode.stringType;
		}
	}
}

}
