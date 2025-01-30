/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * PatternGraphNode.java
 *
 * @author Sebastian Hack, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.LinkedList;
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
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Alternative;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.SymbolTable;

/**
 * AST node that represents a graph pattern as it appears within the pattern part of some rule
 */
public class PatternGraphLhsNode extends PatternGraphBaseNode
{
	static {
		setName(PatternGraphLhsNode.class, "pattern graph lhs");
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
	public CollectNode<EvalStatementsNode> yields;
	public CollectNode<AlternativeDeclNode> alts;
	public CollectNode<IteratedDeclNode> iters;
	public CollectNode<PatternGraphLhsNode> negs; // NACs
	public CollectNode<PatternGraphLhsNode> idpts; // PACs
	public CollectNode<HomNode> homs;
	private CollectNode<TotallyHomNode> totallyHoms;
	public CollectNode<ExactNode> exacts;
	public CollectNode<InducedNode> induceds;

	private HomStorage homStorage;

	protected boolean hasAbstractElements;

	// if this pattern graph is a negative or independent nested inside an iterated
	// it might break the iterated instead of only the current iterated case, if specified
	public boolean iterationBreaking = false;

	private static PatternGraphLhsNode invalid;

	// invalid pattern node just needed for the isGlobalVariable checks, 
	// so that computations stuff that doesn't have a pattern graph is not classified as global 
	public static PatternGraphLhsNode getInvalid()
	{
		if(invalid == null) {
			invalid = new PatternGraphLhsNode("invalid", Coords.getInvalid(), 
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

	public PatternGraphLhsNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params,
			CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
			CollectNode<AlternativeDeclNode> alts, CollectNode<IteratedDeclNode> iters,
			CollectNode<PatternGraphLhsNode> negs, CollectNode<PatternGraphLhsNode> idpts,
			CollectNode<ExprNode> conditions, 
			CollectNode<ExprNode> returns,
			CollectNode<HomNode> homs, CollectNode<TotallyHomNode> totallyHoms, 
			CollectNode<ExactNode> exacts, CollectNode<InducedNode> induceds,
			int modifiers, int context) {
		super(nameOfGraph, coords, connections, params, subpatterns,
				returns, context);
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

		this.directlyNestingLHSGraph = this;
		if(params != null)
			addParamsToConnections(params); // treat non-var parameters like connections
	}

	public void addYieldings(CollectNode<EvalStatementsNode> yields)
	{
		this.yields = yields;
		becomeParent(this.yields);
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
		children.add(alts);
		children.add(iters);
		children.add(negs);
		children.add(idpts);
		children.add(returns);
		children.add(yields);
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
		childrenNames.add("alternatives");
		childrenNames.add("iters");
		childrenNames.add("negatives");
		childrenNames.add("independents");
		childrenNames.add("return");
		childrenNames.add("yields");
		childrenNames.add("conditions");
		childrenNames.add("homs");
		childrenNames.add("totallyHoms");
		childrenNames.add("exacts");
		childrenNames.add("induceds");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean result = super.resolveLocal();
		
		determineExistenceOfAbstractElements();
		
		return result;
	}

	void determineExistenceOfAbstractElements()
	{
		for(ConnectionCharacter cc : connections.getChildren()) {
			if(cc instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode)cc;
				if(conn.getEdge().getDeclType().isAbstract()
						|| conn.getSrc().getDeclType().isAbstract()
						|| conn.getTgt().getDeclType().isAbstract())
					hasAbstractElements = true;
			}
			else if(cc instanceof SingleNodeConnNode) {
				SingleNodeConnNode conn = (SingleNodeConnNode)cc;
				if(conn.getNode().getDeclType().isAbstract())
					hasAbstractElements = true;
			}
		}
	}

	@Override
	protected Set<NodeDeclNode> getNodesImpl()
	{
		assert isResolved();

		LinkedHashSet<NodeDeclNode> tempNodes = new LinkedHashSet<NodeDeclNode>();

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addNodes(tempNodes);
		}

		for(HomNode hom : homs.getChildren()) {
			for(NodeDeclNode homNode : hom.getHomNodes()) {
				tempNodes.add(homNode);
			}
		}

		return tempNodes;
	}

	@Override
	protected Set<EdgeDeclNode> getEdgesImpl()
	{
		assert isResolved();

		LinkedHashSet<EdgeDeclNode> tempEdges = new LinkedHashSet<EdgeDeclNode>();

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addEdge(tempEdges);
		}

		for(HomNode hom : homs.getChildren()) {
			for(EdgeDeclNode homEdge : hom.getHomEdges()) {
				tempEdges.add(homEdge);
			}
		}

		return tempEdges;
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

	public PatternGraphLhsNode getParentPatternGraph()
	{
		for(BaseNode parent : getParents()) {
			if(!(parent instanceof CollectNode<?>))
				continue;

			for(BaseNode grandParent : parent.getParents()) {
				if(grandParent instanceof PatternGraphLhsNode) {
					return (PatternGraphLhsNode)grandParent;
				}
			}
		}

		return null;
	}

	public boolean isInduced()
	{
		return (modifiers & MOD_INDUCED) != 0;
	}

	public boolean isDangling()
	{
		return (modifiers & MOD_DANGLING) != 0;
	}

	public boolean isIdentification()
	{
		return (modifiers & MOD_IDENTIFICATION) != 0;
	}

	public boolean isExact()
	{
		return (modifiers & MOD_EXACT) != 0;
	}

	public NodeDeclNode getAnonymousDummyNode(TypeDeclNode nodeRoot, int context)
	{
		IdentNode nodeName = new IdentNode(
				getScope().defineAnonymous("dummy_node", SymbolTable.getInvalid(), Coords.getBuiltin()));
		NodeDeclNode dummyNode = NodeDeclNode.getDummy(nodeName, nodeRoot, context, this);
		return dummyNode;
	}

	public EdgeDeclNode getAnonymousEdgeDecl(TypeDeclNode edgeRoot, int context)
	{
		IdentNode edgeName = new IdentNode(
				getScope().defineAnonymous("edge", SymbolTable.getInvalid(), Coords.getBuiltin()));
		EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot, context, this, this);
		return edge;
	}

