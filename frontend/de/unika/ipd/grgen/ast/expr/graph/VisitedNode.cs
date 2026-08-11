/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using UntypedExecVarTypeNode = de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class VisitedNode : ExprNode
	{
		static VisitedNode()
		{
			SetClassName(typeof(VisitedNode), "visited");
		}

		private ExprNode visitorIDExpr;
		private ExprNode entityExpr;

		public VisitedNode(Coords coords, ExprNode visitorIDExpr, ExprNode entityExpr)
			: base(coords)
		{

			this.visitorIDExpr = visitorIDExpr;
			BecomeParent(visitorIDExpr);

			this.entityExpr = entityExpr;
			BecomeParent(entityExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(visitorIDExpr);
				children.Add(entityExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("visitorID");
				childrenNames.Add("entity");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			if(visitorIDExpr.Type is UntypedExecVarTypeNode)
				return true;
			if(!visitorIDExpr.Type.IsEqual(BasicTypeNode.intType))
			{
				visitorIDExpr.ReportError("The visited construct expects as index argument (visitorId) a value of type int"
						+ " (but is given a value of type " + visitorIDExpr.Type.TypeName + ").");
				return false;
			}
			if(entityExpr.Type is UntypedExecVarTypeNode)
				return true;
			if(entityExpr.Type is EdgeTypeNode)
				return true;
			if(entityExpr.Type is NodeTypeNode)
				return true;
			ReportError("The visited construct expects as entity argument a value of type node or edge"
					+ " (but is given a value of type " + entityExpr.Type.TypeName + ").");
			return true;
		}

		protected internal override IR ConstructIR()
		{
			visitorIDExpr = visitorIDExpr.Evaluate();
			entityExpr = entityExpr.Evaluate();
			return new Visited(visitorIDExpr.CheckIR<Expression>(typeof(Expression)), entityExpr.CheckIR<Expression>(typeof(Expression)));
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.booleanType;
			}
		}
	}

}
