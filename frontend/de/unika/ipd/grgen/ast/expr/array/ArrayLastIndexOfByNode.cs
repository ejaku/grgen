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
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayLastIndexOfByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfByExpr;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayLastIndexOfByNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayLastIndexOfByNode()
		{
			SetClassName(typeof(ArrayLastIndexOfByNode), "array last index of by");
		}

		private IdentNode attribute;
		private DeclNode member;
		private ExprNode valueExpr;
		private ExprNode startIndexExpr;

		public ArrayLastIndexOfByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr)
			: base(coords, targetExpr)
		{
			this.attribute = attribute;
			this.valueExpr = BecomeParent(valueExpr);
		}

		public ArrayLastIndexOfByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr,
				ExprNode startIndexExpr)
			: base(coords, targetExpr)
		{
			this.attribute = attribute;
			this.valueExpr = BecomeParent(valueExpr);
			this.startIndexExpr = BecomeParent(startIndexExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(targetExpr);
				children.Add(valueExpr);
				if(startIndexExpr != null)
					children.Add(startIndexExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("targetExpr");
				childrenNames.Add("valueExpr");
				if(startIndexExpr != null)
					childrenNames.Add("startIndex");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			// target type already checked during resolving into this node
			ArrayTypeNode arrayType = TargetTypeExact;
			if(!(arrayType.valueType is InheritanceTypeNode)
					&& !(arrayType.valueType is MatchTypeNode))
			{
				targetExpr.ReportError("The array function method lastIndexOfBy can only be employed on an object of type array<nodes, edges, class objects, transient class objects, match types, match class types>"
						+ " (but is employed on an object of type " + arrayType.TypeName + ").");
				return false;
			}

			member = Resolver.ResolveMember(arrayType.valueType, attribute);
			if(member == null)
				return false;

			TypeNode memberType = member.DeclType;
			TypeNode valueType = valueExpr.Type;
			if(!valueType.IsEqual(memberType))
			{
				ExprNode valueExprOld = valueExpr;
				valueExpr = BecomeParent(valueExpr.AdjustType(memberType, Coords));
				if(valueExpr == ConstNode.Invalid)
				{
					valueExprOld.ReportError("The array function method lastIndexOfBy expects as 1. argument (valueToSearchFor) a value of type " + memberType.TypeName
							+ " (but is given a value of type " + valueType.TypeName + ").");
					return false;
				}
			}
			if(startIndexExpr != null && !startIndexExpr.Type.IsEqual(BasicTypeNode.intType))
			{
				startIndexExpr.ReportError("The array function method lastIndexOfBy expects as 2. argument (startIndex) a value of type int"
						+ " (but is given a value of type " + startIndexExpr.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.intType;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			valueExpr = valueExpr.Evaluate();
			if(startIndexExpr != null)
			{
				startIndexExpr = startIndexExpr.Evaluate();
				return new ArrayLastIndexOfByExpr(targetExpr.CheckIR(typeof(Expression)),
						member.CheckIR(typeof(Entity)),
						valueExpr.CheckIR(typeof(Expression)),
						startIndexExpr.CheckIR(typeof(Expression)));
			}
			else
			{
				return new ArrayLastIndexOfByExpr(targetExpr.CheckIR(typeof(Expression)),
						member.CheckIR(typeof(Entity)),
						valueExpr.CheckIR(typeof(Expression)));
			}
		}
	}

}
