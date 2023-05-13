/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import java.util.EnumSet;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.MemberExpression;
import de.unika.ipd.grgen.ir.expr.array.ArrayMapExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayRemoveIfExpr;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Holds a collection of entities needed by an expression.
 */
public class NeededEntities
{
	// specifies the needed entities that are to be collected
	public enum Needs
	{
		NODES, // Specifies, whether needed nodes shall be collected.
		EDGES, // Specifies, whether needed edges shall be collected.
		VARS, // Specifies, whether needed variables shall be collected.
		ALL_ENTITIES, // Specifies, whether all needed entities (nodes, edges, vars) shall be collected.
		ALL_ATTRIBUTES, // Specifies, whether all pattern graph entities needed for attributes 
		 				// and the according attributes shall be collected. If this is true,
		 				// the pattern graph entities used to access the attributes will not be
		 				// automatically added to the nodes, edges, and entities sets, but only
		 				// in the attrNodes and attrEdges sets.
		CONTAINER_EXPRS, // Specifies, whether map, set, array, deque expressions shall be collected.
		COMPUTATION_CONTEXT, // Specifies, whether entities declared in computation context shall be collected.
		MEMBERS, // Specifies, whether entities referenced in member expressions 
		 		 // of member initializations in the model shall be collected.
		LAMBDAS // Specifies, whether lamba expressions (to be evaluated multiple times) shall be collected
				// also causes lambda expression variables to appear in the variables/entities in case these are collected.
	}
	
	/**
	 * Instantiates a new NeededEntities object.
	 */
	public NeededEntities(EnumSet<Needs> needs)
	{
		if(needs.contains(Needs.NODES))
			nodes = new LinkedHashSet<Node>();
		if(needs.contains(Needs.EDGES))
			edges = new LinkedHashSet<Edge>();
		if(needs.contains(Needs.VARS))
			variables = new LinkedHashSet<Variable>();
		if(needs.contains(Needs.ALL_ENTITIES))
			entities = new LinkedHashSet<Entity>();
		if(needs.contains(Needs.ALL_ATTRIBUTES)) {
			attrEntityMap = new LinkedHashMap<GraphEntity, HashSet<Entity>>();
			attrNodes = new LinkedHashSet<Node>();
			attrEdges = new LinkedHashSet<Edge>();
		}
		if(needs.contains(Needs.CONTAINER_EXPRS)) {
			this.collectContainerExprs = true;
			containerExprs = new LinkedHashSet<Expression>();
		}
		if(needs.contains(Needs.MEMBERS)) {
			members = new LinkedHashSet<Entity>();
		}
		if(needs.contains(Needs.COMPUTATION_CONTEXT)) {
			collectComputationContext = true;
		}
		if(needs.contains(Needs.LAMBDAS)) {
			lambdaExprs = new LinkedHashSet<Expression>();
		}
	}

	/**
	 * Specifies whether the graph is needed.
	 */
	public boolean isGraphUsed;

	/**
	 * The nodes needed.
	 */
	public HashSet<Node> nodes;

	/**
	 * The edges needed.
	 */
	public HashSet<Edge> edges;

	/**
	 * The variables needed.
	 */
	public HashSet<Variable> variables;

	/**
	 * The entities needed (nodes, edges, and variables).
	 */
	public HashSet<Entity> entities;

	/**
	 * The members needed (from member expressions for member initialization).
	 */
	public HashSet<Entity> members;

	/**
	 * The pattern graph entities needed for attributes mapped to the according attributes.
	 */
	public HashMap<GraphEntity, HashSet<Entity>> attrEntityMap;

	/**
	 * The nodes needed for attributes.
	 */
	public HashSet<Node> attrNodes;

	/**
	 * The edges needed for attributes.
	 */
	public HashSet<Edge> attrEdges;

	/**
	 * Specifies whether container expressions should be collected.
	 * Needs to temporarily set to false, that's why nulling containerExprs is not sufficient.
	 */
	public boolean collectContainerExprs;

	/**
	 * The container expressions.
	 */
	public HashSet<Expression> containerExprs;

	/**
	 * Specifies whether entities declared in computation context should be collected.
	 */
	public boolean collectComputationContext = false;

	/**
	 * The lambda expressions.
	 */
	public HashSet<Expression> lambdaExprs;

	/**
	 * Adds a needed graph entity.
	 * @param entity The needed entity.
	 */
	public void add(GraphEntity entity)
	{
		if((entity.getContext() & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
				&& !collectComputationContext)
			return;

		if(entity instanceof Node) {
			if(nodes != null)
				nodes.add((Node)entity);
		} else if(entity instanceof Edge) {
			if(edges != null)
				edges.add((Edge)entity);
		} else
			throw new UnsupportedOperationException("Unsupported entity (" + entity + ")");

		if(entities != null)
			entities.add(entity);
	}

	/**
	 * Adds a needed node.
	 * @param node The needed node.
	 */
	public void add(Node node)
	{
		if((node.getContext() & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
				&& !collectComputationContext)
			return;
		if(nodes != null)
			nodes.add(node);
		if(entities != null)
			entities.add(node);
	}

	/**
	 * Adds a needed edge.
	 * @param edge The needed edge.
	 */
	public void add(Edge edge)
	{
		if((edge.getContext() & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
				&& !collectComputationContext)
			return;
		if(edges != null)
			edges.add(edge);
		if(entities != null)
			entities.add(edge);
	}

	/**
	 * Adds a needed variable.
	 * @param var The needed variable.
	 */
	public void add(Variable var)
	{
		if((var.getContext() & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
				&& !collectComputationContext)
			return;
		if(variables != null && (!var.isLambdaExpressionVariable || lambdaExprs != null))
			variables.add(var);
		if(entities != null && (!var.isLambdaExpressionVariable || lambdaExprs != null))
			entities.add(var);
	}

	/**
	 * Adds a needed attribute.
	 * @param grEnt The entity being accessed.
	 * @param attr The needed attribute.
	 */
	public void addAttr(GraphEntity grEnt, Entity attr)
	{
		if((grEnt.getContext() & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
				&& !collectComputationContext)
			return;

		if(attrEntityMap == null) {
			add(grEnt);
			return;
		}

		HashSet<Entity> attrs = attrEntityMap.get(grEnt);
		if(attrs == null)
			attrEntityMap.put(grEnt, attrs = new LinkedHashSet<Entity>());
		attrs.add(attr);

		if(grEnt instanceof Node)
			attrNodes.add((Node)grEnt);
		else if(grEnt instanceof Edge)
			attrEdges.add((Edge)grEnt);
		else
			throw new UnsupportedOperationException("Unsupported entity (" + grEnt + ")");
	}

	/**
	 * Adds a container expression.
	 * @param expr The container expressions.
	 */
	public void add(Expression expr)
	{
		if(collectContainerExprs)
			containerExprs.add(expr);
	}

	/**
	 * Adds a member expression.
	 * @param expr The member expressions.
	 */
	public void add(MemberExpression expr)
	{
		if(members != null)
			members.add(expr.getMember());
	}

	/**
	 * Adds a lambda expression.
	 * @param expr The lambda expressions.
	 */
	public void add(ArrayMapExpr expr)
	{
		if(lambdaExprs != null)
			lambdaExprs.add(expr);
	}

	/**
	 * Adds a lambda expression.
	 * @param expr The lambda expressions.
	 */
	public void add(ArrayRemoveIfExpr expr)
	{
		if(lambdaExprs != null)
			lambdaExprs.add(expr);
	}

	public void needsGraph()
	{
		isGraphUsed = true;
	}
}
