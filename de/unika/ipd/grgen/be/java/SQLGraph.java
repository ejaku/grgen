/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.DefaultIntegerId;
import de.unika.ipd.libgr.IntegerId;
import de.unika.ipd.libgr.graph.Edge;
import de.unika.ipd.libgr.graph.EdgeType;
import de.unika.ipd.libgr.graph.Graph;
import de.unika.ipd.libgr.graph.InheritanceType;
import de.unika.ipd.libgr.graph.Node;
import de.unika.ipd.libgr.graph.NodeType;
import de.unika.ipd.libgr.graph.TypeModel;


/**
 * A sql graph.
 */
class SQLGraph implements Graph, TypeModel {

	private final String name;
	
	private IDTypeModel typeModel;
	
	private Queries queries;
	
	private ErrorReporter reporter;
	
	/**
	 * Return an ID from a result set.
	 * This method should be applied for result sets that return one row and one
	 * column containing an integer.
	 * @param res The result set.
	 * @return The ID.
	 */
	private int getResultId(ResultSet res) {
		int id = IntegerId.INVALID;
		
		try {
			for(int i = 0; res.next(); i++) {
				if(i == 0)
					id = res.getInt(0);
				else
					reporter.error("Query returned more than one row");
			}
		} catch(SQLException e) {
			reporter.error("Could not retrieve data");
		}
		
		return id;
	}
	
	/**
	 * Get all IDs in the result set.
	 * @param res The result set.
	 * @return An iterator iterating over {@link Integer} objects containing
	 * the IDs. The set may also be empty.
	 */
	private Iterator getResultIds(ResultSet res) {
		
		try {
			Collection ids = new LinkedList();

			while(res.next())
				ids.add(new Integer(res.getInt(0)));
				
			return ids.iterator();
			
		} catch(SQLException e) {
			reporter.error("Could not retrieve data");
			return null;
		}
	}

	
	class SQLNode extends DefaultIntegerId implements Node {
		int[] id = new int[1];
		
