/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.util.ArrayIterator;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumpableProxy;
import de.unika.ipd.grgen.util.MultiIterator;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A graph pattern. 
 * This is used for tests and the left and right sides of a rule.
 * These graphs have own classes for the nodes and edges as proxy objects
 * to the actual Node and Edge objects. The reason for this is:
 * The nodes and edges in a rule   that are common to the left and the right 
 * side exist only once as a object (that's due to the fact, that these 
 * objects are created from the AST declaration, which exist only once per
 * defined object). But we want to dicriminate between the nodes on the
 * left and right hand side od a rule, even, if they represent the same
 * declared nodes. 
 */
public class Graph extends IR {

	protected abstract class GraphObject extends GraphDumpableProxy implements Walkable {
		public GraphObject(GraphDumpable gd) {
			super(gd);
		}
	}

	protected class GraphNode extends Node {
		protected Set outgoing;
		protected Set incoming;
		protected Node node;
		
		public GraphNode(Node n) {
			super(n.getIdent(), n.getNodeType());
			this.incoming = new HashSet();
			this.outgoing = new HashSet();
			this.node = n;
		}

    /**
     * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
     */
    public Iterator getWalkableChildren() {
    	return new MultiIterator(new Iterator[] {
    		outgoing.iterator(), incoming.iterator(), node.getWalkableChildren()
    	});
    }
	}
	
	protected class GraphEdge extends Edge {
		protected GraphNode source;
		protected GraphNode target;
		protected Edge edge;
		
		public GraphEdge(Edge e) {
			super(e.getIdent(), e.getEdgeType(), e.isNegated());
			this.edge = e;
		}

    /**
     * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
     */
    public Iterator getWalkableChildren() {
    	assert source != null && target != null : "edge must be initalized";
			return new MultiIterator(new Iterator[] {
				new ArrayIterator(new Object[] { source, target }),
				edge.getWalkableChildren()
			});
    }

	}

	/** Map that maps a node to an internal node. */
	private Map nodes = new HashMap();
	
	/** Map that maps a non-negated edge to an internal edge. */
	private Map edges = new HashMap();
	
	private GraphNode getOrSetNode(Node n) {
		GraphNode res;
		if(!nodes.containsKey(n)) {
			res = new GraphNode(n);
			nodes.put(n, res);
		} else
			res = (GraphNode) nodes.get(n);
		
		return res;
	}
	
	private GraphEdge getOrSetEdge(Edge e) {
		GraphEdge res;
		Map map = edges;
		
		if(!map.containsKey(e)) {
			res = new GraphEdge(e);
			map.put(e, res);
		} else
			res = (GraphEdge) map.get(e);
		
		return res;
	}

	private GraphNode checkNode(Node n) {
		assert nodes.containsKey(n) : "Node must be in graph: " + n;
		return (GraphNode) nodes.get(n);
	}

	private GraphEdge checkEdge(Edge e) {
		assert edges.containsKey(e) : "Edge must be in graph: " + e;
		return (GraphEdge) edges.get(e);
	}

	/**
	 * Make a new graph.
	 */
	public Graph() {
		super("graph");
	}
	
	/**
	 * Allows another class to append a suffix to the graph's name.
	 * This is useful for rules, that can add "left" or "right" to the
	 * graph's name.
	 * @param s A suffix for the graph's name. 
	 */
	public void setNameSuffix(String s) {
		setName("graph " + s);
	}

	/**
	 * Check if a node is contained in the graph.
	 * @param node The node
	 * @return true, if the node is contained the graph, false, if not.
	 */
	public boolean hasNode(Node node) {
		return nodes.containsKey(node);
	}
	
	/**
	 * Check if an edge is contained in the graph.
	 * @param edge The edge
	 * @return true, if the edge is contained the graph, false, if not.
	 */
	public boolean hasEdge(Edge edge) {
		return edges.containsKey(edge);
	}

