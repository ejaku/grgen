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
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayCopyConstructor = de.unika.ipd.grgen.ir.expr.array.ArrayCopyConstructor;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayCopyConstructorNode : ExprNode
	{
		static ArrayCopyConstructorNode()
		{
			SetClassName(typeof(ArrayCopyConstructorNode), "array copy constructor");
		}

		private ArrayTypeNode arrayType;
		private ExprNode arrayToCopy;
		private BaseNode lhsUnresolved;

		public ArrayCopyConstructorNode(Coords coords, IdentNode member, ArrayTypeNode arrayType, ExprNode arrayToCopy)
			: base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.arrayType = arrayType;
			this.arrayToCopy = arrayToCopy;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(arrayToCopy);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("arrayToCopy");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			if(arrayType != null)
				return arrayType.Resolve();
			else
				return true;
		}

		protected internal override bool CheckLocal()
		{
			bool success = true;

			if(lhsUnresolved != null)
			{
				ReportError("An array copy constructor is not allowed in an array initialization in the model.");
				success = false;
			}
			else
			{
				if(arrayToCopy.Type is ArrayTypeNode)
				{
					ArrayTypeNode sourceArrayType = (ArrayTypeNode)arrayToCopy.Type;
					success &= CheckCopyConstructorTypes(arrayType.valueType, sourceArrayType.valueType, "array", false);
				}
				else
				{
					ReportError("An array copy constructor expects a value of array type to copy"
							+ " (but is given " + arrayToCopy.Type.TypeName + ").");
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
				return arrayType;
			}
		}

		protected internal override IR ConstructIR()
		{
			arrayToCopy = arrayToCopy.Evaluate();
			return new ArrayCopyConstructor(arrayToCopy.CheckIR<Expression>(typeof(Expression)), arrayType.CheckIR<ArrayType>(typeof(ArrayType)));
		}

		public static string KindStr
		{
			get
			{
				return "array copy constructor";
			}
		}
	}

}
