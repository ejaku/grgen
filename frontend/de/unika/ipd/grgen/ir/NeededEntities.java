package de.unika.ipd.grgen.ir;

import java.util.LinkedHashSet;
import java.util.Set;

/**
 * Holds a collection of entities needed by an expression.  
 */
public class NeededEntities {
	/**
	 * Instantiates a new NeededEntities object.
	 * @param collectNodes Specifies, whether needed nodes shall be collected.
	 * @param collectEdges Specifies, whether needed edges shall be collected.
	 * @param collectVars Specifies, whether needed variables shall be collected.
	 */
	public NeededEntities(boolean collectNodes, boolean collectEdges, boolean collectVars) {
		if(collectNodes) nodes     = new LinkedHashSet<Node>();
		if(collectEdges) edges     = new LinkedHashSet<Edge>();
		if(collectVars)  variables = new LinkedHashSet<Variable>();
	}
	
	/**
	 * Specifies whether the graph is needed. 
	 */
	public boolean isGraphUsed;

	/**
	 * The nodes needed.
	 * May be null, if not needed.
	 */
	public Set<Node> nodes;

	/**
	 * The edges needed.
	 */
	public Set<Edge> edges;

	/**
	 * The variables needed.
	 */
	public Set<Variable> variables;
	
	/**
	 * Adds a needed node.
	 * @param node The needed node.
	 */
	public void add(Node node) {
		if(nodes != null) nodes.add(node);
	}
	
	/**
	 * Adds a needed edge.
	 * @param edge The needed edge.
	 */
	public void add(Edge edge) {
		if(edges != null) edges.add(edge);
	}
	
	/**
	 * Adds a needed variable.
	 * @param var The needed variable.
	 */
	public void add(Variable var) {
		if(variables != null) variables.add(var);
	}
	
	public void needsGraph() {
		isGraphUsed = true;
	}
}