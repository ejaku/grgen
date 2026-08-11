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
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ContainerSingleElementInitNode = de.unika.ipd.grgen.ast.expr.ContainerSingleElementInitNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayInitNode : ContainerSingleElementInitNode
	{
		static ArrayInitNode()
		{
			SetClassName(typeof(ArrayInitNode), "array init");
		}

		// if array init node is used in model, for member init
		//     then lhs != null, arrayType == null
		// if array init node is used in actions, for anonymous const array with specified type
		//     then lhs == null, arrayType != null -- adjust type of array items to this type
		private BaseNode lhsUnresolved;
		private DeclNode lhs;
		private ArrayTypeNode arrayType;

		public ArrayInitNode(Coords coords, IdentNode member, ArrayTypeNode arrayType)
			: base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.arrayType = arrayType;
		}

		private static readonly MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

		protected internal override bool ResolveLocal()
		{
			if(lhsUnresolved != null)
			{
				if(!lhsResolver.Resolve(lhsUnresolved))
					return false;
				lhs = lhsResolver.GetResult(typeof(DeclNode));
				return lhsResolver.Finish();
			}
			else
			{
				if(arrayType == null)
					arrayType = CreateArrayType();
				return arrayType.Resolve();
			}
		}

		protected internal override bool CheckLocal()
		{
			bool success = CheckContainerItems();

			if(!IsConstant() && lhs != null)
			{
				ReportError("Only constant items are allowed in an array initialization in the model.");
				success = false;
			}

			return success;
		}

		protected internal virtual ArrayTypeNode CreateArrayType()
		{
			TypeNode itemTypeNode = containerItems.ChildrenExact.GetEnumerator().Next().GetType();
			IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).Ident;
			return new ArrayTypeNode(itemTypeIdent);
		}

		public override ContainerTypeNode ContainerType
		{
			get
			{
				Debug.Assert((IsResolved()));
				if(lhs != null)
				{
					TypeNode type = lhs.DeclType;
					return (ArrayTypeNode)type;
				}
				else
					return arrayType;
			}
		}

		public override bool IsInitInModel()
		{
			return arrayType == null;
		}

		public virtual ExprNode GetAtIndex(ConstNode node)
		{
			int? index = (int?)node.Value;
			if(index.Value < 0)
				return null;
			if(index.Value >= containerItems.Size())
				return null;
			return containerItems.ChildrenAsList[index.Value];
		}

		protected internal override IR ConstructIR()
		{
			IList<Expression> items = ConstructItems();
			Entity member = lhs != null ? lhs.IREntity : null;
			ArrayType type = arrayType != null ? arrayType.CheckIR(typeof(ArrayType)) : null;
			return new ArrayInit(items, member, type, IsConstant());
		}

		public virtual ArrayInit IRArrayInit
		{
			get
			{
				return CheckIR(typeof(ArrayInit));
			}
		}

		public static string KindStr
		{
			get
			{
				return "array initialization";
			}
		}
	}

}
