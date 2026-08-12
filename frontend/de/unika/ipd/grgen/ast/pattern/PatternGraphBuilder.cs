/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.pattern
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
	using CopyKind = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode.CopyKind;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Exec = de.unika.ipd.grgen.ir.Exec;
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Typeof = de.unika.ipd.grgen.ir.expr.Typeof;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using RetypedEdge = de.unika.ipd.grgen.ir.pattern.RetypedEdge;
	using RetypedNode = de.unika.ipd.grgen.ir.pattern.RetypedNode;
	using SubpatternDependentReplacement = de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

	/// <summary>
	/// Builder class that adds elements from an AST pattern graph to an IR pattern graph
	/// </summary>
	public class PatternGraphBuilder
	{
		public static void AddElementsHiddenInUsedConstructs(PatternGraphLhsNode patternGraphNode, PatternGraphLhs patternGraph)
		{
			// add subpattern usage connection elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the subpattern usage connection)
			foreach(SubpatternUsageDeclNode subpatternUsageNode in patternGraphNode.subpatterns.ChildrenExact)
				AddSubpatternUsageArgument(patternGraph, subpatternUsageNode);

			// add subpattern usage yield elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the subpattern usage yield)
			foreach(SubpatternUsageDeclNode subpatternUsageNode in patternGraphNode.subpatterns.ChildrenExact)
				AddSubpatternUsageYieldArgument(patternGraph, subpatternUsageNode);

			// add elements only mentioned in typeof to the pattern
			foreach(Node node in patternGraph.Nodes)
				AddNodeFromTypeof(patternGraph, node);
			foreach(Edge edge in patternGraph.Edges)
				AddEdgeFromTypeof(patternGraph, edge);

			// add Condition elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the condition)
			NeededEntities needs = new NeededEntities(NeededEntities.Needs.NODES | NeededEntities.Needs.EDGES | NeededEntities.Needs.VARS | NeededEntities.Needs.CONTAINER_EXPRS);
			foreach(Expression condition in patternGraph.Conditions)
				condition.CollectNeededEntities(needs);
			AddNeededEntities(patternGraph, needs);

			// add Yielded elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the yield)
			needs = new NeededEntities(NeededEntities.Needs.NODES | NeededEntities.Needs.EDGES | NeededEntities.Needs.VARS | NeededEntities.Needs.CONTAINER_EXPRS);
			foreach(EvalStatements yield in patternGraph.Yields)
				yield.CollectNeededEntities(needs);
			AddNeededEntities(patternGraph, needs);

			// add elements only mentioned in hom-declaration to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the hom-declaration)
			foreach(ICollection<GraphEntity> homEntities in patternGraph.Homomorphic)
				AddHomElements(patternGraph, homEntities);

			// add elements only mentioned in "map by / draw from storage" entities to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the "map by / draw from storage" node)
			foreach(Node node in patternGraph.Nodes)
				AddElementsFromStorageAccess(patternGraph, node);

			foreach(Node node in patternGraph.Nodes)
			{
				// add old node of lhs retype
				if(node is RetypedNode && !node.IsRHSEntity())
					patternGraph.AddNodeIfNotYetContained(((RetypedNode)node).OldNode);
			}

			foreach(Edge edge in patternGraph.Edges)
				AddElementsFromStorageAccess(patternGraph, edge);

			foreach(Edge edge in patternGraph.Edges)
			{
				// add old edge of lhs retype
				if(edge is RetypedEdge && !edge.IsRHSEntity())
					patternGraph.AddEdgeIfNotYetContained(((RetypedEdge)edge).OldEdge);
			}

			// add index access elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the index access)
			needs = new NeededEntities(NeededEntities.Needs.NODES | NeededEntities.Needs.EDGES | NeededEntities.Needs.VARS | NeededEntities.Needs.CONTAINER_EXPRS);
			foreach(Node node in patternGraph.Nodes)
			{
				if(node.indexAccess != null)
					node.indexAccess.CollectNeededEntities(needs);
			}
			foreach(Edge edge in patternGraph.Edges)
			{
				if(edge.indexAccess != null)
					edge.indexAccess.CollectNeededEntities(needs);
			}
			AddNeededEntities(patternGraph, needs);
		}

		/// <summary>
		/// Generates a type condition if the given pattern graph entity inherits its type
		/// from another element via a typeof expression (dynamic type checks).
		/// </summary>
		public static void GenTypeConditionsFromTypeof(PatternGraphLhs patternGraph, GraphEntity elem)
		{
			if(elem.InheritsType())
			{
				Debug.Assert(elem.Copy == ConstraintDeclNode.CopyKind.None); // must extend this function and lgsp nodes if left hand side copy/copyof are wanted
														// (meaning compare attributes of exact dynamic types)

				Expression e1 = new Typeof(elem);
				Expression e2 = new Typeof(elem.Typeof);

				Operator op = new Operator(BasicTypeNode.booleanType.IRPrimitiveType, OperatorCode.GE);
				op.AddOperand(e1);
				op.AddOperand(e2);

				patternGraph.AddCondition(op);
			}
		}

		private static void AddSubpatternUsageArgument(PatternGraphLhs patternGraph, SubpatternUsageDeclNode subpatternUsageNode)
		{
			IList<Expression> subpatternConnections = subpatternUsageNode.CheckIR<SubpatternUsage>(typeof(SubpatternUsage)).SubpatternConnections;
			foreach(Expression expr in subpatternConnections)
			{
				if(expr is GraphEntityExpression)
				{
					GraphEntity connection = ((GraphEntityExpression)expr).GraphEntity;
					if(connection is Node)
						patternGraph.AddNodeIfNotYetContained((Node)connection);
					else if(connection is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)connection);
					else
						Debug.Assert((false));
				}
				else
				{
					NeededEntities needs = new NeededEntities(NeededEntities.Needs.VARS);
					expr.CollectNeededEntities(needs);
					foreach(Variable neededVariable in needs.variables)
					{
						if(!patternGraph.HasVar(neededVariable))
							patternGraph.AddVariable(neededVariable);
					}
				}
			}
		}

		private static void AddSubpatternUsageYieldArgument(PatternGraphLhs patternGraph, SubpatternUsageDeclNode subpatternUsageNode)
		{
			IList<Expression> subpatternYields = subpatternUsageNode.CheckIR<SubpatternUsage>(typeof(SubpatternUsage)).SubpatternYields;
			foreach(Expression expr in subpatternYields)
			{
				if(expr is GraphEntityExpression)
				{
					GraphEntity connection = ((GraphEntityExpression)expr).GraphEntity;
					if(connection is Node)
						patternGraph.AddNodeIfNotYetContained((Node)connection);
					else if(connection is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)connection);
					else
						Debug.Assert((false));
				}
				else
				{
					NeededEntities needs = new NeededEntities(NeededEntities.Needs.VARS);
					expr.CollectNeededEntities(needs);
					foreach(Variable neededVariable in needs.variables)
					{
						if(!patternGraph.HasVar(neededVariable))
							patternGraph.AddVariable(neededVariable);
					}
				}
			}
		}

		private static void AddNodeFromTypeof(PatternGraphLhs patternGraph, Node node)
		{
			if(node.InheritsType())
				patternGraph.AddNodeIfNotYetContained((Node)node.Typeof);
		}

		private static void AddEdgeFromTypeof(PatternGraphLhs patternGraph, Edge edge)
		{
			if(edge.InheritsType())
				patternGraph.AddEdgeIfNotYetContained((Edge)edge.Typeof);
		}

		private static void AddHomElements<T1>(PatternGraphLhs patternGraph, ICollection<T1> homEntities) where T1 : de.unika.ipd.grgen.ir.pattern.GraphEntity
		{
			foreach(GraphEntity homEntity in homEntities)
			{
				if(homEntity is Node)
					patternGraph.AddNodeIfNotYetContained((Node)homEntity);
				else
					patternGraph.AddEdgeIfNotYetContained((Edge)homEntity);
			}
		}

		private static void AddElementsFromStorageAccess(PatternGraphLhs patternGraph, Node node)
		{
			if(node.storageAccess != null)
			{
				if(node.storageAccess.storageVariable != null)
				{
					Variable storageVariable = node.storageAccess.storageVariable;
					if(!patternGraph.HasVar(storageVariable))
						patternGraph.AddVariable(storageVariable);
				}
				else if(node.storageAccess.storageAttribute != null)
				{
					Qualification storageAttributeAccess = node.storageAccess.storageAttribute;
					if(storageAttributeAccess.Owner is Node)
						patternGraph.AddNodeIfNotYetContained((Node)storageAttributeAccess.Owner);
					else if(storageAttributeAccess.Owner is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)storageAttributeAccess.Owner);
				}
			}

			if(node.storageAccessIndex != null)
			{
				if(node.storageAccessIndex.indexGraphEntity != null)
				{
					GraphEntity indexGraphEntity = node.storageAccessIndex.indexGraphEntity;
					if(indexGraphEntity is Node)
						patternGraph.AddNodeIfNotYetContained((Node)indexGraphEntity);
					else if(indexGraphEntity is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)indexGraphEntity);
				}
			}
		}

		private static void AddElementsFromStorageAccess(PatternGraphLhs patternGraph, Edge edge)
		{
			if(edge.storageAccess != null)
			{
				if(edge.storageAccess.storageVariable != null)
				{
					Variable storageVariable = edge.storageAccess.storageVariable;
					if(!patternGraph.HasVar(storageVariable))
						patternGraph.AddVariable(storageVariable);
				}
				else if(edge.storageAccess.storageAttribute != null)
				{
					Qualification storageAttributeAccess = edge.storageAccess.storageAttribute;
					if(storageAttributeAccess.Owner is Node)
						patternGraph.AddNodeIfNotYetContained((Node)storageAttributeAccess.Owner);
					else if(storageAttributeAccess.Owner is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)storageAttributeAccess.Owner);
				}
			}

			if(edge.storageAccessIndex != null)
			{
				if(edge.storageAccessIndex.indexGraphEntity != null)
				{
					GraphEntity indexGraphEntity = edge.storageAccessIndex.indexGraphEntity;
					if(indexGraphEntity is Node)
						patternGraph.AddNodeIfNotYetContained((Node)indexGraphEntity);
					else if(indexGraphEntity is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)indexGraphEntity);
				}
			}
		}

		protected internal static void AddNeededEntities(PatternGraphLhs patternGraph, NeededEntities needs)
		{
			foreach(Node neededNode in needs.nodes)
				patternGraph.AddNodeIfNotYetContained(neededNode);
			foreach(Edge neededEdge in needs.edges)
				patternGraph.AddEdgeIfNotYetContained(neededEdge);
			foreach(Variable neededVariable in needs.variables)
			{
				if(!patternGraph.HasVar(neededVariable))
					patternGraph.AddVariable(neededVariable);
			}
		}

		public static void AddHoms(PatternGraphLhs patternGraph, ISet<ConstraintDeclNode> homEntityNodes)
		{
			// homSet is not empty, first element defines type of all elements
			if(homEntityNodes.GetEnumerator().Next() is NodeDeclNode)
			{
				HashSet<Node> homNodes = new HashSet<Node>();
				foreach(DeclNode node in homEntityNodes)
					homNodes.Add(node.CheckIR<Node>(typeof(Node)));
				patternGraph.AddHomomorphicNodes(homNodes);
			}
			else
			{
				HashSet<Edge> homEdges = new HashSet<Edge>();
				foreach(DeclNode edge in homEntityNodes)
					homEdges.Add(edge.CheckIR<Edge>(typeof(Edge)));
				patternGraph.AddHomomorphicEdges(homEdges);
			}
		}

		public static void AddTotallyHom(PatternGraphLhs patternGraph, TotallyHomNode totallyHomNode)
		{
			if(totallyHomNode.node != null)
			{
				HashSet<Node> totallyHomNodes = new HashSet<Node>();
				foreach(NodeDeclNode node in totallyHomNode.childrenNode)
					totallyHomNodes.Add(node.CheckIR<Node>(typeof(Node)));
				patternGraph.AddTotallyHomomorphic(totallyHomNode.node.CheckIR<Node>(typeof(Node)), totallyHomNodes);
			}
			else
			{
				HashSet<Edge> totallyHomEdges = new HashSet<Edge>();
				foreach(EdgeDeclNode edge in totallyHomNode.childrenEdge)
					totallyHomEdges.Add(edge.CheckIR<Edge>(typeof(Edge)));
				patternGraph.AddTotallyHomomorphic(totallyHomNode.edge.CheckIR<Edge>(typeof(Edge)), totallyHomEdges);
			}
		}

		// ensure def to be yielded to elements are hom to all others
		// so backend doing some fake search planning for them is not scheduling checks for them
		public static void EnsureDefNodesAreHomToAllOthers(PatternGraphLhs patternGraph, Node node)
		{
			if(node.IsDefToBeYieldedTo())
				patternGraph.AddHomToAll(node);
		}

		public static void EnsureDefEdgesAreHomToAllOthers(PatternGraphLhs patternGraph, Edge edge)
		{
			if(edge.IsDefToBeYieldedTo())
				patternGraph.AddHomToAll(edge);
		}

		// ensure lhs retype elements are hom to their old element
		public static void EnsureRetypedNodeHomToOldNode(PatternGraphLhs patternGraph, Node node)
		{
			if(node is RetypedNode && !node.IsRHSEntity())
			{
				IList<Node> homNodes = new List<Node>();
				homNodes.Add(node);
				homNodes.Add(((RetypedNode)node).OldNode);
				patternGraph.AddHomomorphicNodes(homNodes);
			}
		}

		public static void EnsureRetypedEdgeHomToOldEdge(PatternGraphLhs patternGraph, Edge edge)
		{
			if(edge is RetypedEdge && !edge.IsRHSEntity())
			{
				IList<Edge> homEdges = new List<Edge>();
				homEdges.Add(edge);
				homEdges.Add(((RetypedEdge)edge).OldEdge);
				patternGraph.AddHomomorphicEdges(homEdges);
			}
		}

		//----------------------------------------------------------------------------------------------------

		public static void AddSubpatternReplacementUsageArguments(PatternGraphRhs patternGraph, OrderedReplacementsNode ors)
		{
			foreach(OrderedReplacementNode orderedReplNode in ors.ChildrenExact)
			{
				if(!(orderedReplNode is SubpatternReplNode)) // only arguments of subpattern repl node (appearing before ---) are inserted to RHS pattern
					continue;
				SubpatternReplNode subpatternReplNode = (SubpatternReplNode)orderedReplNode;
				SubpatternDependentReplacement subpatternDepRepl = subpatternReplNode.CheckIR<SubpatternDependentReplacement>(typeof(SubpatternDependentReplacement));
				IList<Expression> connections = subpatternDepRepl.ReplConnections;
				foreach(Expression expr in connections)
					AddSubpatternReplacementUsageArgument(patternGraph, expr);
			}
		}

		private static void AddSubpatternReplacementUsageArgument(PatternGraphRhs patternGraph, Expression expr)
		{
			if(expr is GraphEntityExpression)
			{
				GraphEntity connection = ((GraphEntityExpression)expr).GraphEntity;
				if(connection is Node)
					patternGraph.AddNodeIfNotYetContained((Node)connection);
				else if(connection is Edge)
					patternGraph.AddEdgeIfNotYetContained((Edge)connection);
				else
					Debug.Assert((false));
			}
			else
			{
				NeededEntities needs = new NeededEntities(NeededEntities.Needs.VARS);
				expr.CollectNeededEntities(needs);
				foreach(Variable neededVariable in needs.variables)
				{
					if(!patternGraph.HasVar(neededVariable))
						patternGraph.AddVariable(neededVariable);
				}
			}
		}

		public static void AddElementsUsedInDeferredExec(PatternGraphRhs patternGraph, ImperativeStmt impStmt)
		{
			if(impStmt is Exec)
			{
				ISet<Entity> neededEntities = ((Exec)impStmt).GetNeededEntities(false);
				foreach(Entity entity in neededEntities)
				{
					if(entity is Node)
						patternGraph.AddNodeIfNotYetContained((Node)entity);
					else if(entity is Edge)
						patternGraph.AddEdgeIfNotYetContained((Edge)entity);
					else
					{
						if(!patternGraph.HasVar((Variable)entity))
							patternGraph.AddVariable((Variable)entity);
					}
				}
			}
		}
	}

}
