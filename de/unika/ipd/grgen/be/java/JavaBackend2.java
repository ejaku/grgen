/**
 * Created on Mar 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;

import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.sql.IDBase;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.Visitor;
import de.unika.ipd.grgen.util.Walkable;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.DefaultIntegerId;
import de.unika.ipd.libgr.graph.EdgeType;
import de.unika.ipd.libgr.graph.InheritanceType;
import de.unika.ipd.libgr.graph.NodeType;
import de.unika.ipd.libgr.graph.TypeModel;


/**
 * A Java backend.
 */
public class JavaBackend2 extends IDBase implements Backend, TypeModel {

	/** The IR unit. */
	protected Unit unit;
	
	/** The error reporter. */
	protected ErrorReporter reporter;

	/** Map all IR types to libgr types. */
	protected final Map irMap = new HashMap();
	
	/** A list of all node types. */
	protected final Collection nodeTypes = new LinkedList();

	/** A list of all edge types. */
	protected final Collection edgeTypes = new LinkedList();
	
	/** The node root type. */
	protected NodeType nodeRootType;
	
	/** The edge root type. */
	protected EdgeType edgeRootType;
	
	protected boolean[][] nodeTypeIsA;
	
	protected boolean[][] edgeTypeIsA;
	
	/**
	 * Get the IR unit.
	 * @return The IR unit.
	 */
	protected Unit getUnit() {
		return unit;
	}

	private abstract class JavaBackendType extends DefaultIntegerId {
		
		de.unika.ipd.grgen.ir.InheritanceType type;
		Collection inherits = new LinkedList();
		
		protected JavaBackendType(int id, de.unika.ipd.grgen.ir.InheritanceType type) {
			super(id);
			this.type = type;
		}
		
		public Iterator getAttributeTypes() {
			return null;
		}

		public Iterator getSuperTypes() {
			return inherits.iterator();
		}

		public boolean isRoot() {
			return type.isRoot();
		}
		
		public String getName() {
			return type.getIdent().toString(); 
		}
	}
	
	protected class JavaBackendNodeType extends JavaBackendType implements NodeType {
		JavaBackendNodeType(int id, de.unika.ipd.grgen.ir.InheritanceType type) {
			super(id, type);
		}
		
		public boolean isA(InheritanceType t) {
			if(t instanceof JavaBackendNodeType) {
				JavaBackendNodeType ty = (JavaBackendNodeType) t;
				
				return nodeTypeIsA[getId()][ty.getId()];
			}
			
			return false;
		}
	}
	
	protected class JavaBackendEdgeType extends JavaBackendType implements EdgeType {
		JavaBackendEdgeType(int id, de.unika.ipd.grgen.ir.InheritanceType type) {
			super(id, type);
		}
		
		public boolean isA(InheritanceType t) {
			if(t instanceof JavaBackendNodeType) {
				JavaBackendEdgeType ty = (JavaBackendEdgeType) t;
				
				return edgeTypeIsA[getId()][ty.getId()];
			}
			
			return false;
		}
	}
	
