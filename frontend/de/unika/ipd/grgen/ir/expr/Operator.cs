/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// An operator in an expression.
/// </summary>
public class Operator : Expression
{
	/// <summary>
	/// The operands of the expression. </summary>
	protected internal IList<Expression> operands = new List<Expression>();

	/// <summary>
	/// The opcode of the operator. </summary>
	private OperatorCode opCode;

	/// <param name="type"> The type of the operator. </param>
	public Operator(Type type, OperatorCode opCode)
		: base("operator", type)
	{
		this.opCode = opCode;
	}

	/// <returns> The opcode of this operator. </returns>
	public virtual OperatorCode OpCode
	{
		get
		{
		return opCode;
		}
	}

	/// <returns> The number of operands. </returns>
	public virtual int Arity()
	{
		return operands.Count;
	}

	/// <summary>
	/// Get the ith operand. </summary>
	/// <param name="index"> The index of the operand </param>
	/// <returns> The operand, if <code>index</code> was valid, <code>null</code> if not. </returns>
	public virtual Expression GetOperand(int index)
	{
		return index >= 0 || index < operands.Count ? operands[index] : null;
	}

	/// <summary>
	/// Adds an operand e to the expression. </summary>
	public virtual void AddOperand(Expression e)
	{
		operands.Add(e);
	}

	public override string GetEdgeLabel(int edge)
	{
		return "op " + edge;
	}

	public override string NodeLabel
	{
		get
		{
		return Type.Ident + " " + opCode.ToString().ToLower()
				+ "(" + opCode + ")";
		}
	}

	public virtual ICollection<Expression> WalkableChildren
	{
		get
		{
		return operands;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		foreach(Expression child in WalkableChildren)
			child.CollectNeededEntities(needs);
	}
}

}
