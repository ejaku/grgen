/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.Typeof;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.pattern.RetypedEdge;
import de.unika.ipd.grgen.ir.pattern.RetypedNode;
import de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * Builder class that adds elements from an AST pattern graph to an IR pattern graph
 */
public class PatternGraphBuilder
{
	public static void addElementsHiddenInUsedConstructs(PatternGraphLhsNode patternGraphNode, PatternGraphLhs patternGraph)
	{
		// add subpattern usage connection elements only mentioned there to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the subpattern usage connection)
		for(SubpatternUsageDeclNode subpatternUsageNode : patternGraphNode.subpatterns.getChildren()) {
			addSubpatternUsageArgument(patternGraph, subpatternUsageNode);
		}

		// add subpattern usage yield elements only mentioned there to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the subpattern usage yield)
		for(SubpatternUsageDeclNode subpatternUsageNode : patternGraphNode.subpatterns.getChildren()) {
			addSubpatternUsageYieldArgument(patternGraph, subpatternUsageNode);
		}

		// add elements only mentioned in typeof to the pattern
		for(Node node : patternGraph.getNodes()) {
			addNodeFromTypeof(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			addEdgeFromTypeof(patternGraph, edge);
		}

		// add Condition elements only mentioned there to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the condition)
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(Expression condition : patternGraph.getConditions()) {
			condition.collectNeededEntities(needs);
		}
		addNeededEntities(patternGraph, needs);

		// add Yielded elements only mentioned there to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the yield)
		needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(EvalStatements yield : patternGraph.getYields()) {
			yield.collectNeededEntities(needs);
		}
		addNeededEntities(patternGraph, needs);

		// add elements only mentioned in hom-declaration to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the hom-declaration)
		for(Collection<? extends GraphEntity> homEntities : patternGraph.getHomomorphic()) {
			addHomElements(patternGraph, homEntities);
		}

		// add elements only mentioned in "map by / draw from storage" entities to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the "map by / draw from storage" node)
		for(Node node : patternGraph.getNodes()) {
			addElementsFromStorageAccess(patternGraph, node);
		}

		for(Node node : patternGraph.getNodes()) {
			// add old node of lhs retype
			if(node instanceof RetypedNode && !node.isRHSEntity()) {
				patternGraph.addNodeIfNotYetContained(((RetypedNode)node).getOldNode());
			}
		}

		for(Edge edge : patternGraph.getEdges()) {
			addElementsFromStorageAccess(patternGraph, edge);
		}

		for(Edge edge : patternGraph.getEdges()) {
			// add old edge of lhs retype
			if(edge instanceof RetypedEdge && !edge.isRHSEntity()) {
				patternGraph.addEdgeIfNotYetContained(((RetypedEdge)edge).getOldEdge());
			}
		}

		// add index access elements only mentioned there to the IR
		// (they're declared in an enclosing pattern graph and locally only show up in the index access)
		needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(Node node : patternGraph.getNodes()) {
			if(node.indexAccess != null) {
				node.indexAccess.collectNeededEntities(needs);
			}
		}
		for(Edge edge : patternGraph.getEdges()) {
			if(edge.indexAccess != null) {
				edge.indexAccess.collectNeededEntities(needs);
			}
		}
		addNeededEntities(patternGraph, needs);
	}

	/**
	 * Generates a type condition if the given pattern graph entity inherits its type
	 * from another element via a typeof expression (dynamic type checks).
	 */
	public static void genTypeConditionsFromTypeof(PatternGraphLhs patternGraph, GraphEntity elem)
	{
		if(elem.inheritsType()) {
			assert !elem.isCopy(); // must extend this function and lgsp nodes if left hand side copy/copyof are wanted
								   // (meaning compare attributes of exact dynamic types)

			Expression e1 = new Typeof(elem);
			Expression e2 = new Typeof(elem.getTypeof());

			Operator op = new Operator(BasicTypeNode.booleanType.getPrimitiveType(), Operator.OperatorCode.GE);
			op.addOperand(e1);
			op.addOperand(e2);

			patternGraph.addCondition(op);
		}
	}

	private static void addSubpatternUsageArgument(PatternGraphLhs patternGraph, SubpatternUsageDeclNode subpatternUsageNode)
	{
		List<Expression> subpatternConnections = subpatternUsageNode.checkIR(SubpatternUsage.class).getSubpatternConnections();
		for(Expression expr : subpatternConnections) {
			if(expr instanceof GraphEntityExpression) {
				GraphEntity connection = ((GraphEntityExpression)expr).getGraphEntity();
				if(connection instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)connection);
				} else if(connection instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)connection);
				} else {
					assert(false);
				}
			} else {
				NeededEntities needs = new NeededEntities(false, false, true, false, false, false, false, false);
				expr.collectNeededEntities(needs);
				for(Variable neededVariable : needs.variables) {
					if(!patternGraph.hasVar(neededVariable)) {
						patternGraph.addVariable(neededVariable);
					}
				}
			}
		}
	}

	private static void addSubpatternUsageYieldArgument(PatternGraphLhs patternGraph, SubpatternUsageDeclNode subpatternUsageNode)
	{
		List<Expression> subpatternYields = subpatternUsageNode.checkIR(SubpatternUsage.class).getSubpatternYields();
		for(Expression expr : subpatternYields) {
			if(expr instanceof GraphEntityExpression) {
				GraphEntity connection = ((GraphEntityExpression)expr).getGraphEntity();
				if(connection instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)connection);
				} else if(connection instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)connection);
				} else {
					assert(false);
				}
			} else {
				NeededEntities needs = new NeededEntities(false, false, true, false, false, false, false, false);
				expr.collectNeededEntities(needs);
				for(Variable neededVariable : needs.variables) {
					if(!patternGraph.hasVar(neededVariable)) {
						patternGraph.addVariable(neededVariable);
					}
				}
			}
		}
	}

	private static void addNodeFromTypeof(PatternGraphLhs patternGraph, Node node)
	{
		if(node.inheritsType()) {
			patternGraph.addNodeIfNotYetContained((Node)node.getTypeof());
		}
	}

	private static void addEdgeFromTypeof(PatternGraphLhs patternGraph, Edge edge)
	{
		if(edge.inheritsType()) {
			patternGraph.addEdgeIfNotYetContained((Edge)edge.getTypeof());
		}
	}

	private static void addHomElements(PatternGraphLhs patternGraph, Collection<? extends GraphEntity> homEntities)
	{
		for(GraphEntity homEntity : homEntities) {
			if(homEntity instanceof Node) {
				patternGraph.addNodeIfNotYetContained((Node)homEntity);
			} else {
				patternGraph.addEdgeIfNotYetContained((Edge)homEntity);
			}
		}
	}

	private static void addElementsFromStorageAccess(PatternGraphLhs patternGraph, Node node)
	{
		if(node.storageAccess != null) {
			if(node.storageAccess.storageVariable != null) {
				Variable storageVariable = node.storageAccess.storageVariable;
				if(!patternGraph.hasVar(storageVariable)) {
					patternGraph.addVariable(storageVariable);
				}
			} else if(node.storageAccess.storageAttribute != null) {
				Qualification storageAttributeAccess = node.storageAccess.storageAttribute;
				if(storageAttributeAccess.getOwner() instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)storageAttributeAccess.getOwner());
				} else if(storageAttributeAccess.getOwner() instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)storageAttributeAccess.getOwner());
				}
			}
		}

		if(node.storageAccessIndex != null) {
			if(node.storageAccessIndex.indexGraphEntity != null) {
				GraphEntity indexGraphEntity = node.storageAccessIndex.indexGraphEntity;
				if(indexGraphEntity instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)indexGraphEntity);
				} else if(indexGraphEntity instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)indexGraphEntity);
				}
			}
		}
	}

	private static void addElementsFromStorageAccess(PatternGraphLhs patternGraph, Edge edge)
	{
		if(edge.storageAccess != null) {
			if(edge.storageAccess.storageVariable != null) {
				Variable storageVariable = edge.storageAccess.storageVariable;
				if(!patternGraph.hasVar(storageVariable)) {
					patternGraph.addVariable(storageVariable);
				}
			} else if(edge.storageAccess.storageAttribute != null) {
				Qualification storageAttributeAccess = edge.storageAccess.storageAttribute;
				if(storageAttributeAccess.getOwner() instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)storageAttributeAccess.getOwner());
				} else if(storageAttributeAccess.getOwner() instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)storageAttributeAccess.getOwner());
				}
			}
		}

		if(edge.storageAccessIndex != null) {
			if(edge.storageAccessIndex.indexGraphEntity != null) {
				GraphEntity indexGraphEntity = edge.storageAccessIndex.indexGraphEntity;
				if(indexGraphEntity instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)indexGraphEntity);
				} else if(indexGraphEntity instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)indexGraphEntity);
				}
			}
		}
	}

	protected static void addNeededEntities(PatternGraphLhs patternGraph, NeededEntities needs)
	{
		for(Node neededNode : needs.nodes) {
			patternGraph.addNodeIfNotYetContained(neededNode);
		}
		for(Edge neededEdge : needs.edges) {
			patternGraph.addEdgeIfNotYetContained(neededEdge);
		}
		for(Variable neededVariable : needs.variables) {
			if(!patternGraph.hasVar(neededVariable)) {
				patternGraph.addVariable(neededVariable);
			}
		}
	}

	public static void addHoms(PatternGraphLhs patternGraph, Set<ConstraintDeclNode> homEntityNodes)
	{
		// homSet is not empty, first element defines type of all elements
		if(homEntityNodes.iterator().next() instanceof NodeDeclNode) {
			HashSet<Node> homNodes = new HashSet<Node>();
			for(DeclNode node : homEntityNodes) {
				homNodes.add(node.checkIR(Node.class));
			}
			patternGraph.addHomomorphicNodes(homNodes);
		} else {
			HashSet<Edge> homEdges = new HashSet<Edge>();
			for(DeclNode edge : homEntityNodes) {
				homEdges.add(edge.checkIR(Edge.class));
			}
			patternGraph.addHomomorphicEdges(homEdges);
		}
	}

	public static void addTotallyHom(PatternGraphLhs patternGraph, TotallyHomNode totallyHomNode)
	{
		if(totallyHomNode.node != null) {
			HashSet<Node> totallyHomNodes = new HashSet<Node>();
			for(NodeDeclNode node : totallyHomNode.childrenNode) {
				totallyHomNodes.add(node.checkIR(Node.class));
			}
			patternGraph.addTotallyHomomorphic(totallyHomNode.node.checkIR(Node.class), totallyHomNodes);
		} else {
			HashSet<Edge> totallyHomEdges = new HashSet<Edge>();
			for(EdgeDeclNode edge : totallyHomNode.childrenEdge) {
				totallyHomEdges.add(edge.checkIR(Edge.class));
			}
			patternGraph.addTotallyHomomorphic(totallyHomNode.edge.checkIR(Edge.class), totallyHomEdges);
		}
	}

	// ensure def to be yielded to elements are hom to all others
	// so backend doing some fake search planning for them is not scheduling checks for them
	public static void ensureDefNodesAreHomToAllOthers(PatternGraphLhs patternGraph, Node node)
	{
		if(node.isDefToBeYieldedTo()) {
			patternGraph.addHomToAll(node);
		}
	}

	public static void ensureDefEdgesAreHomToAllOthers(PatternGraphLhs patternGraph, Edge edge)
	{
		if(edge.isDefToBeYieldedTo()) {
			patternGraph.addHomToAll(edge);
		}
	}

	// ensure lhs retype elements are hom to their old element
	public static void ensureRetypedNodeHomToOldNode(PatternGraphLhs patternGraph, Node node)
	{
		if(node instanceof RetypedNode && !node.isRHSEntity()) {
			Vector<Node> homNodes = new Vector<Node>();
			homNodes.add(node);
			homNodes.add(((RetypedNode)node).getOldNode());
			patternGraph.addHomomorphicNodes(homNodes);
		}
	}

	public static void ensureRetypedEdgeHomToOldEdge(PatternGraphLhs patternGraph, Edge edge)
	{
		if(edge instanceof RetypedEdge && !edge.isRHSEntity()) {
			Vector<Edge> homEdges = new Vector<Edge>();
			homEdges.add(edge);
			homEdges.add(((RetypedEdge)edge).getOldEdge());
			patternGraph.addHomomorphicEdges(homEdges);
		}
	}
	
	//----------------------------------------------------------------------------------------------------
	
	public static void addSubpatternReplacementUsageArguments(PatternGraphRhs patternGraph, OrderedReplacementsNode ors)
	{
		for(OrderedReplacementNode orderedReplNode : ors.getChildren()) {
			if(!(orderedReplNode instanceof SubpatternReplNode)) // only arguments of subpattern repl node (appearing before ---) are inserted to RHS pattern
				continue;
			SubpatternReplNode subpatternReplNode = (SubpatternReplNode)orderedReplNode;
			SubpatternDependentReplacement subpatternDepRepl = subpatternReplNode.checkIR(SubpatternDependentReplacement.class); 
			List<Expression> connections = subpatternDepRepl.getReplConnections();
			for(Expression expr : connections) {
				addSubpatternReplacementUsageArgument(patternGraph, expr);
			}
		}
	}

	private static void addSubpatternReplacementUsageArgument(PatternGraphRhs patternGraph, Expression expr)
	{
		if(expr instanceof GraphEntityExpression) {
			GraphEntity connection = ((GraphEntityExpression)expr).getGraphEntity();
			if(connection instanceof Node) {
				patternGraph.addNodeIfNotYetContained((Node)connection);
			} else if(connection instanceof Edge) {
				patternGraph.addEdgeIfNotYetContained((Edge)connection);
			} else {
				assert(false);
			}
		} else {
			NeededEntities needs = new NeededEntities(false, false, true, false, false, false, false, false);
			expr.collectNeededEntities(needs);
			for(Variable neededVariable : needs.variables) {
				if(!patternGraph.hasVar(neededVariable)) {
					patternGraph.addVariable(neededVariable);
				}
			}
		}
	}

	public static void addElementsUsedInDeferredExec(PatternGraphRhs patternGraph, ImperativeStmt impStmt)
	{
		if(impStmt instanceof Exec) {
			Set<Entity> neededEntities = ((Exec)impStmt).getNeededEntities(false);
			for(Entity entity : neededEntities) {
				if(entity instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)entity);
				} else if(entity instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)entity);
				} else {
					if(!patternGraph.hasVar((Variable)entity)) {
						patternGraph.addVariable((Variable)entity);
					}
				}
			}
		}
	}
}
