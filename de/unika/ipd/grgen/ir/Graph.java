/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.*;
import java.util.*;

/**
 * A graph pattern.
 * This is used for tests and the left and right sides and the NAC part of a rule.
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
			super(n.getIdent(), n.getNodeType(), EmptyAttributes.get());
			this.incoming = new HashSet();
			this.outgoing = new HashSet();
			this.node = n;
		}

    /**
     * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
     */
    public Iterator getWalkableChildren() {
    	return new MultiIterator(new Collection[] {
    		outgoing, incoming
    	});
    }
	}
	
	protected class GraphEdge extends Edge {
		protected GraphNode source;
		protected GraphNode target;
		protected Edge edge;
		
		public GraphEdge(Edge e) {
			super(e.getIdent(), e.getEdgeType(), EmptyAttributes.get());
			this.edge = e;
		}

	    /**
	     * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	     */
	    public Iterator getWalkableChildren() {
	    	assert source != null && target != null : "edge must be initalized";
	    	return new ArrayIterator(new Object[] {
					source, target
	    	});
	    }
	}

	/** Map that maps a node to an internal node. */
	private Map nodes = new HashMap();
	
	/** Map that maps an edge to an internal edge. */
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
	 * @param A collection to put all the nodes in.
	 * @return The given collection.
	 */
	public Collection getNodes(Collection col) {
		col.addAll(nodes.keySet());
		return col;
	}
	
	/**
	 * Get an iterator iterating over all nodes.
	 * @return An iterator iterating over all nodes.
	 */
	public Iterator getNodes() {
		return getNodes(new LinkedList()).iterator();
	}

	/**
	 * Get a set containing all edges in this graph.
	 * @param col A collection to put all the edges in.
	 * @return The given collection.
	 */
	public Collection getEdges(Collection col) {
		col.addAll(edges.keySet());
		return col;
	}
	
	/**
	 * Get an iterator iterating over all nodes.
	 * @return An iterator iterating over all nodes.
	 */
	public Iterator getEdges() {
		return getEdges(new LinkedList()).iterator();
	}
		
	private Set getEdgeSet(Iterator it) {
		Set res = new HashSet();
		while(it.hasNext())
			res.add(((GraphEdge) it.next()).edge);

		return res;
	}
	
	/**
	 * Get the number of ingoing edges.
	 * @param node The node.
	 * @return The number of ingoing edges;
	 */
	public int getInDegree(Node node) {
		GraphNode gn = checkNode(node);
		return gn.incoming.size();
	}
	
	/**
	 * Get the number of outgoing edges.
	 * @param node The node.
	 * @return The number of outgoing edges;
	 */
	public int getOutDegree(Node node) {
		GraphNode gn = checkNode(node);
		return gn.outgoing.size();
	}

	/**
	 * Get the set of all incoming edges for a node.
	 * @param n The node.
	 * @param c A set where the edges are put to.
	 */
	public Collection getIncoming(Node n, Collection c) {
		GraphNode gn = checkNode(n);
		for(Iterator it = gn.incoming.iterator(); it.hasNext();) {
			GraphEdge e = (GraphEdge) it.next();
			c.add(e.edge);
		}
		return c;
	}
	
	/**
	 * Get an iterator iterating over all incoming edges of a node.
	 * @param n The node
	 * @return The iterator.
	 */
	public Iterator getIncoming(Node n) {
		return getIncoming(n, new LinkedList()).iterator();
	}
	
	/**
	 * Get the set of outgoing edges for a node.
	 * @param n The node.
	 * @param c A set where the edges are put to.
	 */
	public Collection getOutgoing(Node n, Collection c) {
		GraphNode gn = checkNode(n);
		for(Iterator it = gn.outgoing.iterator(); it.hasNext();) {
			GraphEdge e = (GraphEdge) it.next();
			c.add(e.edge);
		}
		return c;
	}
	
	/**
	 * Get an iterator iterating over all outgoing edges of a node.
	 * @param n The node
	 * @return The iterator.
	 */
	public Iterator getOutgoing(Node n) {
		return getOutgoing(n, new LinkedList()).iterator();
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
	 * Get an "end" of an edge.
	 * @param e The edge.
	 * @param source The end.
	 * @return If <code>source</code> was true, this method returns the
	 * source node of the edge, if <code>source</code> was false, it
	 * returns the target node.
	 */
	public Node getEnd(Edge e, boolean source) {
		return source ? getSource(e) : getTarget(e);
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
		
		for(Iterator it = getEdges(); it.hasNext();) {
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
  
  /**
   * Check, if a node is a single node.
   * A node is <i>single</i>, if it has no incident edges.
   * @param node The node.
   * @return true, if the node is single, false if not.
   */
  public boolean isSingle(Node node) {
  	GraphNode gn = checkNode(node);
  	return ! (gn.incoming.iterator().hasNext() || gn.outgoing.iterator().hasNext());
  }

}
