/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.stmt.graph
{
using System.Diagnostics;

using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class GraphAddCopyEdgeProc : BuiltinProcedureInvocationBase
{
	private readonly Expression sourceNode;
	private readonly Expression targetNode;
	private readonly Expression oldEdge;

	private readonly Type returnType;

	private readonly bool deep;

	public GraphAddCopyEdgeProc(Expression edgeType,
			Expression sourceNode,
			Expression targetNode,
			Type returnType,
			bool deep)
		: base("graph add copy edge procedure")
	{
		this.oldEdge = edgeType;
		this.sourceNode = sourceNode;
		this.targetNode = targetNode;
		this.returnType = returnType;
		this.deep = deep;
	}

	public virtual Expression OldEdgeExpr
	{
		get
		{
			return oldEdge;
		}
	}

	public virtual Expression SourceNodeExpr
	{
		get
		{
			return sourceNode;
		}
	}

	public virtual Expression TargetNodeExpr
	{
		get
		{
			return targetNode;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		oldEdge.CollectNeededEntities(needs);
		sourceNode.CollectNeededEntities(needs);
		targetNode.CollectNeededEntities(needs);
	}

	public override int ReturnArity()
	{
		return 1;
	}

	public override Type GetReturnType(int index)
	{
		Debug.Assert((index == 0));
		return returnType;
	}

	public virtual bool Deep
	{
		get
		{
			return deep;
		}
	}
}

}
