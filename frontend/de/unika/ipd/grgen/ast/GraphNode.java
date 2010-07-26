/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.OrderedReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.YieldedEntities;
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

	protected CollectNode<BaseNode> connectionsUnresolved;
	protected CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
	protected CollectNode<SubpatternUsageNode> subpatterns;
	protected CollectNode<OrderedReplacementNode> orderedReplacements;
	protected CollectNode<ExprNode> returns;
	protected CollectNode<BaseNode> imperativeStmts;
	protected CollectNode<BaseNode> params;
	protected CollectNode<YieldedEntitiesNode> receivedYields;

	protected boolean hasAbstractElements;

	// Cache variables
	protected Set<NodeDeclNode> nodes;
	protected Set<EdgeDeclNode> edges;
	
	/** context(action or pattern, lhs not rhs) in which this node occurs*/
	protected int context = 0;

	PatternGraphNode directlyNestingLHSGraph;

	protected String nameOfGraph;

	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 */
	public GraphNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params,
			CollectNode<SubpatternUsageNode> subpatterns,
			CollectNode<OrderedReplacementNode> orderedReplacements,
			CollectNode<ExprNode> returns, CollectNode<BaseNode> imperativeStmts,
			CollectNode<YieldedEntitiesNode> receivedYields,
			int context, PatternGraphNode directlyNestingLHSGraph) {
		super(coords);
		this.nameOfGraph = nameOfGraph;
		this.connectionsUnresolved = connections;
		becomeParent(this.connectionsUnresolved);
		this.subpatterns = subpatterns;
		becomeParent(this.subpatterns);
		this.orderedReplacements = orderedReplacements;
		becomeParent(this.orderedReplacements);
		this.returns = returns;
		becomeParent(this.returns);
		this.imperativeStmts = imperativeStmts;
		becomeParent(imperativeStmts);
		this.receivedYields = receivedYields;
		becomeParent(receivedYields);
		this.params = params;
		becomeParent(this.params);
		this.context = context;

		// if we're constructed as part of a PatternGraphNode 
		// this code will be executed by the PatternGraphNode
		if(directlyNestingLHSGraph!=null) {
			this.directlyNestingLHSGraph = directlyNestingLHSGraph;
			// treat non-var parameters like connections
			addParamsToConnections(params);
		}
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(connectionsUnresolved, connections));
		children.add(params);
		children.add(subpatterns);
		children.add(orderedReplacements);
		children.add(returns);
		children.add(imperativeStmts);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
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
	@Override
	protected boolean resolveLocal() {
		if(receivedYields!=null) // only given for rhs as of now
			addYieldsToConnections();

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
        				connections.addChild(new SingleNodeConnNode(ent.getEntityNode()));
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
						&& !(paramVar.getDeclType() instanceof EnumTypeNode)
						&& !(paramVar.getDeclType() instanceof MapTypeNode)
						&& !(paramVar.getDeclType() instanceof SetTypeNode)) {
					paramVar.typeUnresolved.reportError("Type of variable \""
							+ paramVar.getIdentNode() + "\" must be a basic type (like int or string), or an enum, or a map or a set");
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
	@Override
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
	protected PatternGraph getGraph() {
		return checkIR(PatternGraph.class);
	}

	/**
	 * Construct the IR object.
	 * It is a Graph and all the connections (children of the pattern AST node) are put into it.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		PatternGraph gr = new PatternGraph(nameOfGraph, 0);
		gr.setDirectlyNestingLHSGraph(directlyNestingLHSGraph.getGraph());
		
		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addToGraph(gr);
		}
		
		for(SubpatternUsageNode n : subpatterns.getChildren()) {
			gr.addSubpatternUsage(n.checkIR(SubpatternUsage.class));
		}

		for(OrderedReplacementNode n : orderedReplacements.getChildren()) {
			gr.addOrderedReplacement((OrderedReplacement)n.getIR());
		}

		for(BaseNode imp : imperativeStmts.getChildren()) {
			gr.addImperativeStmt((ImperativeStmt)imp.getIR());
		}

		for(YieldedEntitiesNode n : receivedYields.getChildren()) {
			gr.addYieldedEntities(n.checkIR(YieldedEntities.class));
		}
		
		for(ImperativeStmt impStmt : gr.getImperativeStmts()) {
			for(YieldedEntities yieldedEntities : gr.getYieldedEntities()) {
				if(yieldedEntities.getOrigin()==impStmt) {
					((Exec)impStmt).addYieldedEntities(yieldedEntities);
				}
			}
		}

		return gr;
	}

	protected void addParamsToConnections(CollectNode<BaseNode> params)
    {
    	for (BaseNode n : params.getChildren()) {
			// directly nesting lhs pattern is null for parameters of lhs/rhs pattern
			// because it doesn't exist at the time the parameters are parsed -> patch it in here
			if(n instanceof VarDeclNode) {
				((VarDeclNode)n).directlyNestingLHSGraph = directlyNestingLHSGraph;
				continue;
			} else if(n instanceof SingleNodeConnNode) {
				SingleNodeConnNode sncn = (SingleNodeConnNode)n;
				((NodeDeclNode)sncn.nodeUnresolved).directlyNestingLHSGraph = directlyNestingLHSGraph;
			} else if(n instanceof ConstraintDeclNode) {
				((ConstraintDeclNode)n).directlyNestingLHSGraph = directlyNestingLHSGraph;
			} else {
				// don't need to adapt left/right nodes as only dummies
				// TODO casts checked?
				ConnectionNode cn = (ConnectionNode)n;
				((EdgeDeclNode)cn.edgeUnresolved).directlyNestingLHSGraph = directlyNestingLHSGraph;
			}

            connectionsUnresolved.addChild(n);
        }
    }

	protected void addYieldsToConnections()
    {
		for(YieldedEntitiesNode yieldedEntities : receivedYields.getChildren())
			for(BaseNode entity : yieldedEntities.getChildren())
				connectionsUnresolved.addChild(entity);
    }
	
	protected Vector<DeclNode> getParamDecls() {
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
