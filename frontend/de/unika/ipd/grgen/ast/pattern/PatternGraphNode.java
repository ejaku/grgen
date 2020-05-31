/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * PatternGraphNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.RuleDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeCaseDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.BoolConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.Typeof;
import de.unika.ipd.grgen.ir.pattern.Alternative;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.RetypedEdge;
import de.unika.ipd.grgen.ir.pattern.RetypedNode;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.SymbolTable;

/**
 * AST node that represents a graph pattern as it appears within the pattern
 * part of some rule Extension of the graph pattern of the rewrite part
 */
// TODO: a pattern graph is not a graph, factor the common stuff out into a base class
public class PatternGraphNode extends GraphNode
{
	static {
		setName(PatternGraphNode.class, "pattern_graph");
	}

	public static final int MOD_DANGLING = 1; // dangling+identification=dpo
	public static final int MOD_IDENTIFICATION = 2;
	public static final int MOD_EXACT = 4;
	public static final int MOD_INDUCED = 8;
	public static final int MOD_PATTERN_LOCKED = 16;
	public static final int MOD_PATTERNPATH_LOCKED = 32;

	/** The modifiers for this type. An ORed combination of the constants above. */
	private int modifiers = 0;

	private CollectNode<ExprNode> conditions;
	public CollectNode<AlternativeDeclNode> alts;
	public CollectNode<IteratedDeclNode> iters;
	public CollectNode<PatternGraphNode> negs; // NACs
	public CollectNode<PatternGraphNode> idpts; // PACs
	private CollectNode<HomNode> homs;
	private CollectNode<TotallyHomNode> totallyHoms;
	private CollectNode<ExactNode> exacts;
	private CollectNode<InducedNode> induceds;

	// Cache variable
	private Collection<Set<ConstraintDeclNode>> homSets = null;

	/**
	 *  Map an edge to his homomorphic set.
	 *
	 *  NOTE: Use getCorrespondentHomSet() to get the homomorphic set.
	 */
	private Map<EdgeDeclNode, Set<EdgeDeclNode>> edgeHomMap =
		new LinkedHashMap<EdgeDeclNode, Set<EdgeDeclNode>>();

	/**
	 *  Map a node to his homomorphic set.
	 *
	 *  NOTE: Use getCorrespondentHomSet() to get the homomorphic set.
	 */
	private Map<NodeDeclNode, Set<NodeDeclNode>> nodeHomMap =
		new LinkedHashMap<NodeDeclNode, Set<NodeDeclNode>>();

	/** All nodes which needed a single node NAC. */
	private Set<NodeDeclNode> singleNodeNegNodes =
		new LinkedHashSet<NodeDeclNode>();

	/** All node pairs which needed a double node NAC. */
	private Set<List<NodeDeclNode>> doubleNodeNegPairs =
		new LinkedHashSet<List<NodeDeclNode>>();

	/** Map a homomorphic set to a set of edges (of the NAC). */
	private Map<Set<NodeDeclNode>, Set<ConnectionNode>> singleNodeNegMap =
		new LinkedHashMap<Set<NodeDeclNode>, Set<ConnectionNode>>();

	/**
	 * Map each pair of homomorphic sets of nodes to a set of edges (of the
	 * NAC).
	 */
	private Map<List<Set<NodeDeclNode>>, Set<ConnectionNode>> doubleNodeNegMap =
		new LinkedHashMap<List<Set<NodeDeclNode>>, Set<ConnectionNode>>();

	// counts number of implicit single and double node negative patterns
	// created from pattern modifiers, in order to get unique negative names
	int implicitNegCounter = 0;

	// if this pattern graph is a negative or independent nested inside an iterated
	// it might break the iterated instead of only the current iterated case, if specified
	public boolean iterationBreaking = false;

	private static PatternGraphNode invalid;

	// invalid pattern node just needed for the isGlobalVariable checks, 
	// so that computations stuff that doesn't have a pattern graph is not classified as global 
	public static PatternGraphNode getInvalid()
	{
		if(invalid == null) {
			invalid = new PatternGraphNode("invalid", Coords.getInvalid(), 
					null, null, 
					null, null, 
					null, null,
					null, null, 
					null, 
					null, 
					null, null,
					null, null,
					0, BaseNode.CONTEXT_COMPUTATION);
		}
		return invalid;
	}

