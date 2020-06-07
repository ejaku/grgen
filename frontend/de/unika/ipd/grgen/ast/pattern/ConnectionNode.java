/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.DummyNodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeTypeChangeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.pattern.Graph;

/**
 * AST node that represents a Connection (an edge connecting two nodes)
 * children: LEFT:NodeDeclNode, EDGE:EdgeDeclNode, RIGHT:NodeDeclNode
 */
public class ConnectionNode extends ConnectionCharacter
{
	static {
		setName(ConnectionNode.class, "connection");
	}

	/** possible connection kinds */
	public enum ConnectionKind
	{
		ARBITRARY,
		ARBITRARY_DIRECTED,
		DIRECTED,
		UNDIRECTED
	}

	private ConnectionKind connectionKind;

	/** possible redirection kinds */
	public static final int NO_REDIRECTION = 0;
	public static final int REDIRECT_SOURCE = 1;
	public static final int REDIRECT_TARGET = 2;
	public static final int REDIRECT_SOURCE_AND_TARGET = REDIRECT_SOURCE | REDIRECT_TARGET;

	private int redirectionKind;

	private NodeDeclNode left;
	private EdgeDeclNode edge;
	private NodeDeclNode right;

	private BaseNode leftUnresolved;
	public BaseNode edgeUnresolved;
	private BaseNode rightUnresolved;

	/** Construct a new connection node.
	 *  A connection node has two node nodes and one edge node
	 *  @param left First node
	 *  @param edge Edge that connects n1 with n2
	 *  @param right Second node.
	 *  @param direction Direction of the connection.
	 *  @param redirection Potential redirection of the edge in the connection.
	 */
	public ConnectionNode(BaseNode left, BaseNode edge, BaseNode right, ConnectionKind direction, int redirection)
	{
		super(edge.getCoords());
		leftUnresolved = left;
		becomeParent(leftUnresolved);
		edgeUnresolved = edge;
		becomeParent(edgeUnresolved);
		rightUnresolved = right;
		becomeParent(rightUnresolved);
		connectionKind = direction;
		redirectionKind = redirection;
	}

