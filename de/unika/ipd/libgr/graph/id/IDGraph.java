/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph.id;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.libgr.DefaultIntegerId;
import de.unika.ipd.libgr.IntegerId;
import de.unika.ipd.libgr.graph.Edge;
import de.unika.ipd.libgr.graph.EdgeType;
import de.unika.ipd.libgr.graph.Graph;
import de.unika.ipd.libgr.graph.Node;
import de.unika.ipd.libgr.graph.NodeType;


/**
 * A graph that uses an ID graph model. 
 */
public class IDGraph implements Graph {

	/** The graph model. */
	private IDGraphModel graphModel;
	
	/** The name of the graph. */
	private String name;
	
	/**
	 * The implementation of the node class. 
	 */
	private class IDNode extends DefaultIntegerId implements Node {
		
		IDNode(int id) {
			super(id);
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Node#getIncoming()
		 */
		public Iterator getIncoming() {
			int[] incoming = graphModel.getIncoming(getId());
			Collection res = new LinkedList();
			
			for(int i = 0; i < incoming.length; i++) 
				res.add(new IDNode(incoming[i]));

			return res.iterator();
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Node#getOutgoing()
		 */
		public Iterator getOutgoing() {
			int[] outgoing = graphModel.getIncoming(getId());
			Collection res = new LinkedList();
			
			for(int i = 0; i < outgoing.length; i++) 
				res.add(new IDNode(outgoing[i]));

			return res.iterator();
		}

		/**
		 * @see de.unika.ipd.libgr.graph.Node#getType()
		 */
		public NodeType getType() {
			// TODO Auto-generated method stub
			return null;
		}
	}
	
	/**
	 * The implementation of the edge interface. 
	 */
	private class IDEdge extends DefaultIntegerId implements Edge {
		
		IDEdge(int id) {
			super(id);
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Edge#getSource()
		 */
		public Node getSource() {
			return new IDNode(graphModel.getSource(getId()));
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Edge#getTarget()
		 */
		public Node getTarget() {
			return new IDNode(graphModel.getTarget(getId()));
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Edge#getType()
		 */
		public EdgeType getType() {
			return null;
		}
	}
	
	private int getId(Object obj) {
		int res = IntegerId.INVALID;
		
		if(obj instanceof IntegerId)
			res = ((IntegerId) obj).getId();
		else if(obj instanceof Integer)
			res = ((Integer) obj).intValue();
		
		return res;
	}
	
	public IDGraph(String name, IDGraphModel graphModel) {
		this.name = name;
		this.graphModel = graphModel;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#add(de.unika.ipd.libgr.graph.EdgeType, de.unika.ipd.libgr.graph.Node, de.unika.ipd.libgr.graph.Node)
	 */
	public Edge add(EdgeType t, Node src, Node tgt) {
		int edgeType = getId(t);
		int srcNode = getId(src);
		int tgtNode = getId(tgt);
		
		return new IDEdge(graphModel.add(edgeType, srcNode, tgtNode));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#add(de.unika.ipd.libgr.graph.NodeType)
	 */
	public Node add(NodeType t) {
		int nodeType = getId(t);
		return new IDNode(graphModel.add(nodeType));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#dump(de.unika.ipd.grgen.util.GraphDumper)
	 */
	public void dump(GraphDumper dumper) {
		// TODO Auto-generated method stub
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#remove(de.unika.ipd.libgr.graph.Edge)
	 */
	public boolean remove(Edge edge) {
		int eid = getId(edge);
		return graphModel.removeEdge(eid);
	}

	/**
	 * @see de.unika.ipd.libgr.graph.Graph#remove(de.unika.ipd.libgr.graph.Node)
	 */
	public boolean remove(Node node) {
		int nid = getId(node);
		return graphModel.removeEdge(nid);
	}

	/**
	 * @see de.unika.ipd.libgr.Named#getName()
	 */
	public String getName() {
		return name;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getEdgeRootType()
	 */
	public EdgeType getEdgeRootType() {
		// TODO Auto-generated method stub
		return null;
	}
	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getEdgeTypes()
	 */
	public Iterator getEdgeTypes() {
		// TODO Auto-generated method stub
		return null;
	}
	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getNodeRootType()
	 */
	public NodeType getNodeRootType() {
		// TODO Auto-generated method stub
		return null;
	}
	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getNodeTypes()
	 */
	public Iterator getNodeTypes() {
		// TODO Auto-generated method stub
		return null;
	}
}