		SQLNode(int id) {
			super(id);
			this.id[0] = id;
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Node#getIncoming()
		 */
		public Iterator getIncoming() {
			List nodes = new LinkedList();
			Iterator ids = getResultIds(queries.exec(Queries.NODE_INCOMING, id)); 
			
			while(ids.hasNext()) {
				int id = ((Integer) ids.next()).intValue();
				nodes.add(getNode(id));
			}
			
			return nodes.iterator();
		}

		/**
		 * @see de.unika.ipd.libgr.graph.Node#getOutgoing()
		 */
		public Iterator getOutgoing() {
			List nodes = new LinkedList();
			Iterator ids = getResultIds(queries.exec(Queries.NODE_OUTGOING, id)); 
			
			while(ids.hasNext()) {
				int id = ((Integer) ids.next()).intValue();
				nodes.add(getNode(id));
			}
			
			return nodes.iterator();
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Node#getType()
		 */
		public NodeType getType() {
			int tid = getResultId(queries.exec(Queries.NODE_GET_TYPE, id));
			return getNodeType(tid);
		}
	}
	
	class SQLEdge extends DefaultIntegerId implements Edge {
		int[] id = new int[1];
		
		SQLEdge(int id) {
			super(id);
			this.id[0] = id;
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.Edge#getSource()
		 */
		public Node getSource() {
			return getNode(getResultId(queries.exec(Queries.EDGE_SOURCE, id)));
		}

		/**
		 * @see de.unika.ipd.libgr.graph.Edge#getTarget()
		 */
		public Node getTarget() {
			return getNode(getResultId(queries.exec(Queries.EDGE_TARGET, id)));
		}

		/**
		 * @see de.unika.ipd.libgr.graph.Edge#getType()
		 */
		public EdgeType getType() {
			return getEdgeType(getResultId(queries.exec(Queries.EDGE_GET_TYPE, id)));
		}
	}
	
	class SQLNodeType extends DefaultIntegerId implements NodeType {

		SQLNodeType(int id) {
			super(id);
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.AttributedType#getAttributeTypes()
		 */
		public Iterator getAttributeTypes() {
			// TODO NYI
			return null;
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.InheritanceType#getSuperTypes()
		 */
		public Iterator getSuperTypes() {
			int[] superTypes = typeModel.getNodeTypeSuperTypes(id);
			Collection result = new LinkedList();
			
			for(int i = 0; i < superTypes.length; i++)
				result.add(getNodeType(superTypes[i]));
				
			return result.iterator();
		}

		/**
		 * @see de.unika.ipd.libgr.graph.InheritanceType#isA(de.unika.ipd.libgr.graph.InheritanceType)
		 */
		public boolean isA(InheritanceType t) {
			boolean res = false;
			
			if(t instanceof SQLNodeType) 
				res = typeModel.nodeTypeIsA(id, ((SQLNodeType) t).id);
			
			return res;
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.InheritanceType#isRoot()
		 */
		public boolean isRoot() {
			return id == typeModel.getNodeRootType();
		}
		
		public String getName() {
			return typeModel.getNodeTypeName(id);
		}
	}
	
	class SQLEdgeType extends DefaultIntegerId implements EdgeType {

		SQLEdgeType(int id) {
			super(id);
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.AttributedType#getAttributeTypes()
		 */
		public Iterator getAttributeTypes() {
			// TODO NYI
			return null;
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.InheritanceType#getSuperTypes()
		 */
		public Iterator getSuperTypes() {
			int[] superTypes = typeModel.getEdgeTypeSuperTypes(id);
			Collection result = new LinkedList();
			
			for(int i = 0; i < superTypes.length; i++)
				result.add(getEdgeType(superTypes[i]));
				
			return result.iterator();
		}

		/**
		 * @see de.unika.ipd.libgr.graph.InheritanceType#isA(de.unika.ipd.libgr.graph.InheritanceType)
		 */
		public boolean isA(InheritanceType t) {
			boolean res = false;
			
			if(t instanceof SQLEdgeType) 
				res = typeModel.edgeTypeIsA(id, ((SQLEdgeType) t).id);
			
			return res;
		}
		
		/**
		 * @see de.unika.ipd.libgr.graph.InheritanceType#isRoot()
		 */
		public boolean isRoot() {
			return id == typeModel.getEdgeRootType();
		}
		
		public String getName() {
			return typeModel.getEdgeTypeName(id);
		}
	}
	
	Node getNode(int id) {
		return new SQLNode(id);
	}
	
	Edge getEdge(int id) {
		return new SQLEdge(id);
	}
	
	NodeType getNodeType(int id) {
		return new SQLNodeType(id);
	}
	
	EdgeType getEdgeType(int id) {
		return new SQLEdgeType(id);
	}

	private int getId(Object obj) {
		int id = IntegerId.INVALID;
		
		if(obj instanceof IntegerId) 
			id = ((IntegerId) obj).getId();
		
		return id;
	}
	
	SQLGraph(String name, IDTypeModel typeModel, Queries queries, ErrorReporter reporter) {
		this.name = name;
		this.typeModel = typeModel;
		this.queries = queries;
		this.reporter = reporter;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#add(de.unika.ipd.libgr.graph.EdgeType, de.unika.ipd.libgr.graph.Node, de.unika.ipd.libgr.graph.Node)
	 */
	public Edge add(EdgeType t, Node src, Node tgt) {
		
		int[] params = new int[] { getId(t), getId(src), getId(tgt) };
		int e = getResultId(queries.exec(Queries.ADD_EDGE, params));
		
		return getEdge(e);
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#add(de.unika.ipd.libgr.graph.NodeType)
	 */
	public Node add(NodeType t) {
		int[] params = new int[] { getId(t) };
		int n = getResultId(queries.exec(Queries.ADD_NODE, params));
		return getNode(n);
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#dump(de.unika.ipd.grgen.util.GraphDumper)
	 */
	public void dump(GraphDumper dumper) {
		// TODO Auto-generated method stub

	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#getTypeModel()
	 */
	public TypeModel getTypeModel() {
		return this;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#remove(de.unika.ipd.libgr.graph.Edge)
	 */
	public boolean remove(Edge edge) {
		int e = getId(edge);
		return queries.execUpdate(Queries.REMOVE_EDGE, new int[] { e }) != 0;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.Graph#remove(de.unika.ipd.libgr.graph.Node)
	 */
	public boolean remove(Node node) {
		int n = getId(node);
		return queries.execUpdate(Queries.REMOVE_NODE, new int[] { n }) != 0;
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
		return getEdgeType(typeModel.getEdgeRootType());
	}

	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getEdgeTypes()
	 */
	public Iterator getEdgeTypes() {
		Collection result = new LinkedList();
		int[] types = typeModel.getEdgeTypes();
		
		for(int i = 0; i < types.length; i++)
			result.add(getEdgeType(types[i]));
		
		return result.iterator();
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getNodeRootType()
	 */
	public NodeType getNodeRootType() {
		return getNodeType(typeModel.getNodeRootType());
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.TypeModel#getNodeTypes()
	 */
	public Iterator getNodeTypes() {
		Collection result = new LinkedList();
		int[] types = typeModel.getNodeTypes();
		
		for(int i = 0; i < types.length; i++)
			result.add(getNodeType(types[i]));
		
		return result.iterator();
		
	}
}
