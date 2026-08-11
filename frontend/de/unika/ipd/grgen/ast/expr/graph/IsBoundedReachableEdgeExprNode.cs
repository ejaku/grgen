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
	using BooleanTypeNode = de.unika.ipd.grgen.ast.type.basic.BooleanTypeNode;
	using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using IsBoundedReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableEdgeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Direction = de.unika.ipd.grgen.util.Direction;

	/// <summary>
	/// An ast node telling whether an end edge can be reached from a start node within a given number of steps into depth,
	/// via incoming/outgoing/incident edges of given type, from/to a node of given type.
	/// Should extend IsInEdgeNeighborhoodQueryExprNode and BoundedNeighborhoodQueryExprNode, but Java does not support multiple inheritance...
	/// </summary>
	public class IsBoundedReachableEdgeExprNode : NeighborhoodQueryExprNode
	{
		static IsBoundedReachableEdgeExprNode()
		{
			SetClassName(typeof(IsBoundedReachableEdgeExprNode), "is bounded reachable edge expr");
		}

		private ExprNode endEdgeExpr;
		private ExprNode depthExpr;


		public IsBoundedReachableEdgeExprNode(Coords coords,
				ExprNode startNodeExpr, ExprNode endEdgeExpr, ExprNode depthExpr,
				ExprNode incidentTypeExpr, Direction direction,
				ExprNode adjacentTypeExpr)
			: base(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr)
		{
			this.endEdgeExpr = endEdgeExpr;
			BecomeParent(this.endEdgeExpr);
			this.depthExpr = depthExpr;
			BecomeParent(this.depthExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(startNodeExpr);
				children.Add(endEdgeExpr);
				children.Add(depthExpr);
				children.Add(incidentTypeExpr);
				children.Add(adjacentTypeExpr);
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
				childrenNames.Add("start node expr");
				childrenNames.Add("end edge expr");
				childrenNames.Add("depth expr");
				childrenNames.Add("incident type expr");
				childrenNames.Add("adjacent type expr");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(startNodeExpr.Type is NodeTypeNode))
			{
				ReportError("The function " + ShortSignature() + " expects as 1. argument a value of type node"
						+ " (but is given a value of type " + startNodeExpr.Type.TypeName + ").");
				return false;
			}
			if(!(endEdgeExpr.Type is EdgeTypeNode))
			{
				ReportError("The function " + ShortSignature() + " expects as 2. argument a value of type edge"
						+ " (but is given a value of type " + endEdgeExpr.Type.TypeName + ").");
				return false;
			}
			if(!(depthExpr.Type is IntTypeNode))
			{
				ReportError("The function " + ShortSignature() + " expects as 3. argument a value of type int"
						+ " (but is given a value of type " + depthExpr.Type.TypeName + ").");
				return false;
			}
			if(!(incidentTypeExpr.Type is EdgeTypeNode))
			{
				ReportError("The function " + ShortSignature() + " expects as 4. argument a value of type edge type"
						+ " (but is given a value of type " + incidentTypeExpr.Type.TypeName + ").");
				return false;
			}
			if(!(adjacentTypeExpr.Type is NodeTypeNode))
			{
				ReportError("The function " + ShortSignature() + " expects as 5. argument a value of type node type"
						+ " (but is given a value of type " + adjacentTypeExpr.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override string ShortSignature()
		{
			return "isBoundedReachableEdge(.,.,.,.,.)";
		}

		protected internal override IR ConstructIR()
		{
			startNodeExpr = startNodeExpr.Evaluate();
			endEdgeExpr = endEdgeExpr.Evaluate();
			incidentTypeExpr = incidentTypeExpr.Evaluate();
			adjacentTypeExpr = adjacentTypeExpr.Evaluate();
			// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
			return new IsBoundedReachableEdgeExpr(startNodeExpr.CheckIR(typeof(Expression)),
					endEdgeExpr.CheckIR(typeof(Expression)), depthExpr.CheckIR(typeof(Expression)),
					incidentTypeExpr.CheckIR(typeof(Expression)), direction,
					adjacentTypeExpr.CheckIR(typeof(Expression)),
					Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return BooleanTypeNode.booleanType;
			}
		}
	}

}