	public Collection<Set<ConstraintDeclNode>> getHoms()
	{
		if(homStorage == null)
			homStorage = new HomStorage(this);
		return homStorage.getHoms();
	}

	/** Return the correspondent homomorphic set. */
	public Set<NodeDeclNode> getHomomorphic(NodeDeclNode node)
	{
		if(homStorage == null)
			homStorage = new HomStorage(this);
		return homStorage.getHomomorphic(node);
	}

	/** Return the correspondent homomorphic set. */
	public Set<EdgeDeclNode> getHomomorphic(EdgeDeclNode edge)
	{
		if(homStorage == null)
			homStorage = new HomStorage(this);
		return homStorage.getHomomorphic(edge);
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

				if(hom != null) {
					hom.reportWarning("The " + elem1.getKind() + " " + elem1.ident + " and the " + elem2.getKind() + " " + elem2.ident
							+ " have no common subtype and thus can never match the same element.");
				}
			}

			alreadyProcessed.add(elem1);
		}
	}
	
	boolean noRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent()
	{
		boolean result = true;
		for(PatternGraphLhsNode pattern : negs.getChildren()) {
			for(IteratedDeclNode iter : pattern.iters.getChildren()) {
				if(iter.right != null) {
					iter.right.reportError("An iterated contained within a negative cannot possess a rewrite part"
							+ " (the negative is a pure (negative) application condition).");
					result = false;
				}
			}
			for(AlternativeDeclNode alt : pattern.alts.getChildren()) {
				for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
					if(altCase.right != null) {
						altCase.right.reportError("An alternative case contained within a negative cannot possess a rewrite part"
								+ " (the negative is a pure (negative) application condition).");
						result = false;
					}
				}
			}
		}
		for(PatternGraphLhsNode pattern : idpts.getChildren()) {
			for(IteratedDeclNode iter : pattern.iters.getChildren()) {
				if(iter.right != null) {
					iter.right.reportError("An iterated contained within an independent cannot possess a rewrite part"
								+ " (the independent is a pure (positive) application condition).");
					result = false;
				}
			}
			for(AlternativeDeclNode alt : pattern.alts.getChildren()) {
				for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
					if(altCase.right != null) {
						altCase.right.reportError("An alternative case contained within an independent cannot possess a rewrite part"
								+ " (the independent is a pure (positive) application condition).");
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
				for(EvalStatementsNode evalStmts : iter.right.getRhsGraph().evals.getChildren()) {
					evalStmts.noExecStatement();
				}
			}
		}
		for(AlternativeDeclNode alt : alts.getChildren()) {
			for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
				if(altCase.right != null) {
					for(EvalStatementsNode evalStmts : altCase.right.getRhsGraph().evals.getChildren()) {
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
		boolean expr = true;
		
		for(ExprNode exp : conditions.getChildren()) {
			if(!exp.getType().isEqual(BasicTypeNode.booleanType)) {
				exp.reportError("An expression in an if condition must be of type boolean (but is of type " + exp.getType().getTypeName() + ").");
				expr = false;
			}
		}

		boolean noReturnInNegOrIdpt = true;
		if((context & CONTEXT_NEGATIVE) == CONTEXT_NEGATIVE) {
			if(returns.size() != 0) {
				reportError("A return is not allowed in a negative block.");
				noReturnInNegOrIdpt = false;
			}
		}
		if((context & CONTEXT_INDEPENDENT) == CONTEXT_INDEPENDENT) {
			if(returns.size() != 0) {
				reportError("A return is not allowed in an independent block.");
				noReturnInNegOrIdpt = false;
			}
		}

		warnOnSuperfluousHoms();

		return isEdgeReuseOk() & expr & noReturnInNegOrIdpt 
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
					res &= iter.right.patternGraph.iteratedNotReferenced(iterName);
					res &= iter.right.patternGraph.iteratedNotReferencedInDefElementInitialization(iterName);
				}
			}
			for(AlternativeDeclNode alt : alts.getChildren()) {
				for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
					res &= altCase.pattern.iteratedNotReferenced(iterName);
					if(altCase.right != null) {
						res &= altCase.right.patternGraph.iteratedNotReferenced(iterName);
						res &= altCase.right.patternGraph.iteratedNotReferencedInDefElementInitialization(iterName);
					}
				}
			}
			for(PatternGraphLhsNode idpt : idpts.getChildren()) {
				res &= idpt.iteratedNotReferenced(iterName);
			}
		}
		return res;
	}

	protected boolean iteratedNotReferenced(String iterName)
	{
		boolean res = true;
		for(EvalStatementsNode yieldStatements : yields.getChildren()) {
			for(EvalStatementNode yieldStatement : yieldStatements.getChildren()) {
				res &= yieldStatement.iteratedNotReferenced(iterName);
			}
		}
		return res;
	}

	public boolean checkFilterVariable(IdentNode errorTarget, String filterNameWithEntitySuffix, String filterVariable)
	{
		VarDeclNode variable = tryGetVar(filterVariable);
		if(variable == null) {
			errorTarget.reportError("The variable " + filterVariable + " is not known"
					+ filterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		TypeNode filterVariableType = variable.getDeclType();
		if(!filterVariableType.isOrderableType()) {
			errorTarget.reportError("The variable " + filterVariable + " must be of one of the following types: " + TypeNode.getOrderableTypesAsString()
				+ " (but is of type " + filterVariableType.getTypeName() + ")"
				+ filterSpecification(filterNameWithEntitySuffix) + ".");
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
			errorTarget.reportError("The entity " + filterEntity + " is not known"
					+ filterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		TypeNode filterVariableType = entity.getDeclType();
		if(!filterVariableType.isFilterableType()) {
			errorTarget.reportError("The entity " + filterEntity + " must be of one of the following types: " + TypeNode.getFilterableTypesAsString()
					+ " (but is of type " + filterVariableType.getTypeName() + ")"
					+ filterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		return true;
	}
	
	private String filterSpecification(String filterNameWithEntitySuffix)
	{
		return " (in filter " + filterNameWithEntitySuffix + " for " + nameOfGraph + ")";
	}

	/**
	 * Get the correctly casted IR object.
	 *
	 * @return The IR object.
	 */
	public PatternGraphLhs getPatternGraph()
	{
		return checkIR(PatternGraphLhs.class);
	}

	/** NOTE: Use this only in DPO-Mode,i.e. if the pattern is part of a rule */
	public RuleDeclNode getRule()
	{
		for(BaseNode parent : getParents()) {
			if(parent instanceof RuleDeclNode) {
				return (RuleDeclNode)parent;
			}
		}
		assert false;
		return null;
	}

	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) {
			return getIR();
		}

		PatternGraphLhs patternGraph = new PatternGraphLhs(nameOfGraph, modifiers);
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

		for(PatternGraphLhsNode negativeNode : negs.getChildren()) {
			PatternGraphLhs negative = negativeNode.getPatternGraph();
			patternGraph.addNegGraph(negative);
			if(negative.isIterationBreaking()) {
				patternGraph.setIterationBreaking(true);
			}
		}

		for(PatternGraphLhsNode independentNode : idpts.getChildren()) {
			PatternGraphLhs independent = independentNode.getPatternGraph();
			patternGraph.addIdptGraph(independent);
			if(independent.isIterationBreaking()) {
				patternGraph.setIterationBreaking(true);
			}
		}

		for(ExprNode condition : conditions.getChildren()) {
			ExprNode conditionEvaluated = condition.evaluate(); // compile time evaluation (constant folding)
			warnIfConditionIsConstant(conditionEvaluated);
			patternGraph.addCondition(conditionEvaluated.checkIR(Expression.class));
		}

		for(EvalStatements yields : getYieldStatements()) {
			patternGraph.addYield(yields);
		}

		for(Node node : patternGraph.getNodes()) {
			PatternGraphBuilder.genTypeConditionsFromTypeof(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			PatternGraphBuilder.genTypeConditionsFromTypeof(patternGraph, edge);
		}

		for(Set<ConstraintDeclNode> homEntityNodes : getHoms()) {
			PatternGraphBuilder.addHoms(patternGraph, homEntityNodes);
		}

		for(TotallyHomNode totallyHomNode : totallyHoms.getChildren()) {
			PatternGraphBuilder.addTotallyHom(patternGraph, totallyHomNode);
		}

		for(Node node : patternGraph.getNodes()) {
			PatternGraphBuilder.ensureDefNodesAreHomToAllOthers(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			PatternGraphBuilder.ensureDefEdgesAreHomToAllOthers(patternGraph, edge);
		}

		for(Node node : patternGraph.getNodes()) {
			PatternGraphBuilder.ensureRetypedNodeHomToOldNode(patternGraph, node);
		}
		for(Edge edge : patternGraph.getEdges()) {
			PatternGraphBuilder.ensureRetypedEdgeHomToOldEdge(patternGraph, edge);
		}

		PatternGraphBuilder.addElementsHiddenInUsedConstructs(this, patternGraph);

		return patternGraph;
	}
	
	private static void warnIfConditionIsConstant(ExprNode expr)
	{
		if(expr instanceof BoolConstNode) {
			if(((Boolean)((BoolConstNode)expr).getValue()).booleanValue()) {
				expr.reportWarning("The if condition is always true.");
			} else {
				expr.reportWarning("The if condition is always false, thus the pattern will never match.");
			}
		}
	}
	
	public Collection<EvalStatements> getYieldStatements()
	{
		Collection<EvalStatements> ret = new LinkedList<EvalStatements>();

		for(EvalStatementsNode evalStatements : yields.getChildren()) {
			ret.add(evalStatements.checkIR(EvalStatements.class));
		}

		return ret;
	}
}