	protected void buildTypes() {
		/*
		 * A visitor that creates libgr node/edge types for all IR node/edge types 
		 * in the both lists edgeTypes and nodeTypes and maps each IR node/edge type 
		 * to the corresponding libgr node/edge type.  
		 */
		Visitor v = new Visitor() {
			
			int nodeTypeId = 0;
			int edgeTypeId = 0;
			
			public void visit(Walkable w) {
				
				if(w instanceof de.unika.ipd.grgen.ir.NodeType) {
					
					JavaBackendType ty = new JavaBackendNodeType(nodeTypeId++, 
							(de.unika.ipd.grgen.ir.NodeType) w); 
					
					nodeTypes.add(ty);
					
					irMap.put(w, ty);
					
				} else if(w instanceof de.unika.ipd.grgen.ir.EdgeType) {
					
					JavaBackendType ty = new JavaBackendEdgeType(edgeTypeId++, 
							(de.unika.ipd.grgen.ir.EdgeType) w);	 
					
					edgeTypes.add(ty);
					
					irMap.put(w, ty);
				}
			}
			
		};
		
		(new PostWalker(v)).walk(unit);
		
		/*
		 * after that, we establish the supertype sets for all libgr node/edge types.
		 */
		for(Iterator it = irMap.keySet().iterator(); it.hasNext();) {
			de.unika.ipd.grgen.ir.InheritanceType irt = 
				(de.unika.ipd.grgen.ir.InheritanceType) it.next();
			
			JavaBackendType jbt = (JavaBackendType) irMap.get(irt);
			
			for(Iterator types = irt.getInherits(); types.hasNext();) 
				jbt.inherits.add(types.next());
		}

		/*
		 * Find the node root type.
		 */
		for(Iterator it = nodeTypes.iterator(); it.hasNext();) {
			JavaBackendNodeType jbt = (JavaBackendNodeType) it.next();
			if(jbt.isRoot())
				nodeRootType = jbt;
		}
		
		/*
		 * Find the edge root type.
		 */
		for(Iterator it = edgeTypes.iterator(); it.hasNext();) {
			JavaBackendEdgeType jbt = (JavaBackendEdgeType) it.next();
			if(jbt.isRoot())
				edgeRootType = jbt;
		}

		/*
		 * now we build the inheritance relation for both, node and edge types.
		 */
		nodeTypeIsA = computeIsA(nodeTypes);
		edgeTypeIsA = computeIsA(edgeTypes);
	}
	
	private boolean[][] computeIsA(Collection a) {
		int maxId = -1;
		
		for(Iterator it = a.iterator(); it.hasNext();) {
			JavaBackendType ty = (JavaBackendType) it.next();
			int id = ty.getId();
			
			maxId = id > maxId ? id : maxId;
		}
		
		boolean[][] res = new boolean[maxId][maxId];
		boolean[] helper = new boolean[maxId];
		
		for(Iterator it = a.iterator(); it.hasNext();) {
			JavaBackendType ty = (JavaBackendType) it.next();
			computeIsAHelper(ty, res, helper);
		}
		
		return res;
	}
	
	private void computeIsAHelper(JavaBackendType ty, boolean[][] res, 
			boolean[] alreadyDone) {
		
		int id = ty.getId();
		
		if(!alreadyDone[id]) {
			
			for(Iterator it = ty.type.getInherits(); it.hasNext(); ) {
				JavaBackendType inht = (JavaBackendType) irMap.get(it.next());
				computeIsAHelper(inht, res, alreadyDone);
				res[id][inht.getId()] = true;
			}
			
			alreadyDone[id] = true;
		}
		
	}
	
	public TypeModel getTypeModel() {
		return this;
	}
	
	public void done() {
	}

	public void generate() {
	}

	/**
	 * Initialize the backend.
	 * @param unit The IR unit.
	 * @param reporter An error reporter.
	 * @param outputPath An output path (ignored here).
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		this.unit = unit;
		this.reporter = reporter;
		
		buildTypes();
	}
	
	/**
	 * Get the root type of the edges.
	 * @return The edge root type.
	 */
	public EdgeType getEdgeRootType() {
		return edgeRootType;
	}
	
	/**
	 * Get an iterator for all edge types.
	 * @return An iterator iterating over all edge types.
	 */
	public Iterator getEdgeTypes() {
		return edgeTypes.iterator();
	}
	
	/**
	 * Get the root type of the nodes.
	 * @return The node root type.
	 */
	public NodeType getNodeRootType() {
		return nodeRootType;
	}
	
	/**
	 * Get an iterator for all node types.
	 * @return An iterator iterating over all node types.
	 */
	public Iterator getNodeTypes() {
		return nodeTypes.iterator();
	}
}
