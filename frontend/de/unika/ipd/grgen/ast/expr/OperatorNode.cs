/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using ByteTypeNode = de.unika.ipd.grgen.ast.type.basic.ByteTypeNode;
using ShortTypeNode = de.unika.ipd.grgen.ast.type.basic.ShortTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// Expression operators.
/// </summary>
public abstract class OperatorNode : ExprNode
{
	/// <summary>
	/// The operator. </summary>
	private Operator @operator;

	/// <summary>
	/// The corresponding operator declaration. </summary>
	private OperatorDeclNode operatorDecl;

	public IList<ExprNode> children = new List<ExprNode>();

	/// <summary>
	/// Make a new operator node. </summary>
	/// <param name="coords"> The source coordinates of that node. </param>
	/// <param name="opId"> The operator ID. </param>
	public OperatorNode(Coords coords, Operator @operator)
		: base(coords)
	{
		this.@operator = @operator;
	}

	public virtual void AddChild(ExprNode n)
	{
		BecomeParent(n);
		children.Add(n);
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = true;
		TypeNode type = Type;
		int arity = OperatorDeclNode.GetArity(@operator);

		if(children.Count != arity)
		{
			ReportError("Wrong operator arity: " + children.Count
					+ " (for " + OperatorDeclNode.GetName(@operator) + " expecting " + arity + " operands).");
			res = false;
		}

		// Here the error must have been already reported
		if(type.IsEqual(BasicTypeNode.errorType))
			res = false;

		return res;
	}

	/// <summary>
	/// Determine the operator that will be used with this operator node.
	/// The method gets the operand types of this node and determines the
	/// operator, that will need the least implicit type casts using the
	/// operands' types (this is done via <seealso cref="Operator.getNearestOperator(int, TypeNode[])"/>).
	/// If no such operator is found, an error message is reported. </summary>
	/// <returns> The proper operator for this node, <code>null</code> otherwise. </returns>
	private OperatorDeclNode ComputeOperator()
	{
		OperatorDeclNode operatorDecl = null;
		IList<TypeNode> argTypes = new List<TypeNode>();

		for(int i = 0; i < children.Count; i++)
		{
			ExprNode op = children[i];
			TypeNode type = op.Type;
			if(type is ByteTypeNode || type is ShortTypeNode)
			{
				if(children.Count < 3)
					type = BasicTypeNode.intType;
			}
			argTypes.Add(type);
		}

		operatorDecl = OperatorDeclNode.GetNearestOperator(@operator, argTypes);
		if(!operatorDecl.IsValid())
		{
			StringBuilder @params = new StringBuilder();
			bool errorReported = false;

			@params.Append('(');
			for(int i = 0; i < children.Count; i++)
			{
				if(argTypes[i].IsEqual(BasicTypeNode.errorType))
					errorReported = true;
				else
					@params.Append((i > 0 ? ", " : "") + argTypes[i].ToString());
			}
			@params.Append(')');

			if(!errorReported)
				ReportError("The operator " + OperatorDeclNode.GetName(@operator) + @params + " is not known.");
		}
		else
		{
			// Insert implicit type casts for the arguments that need them.
			TypeNode[] opTypes = operatorDecl.OperandTypes;
			Debug.Assert((opTypes.Length == argTypes.Count));
			for(int i = 0; i < argTypes.Count; i++)
			{
				if(!argTypes[i].IsEqual(opTypes[i]))
				{
					ExprNode child = children[i];
					ExprNode adjusted = child.AdjustType(opTypes[i]);
					BecomeParent(adjusted);
					children[i] = adjusted;
				}
			}
		}

		return operatorDecl;
	}

	public OperatorDeclNode OperatorDecl
	{
		get
		{
			if(operatorDecl == null)
				operatorDecl = ComputeOperator();

			return operatorDecl;
		}
	}

	public Operator Operator
	{
		get
		{
			return @operator;
		}
	}

	/// <summary>
	/// Get the type of this expression. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.getType()"
	/// If a proper operator for this node can be found, the type of this
	/// node is the result type of the operator, else it's the error type
	/// <seealso cref="BasicTypeNode.errorType"/>./>
	public override TypeNode Type
	{
		get
		{
			return OperatorDecl.ResultType;
		}
	}
}

}