	/** Construct a new already resolved and checked connection node.
	 *  A connection node has two node nodes and one edge node
	 *  @param left First node
	 *  @param edge Edge that connects n1 with n2
	 *  @param right Second node.
	 *  @param direction Direction of the connection.
	 */
	public ConnectionNode(NodeDeclNode left, EdgeDeclNode edge, NodeDeclNode right, ConnectionKind direction, BaseNode parent)
	{
		this(left, edge, right, direction, NO_REDIRECTION);
		parent.becomeParent(this);

		resolve();
		check();
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(leftUnresolved, left));
		children.add(getValidVersion(edgeUnresolved, edge));
		children.add(getValidVersion(rightUnresolved, right));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("src");
		childrenNames.add("edge");
		childrenNames.add("tgt");
		return childrenNames;
	}

	private static DeclarationResolver<NodeDeclNode> nodeResolver =
			new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
	private static DeclarationResolver<EdgeDeclNode> edgeResolver =
			new DeclarationResolver<EdgeDeclNode>(EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean res = fixupDefinition(leftUnresolved, leftUnresolved.getScope());
		res &= fixupDefinition(edgeUnresolved, edgeUnresolved.getScope());
		res &= fixupDefinition(rightUnresolved, rightUnresolved.getScope());
		if(!res)
			return false;

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
	@Override
	protected boolean checkLocal()
	{
		boolean sucess = nodeTypeChecker.check(left, error)
				& edgeTypeChecker.check(edge, error)
				& nodeTypeChecker.check(right, error)
				& checkEdgeRootType()
				& areDanglingEdgesInReplacementDeclaredInPattern()
				& noDefNonDefMixedConnection()
				& checkDeclaredOnLHS();

		if(!sucess) {
			return false;
		}
		warnArbitraryRootType();

		return true;
	}

	private void warnArbitraryRootType()
	{
		if(connectionKind != ConnectionKind.ARBITRARY) {
			return;
		}

		if(!(edge.getDeclType() instanceof ArbitraryEdgeTypeNode)) {
			edge.reportWarning("The type of " + edge.getIdentNode().toString() + " differs from "
					+ getArbitraryEdgeRootTypeDecl().getIdentNode().toString()
					+ ", please use another edge kind instead of ?--? (e.g. -->)");
		}

		return;
	}

	private boolean checkEdgeRootType()
	{
		TypeDeclNode rootDecl = null;
		switch(connectionKind) {
		case ARBITRARY:
			rootDecl = getArbitraryEdgeRootTypeDecl();
			break;

		case ARBITRARY_DIRECTED:
			rootDecl = getDirectedEdgeRootTypeDecl();
			break;

		case DIRECTED:
			rootDecl = getDirectedEdgeRootTypeDecl();
			break;

		case UNDIRECTED:
			rootDecl = getUndirectedEdgeRootTypeDecl();
			break;

		default:
			assert false;
			break;
		}

		DeclaredTypeNode rootType = rootDecl != null ? rootDecl.getDeclType() : null;

		if(!edge.getDeclType().isCompatibleTo(rootType)) {
			reportError("Edge kind is incompatible with edge type");
			return false;
		}

		return true;
	}

	private boolean areDanglingEdgesInReplacementDeclaredInPattern()
	{
		if(!(left instanceof DummyNodeDeclNode) && !(right instanceof DummyNodeDeclNode)) {
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
		if(edge instanceof EdgeTypeChangeDeclNode) {
			return true; // edge is a type change edge of an edge declared within the pattern
		}
		if(edge.defEntityToBeYieldedTo) {
			return true; // edge is a def to be yielded to, i.e. output variable
		}

		edge.reportError("dangling edges in replace/modify part must have been declared in pattern part");
		return false;
	}

	private boolean noDefNonDefMixedConnection()
	{
		if(left.defEntityToBeYieldedTo) {
			if((left.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
					&& (edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				left.reportError("A lhs def node can't connect to a lhs non-def edge (" + left.toString() + ")");
				return false;
			}
		}
		if(right.defEntityToBeYieldedTo) {
			if((right.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
					&& (edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				right.reportError("A lhs def node can't connect to a lhs non-def edge (" + right.toString() + ")");
				return false;
			}
		}
		if(edge.defEntityToBeYieldedTo) {
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				if(!(left instanceof DummyNodeDeclNode && right instanceof DummyNodeDeclNode)) {
					edge.reportError("A lhs def edge can't connect to nodes (" + edge.toString() + ")");
					return false;
				}
			}
		}

		return true;
	}

	private boolean checkDeclaredOnLHS()
	{
		if(redirectionKind != NO_REDIRECTION) {
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
				edge.reportError("An edge to be redirected must have been declared on the left hand side (thus matched).");
				return false;
			}
			if(connectionKind != ConnectionKind.DIRECTED) {
				edge.reportError("Only directed edges may be redirected (to other nodes).");
				return false;
			}
		}
		if(connectionKind == ConnectionKind.ARBITRARY) {
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
				reportError("New instances of ?--? are not allowed in RHS");
				return false;
			}
		}
		if(connectionKind == ConnectionKind.ARBITRARY_DIRECTED) {
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
				reportError("New instances of <--> are not allowed in RHS");
				return false;
			}
		}
		return true;
	}

	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternGraphNode#constructIR()}.
	 * @param gr The IR graph.
	 */
	@Override
	public void addToGraph(Graph gr)
	{
		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode(), connectionKind == ConnectionKind.DIRECTED,
				(redirectionKind & REDIRECT_SOURCE) == REDIRECT_SOURCE,
				(redirectionKind & REDIRECT_TARGET) == REDIRECT_TARGET);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.pattern.ConnectionCharacter#addEdges(java.util.Set)
	 */
	@Override
	public void addEdge(Set<EdgeDeclNode> set)
	{
		assert isResolved();

		set.add(edge);
	}

	public ConnectionKind getConnectionKind()
	{
		return connectionKind;
	}

	public int getRedirectionKind()
	{
		return redirectionKind;
	}
	
	@Override
	public EdgeDeclNode getEdge()
	{
		return edge;
	}

	@Override
	public NodeDeclNode getSrc()
	{
		return left;
	}

	@Override
	public void setSrc(NodeDeclNode n)
	{
		assert(n != null);
		switchParenthood(left, n);
		left = n;
	}

	@Override
	public NodeDeclNode getTgt()
	{
		return right;
	}

	@Override
	public void setTgt(NodeDeclNode n)
	{
		assert(n != null);
		switchParenthood(right, n);
		right = n;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.pattern.ConnectionCharacter#addNodes(java.util.Set)
	 */
	@Override
	public void addNodes(Set<NodeDeclNode> set)
	{
		assert isResolved();

		set.add(left);
		set.add(right);
	}

	public static String getKindStr()
	{
		return "connection node";
	}

	public static String getUseStr()
	{
		return "ConnectionNode";
	}
}
