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
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ContainerSingleElementInitNode = de.unika.ipd.grgen.ast.expr.ContainerSingleElementInitNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
	using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class DequeInitNode : ContainerSingleElementInitNode
	{
		static DequeInitNode()
		{
			SetClassName(typeof(DequeInitNode), "deque init");
		}

		// if deque init node is used in model, for member init
		//     then lhs != null, dequeType == null
		// if deque init node is used in actions, for anonymous const deque with specified type
		//     then lhs == null, dequeType != null -- adjust type of deque items to this type
		private BaseNode lhsUnresolved;
		private DeclNode lhs;
		private DequeTypeNode dequeType;

		public DequeInitNode(Coords coords, IdentNode member, DequeTypeNode dequeType)
			: base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.dequeType = dequeType;
		}

		private static readonly MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

		protected internal override bool ResolveLocal()
		{
			if(lhsUnresolved != null)
			{
				if(!lhsResolver.Resolve(lhsUnresolved))
					return false;
				lhs = lhsResolver.GetResult<DeclNode>(typeof(DeclNode));
				return lhsResolver.Finish();
			}
			else
			{
				if(dequeType == null)
					dequeType = CreateDequeType();
				return dequeType.Resolve();
			}
		}

		protected internal override bool CheckLocal()
		{
			bool success = CheckContainerItems();

			if(!IsConstant() && lhs != null)
			{
				ReportError("Only constant items are allowed in a deque initialization in the model.");
				success = false;
			}

			return success;
		}

		protected internal virtual DequeTypeNode CreateDequeType()
		{
			TypeNode itemTypeNode = EnumeratorHelper.GetFirstElement(containerItems.ChildrenExact).Type;
			IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).Ident;
			return new DequeTypeNode(itemTypeIdent);
		}

		public override ContainerTypeNode ContainerType
		{
			get
			{
				Debug.Assert((IsResolved()));
				if(lhs != null)
				{
					TypeNode type = lhs.DeclType;
					return (DequeTypeNode)type;
				}
				else
					return dequeType;
			}
		}

		public override bool IsInitInModel()
		{
			return dequeType == null;
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
			DequeType type = dequeType != null ? dequeType.CheckIR<DequeType>(typeof(DequeType)) : null;
			return new DequeInit(items, member, type, IsConstant());
		}

		public virtual DequeInit IRDequeInit
		{
			get
			{
				return CheckIR<DequeInit>(typeof(DequeInit));
			}
		}

		public static new string KindStr
		{
			get
			{
				return "deque initialization";
			}
		}
	}

}
