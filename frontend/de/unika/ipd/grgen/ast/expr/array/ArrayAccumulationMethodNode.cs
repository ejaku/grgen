/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.array
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public abstract class ArrayAccumulationMethodNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayAccumulationMethodNode()
		{
			SetClassName(typeof(ArrayAccumulationMethodNode), "array accumulation method");
		}

		protected internal ArrayAccumulationMethodNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(targetExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("targetExpr");
				return childrenNames;
			}
		}

		// returns whether an array of the given type can be accumulated by this accumulation method
		public abstract bool IsValidTargetTypeOfAccumulation(TypeNode type);

		// returns the types allowed as target types of this accumulation method
		public abstract string ValidTargetTypesOfAccumulation {get;}

		// returns DUMMY object only to be used for checking with isValidTargetTypeOfAccumulation
		public static ArrayAccumulationMethodNode GetArrayMethodNode(string method)
		{
			switch(method)
			{
			case "sum":
				return new ArraySumNode(null, null);
			case "prod":
				return new ArrayProdNode(null, null);
			case "min":
				return new ArrayMinNode(null, null);
			case "max":
				return new ArrayMaxNode(null, null);
			case "avg":
				return new ArrayAvgNode(null, null);
			case "med":
				return new ArrayMedNode(null, null);
			case "medUnordered":
				return new ArrayMedUnorderedNode(null, null);
			case "var":
				return new ArrayVarNode(null, null);
			case "dev":
				return new ArrayDevNode(null, null);
			case "and":
				return new ArrayAndNode(null, null);
			case "or":
				return new ArrayOrNode(null, null);
			default:
				return null;
			}
		}
	}

}
