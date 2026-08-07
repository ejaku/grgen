/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.deque
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using DequeCopyConstructor = de.unika.ipd.grgen.ir.expr.deque.DequeCopyConstructor;
using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class DequeCopyConstructorNode : ExprNode
{
	static DequeCopyConstructorNode()
	{
		SetClassName(typeof(DequeInitNode), "deque copy constructor");
	}

	private DequeTypeNode dequeType;
	private ExprNode dequeToCopy;
	private BaseNode lhsUnresolved;

	public DequeCopyConstructorNode(Coords coords, IdentNode member, DequeTypeNode dequeType, ExprNode dequeToCopy)
		: base(coords)
	{

		if(member != null)
			lhsUnresolved = BecomeParent(member);
		else
			this.dequeType = dequeType;
		this.dequeToCopy = dequeToCopy;
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(dequeToCopy);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("dequeToCopy");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		if(dequeType != null)
			return dequeType.Resolve();
		else
			return true;
	}

	protected internal override bool CheckLocal()
	{
		bool success = true;

		if(lhsUnresolved != null)
		{
			ReportError("A deque copy constructor is not allowed in a deque initialization in the model.");
			success = false;
		}
		else
		{
			if(dequeToCopy.Type is DequeTypeNode)
			{
				DequeTypeNode sourceDequeType = (DequeTypeNode)dequeToCopy.Type;
				success &= CheckCopyConstructorTypes(dequeType.valueType, sourceDequeType.valueType, "deque", false);
			}
			else
			{
				ReportError("A deque copy constructor expects a value of deque type to copy"
						+ " (but is given " + dequeToCopy.Type.TypeName + ").");
				success = false;
			}
		}

		return success;
	}

	public override TypeNode Type
	{
		get
		{
		Debug.Assert((IsResolved()));
		return dequeType;
		}
	}

	protected internal override IR ConstructIR()
	{
		dequeToCopy = dequeToCopy.Evaluate();
		return new DequeCopyConstructor(dequeToCopy.CheckIR(typeof(Expression)), dequeType.CheckIR(typeof(DequeType)));
	}

	public static string KindStr
	{
		get
		{
		return "deque copy constructor";
		}
	}
}

}
