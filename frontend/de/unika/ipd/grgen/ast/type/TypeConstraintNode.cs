/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// TypeConstraintNode.java
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using TypeExprConst = de.unika.ipd.grgen.ir.type.TypeExprConst;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A type constraint expression AST node.
	/// TODO: Only one operand, operands-collect node is senseless? - if yes remove it
	/// </summary>
	public class TypeConstraintNode : TypeExprNode
	{
		static TypeConstraintNode()
		{
			SetClassName(typeof(TypeConstraintNode), "type expr constraint");
		}

		private CollectNode<InheritanceTypeNode> operands;
		private CollectNode<IdentNode> operandsUnresolved;

		public TypeConstraintNode(Coords coords, CollectNode<IdentNode> collect)
			: base(coords, TypeExprNode.TypeOperator.SET)
		{
			this.operandsUnresolved = collect;
			BecomeParent(this.operandsUnresolved);
			Debug.Assert((collect.Size() == 0)); // as of now only used for empty constraints, the other constructor is for the one constraint case, the other cases are modeled by the TypeBinaryExprNode (union: T1+T2)
		}

		public TypeConstraintNode(IdentNode typeIdentUse)
			: base(typeIdentUse.Coords, TypeExprNode.TypeOperator.SET)
		{
			this.operandsUnresolved = new CollectNode<IdentNode>();
			BecomeParent(this.operandsUnresolved);
			operandsUnresolved.AddChild(typeIdentUse);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(operandsUnresolved, operands));
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
				childrenNames.Add("operands");
				return childrenNames;
			}
		}

		private static readonly CollectResolver<InheritanceTypeNode> operandsResolver = new CollectResolver<InheritanceTypeNode>(
				new DeclarationTypeResolver<InheritanceTypeNode>(typeof(InheritanceTypeNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			operands = operandsResolver.Resolve(operandsUnresolved, this);

			return operands != null;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			TypeExprConst cnst = new TypeExprConst();

			foreach(InheritanceTypeNode n in operands.ChildrenExact)
			{
				InheritanceType inh = n.CheckIR<InheritanceType>(typeof(InheritanceType));
				cnst.AddOperand(inh);
			}

			return cnst;
		}
	}

}
