/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ir;

import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;

/**
 * Holds a collection of entities needed by an expression.
 */
public class NeededEntities {
	/**
	 * Instantiates a new NeededEntities object.
	 * @param collectNodes Specifies, whether needed nodes shall be collected.
	 * @param collectEdges Specifies, whether needed edges shall be collected.
	 * @param collectVars Specifies, whether needed variables shall be collected.
	 * @param collectAllEntities Specifies, whether all needed entities
	 *      (nodes, edges, vars) shall be collected.
	 * @param collectAllAttributes Specifies, whether all graph entities needed for attributes
	 *      and the according attributes shall be collected. If this is true,
	 *      the graph entities used to access the attributes will not be
	 *      automatically added to the nodes, edges, and entities sets, but only
	 *      in the attrNodes and attrEdges sets.
	 * @param collectMapSetExprs Specifies, whether map and set expressions shall be collected.
	 */
	public NeededEntities(boolean collectNodes, boolean collectEdges, boolean collectVars,
			boolean collectAllEntities, boolean collectAllAttributes, boolean collectMapSetExprs) {
		if(collectNodes)       nodes     = new LinkedHashSet<Node>();
		if(collectEdges)       edges     = new LinkedHashSet<Edge>();
		if(collectVars)        variables = new LinkedHashSet<Variable>();
		if(collectAllEntities) entities  = new LinkedHashSet<Entity>();
		if(collectAllAttributes) {
			attrEntityMap = new LinkedHashMap<GraphEntity, HashSet<Entity>>();
			attrNodes     = new LinkedHashSet<Node>();
			attrEdges     = new LinkedHashSet<Edge>();
		}
		if(collectMapSetExprs) {
			this.collectMapSetExprs = true;
			mapSetExprs = new LinkedHashSet<Expression>();
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
	 * The graph entities needed for attributes mapped to the according attributes.
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
	 * Specifies whether map and set expressions should be collected.
	 */
	public boolean collectMapSetExprs;
	
	/**
	 * The map and set expressions.
	 */
	public HashSet<Expression> mapSetExprs;
	
	/**
	 * Adds a needed graph entity.
	 * @param entity The needed entity.
	 */
	public void add(GraphEntity entity) {
		if(entity instanceof Node) {
			if(nodes != null) nodes.add((Node) entity);
		}
		else if(entity instanceof Edge) {
			if(edges != null) edges.add((Edge) entity);
		}
		else
			throw new UnsupportedOperationException("Unsupported entity (" + entity + ")");
		
		if(entities != null) entities.add(entity);
	}
	
	/**
	 * Adds a needed node.
	 * @param node The needed node.
	 */
	public void add(Node node) {
		if(nodes != null) nodes.add(node);
		if(entities != null) entities.add(node);
	}

	/**
	 * Adds a needed edge.
	 * @param edge The needed edge.
	 */
	public void add(Edge edge) {
		if(edges != null) edges.add(edge);
		if(entities != null) entities.add(edge);
	}

	/**
	 * Adds a needed variable.
	 * @param var The needed variable.
	 */
	public void add(Variable var) {
		if(variables != null) variables.add(var);
		if(entities != null) entities.add(var);
	}
	
	/**
	 * Adds a needed attribute.
	 * @param grEnt The entity being accessed.
	 * @param attr The needed attribute.
	 */
	public void addAttr(GraphEntity grEnt, Entity attr) {
		if(attrEntityMap == null) {
			add(grEnt);
			return;
		}
		
		HashSet<Entity> attrs = attrEntityMap.get(grEnt);
		if(attrs == null)
			attrEntityMap.put(grEnt, attrs = new LinkedHashSet<Entity>());
		attrs.add(attr);
		
		if(grEnt instanceof Node)
			attrNodes.add((Node) grEnt);
		else if(grEnt instanceof Edge)
			attrEdges.add((Edge) grEnt);
		else
			throw new UnsupportedOperationException("Unsupported entity (" + grEnt + ")");
	}
	
	/**
	 * Adds a map or set expression.
	 * @param expr The map or set expressions.
	 */
	public void add(Expression expr) {
		if(collectMapSetExprs) mapSetExprs.add(expr);
	}

	public void needsGraph() {
		isGraphUsed = true;
	}
}
