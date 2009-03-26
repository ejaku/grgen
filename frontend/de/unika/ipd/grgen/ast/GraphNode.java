/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

/**
 * AST node that represents a graph pattern
 * as it appears within the replace/modify part of some rule
 * or to be used as base class for PatternGraphNode
 * representing the graph pattern of the pattern part of some rule
 */
public class GraphNode extends BaseNode {
	static {
		setName(GraphNode.class, "graph");
	}

	CollectNode<BaseNode> connectionsUnresolved;
	CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
	CollectNode<SubpatternUsageNode> subpatterns;
	CollectNode<SubpatternReplNode> subpatternReplacements;
	CollectNode<ExprNode> returns;
	CollectNode<BaseNode> imperativeStmts;
	CollectNode<BaseNode> params;

	protected boolean hasAbstractElements;

	// Cache variables
	Set<NodeDeclNode> nodes;
	Set<EdgeDeclNode> edges;

	/** context(action or pattern, lhs not rhs) in which this node occurs*/
	int context = 0;

	protected String nameOfGraph;

	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 */
	public GraphNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params,
			CollectNode<SubpatternUsageNode> subpatterns,
			CollectNode<SubpatternReplNode> subpatternReplacements,
			CollectNode<ExprNode> returns, CollectNode<BaseNode> imperativeStmts,
			int context) {
		super(coords);
		this.nameOfGraph = nameOfGraph;
		this.connectionsUnresolved = connections;
		becomeParent(this.connectionsUnresolved);
		this.subpatterns = subpatterns;
		becomeParent(this.subpatterns);
		this.subpatternReplacements = subpatternReplacements;
		becomeParent(this.subpatternReplacements);
		this.returns = returns;
		becomeParent(this.returns);
		this.imperativeStmts = imperativeStmts;
		becomeParent(imperativeStmts);
		this.params = params;
		becomeParent(this.params);
		this.context = context;

		// treat non-var parameters like connections
		addParamsToConnections(params);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(connectionsUnresolved, connections));
		children.add(params);
		children.add(subpatterns);
		children.add(subpatternReplacements);
		children.add(returns);
		children.add(imperativeStmts);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("connections");
		childrenNames.add("params");
		childrenNames.add("subpatterns");
		childrenNames.add("subpatternReplacements");
		childrenNames.add("returns");
		childrenNames.add("imperativeStmts");
		return childrenNames;
	}

	private static final CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode> connectionsResolver =
		new CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(
			new DeclarationTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(
				ConnectionNode.class, SingleNodeConnNode.class,  SingleGraphEntityNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		Triple<CollectNode<ConnectionNode>, CollectNode<SingleNodeConnNode>, CollectNode<SingleGraphEntityNode>> resolve =
			connectionsResolver.resolve(connectionsUnresolved);

		if (resolve != null) {
			if (resolve.first != null) {
    			for (ConnectionNode conn : resolve.first.getChildren()) {
                    connections.addChild(conn);
					if(!conn.resolve()) return false;
					if(conn.getEdge().getDeclType().isAbstract()
							|| conn.getSrc().getDeclType().isAbstract()
							|| conn.getTgt().getDeclType().isAbstract())
						hasAbstractElements = true;
                }
			}

        	if (resolve.second != null) {
            	for (SingleNodeConnNode conn : resolve.second.getChildren()) {
                    connections.addChild(conn);
					if(!conn.resolve()) return false;
					if(conn.getNode().getDeclType().isAbstract())
						hasAbstractElements = true;
                }
			}

        	if (resolve.third != null) {
        		for (SingleGraphEntityNode ent : resolve.third.getChildren()) {
        			// resolve the entity
        			if (!ent.resolve()) {
        				return false;
        			}

        			// add reused single node to connections
        			if (ent.getEntityNode() != null) {
        				connections.addChild(new SingleNodeConnNode(ent.getEntityNode(), ent.directlyNestingLHSGraph));
        			}

        			// add reused subpattern to subpatterns
        			if (ent.getEntitySubpattern() != null) {
        				subpatterns.addChild(ent.getEntitySubpattern());
        			}
                }
    		}

        	becomeParent(connections);
        	becomeParent(subpatterns);
        }

		boolean paramsOK = true;
    	for (BaseNode n : params.getChildren()) {
			if(!(n instanceof VarDeclNode)) continue;

			VarDeclNode paramVar = (VarDeclNode) n;
			if(paramVar.resolve()) {
				if(!(paramVar.getDeclType() instanceof BasicTypeNode)
						&& !(paramVar.getDeclType() instanceof MapTypeNode)
						&& !(paramVar.getDeclType() instanceof SetTypeNode)) {
					paramVar.typeUnresolved.reportError("Type of variable \""
							+ paramVar.getIdentNode() + "\" must be a basic type (like int or string), a map or a set");
					paramsOK = false;
				}
			}
			else paramsOK = false;
        }

		boolean resSubUsages = true;
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			for(SubpatternUsageNode subUsage : subpatterns.getChildren()) {
				if(subUsage.resolve()) {
					PatternGraphNode pattern = subUsage.getSubpatternDeclNode().getPattern();
					if(pattern.hasAbstractElements) {
						subUsage.reportError("Cannot instantiate pattern with abstract elements");
						resSubUsages = false;
					}
				}
				else resSubUsages = false;
			}
		}

		return resolve != null && paramsOK && resSubUsages;
	}

	private static final Checker connectionsChecker = new CollectChecker(new SimpleChecker(ConnectionCharacter.class));

	/**
	 * A pattern node contains just a collect node with connection nodes as its children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		boolean connCheck = connectionsChecker.check(connections, error);

		boolean edgeUsage = true;

		if(connCheck) {
			//check, that each named edge is only used once in a pattern
			HashSet<EdgeCharacter> edges = new HashSet<EdgeCharacter>();
			for (BaseNode n : connections.getChildren()) {
				ConnectionCharacter cc = (ConnectionCharacter)n;
				EdgeCharacter ec = cc.getEdge();

				// add() returns false iff edges already contains ec
				if (ec != null
						&& !(cc instanceof ConnectionNode
								&& cc.getSrc() instanceof DummyNodeDeclNode
								&& cc.getTgt() instanceof DummyNodeDeclNode)
						&& !edges.add(ec)) {
					((EdgeDeclNode) ec).reportError("This (named) edge is used more than once in a graph of this action");
					edgeUsage = false;
				}
			}
		}

		return edgeUsage && connCheck;
	}

	/**
	 * Get an iterator iterating over all connections characters in this pattern.
	 * These are the children of the collect node at position 0.
	 * @return The iterator.
	 */
	protected Collection<BaseNode> getConnections() {
		assert isResolved();

		return connections.getChildren();
	}

	/**
	 * Get a set of all nodes in this pattern.
	 * Use this function after this node has been checked with {@link #checkLocal()}
	 * to ensure, that the children have the right type.
	 * @return A set containing the declarations of all nodes occurring
	 * in this graph pattern.
	 */
	protected Set<NodeDeclNode> getNodes() {
		assert isResolved();

		if(nodes != null) return nodes;

		LinkedHashSet<NodeDeclNode> coll = new LinkedHashSet<NodeDeclNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addNodes(coll);
		}

		nodes = Collections.unmodifiableSet(coll);
		return nodes;
	}

	protected Set<EdgeDeclNode> getEdges() {
		assert isResolved();

		if(edges != null) return edges;

		LinkedHashSet<EdgeDeclNode> coll = new LinkedHashSet<EdgeDeclNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addEdge(coll);
		}

		edges = Collections.unmodifiableSet(coll);
		return edges;
	}

	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public PatternGraph getGraph() {
		return checkIR(PatternGraph.class);
	}

	/**
	 * Construct the IR object.
	 * It is a Graph and all the connections (children of the pattern AST node) are put into it.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		PatternGraph gr = new PatternGraph(nameOfGraph, 0);

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addToGraph(gr);
		}

		for(SubpatternUsageNode n : subpatterns.getChildren()) {
			gr.addSubpatternUsage(n.checkIR(SubpatternUsage.class));
		}

		for(SubpatternReplNode n : subpatternReplacements.getChildren()) {
			gr.addSubpatternReplacement(n.checkIR(SubpatternDependentReplacement.class));
		}

		for(BaseNode imp : imperativeStmts.getChildren()) {
			gr.addImperativeStmt((ImperativeStmt)imp.getIR());
		}

		return gr;
	}

	protected void addParamsToConnections(CollectNode<BaseNode> params)
    {
    	for (BaseNode n : params.getChildren()) {
			if(n instanceof VarDeclNode) continue;
            connectionsUnresolved.addChild(n);
        }
    }

	public Vector<DeclNode> getParamDecls() {
		Vector<DeclNode> res = new Vector<DeclNode>();

		for (BaseNode para : params.getChildren()) {
	        if (para instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) para;
	        	res.add(conn.getEdge().getDecl());
	        }
	        else if (para instanceof SingleNodeConnNode) {
	        	NodeDeclNode node = ((SingleNodeConnNode) para).getNode();
	        	res.add(node);
	        }
			else if(para instanceof VarDeclNode) {
				res.add((VarDeclNode) para);
			}
			else
				throw new UnsupportedOperationException("Unsupported parameter (" + para + ")");
        }

		return res;
	}
}
