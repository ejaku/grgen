/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Graph;
import java.util.Collection;
import java.util.Set;
import java.util.Vector;

/**
 * AST node that represents a Connection (an edge connecting two nodes)
 * children: LEFT:NodeDeclNode, EDGE:EdgeDeclNode, RIGHT:NodeDeclNode
 */
public class ConnectionNode extends BaseNode implements ConnectionCharacter {
	static {
		setName(ConnectionNode.class, "connection");
	}
	
	/** possible connection kinds */
	public static final int ARBITRARY = 0;
	public static final int ARBITRARY_DIRECTED = 1;
	public static final int DIRECTED = 2;
	public static final int UNDIRECTED = 3;
	
	private int connectionKind;

	NodeDeclNode left;
	EdgeDeclNode edge;
	NodeDeclNode right;

	BaseNode leftUnresolved;
	BaseNode edgeUnresolved;
	BaseNode rightUnresolved;

	/** Construct a new connection node.
	 *  A connection node has two node nodes and one edge node
	 *  @param n1 First node
	 *  @param edge Edge that connects n1 with n2
	 *  @param n2 Second node.
	 *  @param n2 Second node.
	 *  @param d Direction of the connection.
	 */
	public ConnectionNode(BaseNode n1, BaseNode e, BaseNode n2, int d) {
		super(e.getCoords());
		leftUnresolved = n1;
		becomeParent(leftUnresolved);
		edgeUnresolved = e;
		becomeParent(edgeUnresolved);
		rightUnresolved = n2;
		becomeParent(rightUnresolved);
		connectionKind = d;
	}

	/** Construct a new already resolved and checked connection node.
	 *  A connection node has two node nodes and one edge node
	 *  @param n1 First node
	 *  @param edge Edge that connects n1 with n2
	 *  @param n2 Second node.
	 *  @param d Direction of the connection.
	 */
	public ConnectionNode(NodeDeclNode n1, EdgeDeclNode e, NodeDeclNode n2, int d, BaseNode parent) {
		this(n1, e, n2, d);
		parent.becomeParent(this);

		resolve();
		check();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(leftUnresolved, left));
		children.add(getValidVersion(edgeUnresolved, edge));
		children.add(getValidVersion(rightUnresolved, right));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("src");
		childrenNames.add("edge");
		childrenNames.add("tgt");
		return childrenNames;
	}

	private static DeclarationResolver<NodeDeclNode> nodeResolver = new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
	private static DeclarationResolver<EdgeDeclNode> edgeResolver = new DeclarationResolver<EdgeDeclNode>(EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		left = nodeResolver.resolve(leftUnresolved, this);
		edge = edgeResolver.resolve(edgeUnresolved, this);
		right = nodeResolver.resolve(rightUnresolved, this);

		return left != null && edge != null && right != null;
	}
	
	private static Checker nodeTypeChecker = new TypeChecker(NodeTypeNode.class);
	private static Checker edgeTypeChecker = new TypeChecker(EdgeTypeNode.class);

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		boolean sucess = nodeTypeChecker.check(left, error)
			& edgeTypeChecker.check(edge, error)
			& nodeTypeChecker.check(right, error)
			& checkEdgeRootType()
			& areDanglingEdgesInReplacementDeclaredInPattern();

		if(!sucess) {
			return false;
		}
		warnArbitraryRootType();
		
		return true;
		
	}

	private void warnArbitraryRootType()
    {
		if (connectionKind != ARBITRARY) {
			return;
		}
	    
		DeclaredTypeNode rootType = getArbitraryEdgeRootType().getDeclType();
		
		boolean res = edge.getDeclType().isEqual(rootType);
		
		if (!res) {
			edge.reportWarning("The type of " + edge.getIdentNode().toString() + " differs from "
					+ getArbitraryEdgeRootType().getIdentNode().toString()
					+ ", please use another edge kind instead of ?--? (e.g. -->)");
		}

		return;
    }

	private boolean checkEdgeRootType() {
		TypeDeclNode rootDecl = null;
		switch (connectionKind) {
        case ARBITRARY:
	        rootDecl = getArbitraryEdgeRootType();
	        break;
	        
        case ARBITRARY_DIRECTED:
        	rootDecl = getDirectedEdgeRootType();
        	break;
	        
        case DIRECTED:
        	rootDecl = getDirectedEdgeRootType();
        	break;
	        
        case UNDIRECTED:
        	rootDecl = getUndirectedEdgeRootType();
        	break;

        default:
	        assert false;
        	break;
        }
		
		DeclaredTypeNode rootType = rootDecl.getDeclType();

		if (!edge.getDeclType().isCompatibleTo(rootType)) {
			reportError("Edge kind is incompatible with edge type");
			return false;
		}
		
		return true;
    }

	protected boolean areDanglingEdgesInReplacementDeclaredInPattern() {
		if(!(left instanceof DummyNodeDeclNode)
		   && !(right instanceof DummyNodeDeclNode)) {
			return true; // edge not dangling
		}

		// edge dangling
		if(left instanceof DummyNodeDeclNode) {
			if((left.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				return true; // we're within the pattern, not the replacement
			}
		}
		if(right instanceof DummyNodeDeclNode) {
			if((right.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				return true; // we're within the pattern, not the replacement
			}
		}

		// edge dangling and located within the replacement
		if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
			return true; // edge was declared in the pattern
		}
		if(edge instanceof EdgeTypeChangeNode) {
			return true; // edge is a type change edge of an edge declared within the pattern
		}

		edge.reportError("dangling edges in replace/modify part must have been declared in pattern part");
		return false;
	}

	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternGraphNode#constructIR()}.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr) {
		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode());
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdges(java.util.Set)
	 */
	public void addEdge(Set<BaseNode> set) {
		set.add(edge);
	}
	
	public int getConnectionKind() {
		return connectionKind;
	}

	public EdgeDeclNode getEdge() {
		return edge;
	}

	public NodeDeclNode getSrc() {
		return left;
	}

	public void setSrc(NodeDeclNode n) {
		assert(n!=null);
		switchParenthood(left, n);
		left = n;
	}

	public NodeDeclNode getTgt() {
		return right;
	}

	public void setTgt(NodeDeclNode n) {
		assert(n != null);
		switchParenthood(right, n);
		right = n;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set)
	 */
	public void addNodes(Set<BaseNode> set) {
		set.add(left);
		set.add(right);
	}
}