	/**
	 * Get a set containing all nodes in this graph.
	 * @return A set containing all nodes in this graph.
	 */
	public Set getNodes() {
		Set res = new HashSet();
		res.addAll(nodes.keySet());
		return res;
	}
	
	/**
	 * Get a set containing all edges in this graph.
	 * @return A set containing all edges in this graph.
	 */
	public Set getEdges() {
		return new HashSet(edges.keySet());
	}
	
	/**
	 * Get a set containing all negated edges.
	 * @return A set with all negated edges.
	 */
	public Set getNegatedEdges() {
		Set res = new HashSet();
		for(Iterator it = edges.keySet().iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			if(e.isNegated())
				res.add(e);
		}
		
		return res;
	}
	
	private Set getEdgeSet(Iterator it) {
		Set res = new HashSet(); 
		while(it.hasNext()) 
			res.add(((GraphEdge) it.next()).edge);

		return res;
	}
	
	/**
	 * Get the set of all incoming edges for a node.
	 * @param n The node.
	 * @param c A set where the edges are put to.
	 */
	public void getIncoming(Node n, Collection c) {
		GraphNode gn = checkNode(n);
		c.addAll(gn.incoming);
	}
	
	/**
	 * Get the set of outgoing edges for a node.
	 * @param n The node.
	 * @param c A set where the edges are put to.
	 */
	public void getOutgoing(Node n, Collection c) {
		GraphNode gn = checkNode(n);
		c.addAll(gn.outgoing);
	}
	
	/**
	 * Get the source node of an edge.
	 * @param e The edge.
	 * @return The node, the edge leaves.
	 */
	public Node getSource(Edge e) {
		GraphEdge ge = checkEdge(e);
		return ge.source.node;
	}
	
	/**
	 * Get the target node of an edge.
	 * @param e The edge
	 * @return The node that the edge points to.
	 */
	public Node getTarget(Edge e) {
		GraphEdge ge = checkEdge(e);
		return ge.target.node;
	}
	
	/**
	 * Replace an edge in this graph by a similar one.
	 * Replace an edge, that has the same type, target and source node like
	 * <code>edge</code> by <code>edge</code>.
	 * This is called by the coalesce phase in rules. 
	 * @see Rule#coalesceAnonymousEdges()
	 * @param gr The graph <code>edge</code>
	 * @param e The edge which shall replace a similar one.
	 */
	protected void replaceSimilarEdges(Graph gr, Edge edge) {
		Node src = gr.getSource(edge);
		Node tgt = gr.getTarget(edge);
		EdgeType edgeType = edge.getEdgeType();
		
		debug.entering();
		
		for(Iterator it = getEdges().iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			
			if(src == getSource(e) && tgt == getTarget(e) 
				&& edgeType.isEqual(e.getEdgeType())) {

				debug.report(NOTE, "Exchanging " + e.getIdent() 
					+ " with " + edge.getIdent());
					
				// Modify the graph edge to refer to the coalesced edge.
				GraphEdge ge = checkEdge(e);
				ge.edge = edge;
				
				// Remove the deleted edge from the edges map and enter the new one.
				edges.remove(e);
				edges.put(edge, ge);
			}
		}
		
		debug.leaving();
	}
	
	/**
	 * Add a connection to the graph.
	 * @param left The left node.
	 * @param edge The edge connecting the left and the right node.
	 * @param right The right node.
	 */
	public void addConnection(Node left, Edge edge, Node right) {
		// Get the nodes and edges from the map.	
		GraphNode l = getOrSetNode(left);
		GraphNode r = getOrSetNode(right);
		GraphEdge e = getOrSetEdge(edge);
		
		// Update outgoing and incoming of the nodes.
		l.outgoing.add(e);
		r.incoming.add(e);
		
		// Set the edge source and target
		e.source = l;
		e.target = r;
	}
	
	/**
	 * Add a single node (without an edge) to the graph.
	 * @param node The node.
	 */
	public void addSingleNode(Node node) {
		getOrSetNode(node);
	}

  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
    return nodes.values().iterator();
  }

}
