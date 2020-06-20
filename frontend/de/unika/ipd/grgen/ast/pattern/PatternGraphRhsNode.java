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

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeTypeChangeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeTypeChangeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
import de.unika.ipd.grgen.util.Pair;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Set;
import java.util.Vector;

/**
 * AST node that represents a graph pattern as it appears within the replace/modify part of some rule
 */
public class PatternGraphRhsNode extends PatternGraphBaseNode
{
	static {
		setName(PatternGraphRhsNode.class, "pattern graph rhs");
	}

	protected CollectNode<SubpatternReplNode> subpatternRepls;
	public CollectNode<EvalStatementsNode> evals;
	protected CollectNode<OrderedReplacementsNode> orderedReplacements;
	public CollectNode<BaseNode> imperativeStmts;


	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 */
	public PatternGraphRhsNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params,
			CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
			CollectNode<OrderedReplacementsNode> orderedReplacements, CollectNode<ExprNode> returns,
			CollectNode<BaseNode> imperativeStmts, int context, PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(nameOfGraph, coords, connections, params,
				subpatterns, returns, context);
		this.subpatternRepls = subpatternRepls;
		becomeParent(this.subpatternRepls);
		this.orderedReplacements = orderedReplacements;
		becomeParent(this.orderedReplacements);
		this.imperativeStmts = imperativeStmts;
		becomeParent(imperativeStmts);
		this.context = context;

		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		if(params != null)
			addParamsToConnections(params); // treat non-var parameters like connections
	}

	public void addEvals(CollectNode<EvalStatementsNode> evals)
	{
		this.evals = evals;
		becomeParent(this.evals);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(connectionsUnresolved, connections));
		children.add(params);
		children.add(defVariablesToBeYieldedTo);
		children.add(subpatterns);
		children.add(subpatternRepls);
		children.add(orderedReplacements);
		children.add(evals);
		children.add(returns);
		children.add(imperativeStmts);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("connections");
		childrenNames.add("params");
		childrenNames.add("defVariablesToBeYieldedTo");
		childrenNames.add("subpatterns");
		childrenNames.add("subpatternReplacements");
		childrenNames.add("orderedReplacements");
		childrenNames.add("evals");
		childrenNames.add("returns");
		childrenNames.add("imperativeStmts");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		replaceSubpatternReplacementsIntoOrderedReplacements();

		return super.resolveLocal();
	}

	// replace subpattern replacement node placeholder just specifying position in ordered list 
	// by subpattern replacement node from unordered list with correct arguments
	// move missing replacement nodes to the begin of the ordered list, it is the base list for further processing
	private void replaceSubpatternReplacementsIntoOrderedReplacements()
	{
		Iterator<SubpatternReplNode> it = subpatternRepls.getChildren().iterator();
		while(it.hasNext()) {
			SubpatternReplNode subpatternRepl = it.next();
			for(OrderedReplacementsNode orderedRepls : orderedReplacements.getChildren()) {
				if(!orderedRepls.getChildren().isEmpty()) {
					Iterator<OrderedReplacementNode> subCand = orderedRepls.getChildren().iterator();
					OrderedReplacementNode orderedRepl = subCand.next();
					if(orderedRepl instanceof SubpatternReplNode) {
						SubpatternReplNode orderedSubpatternRepl = (SubpatternReplNode)orderedRepl;
						String orderedSubpatternReplName = orderedSubpatternRepl.getSubpatternIdent().toString();
						String subpatternReplName = subpatternRepl.getSubpatternIdent().toString();
						if(orderedSubpatternReplName.equals(subpatternReplName)) {
							subCand.remove();
							orderedRepls.addChild(subpatternRepl);
							it.remove();
						}
					}
				}
			}
		}
		for(int i = subpatternRepls.getChildren().size() - 1; i >= 0; --i) {
			SubpatternReplNode subpatternRepl = subpatternRepls.get(i);
			OrderedReplacementsNode orderedRepls = new OrderedReplacementsNode(subpatternRepl.getCoords(),
					subpatternRepl.getSubpatternIdent().getIdent().toString());
			orderedRepls.addChild(subpatternRepl);
			orderedReplacements.addChildAtFront(orderedRepls);
		}
		subpatternRepls.getChildren().clear();
	}

	/**
	 * A pattern node contains just a collect node with connection nodes as its children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		return isEdgeReuseOk() && noExecStatementInEvalHere();
	}

	boolean noExecStatementInEvalHere()
	{
		boolean result = true;
		for(OrderedReplacementsNode orderedRepls : orderedReplacements.getChildren()) {
			result &= orderedRepls.noExecStatement();
		}
		return result;
	}

	public Pair<Boolean, NodeTypeChangeDeclNode> noAmbiguousRetypes(NodeDeclNode node, NodeTypeChangeDeclNode retypeOfNode)
	{
		for(NodeDeclNode retypeCandidate : getNodes()) {
			if(!(retypeCandidate instanceof NodeTypeChangeDeclNode))
				continue;
			NodeTypeChangeDeclNode retype = (NodeTypeChangeDeclNode)retypeCandidate;
			boolean mergeeIsSame = false;
			for(NodeDeclNode mergee : retype.getMergees()) {
				if(mergee == node) {
					mergeeIsSame = true;
					break;
				}
			}
			if(retype.getOldNode() == node || mergeeIsSame) {
				if(retypeOfNode == null)
					retypeOfNode = retype;
				else {
					retype.reportError("Two (and hence ambiguous) retype (/merge) statements for the same node are forbidden,"
							+ " other retype (/merge) statement at " + retypeOfNode.getCoords());
					return new Pair<Boolean, NodeTypeChangeDeclNode>(Boolean.valueOf(false), retypeOfNode);
				}
			}
		}
		return new Pair<Boolean, NodeTypeChangeDeclNode>(Boolean.valueOf(true), retypeOfNode);
	}

	public Pair<Boolean, EdgeTypeChangeDeclNode> noAmbiguousRetypes(EdgeDeclNode edge, EdgeTypeChangeDeclNode retypeOfEdge)
	{
		for(EdgeDeclNode retypeCandidate : getEdges()) {
			if(!(retypeCandidate instanceof EdgeTypeChangeDeclNode))
				continue;
			EdgeTypeChangeDeclNode retype = (EdgeTypeChangeDeclNode)retypeCandidate;
			if(retype.getOldEdge() == edge) {
				if(retypeOfEdge == null)
					retypeOfEdge = retype;
				else {
					retype.reportError("Two (and hence ambiguous) retype statements for the same edge are forbidden,"
							+ " other retype statement at " + retypeOfEdge.getCoords());
					return new Pair<Boolean, EdgeTypeChangeDeclNode>(Boolean.valueOf(false), retypeOfEdge);
				}
			}
		}
		return new Pair<Boolean, EdgeTypeChangeDeclNode>(Boolean.valueOf(true), retypeOfEdge);
	}

	protected boolean iteratedNotReferenced(String iterName)
	{
		boolean res = true;
		for(EvalStatementsNode evalStatements : evals.getChildren()) {
			for(EvalStatementNode evalStatement : evalStatements.getChildren()) {
				res &= evalStatement.iteratedNotReferenced(iterName);
			}
		}
		return res;
	}

	protected boolean iteratedNotReferencedInDefElementInitialization(String iterName)
	{
		boolean res = true;
		for(VarDeclNode var : defVariablesToBeYieldedTo.getChildren()) {
			if(var.initialization != null)
				res &= var.initialization.iteratedNotReferenced(iterName);
		}
		return res;
	}

	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public PatternGraphRhs getGraph()
	{
		return checkIR(PatternGraphRhs.class);
	}

	/**
	 * Construct the IR object.
	 * It is a Graph and all the connections (children of the pattern AST node) are put into it.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		PatternGraphRhs gr = new PatternGraphRhs(nameOfGraph);
		gr.setDirectlyNestingLHSGraph(directlyNestingLHSGraph.getPatternGraph());

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addToGraph(gr);
		}

		for(VarDeclNode n : defVariablesToBeYieldedTo.getChildren()) {
			gr.addVariable(n.checkIR(Variable.class));
		}

		for(SubpatternUsageDeclNode n : subpatterns.getChildren()) {
			gr.addSubpatternUsage(n.checkIR(SubpatternUsage.class));
		}

		for(OrderedReplacementsNode n : orderedReplacements.getChildren()) {
			gr.addOrderedReplacement((OrderedReplacements)n.getIR());
		}

		// add subpattern usage connection elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the subpattern usage connection)
		for(OrderedReplacementsNode ors : orderedReplacements.getChildren()) {
			PatternGraphBuilder.addSubpatternReplacementUsageArguments(gr, ors);
		}

		// don't add elements only mentioned in ordered replacements here to the pattern, it prevents them from being deleted
		// in general we must be cautious with adding stuff to rhs because of that problem

		// don't add elements only mentioned in typeof here to the pattern, it prevents them from being deleted
		// in general we must be cautious with adding stuff to rhs because of that problem

		Set<Node> nodesToAdd = new HashSet<Node>();
		Set<Edge> edgesToAdd = new HashSet<Edge>();

		// add elements which we could not be added before because their container was iterated over
		for(Node node : nodesToAdd) {
			gr.addNodeIfNotYetContained(node);
		}
		for(Edge edge : edgesToAdd) {
			gr.addEdgeIfNotYetContained(edge);
		}

		for(BaseNode imperativeStmt : imperativeStmts.getChildren()) {
			gr.addImperativeStmt((ImperativeStmt)imperativeStmt.getIR());
		}

		// add deferred exec elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the deferred exec)
		for(ImperativeStmt impStmt : gr.getImperativeStmts()) {
			PatternGraphBuilder.addElementsUsedInDeferredExec(gr, impStmt);
		}

		// ensure def to be yielded to elements are hom to all others
		// so backend doing some fake search planning for them is not scheduling checks for them
		for(Node node : gr.getNodes()) {
			if(node.isDefToBeYieldedTo())
				gr.addHomToAll(node);
		}
		for(Edge edge : gr.getEdges()) {
			if(edge.isDefToBeYieldedTo())
				gr.addHomToAll(edge);
		}

		return gr;
	}

	public Collection<OrderedReplacements> getOrderedReplacements()
	{
		Collection<OrderedReplacements> ret = new LinkedList<OrderedReplacements>();

		for(OrderedReplacementsNode n : orderedReplacements.getChildren()) {
			ret.add(n.checkIR(OrderedReplacements.class));
		}

		return ret;
	}

	public Collection<EvalStatements> getEvalStatements()
	{
		Collection<EvalStatements> ret = new LinkedList<EvalStatements>();

		for(EvalStatementsNode evalStatements : evals.getChildren()) {
			ret.add(evalStatements.checkIR(EvalStatements.class));
		}

		return ret;
	}
}