	public PatternGraphNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params,
			CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
			CollectNode<AlternativeDeclNode> alts, CollectNode<IteratedDeclNode> iters,
			CollectNode<PatternGraphNode> negs, CollectNode<PatternGraphNode> idpts,
			CollectNode<ExprNode> conditions, 
			CollectNode<ExprNode> returns,
			CollectNode<HomNode> homs, CollectNode<TotallyHomNode> totallyHoms, 
			CollectNode<ExactNode> exacts, CollectNode<InducedNode> induceds,
			int modifiers, int context) {
		super(nameOfGraph, coords, connections, params, subpatterns, subpatternRepls,
				new CollectNode<OrderedReplacementsNode>(), returns, null, context, null);
		this.alts = alts;
		becomeParent(this.alts);
		this.iters = iters;
		becomeParent(this.iters);
		this.negs = negs;
		becomeParent(this.negs);
		this.idpts = idpts;
		becomeParent(this.idpts);
		this.conditions = conditions;
		becomeParent(this.conditions);
		this.homs = homs;
		becomeParent(this.homs);
		this.totallyHoms = totallyHoms;
		becomeParent(this.totallyHoms);
		this.exacts = exacts;
		becomeParent(this.exacts);
		this.induceds = induceds;
		becomeParent(this.induceds);
		this.modifiers = modifiers;

		directlyNestingLHSGraph = this;
		if(params != null)
			addParamsToConnections(params);
	}

	public void addYieldings(CollectNode<EvalStatementsNode> yieldsEvals)
	{
		this.yieldsEvals = yieldsEvals;
		becomeParent(this.yieldsEvals);
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
		children.add(alts);
		children.add(iters);
		children.add(negs);
		children.add(idpts);
		children.add(returns);
		children.add(yieldsEvals);
		children.add(conditions);
		children.add(homs);
		children.add(totallyHoms);
		children.add(exacts);
		children.add(induceds);
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
		childrenNames.add("alternatives");
		childrenNames.add("iters");
		childrenNames.add("negatives");
		childrenNames.add("independents");
		childrenNames.add("return");
		childrenNames.add("yieldsEvals");
		childrenNames.add("conditions");
		childrenNames.add("homs");
		childrenNames.add("totallyHoms");
		childrenNames.add("exacts");
		childrenNames.add("induceds");
		return childrenNames;
	}

	/**
	 * @see GraphNode#getNodes()
	 */
	@Override
	public Set<NodeDeclNode> getNodes()
	{
		assert isResolved();

		if(nodes != null)
			return nodes;

		LinkedHashSet<NodeDeclNode> tempNodes = new LinkedHashSet<NodeDeclNode>();

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addNodes(tempNodes);
		}

		for(HomNode homNode : homs.getChildren()) {
			for(BaseNode homChild : homNode.getChildren()) {
				if(homChild instanceof NodeDeclNode) {
					tempNodes.add((NodeDeclNode)homChild);
				}
			}
		}

		nodes = Collections.unmodifiableSet(tempNodes);
		return nodes;
	}

	/**
	 * @see GraphNode#getEdges()
	 */
	@Override
	public Set<EdgeDeclNode> getEdges()
	{
		assert isResolved();

		if(edges != null)
			return edges;

		LinkedHashSet<EdgeDeclNode> tempEdges = new LinkedHashSet<EdgeDeclNode>();

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addEdge(tempEdges);
		}

		for(HomNode homNode : homs.getChildren()) {
			for(BaseNode homChild : homNode.getChildren()) {
				if(homChild instanceof EdgeDeclNode) {
					tempEdges.add((EdgeDeclNode)homChild);
				}
			}
		}

		edges = Collections.unmodifiableSet(tempEdges);
		return edges;
	}

	public VarDeclNode getVariable(String name)
	{
		for(VarDeclNode var : getDefVariablesToBeYieldedTo().getChildren()) {
			if(var.getIdentNode().toString().equals(name))
				return var;
		}
		for(DeclNode varCand : getParamDecls()) {
			if(!(varCand instanceof VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			return var;
		}
		return null;
	}

	public NodeDeclNode tryGetNode(String name)
	{
		for(NodeDeclNode node : getNodes()) {
			if(node.ident.toString().equals(name))
				return node;
		}
		return null;
	}

	public EdgeDeclNode tryGetEdge(String name)
	{
		for(EdgeDeclNode edge : getEdges()) {
			if(edge.ident.toString().equals(name))
				return edge;
		}
		return null;
	}

	public VarDeclNode tryGetVar(String name)
	{
		for(VarDeclNode var : defVariablesToBeYieldedTo.getChildren()) {
			if(var.ident.toString().equals(name))
				return var;
		}
		for(DeclNode varCand : getParamDecls()) {
			if(!(varCand instanceof VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			if(var.ident.toString().equals(name))
				return var;
		}
		return null;
	}

	public DeclNode tryGetMember(String name)
	{
		NodeDeclNode node = tryGetNode(name);
		if(node != null)
			return node;
		EdgeDeclNode edge = tryGetEdge(name);
		if(edge != null)
			return edge;
		return tryGetVar(name);
	}

	private void initHomMaps()
	{
		Collection<Set<ConstraintDeclNode>> homSets = getHoms();

		// Each node is homomorphic to itself.
		for(NodeDeclNode node : getNodes()) {
			Set<NodeDeclNode> homSet = new LinkedHashSet<NodeDeclNode>();
			homSet.add(node);
			nodeHomMap.put(node, homSet);
		}

		// Each edge is homomorphic to itself.
		for(EdgeDeclNode edge : getEdges()) {
			Set<EdgeDeclNode> homSet = new LinkedHashSet<EdgeDeclNode>();
			homSet.add(edge);
			edgeHomMap.put(edge, homSet);
		}

		for(Set<ConstraintDeclNode> homSet : homSets) {
			if(homSet.iterator().next() instanceof NodeDeclNode) {
				initNodeHomSet(homSet);
			} else {//if(homSet.iterator().next() instanceof EdgeDeclNode)
				initEdgeHomSet(homSet);
			}
		}
	}

	private void initNodeHomSet(Set<ConstraintDeclNode> homSet)
	{
		for(ConstraintDeclNode elem : homSet) {
			NodeDeclNode node = (NodeDeclNode)elem;
			Set<NodeDeclNode> mapEntry = nodeHomMap.get(node);
			for(ConstraintDeclNode homomorphicNode : homSet) {
				mapEntry.add((NodeDeclNode)homomorphicNode);
			}
		}
	}

	private void initEdgeHomSet(Set<ConstraintDeclNode> homSet)
	{
		for(ConstraintDeclNode elem : homSet) {
			EdgeDeclNode edge = (EdgeDeclNode)elem;
			Set<EdgeDeclNode> mapEntry = edgeHomMap.get(edge);
			for(ConstraintDeclNode homomorphicEdge : homSet) {
				mapEntry.add((EdgeDeclNode)homomorphicEdge);
			}
		}
	}

	private PatternGraphNode getParentPatternGraph()
	{
		for(BaseNode parent : getParents()) {
			if(!(parent instanceof CollectNode<?>))
				continue;

			for(BaseNode grandParent : parent.getParents()) {
				if(grandParent instanceof PatternGraphNode) {
					return (PatternGraphNode)grandParent;
				}
			}
		}

		return null;
	}

	private void initHomSets()
	{
		homSets = new LinkedHashSet<Set<ConstraintDeclNode>>();

		// Own homomorphic sets.
		for(HomNode homNode : homs.getChildren()) {
			homSets.addAll(splitHoms(homNode.getChildren()));
		}

		Set<NodeDeclNode> nodes = getNodes();
		Set<EdgeDeclNode> edges = getEdges();

		// Inherited homomorphic sets.
		for(PatternGraphNode parent = getParentPatternGraph(); parent != null;
				parent = parent.getParentPatternGraph()) {
			for(Set<ConstraintDeclNode> parentHomSet : parent.getHoms()) {
				addInheritedHomSet(parentHomSet, nodes, edges);
			}
		}
	}

	private void addInheritedHomSet(Set<ConstraintDeclNode> parentHomSet,
			Set<NodeDeclNode> nodes, Set<EdgeDeclNode> edges)
	{
		Set<ConstraintDeclNode> inheritedHomSet = new LinkedHashSet<ConstraintDeclNode>();
		if(parentHomSet.iterator().next() instanceof NodeDeclNode) {
			for(ConstraintDeclNode homNode : parentHomSet) {
				if(nodes.contains(homNode)) {
					inheritedHomSet.add(homNode);
				}
			}
			if(inheritedHomSet.size() > 1) {
				homSets.add(inheritedHomSet);
			}
		} else {
			for(ConstraintDeclNode homEdge : parentHomSet) {
				if(edges.contains(homEdge)) {
					inheritedHomSet.add(homEdge);
				}
			}
			if(inheritedHomSet.size() > 1) {
				homSets.add(inheritedHomSet);
			}
		}
	}

	public Collection<Set<ConstraintDeclNode>> getHoms()
	{
		if(homSets == null) {
			initHomSets();
		}

		return homSets;
	}

	/**
	 * Warn if two homomorphic elements can never be matched homomorphic,
	 * because they have incompatible types.
	 */
	private void warnOnSuperfluousHoms()
	{
		Collection<Set<ConstraintDeclNode>> homSets = getHoms();
		for(Set<ConstraintDeclNode> homSet : homSets) {
			warnOnSuperfluousHoms(homSet);
		}
	}

	private void warnOnSuperfluousHoms(Set<ConstraintDeclNode> homSet)
	{
		Set<ConstraintDeclNode> alreadyProcessed = new LinkedHashSet<ConstraintDeclNode>();

		for(ConstraintDeclNode elem1 : homSet) {
			InheritanceTypeNode type1 = elem1.getDeclType();
			for(ConstraintDeclNode elem2 : homSet) {
				if(elem1 == elem2 || alreadyProcessed.contains(elem2))
					continue;

				InheritanceTypeNode type2 = elem2.getDeclType();

				if(InheritanceTypeNode.hasCommonSubtype(type1, type2))
					continue;

				// search hom statement
				HomNode hom = null;
				for(HomNode homNode : homs.getChildren()) {
					Collection<BaseNode> homChildren = homNode.getChildren();
					if(homChildren.contains(elem1) && homChildren.contains(elem2)) {
						hom = homNode;
						break;
					}
				}

				hom.reportWarning(elem1.ident + " and " + elem2.ident
						+ " have no common subtype and thus can never match the same element");
			}

			alreadyProcessed.add(elem1);
		}
	}
	
	boolean noRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent()
	{
		boolean result = true;
		for(PatternGraphNode pattern : negs.getChildren()) {
			for(IteratedDeclNode iter : pattern.iters.getChildren()) {
				if(iter.right != null) {
					iter.right.reportError("An iterated contained within a negative can't possess a rewrite part"
							+ " (the negative is a pure negative application condition)");
					result = false;
				}
			}
			for(AlternativeDeclNode alt : pattern.alts.getChildren()) {
				for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
					if(altCase.right != null) {
						altCase.right.reportError("An alternative case contained within a negative can't possess a rewrite part"
								+ " (the negative is a pure negative application condition)");
						result = false;
					}
				}
			}
		}
		for(PatternGraphNode pattern : idpts.getChildren()) {
			for(IteratedDeclNode iter : pattern.iters.getChildren()) {
				if(iter.right != null) {
					iter.right.reportError("An iterated contained within an independent can't possess a rewrite part"
								+ " (the independent is a pure positive application condition)");
					result = false;
				}
			}
			for(AlternativeDeclNode alt : pattern.alts.getChildren()) {
				for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
					if(altCase.right != null) {
						altCase.right.reportError("An alternative case contained within an independent can't possess a rewrite part"
								+ " (the independent is a pure positive application condition)");
						result = false;
					}
				}
			}
		}
		return result;
	}

	boolean noExecStatementInEvalsOfIteratedOrAlternative()
	{
		boolean result = true;
		for(IteratedDeclNode iter : iters.getChildren()) {
			if(iter.right != null) {
				for(EvalStatementsNode evalStmts : iter.right.getRhsGraph().yieldsEvals.getChildren()) {
					evalStmts.noExecStatement();
				}
			}
		}
		for(AlternativeDeclNode alt : alts.getChildren()) {
			for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
				if(altCase.right != null) {
					for(EvalStatementsNode evalStmts : altCase.right.getRhsGraph().yieldsEvals.getChildren()) {
						evalStmts.noExecStatement();
					}
				}
			}
		}
		return result;
	}

	@Override
	protected boolean checkLocal()
	{
		boolean childs = super.checkLocal();

		boolean expr = true;
		if(childs) {
			for(ExprNode exp : conditions.getChildren()) {
				if(!exp.getType().isEqual(BasicTypeNode.booleanType)) {
					exp.reportError("Expression must be of type boolean");
					expr = false;
				}
			}
		}

		boolean noReturnInNegOrIdpt = true;
		if((context & CONTEXT_NEGATIVE) == CONTEXT_NEGATIVE || (context & CONTEXT_INDEPENDENT) == CONTEXT_INDEPENDENT) {
			if(returns.size() != 0) {
				reportError("return not allowed in negative or independent block");
				noReturnInNegOrIdpt = false;
			}
		}

		warnOnSuperfluousHoms();

		return childs & expr & noReturnInNegOrIdpt 
				& noRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent()
				& noDefElementOrIteratedReferenceInCondition()
				& noIteratedReferenceInDefElementInitialization()
				& iteratedNameIsNotAccessedInNestedPattern()
				& noExecStatementInEvalsOfIteratedOrAlternative();
	}

	private boolean noDefElementOrIteratedReferenceInCondition()
	{
		boolean res = true;
		for(ExprNode cond : conditions.getChildren()) {
			res &= cond.noDefElement("if condition");
			res &= cond.noIteratedReference("if condition");
		}
		return res;
	}

	private boolean noIteratedReferenceInDefElementInitialization()
	{
		boolean res = true;
		for(VarDeclNode var : defVariablesToBeYieldedTo.getChildren()) {
			if(var.initialization != null)
				res &= var.initialization.noIteratedReference("def variable initialization");
		}
		return res;
	}

	private boolean iteratedNameIsNotAccessedInNestedPattern()
	{
		boolean res = true;
		for(IteratedDeclNode iterForNameToCheck : iters.getChildren()) {
			String iterName = iterForNameToCheck.getIdentNode().toString();
			for(IteratedDeclNode iter : iters.getChildren()) {
				res &= iter.pattern.iteratedNotReferenced(iterName);
				if(iter.right != null) {
					res &= iter.right.graph.iteratedNotReferenced(iterName);
					res &= iter.right.graph.iteratedNotReferencedInDefElementInitialization(iterName);
				}
			}
			for(AlternativeDeclNode alt : alts.getChildren()) {
				for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
					res &= altCase.pattern.iteratedNotReferenced(iterName);
					if(altCase.right != null) {
						res &= altCase.right.graph.iteratedNotReferenced(iterName);
						res &= altCase.right.graph.iteratedNotReferencedInDefElementInitialization(iterName);
					}
				}
			}
			for(PatternGraphNode idpt : idpts.getChildren()) {
				res &= idpt.iteratedNotReferenced(iterName);
			}
		}
		return res;
	}

	public boolean checkFilterVariable(IdentNode errorTarget, String filterNameWithEntitySuffix, String filterVariable)
	{
		VarDeclNode variable = tryGetVar(filterVariable);
		if(variable == null) {
			errorTarget.reportError(filterNameWithEntitySuffix + ": unknown variable " + filterVariable);
			return false;
		}
		TypeNode filterVariableType = variable.getDeclType();
		if(!filterVariableType.isOrderableType()) {
			errorTarget.reportError(filterNameWithEntitySuffix + ": the variable " + filterVariable
					+ " must be of one of the following types: " + TypeNode.getOrderableTypesAsString());
			return false;
		}
		return true;
	}

	public boolean checkFilterEntity(IdentNode errorTarget, String filterNameWithEntitySuffix, String filterEntity)
	{
		DeclNode entity = tryGetNode(filterEntity);
		if(entity == null)
			entity = tryGetEdge(filterEntity);
		if(entity == null)
			entity = tryGetVar(filterEntity);
		if(entity == null) {
			errorTarget.reportError(filterNameWithEntitySuffix + ": unknown entity " + filterEntity);
			return false;
		}
		TypeNode filterVariableType = entity.getDeclType();
		if(!filterVariableType.isFilterableType()) {
			errorTarget.reportError(filterNameWithEntitySuffix + ": the entity " + filterEntity
					+ " must be of one of the following types: " + TypeNode.getFilterableTypesAsString());
			return false;
		}
		return true;
	}

	/**
	 * Get the correctly casted IR object.
	 *
	 * @return The IR object.
	 */
	public PatternGraph getPatternGraph()
	{
		return checkIR(PatternGraph.class);
	}

	/** NOTE: Use this only in DPO-Mode,i.e. if the pattern is part of a rule */
	private RuleDeclNode getRule()
	{
		for(BaseNode parent : getParents()) {
			if(parent instanceof RuleDeclNode) {
				return (RuleDeclNode)parent;
			}
		}
		assert false;
		return null;
	}

	/**
	 * Generates a type condition if the given graph entity inherits its type
	 * from another element via a typeof expression (dynamic type checks).
	 */
	private void genTypeConditionsFromTypeof(PatternGraph gr, GraphEntity elem)
	{
		if(elem.inheritsType()) {
			assert !elem.isCopy(); // must extend this function and lgsp nodes if left hand side copy/copyof are wanted
								   // (meaning compare attributes of exact dynamic types)

			Expression e1 = new Typeof(elem);
			Expression e2 = new Typeof(elem.getTypeof());

			Operator op = new Operator(BasicTypeNode.booleanType.getPrimitiveType(), Operator.GE);
			op.addOperand(e1);
			op.addOperand(e2);

			gr.addCondition(op);
		}
	}

	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) {
			return getIR();
		}

		PatternGraph patternGraph = new PatternGraph(nameOfGraph, modifiers);
		patternGraph.setDirectlyNestingLHSGraph(patternGraph);

		// mark this node as already visited
		setIR(patternGraph);

		if(this == getInvalid())
			return patternGraph;

		patternGraph.setIterationBreaking(iterationBreaking);

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addToGraph(patternGraph);
		}

		for(VarDeclNode varNode : defVariablesToBeYieldedTo.getChildren()) {
			patternGraph.addVariable(varNode.checkIR(Variable.class));
		}

		for(BaseNode subpatternUsage : subpatterns.getChildren()) {
			patternGraph.addSubpatternUsage(subpatternUsage.checkIR(SubpatternUsage.class));
		}

		for(AlternativeDeclNode alternativeNode : alts.getChildren()) {
			patternGraph.addAlternative(alternativeNode.checkIR(Alternative.class));
		}

		for(IteratedDeclNode iteratedNode : iters.getChildren()) {
			patternGraph.addIterated(iteratedNode.checkIR(Rule.class));
		}

		for(PatternGraphNode negativeNode : negs.getChildren()) {
			addNegatives(patternGraph, negativeNode);
		}

		for(PatternGraphNode independentNode : idpts.getChildren()) {
			addIndependents(patternGraph, independentNode);
		}

		for(ExprNode condition : conditions.getChildren()) {
			ExprNode conditionEvaluated = condition.evaluate(); // compile time evaluation (constant folding)
			warnIfConditionIsConstant(conditionEvaluated);
			patternGraph.addCondition(conditionEvaluated.checkIR(Expression.class));
		}

		for(EvalStatements yields : getYieldEvalStatements()) {
			patternGraph.addYield(yields);
		}

		for(Node node : patternGraph.getNodes()) {
			genTypeConditionsFromTypeof(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			genTypeConditionsFromTypeof(patternGraph, edge);
		}

		for(Set<ConstraintDeclNode> homEntityNodes : getHoms()) {
			addHoms(patternGraph, homEntityNodes);
		}

		for(TotallyHomNode totallyHomNode : totallyHoms.getChildren()) {
			addTotallyHom(patternGraph, totallyHomNode);
		}

		for(Node node : patternGraph.getNodes()) {
			ensureDefNodesAreHomToAllOthers(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			ensureDefEdgesAreHomToAllOthers(patternGraph, edge);
		}

		for(Node node : patternGraph.getNodes()) {
			ensureRetypedNodeHomToOldNode(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			ensureRetypedEdgeHomToOldEdge(patternGraph, edge);
		}

		addElementsHiddenInUsedConstructs(patternGraph);

		return patternGraph;
	}

	void addElementsHiddenInUsedConstructs(PatternGraph patternGraph)
	{
		// add subpattern usage connection elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the subpattern usage connection)
		for(SubpatternUsageDeclNode subpatternUsageNode : subpatterns.getChildren()) {
			addSubpatternUsageArgument(patternGraph, subpatternUsageNode);
		}

		// add subpattern usage yield elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the subpattern usage yield)
		for(SubpatternUsageDeclNode subpatternUsageNode : subpatterns.getChildren()) {
			addSubpatternUsageYieldArgument(patternGraph, subpatternUsageNode);
		}

		// add elements only mentioned in typeof to the pattern
		for(Node node : patternGraph.getNodes()) {
			addNodeFromTypeof(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			addEdgeFromTypeof(patternGraph, edge);
		}

		// add Condition elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the condition)
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(Expression condition : patternGraph.getConditions()) {
			condition.collectNeededEntities(needs);
		}
		addNeededEntities(patternGraph, needs);

		// add Yielded elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the yield)
		needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(EvalStatements yield : patternGraph.getYields()) {
			yield.collectNeededEntities(needs);
		}
		addNeededEntities(patternGraph, needs);

		// add elements only mentioned in hom-declaration to the IR
		// (they're declared in an enclosing graph and locally only show up in the hom-declaration)
		for(Collection<? extends GraphEntity> homEntities : patternGraph.getHomomorphic()) {
			addHomElements(patternGraph, homEntities);
		}

		// add elements only mentioned in "map by / draw from storage" entities to the IR
		// (they're declared in an enclosing graph and locally only show up in the "map by / draw from storage" node)
		for(Node node : patternGraph.getNodes()) {
			addElementsFromStorageAccess(patternGraph, node);
		}

		for(Node node : patternGraph.getNodes()) {
			// add old node of lhs retype
			if(node instanceof RetypedNode && !node.isRHSEntity()) {
				patternGraph.addNodeIfNotYetContained(((RetypedNode)node).getOldNode());
			}
		}

		for(Edge edge : patternGraph.getEdges()) {
			addElementsFromStorageAccess(patternGraph, edge);
		}

		for(Edge edge : patternGraph.getEdges()) {
			// add old edge of lhs retype
			if(edge instanceof RetypedEdge && !edge.isRHSEntity()) {
				patternGraph.addEdgeIfNotYetContained(((RetypedEdge)edge).getOldEdge());
			}
		}

		// add index access elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the index access)
		needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(Node node : patternGraph.getNodes()) {
			if(node.indexAccess != null) {
				node.indexAccess.collectNeededEntities(needs);
			}
		}
		for(Edge edge : patternGraph.getEdges()) {
			if(edge.indexAccess != null) {
				edge.indexAccess.collectNeededEntities(needs);
			}
		}
		addNeededEntities(patternGraph, needs);
	}

	void addSubpatternUsageArgument(PatternGraph patternGraph, SubpatternUsageDeclNode subpatternUsageNode)
	{
		List<Expression> subpatternConnections = subpatternUsageNode.checkIR(SubpatternUsage.class).getSubpatternConnections();
		for(Expression expr : subpatternConnections) {
			if(expr instanceof GraphEntityExpression) {
				GraphEntity connection = ((GraphEntityExpression)expr).getGraphEntity();
				if(connection instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)connection);
				} else if(connection instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)connection);
				} else {
					assert(false);
				}
			} else {
				NeededEntities needs = new NeededEntities(false, false, true, false, false, false, false, false);
				expr.collectNeededEntities(needs);
				for(Variable neededVariable : needs.variables) {
					if(!patternGraph.hasVar(neededVariable)) {
						patternGraph.addVariable(neededVariable);
					}
				}
			}
		}
	}

	void addSubpatternUsageYieldArgument(PatternGraph patternGraph, SubpatternUsageDeclNode subpatternUsageNode)
	{
		List<Expression> subpatternYields = subpatternUsageNode.checkIR(SubpatternUsage.class).getSubpatternYields();
		for(Expression expr : subpatternYields) {
			if(expr instanceof GraphEntityExpression) {
				GraphEntity connection = ((GraphEntityExpression)expr).getGraphEntity();
				if(connection instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)connection);
				} else if(connection instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)connection);
				} else {
					assert(false);
				}
			} else {
				NeededEntities needs = new NeededEntities(false, false, true, false, false, false, false, false);
				expr.collectNeededEntities(needs);
				for(Variable neededVariable : needs.variables) {
					if(!patternGraph.hasVar(neededVariable)) {
						patternGraph.addVariable(neededVariable);
					}
				}
			}
		}
	}

	void addNodeFromTypeof(PatternGraph patternGraph, Node node)
	{
		if(node.inheritsType()) {
			patternGraph.addNodeIfNotYetContained((Node)node.getTypeof());
		}
	}

	void addEdgeFromTypeof(PatternGraph patternGraph, Edge edge)
	{
		if(edge.inheritsType()) {
			patternGraph.addEdgeIfNotYetContained((Edge)edge.getTypeof());
		}
	}

	void addHomElements(PatternGraph patternGraph, Collection<? extends GraphEntity> homEntities)
	{
		for(GraphEntity homEntity : homEntities) {
			if(homEntity instanceof Node) {
				patternGraph.addNodeIfNotYetContained((Node)homEntity);
			} else {
				patternGraph.addEdgeIfNotYetContained((Edge)homEntity);
			}
		}
	}

	void addElementsFromStorageAccess(PatternGraph patternGraph, Node node)
	{
		if(node.storageAccess != null) {
			if(node.storageAccess.storageVariable != null) {
				Variable storageVariable = node.storageAccess.storageVariable;
				if(!patternGraph.hasVar(storageVariable)) {
					patternGraph.addVariable(storageVariable);
				}
			} else if(node.storageAccess.storageAttribute != null) {
				Qualification storageAttributeAccess = node.storageAccess.storageAttribute;
				if(storageAttributeAccess.getOwner() instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)storageAttributeAccess.getOwner());
				} else if(storageAttributeAccess.getOwner() instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)storageAttributeAccess.getOwner());
				}
			}
		}

		if(node.storageAccessIndex != null) {
			if(node.storageAccessIndex.indexGraphEntity != null) {
				GraphEntity indexGraphEntity = node.storageAccessIndex.indexGraphEntity;
				if(indexGraphEntity instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)indexGraphEntity);
				} else if(indexGraphEntity instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)indexGraphEntity);
				}
			}
		}
	}

	void addElementsFromStorageAccess(PatternGraph patternGraph, Edge edge)
	{
		if(edge.storageAccess != null) {
			if(edge.storageAccess.storageVariable != null) {
				Variable storageVariable = edge.storageAccess.storageVariable;
				if(!patternGraph.hasVar(storageVariable)) {
					patternGraph.addVariable(storageVariable);
				}
			} else if(edge.storageAccess.storageAttribute != null) {
				Qualification storageAttributeAccess = edge.storageAccess.storageAttribute;
				if(storageAttributeAccess.getOwner() instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)storageAttributeAccess.getOwner());
				} else if(storageAttributeAccess.getOwner() instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)storageAttributeAccess.getOwner());
				}
			}
		}

		if(edge.storageAccessIndex != null) {
			if(edge.storageAccessIndex.indexGraphEntity != null) {
				GraphEntity indexGraphEntity = edge.storageAccessIndex.indexGraphEntity;
				if(indexGraphEntity instanceof Node) {
					patternGraph.addNodeIfNotYetContained((Node)indexGraphEntity);
				} else if(indexGraphEntity instanceof Edge) {
					patternGraph.addEdgeIfNotYetContained((Edge)indexGraphEntity);
				}
			}
		}
	}

	protected void addNeededEntities(PatternGraph patternGraph, NeededEntities needs)
	{
		for(Node neededNode : needs.nodes) {
			patternGraph.addNodeIfNotYetContained(neededNode);
		}
		for(Edge neededEdge : needs.edges) {
			patternGraph.addEdgeIfNotYetContained(neededEdge);
		}
		for(Variable neededVariable : needs.variables) {
			if(!patternGraph.hasVar(neededVariable)) {
				patternGraph.addVariable(neededVariable);
			}
		}
	}

	void warnIfConditionIsConstant(ExprNode expr)
	{
		if(expr instanceof BoolConstNode) {
			if((Boolean)((BoolConstNode)expr).getValue()) {
				expr.reportWarning("Condition is always true");
			} else {
				expr.reportWarning("Condition is always false, pattern will never match");
			}
		}
	}

	void addHoms(PatternGraph patternGraph, Set<ConstraintDeclNode> homEntityNodes)
	{
		// homSet is not empty, first element defines type of all elements
		if(homEntityNodes.iterator().next() instanceof NodeDeclNode) {
			HashSet<Node> homNodes = new HashSet<Node>();
			for(DeclNode node : homEntityNodes) {
				homNodes.add(node.checkIR(Node.class));
			}
			patternGraph.addHomomorphicNodes(homNodes);
		} else {
			HashSet<Edge> homEdges = new HashSet<Edge>();
			for(DeclNode edge : homEntityNodes) {
				homEdges.add(edge.checkIR(Edge.class));
			}
			patternGraph.addHomomorphicEdges(homEdges);
		}
	}

	void addTotallyHom(PatternGraph patternGraph, TotallyHomNode totallyHomNode)
	{
		if(totallyHomNode.node != null) {
			HashSet<Node> totallyHomNodes = new HashSet<Node>();
			for(NodeDeclNode node : totallyHomNode.childrenNode) {
				totallyHomNodes.add(node.checkIR(Node.class));
			}
			patternGraph.addTotallyHomomorphic(totallyHomNode.node.checkIR(Node.class), totallyHomNodes);
		} else {
			HashSet<Edge> totallyHomEdges = new HashSet<Edge>();
			for(EdgeDeclNode edge : totallyHomNode.childrenEdge) {
				totallyHomEdges.add(edge.checkIR(Edge.class));
			}
			patternGraph.addTotallyHomomorphic(totallyHomNode.edge.checkIR(Edge.class), totallyHomEdges);
		}
	}

	void addNegatives(PatternGraph patternGraph, PatternGraphNode negativeNode)
	{
		PatternGraph negative = negativeNode.getPatternGraph();
		patternGraph.addNegGraph(negative);
		if(negative.isIterationBreaking()) {
			patternGraph.setIterationBreaking(true);
		}
	}

	void addIndependents(PatternGraph patternGraph, PatternGraphNode independentNode)
	{
		PatternGraph independent = independentNode.getPatternGraph();
		patternGraph.addIdptGraph(independent);
		if(independent.isIterationBreaking()) {
			patternGraph.setIterationBreaking(true);
		}
	}

	// ensure def to be yielded to elements are hom to all others
	// so backend doing some fake search planning for them is not scheduling checks for them
	void ensureDefNodesAreHomToAllOthers(PatternGraph patternGraph, Node node)
	{
		if(node.isDefToBeYieldedTo()) {
			patternGraph.addHomToAll(node);
		}
	}

	void ensureDefEdgesAreHomToAllOthers(PatternGraph patternGraph, Edge edge)
	{
		if(edge.isDefToBeYieldedTo()) {
			patternGraph.addHomToAll(edge);
		}
	}

	// ensure lhs retype elements are hom to their old element
	void ensureRetypedNodeHomToOldNode(PatternGraph patternGraph, Node node)
	{
		if(node instanceof RetypedNode && !node.isRHSEntity()) {
			Vector<Node> homNodes = new Vector<Node>();
			homNodes.add(node);
			homNodes.add(((RetypedNode)node).getOldNode());
			patternGraph.addHomomorphicNodes(homNodes);
		}
	}

	void ensureRetypedEdgeHomToOldEdge(PatternGraph patternGraph, Edge edge)
	{
		if(edge instanceof RetypedEdge && !edge.isRHSEntity()) {
			Vector<Edge> homEdges = new Vector<Edge>();
			homEdges.add(edge);
			homEdges.add(((RetypedEdge)edge).getOldEdge());
			patternGraph.addHomomorphicEdges(homEdges);
		}
	}

	/**
	 * Split one hom statement into two parts, so deleted and reuse nodes/edges
	 * can't be matched homomorphically.
	 *
	 * This behavior is required for DPO-semantic.
	 * If the rule is not DPO the (casted) original homomorphic set is returned.
	 * Only homomorphic set with two or more entities will returned.
	 *
	 * @param homChildren Children of a HomNode
	 */
	private Set<Set<ConstraintDeclNode>> splitHoms(Collection<? extends BaseNode> homChildren)
	{
		Set<Set<ConstraintDeclNode>> ret = new LinkedHashSet<Set<ConstraintDeclNode>>();
		if(isIdentification()) {
			// homs between deleted entities
			HashSet<ConstraintDeclNode> deleteHomSet = new HashSet<ConstraintDeclNode>();
			// homs between reused entities
			HashSet<ConstraintDeclNode> reuseHomSet = new HashSet<ConstraintDeclNode>();

			for(BaseNode homChild : homChildren) {
				ConstraintDeclNode decl = (ConstraintDeclNode)homChild;

				Set<ConstraintDeclNode> deletedEntities = getRule().getDeletedElements();
				if(deletedEntities.contains(decl)) {
					deleteHomSet.add(decl);
				} else {
					reuseHomSet.add(decl);
				}
			}
			if(deleteHomSet.size() > 1) {
				ret.add(deleteHomSet);
			}
			if(reuseHomSet.size() > 1) {
				ret.add(reuseHomSet);
			}
			return ret;
		}

		Set<ConstraintDeclNode> homSet = new LinkedHashSet<ConstraintDeclNode>();

		for(BaseNode homChild : homChildren) {
			ConstraintDeclNode decl = (ConstraintDeclNode)homChild;

			homSet.add(decl);
		}
		if(homSet.size() > 1) {
			ret.add(homSet);
		}
		return ret;
	}

	private boolean isInduced()
	{
		return (modifiers & MOD_INDUCED) != 0;
	}

	private boolean isDangling()
	{
		return (modifiers & MOD_DANGLING) != 0;
	}

	private boolean isIdentification()
	{
		return (modifiers & MOD_IDENTIFICATION) != 0;
	}

	private boolean isExact()
	{
		return (modifiers & MOD_EXACT) != 0;
	}

	/**
	 * Get all implicit NACs.
	 *
	 * @return The Collection for the NACs.
	 */
	public Collection<PatternGraph> getImplicitNegGraphs()
	{
		Collection<PatternGraph> ret = new LinkedList<PatternGraph>();

		initDoubleNodeNegMap();
		addDoubleNodeNegGraphs(ret);

		initSingleNodeNegMap();
		addSingleNodeNegGraphs(ret);

		return ret;
	}

	private void initDoubleNodeNegMap()
	{
		if(isInduced()) {
			addToDoubleNodeMap(getNodes());

			for(InducedNode induced : induceds.getChildren()) {
				induced.reportWarning("Induced statement occurs in induced pattern");
			}
			return;
		}

		Map<Set<NodeDeclNode>, Integer> generatedInducedSets = new LinkedHashMap<Set<NodeDeclNode>, Integer>();
		for(int i = 0; i < induceds.getChildren().size(); i++) {
			InducedNode induced = induceds.get(i);
			Set<NodeDeclNode> inducedNodes = induced.getInducedNodesSet();
			if(generatedInducedSets.containsKey(inducedNodes)) {
				InducedNode oldOcc = induceds.get(generatedInducedSets.get(inducedNodes));
				induced.reportWarning("Same induced statement also occurs at " + oldOcc.getCoords());
			} else {
				addToDoubleNodeMap(inducedNodes);
				generatedInducedSets.put(inducedNodes, i);
			}
		}

		warnRedundantInducedStatement(generatedInducedSets);
	}

	/**
	 * warn if an induced statement is redundant.
	 *
	 * Algorithm:
	 * Input: Sets V_i of nodes
	 * for each V_i
	 *   K_i = all pairs of nodes of V_i
	 * for each i
	 *   for each k_i of K_i
	 *     for each K_j
	 *       if k_i \in K_j: mark k_i
	 *   if all k_i marked: warn
	 *
	 * @param generatedInducedSets Set of all induced statements
	 */
	private void warnRedundantInducedStatement(Map<Set<NodeDeclNode>, Integer> generatedInducedSets)
	{
		Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap =
				new LinkedHashMap<Map<List<NodeDeclNode>, Boolean>, Integer>();

		// create all pairs of nodes (->edges)
		for(Map.Entry<Set<NodeDeclNode>, Integer> nodeMapEntry : generatedInducedSets.entrySet()) {
			fillInducedEdgeMap(inducedEdgeMap, nodeMapEntry);
		}

		for(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate : inducedEdgeMap.entrySet()) {
			Set<Integer> witnesses = getWitnessesAndMarkEdge(inducedEdgeMap, candidate);

			// all edges marked?
			if(allMarked(candidate)) {
				String witnessesLoc = "";
				for(Integer index : witnesses) {
					witnessesLoc += induceds.get(index).getCoords() + " ";
				}
				witnessesLoc = witnessesLoc.trim();
				induceds.get(candidate.getValue()).reportWarning(
						"Induced statement is redundant, since covered by statement(s) at " + witnessesLoc);
			}
		}
	}

	private void fillInducedEdgeMap(Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap,
			Map.Entry<Set<NodeDeclNode>, Integer> nodeMapEntry)
	{
		// if the Boolean in markedMap is true -> edge is marked
		Map<List<NodeDeclNode>, Boolean> markedMap = new LinkedHashMap<List<NodeDeclNode>, Boolean>();

		for(NodeDeclNode src : nodeMapEntry.getKey()) {
			for(NodeDeclNode tgt : nodeMapEntry.getKey()) {
				List<NodeDeclNode> edge = new LinkedList<NodeDeclNode>();
				edge.add(src);
				edge.add(tgt);

				markedMap.put(edge, false);
			}
		}

		inducedEdgeMap.put(markedMap, nodeMapEntry.getValue());
	}

	private Set<Integer> getWitnessesAndMarkEdge(Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap,
			Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate)
	{
		Set<Integer> witnesses = new LinkedHashSet<Integer>();

		for(Map.Entry<List<NodeDeclNode>, Boolean> candidateMarkedMap : candidate.getKey().entrySet()) {
			// TODO also mark witness edge (and candidate as witness)
			if(!candidateMarkedMap.getValue().booleanValue()) {
				for(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> witness : inducedEdgeMap.entrySet()) {
					if(candidate != witness) {
						// if witness contains edge
						if(witness.getKey().containsKey(candidateMarkedMap.getKey())) {
							// mark Edge
							candidateMarkedMap.setValue(true);
							// add witness
							witnesses.add(witness.getValue());
						}
					}
				}
			}
		}
		
		return witnesses;
	}

	private boolean allMarked(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate)
	{
		boolean allMarked = true;
		
		for(boolean edgeMarked : candidate.getKey().values()) {
			allMarked &= edgeMarked;
		}
		
		return allMarked;
	}

	private void initSingleNodeNegMap()
	{
		if(isExact()) {
			addToSingleNodeMap(getNodes());

			if(isDangling() && !isIdentification()) {
				reportWarning("The keyword \"dangling\" is redundant for exact patterns");
			}

			for(ExactNode exact : exacts.getChildren()) {
				exact.reportWarning("Exact statement occurs in exact pattern");
			}

			return;
		}

		if(isDangling()) {
			Set<ConstraintDeclNode> deletedNodes = getRule().getDeletedElements();
			addToSingleNodeMap(getDpoPatternNodes(deletedNodes));

			for(ExactNode exact : exacts.getChildren()) {
				for(NodeDeclNode exactNode : exact.getExactNodes()) {
					if(deletedNodes.contains(exactNode)) {
						exact.reportWarning("Exact statement for " + exactNode.getUseString() + " "
								+ exactNode.getIdentNode().getSymbol().getText()
								+ " is redundant, since the pattern is declared \"dangling\" or \"dpo\"");
					}
				}
			}
		}

		Map<NodeDeclNode, Integer> generatedExactNodes = new LinkedHashMap<NodeDeclNode, Integer>();		
		for(int i = 0; i < exacts.getChildren().size(); i++) { // exact Statements
			ExactNode exact = exacts.get(i);
			for(NodeDeclNode exactNode : exact.getExactNodes()) {
				// coords of occurrence are not available
				if(generatedExactNodes.containsKey(exactNode)) {
					exact.reportWarning(exactNode.getUseString() + " "
							+ exactNode.getIdentNode().getSymbol().getText()
							+ " already occurs in exact statement at "
							+ exacts.get(generatedExactNodes.get(exactNode)).getCoords());
				} else {
					generatedExactNodes.put(exactNode, i);
				}
			}
		}

		addToSingleNodeMap(generatedExactNodes.keySet());
	}

	/**
	 * Return the set of nodes needed for the singleNodeNegMap if the whole
	 * pattern is dpo.
	 */
	private Set<NodeDeclNode> getDpoPatternNodes(Set<ConstraintDeclNode> deletedEntities)
	{
		Set<NodeDeclNode> deletedNodes = new LinkedHashSet<NodeDeclNode>();

		for(DeclNode declNode : deletedEntities) {
			if(declNode instanceof NodeDeclNode) {
				NodeDeclNode node = (NodeDeclNode)declNode;
				if(!node.isDummy()) {
					deletedNodes.add(node);
				}
			}
		}

		return deletedNodes;
	}

	private void addSingleNodeNegGraphs(Collection<PatternGraph> ret)
	{
		assert isResolved();

		// add existing edges to the corresponding sets
		for(BaseNode connection : connections.getChildren()) {
			if(connection instanceof ConnectionNode) {
				ConnectionNode cn = (ConnectionNode)connection;
				NodeDeclNode src = cn.getSrc();
				if(singleNodeNegNodes.contains(src)) {
					Set<NodeDeclNode> homSet = getHomomorphic(src);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(cn);
					singleNodeNegMap.put(homSet, edges);
				}
				NodeDeclNode tgt = cn.getTgt();
				if(singleNodeNegNodes.contains(tgt)) {
					Set<NodeDeclNode> homSet = getHomomorphic(tgt);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(cn);
					singleNodeNegMap.put(homSet, edges);
				}
			}
		}

		TypeDeclNode edgeRoot = getArbitraryEdgeRootTypeDecl();
		TypeDeclNode nodeRoot = getNodeRootTypeDecl();

		// generate and add pattern graphs
		for(NodeDeclNode singleNodeNegNode : singleNodeNegNodes) {
			//for (int direction = INCOMING; direction <= OUTGOING; direction++) {
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			Set<ConnectionNode> edgeSet = singleNodeNegMap.get(getHomomorphic(singleNodeNegNode));
			PatternGraph neg = new PatternGraph("implneg_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);

			// add edges to NAC
			for(ConnectionNode conn : edgeSet) {
				conn.addToGraph(neg);

				allNegEdges.add(conn.getEdge());
				allNegNodes.add(conn.getSrc());
				allNegNodes.add(conn.getTgt());
			}

			addInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot, context);
			NodeDeclNode dummyNode = getAnonymousDummyNode(nodeRoot, context);

			ConnectionNode conn = new ConnectionNode(singleNodeNegNode, edge, dummyNode, ConnectionNode.ARBITRARY, this);
			conn.addToGraph(neg);

			ret.add(neg);
			//}
		}
	}

	/**
	 * Add a set of nodes to the singleNodeMap.
	 *
	 * @param nodes Set of Nodes.
	 */
	private void addToSingleNodeMap(Set<NodeDeclNode> nodes)
	{
		for(NodeDeclNode node : nodes) {
			if(node.isDummy())
				continue;

			singleNodeNegNodes.add(node);
			Set<NodeDeclNode> homSet = getHomomorphic(node);
			if(!singleNodeNegMap.containsKey(homSet)) {
				Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				singleNodeNegMap.put(homSet, edgeSet);
			}
		}
	}

	/** Return the correspondent homomorphic set. */
	public Set<NodeDeclNode> getHomomorphic(NodeDeclNode node)
	{
		if(!nodeHomMap.containsKey(node)) {
			initHomMaps();
		}

		Set<NodeDeclNode> homSet = nodeHomMap.get(node);

		if(homSet == null) {
			// If the node isn't part of the pattern, return empty set.
			homSet = new LinkedHashSet<NodeDeclNode>();
		}

		return homSet;
	}

	/** Return the correspondent homomorphic set. */
	public Set<EdgeDeclNode> getHomomorphic(EdgeDeclNode edge)
	{
		if(!edgeHomMap.containsKey(edge)) {
			initHomMaps();
		}

		Set<EdgeDeclNode> homSet = edgeHomMap.get(edge);

		if(homSet == null) {
			// If the edge isn't part of the pattern, return empty set.
			homSet = new LinkedHashSet<EdgeDeclNode>();
		}

		return homSet;
	}

	private NodeDeclNode getAnonymousDummyNode(TypeDeclNode nodeRoot, int context)
	{
		IdentNode nodeName = new IdentNode(
				getScope().defineAnonymous("dummy_node", SymbolTable.getInvalid(), Coords.getBuiltin()));
		NodeDeclNode dummyNode = NodeDeclNode.getDummy(nodeName, nodeRoot, context, this);
		return dummyNode;
	}

	private EdgeDeclNode getAnonymousEdgeDecl(TypeDeclNode edgeRoot, int context)
	{
		IdentNode edgeName = new IdentNode(
				getScope().defineAnonymous("edge", SymbolTable.getInvalid(), Coords.getBuiltin()));
		EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot, context, this, this);
		return edge;
	}

	/**
	 * @param negs
	 */
	private void addDoubleNodeNegGraphs(Collection<PatternGraph> ret)
	{
		assert isResolved();

		// add existing edges to the corresponding pattern graph
		for(BaseNode connection : connections.getChildren()) {
			if(connection instanceof ConnectionNode) {
				ConnectionNode cn = (ConnectionNode)connection;

				List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
				key.add(getHomomorphic(cn.getSrc()));
				key.add(getHomomorphic(cn.getTgt()));

				Set<ConnectionNode> edges = doubleNodeNegMap.get(key);
				// edges == null if conn is a dangling edge or one of the nodes
				// is not induced
				if(edges != null) {
					edges.add(cn);
					doubleNodeNegMap.put(key, edges);
				}
			}
		}

		TypeDeclNode edgeRoot = getArbitraryEdgeRootTypeDecl();

		for(List<NodeDeclNode> pair : doubleNodeNegPairs) {
			NodeDeclNode src = pair.get(0);
			NodeDeclNode tgt = pair.get(1);

			if(src.getId().compareTo(tgt.getId()) > 0) {
				continue;
			}

			List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
			key.add(getHomomorphic(src));
			key.add(getHomomorphic(tgt));
			List<Set<NodeDeclNode>> key2 = new LinkedList<Set<NodeDeclNode>>();
			key2.add(getHomomorphic(tgt));
			key2.add(getHomomorphic(src));
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			Set<ConnectionNode> edgeSet = doubleNodeNegMap.get(key);
			edgeSet.addAll(doubleNodeNegMap.get(key2));

			PatternGraph neg = new PatternGraph("implneg_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);

			// add edges to the NAC
			for(ConnectionNode conn : edgeSet) {
				conn.addToGraph(neg);

				allNegEdges.add(conn.getEdge());
				allNegNodes.add(conn.getSrc());
				allNegNodes.add(conn.getTgt());
			}

			addInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot, context);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt, ConnectionNode.ARBITRARY, this);

			conn.addToGraph(neg);

			ret.add(neg);
		}
	}

	/**
	 * Add all necessary homomorphic sets to a NAC.
	 *
	 * If an edge a-e->b is homomorphic to another edge c-f->d f only added if
	 * a is homomorphic to c and b is homomorphic to d.
	 */
	private void addInheritedHomSet(PatternGraph neg, Set<EdgeDeclNode> allNegEdges, Set<NodeDeclNode> allNegNodes)
	{
		// inherit homomorphic nodes
		for(NodeDeclNode node : allNegNodes) {
			Set<Node> homSet = new LinkedHashSet<Node>();
			Set<NodeDeclNode> homNodes = getHomomorphic(node);

			for(NodeDeclNode homNode : homNodes) {
				if(allNegNodes.contains(homNode)) {
					homSet.add(homNode.checkIR(Node.class));
				}
			}
			if(homSet.size() > 1) {
				neg.addHomomorphicNodes(homSet);
			}
		}

		// inherit homomorphic edges
		for(EdgeDeclNode edge : allNegEdges) {
			Set<Edge> homSet = new LinkedHashSet<Edge>();
			Set<EdgeDeclNode> homEdges = getHomomorphic(edge);

			for(EdgeDeclNode homEdge : homEdges) {
				if(allNegEdges.contains(homEdge)) {
					homSet.add(homEdge.checkIR(Edge.class));
				}
			}
			if(homSet.size() > 1) {
				neg.addHomomorphicEdges(homSet);
			}
		}
	}

	private void addToDoubleNodeMap(Set<NodeDeclNode> nodes)
	{
		for(NodeDeclNode src : nodes) {
			if(src.isDummy())
				continue;

			for(NodeDeclNode tgt : nodes) {
				if(tgt.isDummy())
					continue;

				List<NodeDeclNode> pair = new LinkedList<NodeDeclNode>();
				pair.add(src);
				pair.add(tgt);
				doubleNodeNegPairs.add(pair);

				List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
				key.add(getHomomorphic(src));
				key.add(getHomomorphic(tgt));

				if(!doubleNodeNegMap.containsKey(key)) {
					Set<ConnectionNode> edges = new LinkedHashSet<ConnectionNode>();
					doubleNodeNegMap.put(key, edges);
				}
			}
		}
	}
}
