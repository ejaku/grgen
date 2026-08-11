/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IncidenceCountIndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IncidenceCountIndexDeclNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using CountIncidenceFromIndexExpr = de.unika.ipd.grgen.ir.expr.graph.CountIncidenceFromIndexExpr;
	using IncidenceCountIndex = de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class CountIncidenceFromIndexExprNode : BuiltinFunctionInvocationBaseNode
	{
		static CountIncidenceFromIndexExprNode()
		{
			SetClassName(typeof(CountIncidenceFromIndexExprNode), "count incidence from index access expression");
		}

		private BaseNode indexUnresolved;
		private IncidenceCountIndexDeclNode index;
		private ExprNode keyExpr;

		public CountIncidenceFromIndexExprNode(Coords coords, BaseNode index, ExprNode keyExpr)
			: base(coords)
		{
			this.indexUnresolved = BecomeParent(index);
			this.keyExpr = BecomeParent(keyExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(indexUnresolved, index));
				children.Add(keyExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("index");
				childrenNames.Add("keyExpr");
				return childrenNames;
			}
		}

		private static DeclarationResolver<IncidenceCountIndexDeclNode> indexResolver =
				new DeclarationResolver<IncidenceCountIndexDeclNode>(typeof(IncidenceCountIndexDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = base.ResolveLocal();
			index = indexResolver.Resolve(indexUnresolved, this);
			if(index == null)
				ReportError("The function countFromIndex(.,.) expects as 1. argument (index) an incidence count index"
								+ " (but is given " + indexUnresolved.ToStringWithDeclarationCoords() + ").");
			successfullyResolved &= index != null;
			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			TypeNode keyType = index.Type;
			TypeNode keyExprType = keyExpr.Type;

			if(keyExprType is InheritanceTypeNode)
			{
				if(keyExprType.IsCompatibleTo(keyType))
					return true;

				string givenTypeName = keyExprType.TypeName;
				string expectedTypeName = keyType.TypeName;
				ReportError("The function countFromIndex(.,.) expects as 2. argument (keyExpr) a value of type " + expectedTypeName
								+ " (but is given a value of type " + givenTypeName + ").");

				return false;
			}
			else
			{
				if(keyExprType.IsEqual(keyType))
					return true;

				keyExpr = BecomeParent(keyExpr.AdjustType(keyType, Coords));
				return keyExpr != ConstNode.Invalid;
			}
		}

		public override TypeNode Type
		{
			get
			{
				return IntTypeNode.intType;
			}
		}

		protected internal override IR ConstructIR()
		{
			keyExpr = keyExpr.Evaluate();
			return new CountIncidenceFromIndexExpr(index.CheckIR<IncidenceCountIndex>(typeof(IncidenceCountIndex)),
					keyExpr.CheckIR<Expression>(typeof(Expression)));
		}
	}

}
