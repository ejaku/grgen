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

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.parser.Coords;

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
	CollectNode<IdentNode> returnsUnresolved;
	CollectNode<ConstraintDeclNode> returns;
	CollectNode<BaseNode> imperativeStmts;
	CollectNode<BaseNode> params;

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
			CollectNode<IdentNode> returns, CollectNode<BaseNode> imperativeStmts,
			int context) {
		super(coords);
		this.nameOfGraph = nameOfGraph;
		this.connectionsUnresolved = connections;
		becomeParent(this.connectionsUnresolved);
		this.subpatterns = subpatterns;
		becomeParent(this.subpatterns);
		this.subpatternReplacements = subpatternReplacements;
		becomeParent(this.subpatternReplacements);
		this.returnsUnresolved = returns;
		becomeParent(this.returnsUnresolved);
		this.imperativeStmts = imperativeStmts;
		becomeParent(imperativeStmts);
		this.params = params;
		becomeParent(this.params);
		this.context = context;

		// treat parameters like connections
		addParamsToConnections(params);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(connectionsUnresolved, connections));
		children.add(params);
		children.add(subpatterns);
		children.add(subpatternReplacements);
		children.add(getValidVersion(returnsUnresolved, returns));
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
		new CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(new DeclarationTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(ConnectionNode.class, SingleNodeConnNode.class,  SingleGraphEntityNode.class));;

	private static final CollectPairResolver<ConstraintDeclNode> returnsResolver = new CollectPairResolver<ConstraintDeclNode>(
			new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class));


	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		Triple<CollectNode<ConnectionNode>, CollectNode<SingleNodeConnNode>, CollectNode<SingleGraphEntityNode>> resolve = connectionsResolver.resolve(connectionsUnresolved);

		if (resolve != null) {
			if (resolve.first != null) {
    			for (ConnectionNode conn : resolve.first.getChildren()) {
                    connections.addChild(conn);
                }
			}

        	if (resolve.second != null) {
            	for (SingleNodeConnNode conn : resolve.second.getChildren()) {
                    connections.addChild(conn);
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
        				connections.addChild(new SingleNodeConnNode(ent.getEntityNode()));
        			}

        			// add reused subpattern to subpatterns
        			if (ent.getEntitySubpattern() != null) {
        				subpatterns.addChild(ent.getEntitySubpattern());
        			}
                }
    		}
        }

		returns = returnsResolver.resolve(returnsUnresolved);

		return resolve != null && returns != null;
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
	protected Set<BaseNode> getNodes() {
		assert isResolved();

		Set<BaseNode> res = new LinkedHashSet<BaseNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addNodes(res);
		}

		return res;
	}

	protected Set<BaseNode> getEdges() {
		assert isResolved();

		Set<BaseNode> res = new LinkedHashSet<BaseNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addEdge(res);
		}

		return res;
	}

	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public PatternGraph getGraph() {
		return (PatternGraph) checkIR(PatternGraph.class);
	}

	/**
	 * Construct the IR object.
	 * It is a Graph and all the connections (children of the pattern AST node) are put into it.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		PatternGraph gr = new PatternGraph(nameOfGraph, false);

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addToGraph(gr);
		}

		for(BaseNode n : subpatterns.getChildren()) {
			gr.addSubpatternUsage((SubpatternUsage)n.getIR());
		}

		// TODO imperativeStmts
		for(BaseNode imp : imperativeStmts.getChildren()) {
			gr.addImperativeStmt((ImperativeStmt)imp.getIR());
		}

		return gr;
	}

	private void addParamsToConnections(CollectNode<BaseNode> params)
    {
    	for (BaseNode n : params.getChildren()) {
            connectionsUnresolved.addChild(n);
        }
    }

	public Collection<DeclNode> getParamDecls() {
		Collection<DeclNode> res = new Vector<DeclNode>();

		for (BaseNode para : params.getChildren()) {
	        if (para instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) para;
	        	res.add(conn.getEdge().getDecl());
	        }
	        if (para instanceof SingleNodeConnNode) {
	        	NodeDeclNode node = ((SingleNodeConnNode) para).getNode();
	        	res.add(node);
	        }
        }

		return res;
	}
}

