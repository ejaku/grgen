/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.set
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ContainerSingleElementInitNode = de.unika.ipd.grgen.ast.expr.ContainerSingleElementInitNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class SetInitNode : ContainerSingleElementInitNode
	{
		static SetInitNode()
		{
			SetClassName(typeof(SetInitNode), "set init");
		}

		// if set init node is used in model, for member init
		//     then lhs != null, setType == null
		// if set init node is used in actions, for anonymous const set with specified type
		//     then lhs == null, setType != null -- adjust type of set items to this type
		private BaseNode lhsUnresolved;
		private DeclNode lhs;
		private SetTypeNode setType;

		public SetInitNode(Coords coords, IdentNode member, SetTypeNode setType)
			: base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.setType = setType;
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
				if(setType == null)
					setType = CreateSetType();
				return setType.Resolve();
			}
		}

		protected internal override bool CheckLocal()
		{
			bool success = CheckContainerItems();

			if(!IsConstant() && lhs != null)
			{
				ReportError("Only constant items are allowed in a set initialization in the model.");
				success = false;
			}

			return success;
		}

		protected internal virtual SetTypeNode CreateSetType()
		{
			TypeNode itemTypeNode = containerItems.ChildrenExact.GetEnumerator().Next().GetType();
			IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).Ident;
			return new SetTypeNode(itemTypeIdent);
		}

		public override ContainerTypeNode ContainerType
		{
			get
			{
				Debug.Assert((IsResolved()));
				if(lhs != null)
				{
					TypeNode type = lhs.DeclType;
					return (SetTypeNode)type;
				}
				else
					return setType;
			}
		}

		public override bool IsInitInModel()
		{
			return setType == null;
		}

		protected internal override IR ConstructIR()
		{
			IList<Expression> items = ConstructItems();
			Entity member = lhs != null ? lhs.IREntity : null;
			SetType type = setType != null ? setType.CheckIR(typeof(SetType)) : null;
			return new SetInit(items, member, type, IsConstant());
		}

		public virtual SetInit IRSetInit
		{
			get
			{
				return CheckIR(typeof(SetInit));
			}
		}

		public static string KindStr
		{
			get
			{
				return "set initialization";
			}
		}
	}

}
