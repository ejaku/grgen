/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// PatternGraphNode.java
/// 
/// @author Sebastian Hack, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using CollectBaseNode = de.unika.ipd.grgen.ast.CollectBaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using RuleDeclNode = de.unika.ipd.grgen.ast.decl.executable.RuleDeclNode;
using AlternativeCaseDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeCaseDeclNode;
using AlternativeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using BoolConstNode = de.unika.ipd.grgen.ast.expr.BoolConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using EvalStatementsNode = de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
using Coords = de.unika.ipd.grgen.parser.Coords;
using SymbolTable = de.unika.ipd.grgen.parser.SymbolTable;

/// <summary>
/// AST node that represents a graph pattern as it appears within the pattern part of some rule
/// </summary>
public class PatternGraphLhsNode : PatternGraphBaseNode
{
	static PatternGraphLhsNode()
	{
		SetClassName(typeof(PatternGraphLhsNode), "pattern graph lhs");
	}

	public const int MOD_DANGLING = 1; // dangling+identification=dpo
	public const int MOD_IDENTIFICATION = 2;
	public const int MOD_EXACT = 4;
	public const int MOD_INDUCED = 8;
	public const int MOD_PATTERN_LOCKED = 16;
	public const int MOD_PATTERNPATH_LOCKED = 32;

	/// <summary>
	/// The modifiers for this type. An ORed combination of the constants above. </summary>
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

	protected internal bool hasAbstractElements;

	// if this pattern graph is a negative or independent nested inside an iterated
	// it might break the iterated instead of only the current iterated case, if specified
	public bool iterationBreaking = false;

	private static PatternGraphLhsNode invalid;

	// invalid pattern node just needed for the isGlobalVariable checks, 
	// so that computations stuff that doesn't have a pattern graph is not classified as global 
	public static PatternGraphLhsNode Invalid
	{
		get
		{
			if(invalid == null)
				invalid = new PatternGraphLhsNode("invalid", Coords.Invalid,
						null, null,
						null, null,
						null, null,
						null, null,
						null,
						null,
						null, null,
						null, null,
						0, BaseNode.CONTEXT_COMPUTATION);
			return invalid;
		}
	}

	public PatternGraphLhsNode(string nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> @params,
			CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
			CollectNode<AlternativeDeclNode> alts, CollectNode<IteratedDeclNode> iters,
			CollectNode<PatternGraphLhsNode> negs, CollectNode<PatternGraphLhsNode> idpts,
			CollectNode<ExprNode> conditions,
			CollectNode<ExprNode> returns,
			CollectNode<HomNode> homs, CollectNode<TotallyHomNode> totallyHoms,
			CollectNode<ExactNode> exacts, CollectNode<InducedNode> induceds,
			int modifiers, int context)
		: base(nameOfGraph, coords, connections, @params, subpatterns,
				returns, context)
	{
		this.alts = alts;
		BecomeParent(this.alts);
		this.iters = iters;
		BecomeParent(this.iters);
		this.negs = negs;
		BecomeParent(this.negs);
		this.idpts = idpts;
		BecomeParent(this.idpts);
		this.conditions = conditions;
		BecomeParent(this.conditions);
		this.homs = homs;
		BecomeParent(this.homs);
		this.totallyHoms = totallyHoms;
		BecomeParent(this.totallyHoms);
		this.exacts = exacts;
		BecomeParent(this.exacts);
		this.induceds = induceds;
		BecomeParent(this.induceds);
		this.modifiers = modifiers;

		this.directlyNestingLHSGraph = this;
		if(@params != null)
			AddParamsToConnections(@params); // treat non-var parameters like connections
	}

	public virtual void AddYieldings(CollectNode<EvalStatementsNode> yields)
	{
		this.yields = yields;
		BecomeParent(this.yields);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersionCollectNode(connectionsUnresolved, connections));
			children.Add(@params);
			children.Add(defVariablesToBeYieldedTo);
			children.Add(subpatterns);
			children.Add(alts);
			children.Add(iters);
			children.Add(negs);
			children.Add(idpts);
			children.Add(returns);
			children.Add(yields);
			children.Add(conditions);
			children.Add(homs);
			children.Add(totallyHoms);
			children.Add(exacts);
			children.Add(induceds);
			return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("connections");
			childrenNames.Add("params");
			childrenNames.Add("defVariablesToBeYieldedTo");
			childrenNames.Add("subpatterns");
			childrenNames.Add("alternatives");
			childrenNames.Add("iters");
			childrenNames.Add("negatives");
			childrenNames.Add("independents");
			childrenNames.Add("return");
			childrenNames.Add("yields");
			childrenNames.Add("conditions");
			childrenNames.Add("homs");
			childrenNames.Add("totallyHoms");
			childrenNames.Add("exacts");
			childrenNames.Add("induceds");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool result = base.ResolveLocal();

		DetermineExistenceOfAbstractElements();

		return result;
	}

	internal virtual void DetermineExistenceOfAbstractElements()
	{
		foreach(ConnectionCharacter cc in connections.ChildrenExact)
		{
			if(cc is ConnectionNode)
			{
				ConnectionNode conn = (ConnectionNode)cc;
				if(conn.Edge.DeclInhType.IsAbstract()
						|| conn.Src.DeclInhType.IsAbstract()
						|| conn.Tgt.DeclInhType.IsAbstract())
					hasAbstractElements = true;
			}
			else if(cc is SingleNodeConnNode)
			{
				SingleNodeConnNode conn = (SingleNodeConnNode)cc;
				if(conn.Node.DeclInhType.IsAbstract())
					hasAbstractElements = true;
			}
		}
	}

	protected internal override ISet<NodeDeclNode> NodesImpl
	{
		get
		{
			Debug.Assert(IsResolved());

			LinkedHashSet<NodeDeclNode> tempNodes = new LinkedHashSet<NodeDeclNode>();

			foreach(ConnectionCharacter connection in connections.ChildrenExact)
				connection.AddNodes(tempNodes);

			foreach(HomNode hom in homs.ChildrenExact)
			{
				foreach(NodeDeclNode homNode in hom.HomNodes)
					tempNodes.Add(homNode);
			}

			return tempNodes;
		}
	}

	protected internal override ISet<EdgeDeclNode> EdgesImpl
	{
		get
		{
			Debug.Assert(IsResolved());

			LinkedHashSet<EdgeDeclNode> tempEdges = new LinkedHashSet<EdgeDeclNode>();

			foreach(ConnectionCharacter connection in connections.ChildrenExact)
				connection.AddEdge(tempEdges);

			foreach(HomNode hom in homs.ChildrenExact)
			{
				foreach(EdgeDeclNode homEdge in hom.HomEdges)
					tempEdges.Add(homEdge);
			}

			return tempEdges;
		}
	}

	public virtual NodeDeclNode TryGetNode(string name)
	{
		foreach(NodeDeclNode node in Nodes)
		{
			if(node.ident.ToString().Equals(name))
				return node;
		}
		return null;
	}

	public virtual EdgeDeclNode TryGetEdge(string name)
	{
		foreach(EdgeDeclNode edge in Edges)
		{
			if(edge.ident.ToString().Equals(name))
				return edge;
		}
		return null;
	}

	public virtual VarDeclNode TryGetVar(string name)
	{
		foreach(VarDeclNode var in defVariablesToBeYieldedTo.ChildrenExact)
		{
			if(var.ident.ToString().Equals(name))
				return var;
		}
		foreach(DeclNode varCand in ParamDecls)
		{
			if(!(varCand is VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			if(var.ident.ToString().Equals(name))
				return var;
		}
		return null;
	}

	public virtual DeclNode TryGetMember(string name)
	{
		NodeDeclNode node = TryGetNode(name);
		if(node != null)
			return node;
		EdgeDeclNode edge = TryGetEdge(name);
		if(edge != null)
			return edge;
		return TryGetVar(name);
	}

	public virtual PatternGraphLhsNode ParentPatternGraph
	{
		get
		{
			foreach(BaseNode parent in Parents)
			{
				if(!(parent is CollectBaseNode))
					continue;

				foreach(BaseNode grandParent in parent.Parents)
				{
					if(grandParent is PatternGraphLhsNode)
						return (PatternGraphLhsNode)grandParent;
				}
			}

			return null;
		}
	}

	public virtual bool IsInduced()
	{
		return (modifiers & MOD_INDUCED) != 0;
	}

	public virtual bool IsDangling()
	{
		return (modifiers & MOD_DANGLING) != 0;
	}

	public virtual bool IsIdentification()
	{
		return (modifiers & MOD_IDENTIFICATION) != 0;
	}

	public virtual bool IsExact()
	{
		return (modifiers & MOD_EXACT) != 0;
	}

	public virtual NodeDeclNode GetAnonymousDummyNode(TypeDeclNode nodeRoot, int context)
	{
		IdentNode nodeName = new IdentNode(
				Scope.DefineAnonymous("dummy_node", SymbolTable.Invalid, Coords.Builtin));
		NodeDeclNode dummyNode = NodeDeclNode.GetDummy(nodeName, nodeRoot, context, this);
		return dummyNode;
	}

	public virtual EdgeDeclNode GetAnonymousEdgeDecl(TypeDeclNode edgeRoot, int context)
	{
		IdentNode edgeName = new IdentNode(
				Scope.DefineAnonymous("edge", SymbolTable.Invalid, Coords.Builtin));
		EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot, context, this, this);
		return edge;
	}

	public virtual ICollection<ISet<ConstraintDeclNode>> Homs
	{
		get
		{
			if(homStorage == null)
				homStorage = new HomStorage(this);
			return homStorage.Homs;
		}
	}

	/// <summary>
	/// Return the correspondent homomorphic set. </summary>
	public virtual ISet<NodeDeclNode> GetHomomorphic(NodeDeclNode node)
	{
		if(homStorage == null)
			homStorage = new HomStorage(this);
		return homStorage.GetHomomorphic(node);
	}

	/// <summary>
	/// Return the correspondent homomorphic set. </summary>
	public virtual ISet<EdgeDeclNode> GetHomomorphic(EdgeDeclNode edge)
	{
		if(homStorage == null)
			homStorage = new HomStorage(this);
		return homStorage.GetHomomorphic(edge);
	}

	/// <summary>
	/// Warn if two homomorphic elements can never be matched homomorphic,
	/// because they have incompatible types.
	/// </summary>
	private void WarnOnSuperfluousHoms()
	{
		ICollection<ISet<ConstraintDeclNode>> homSets = Homs;
		foreach(ISet<ConstraintDeclNode> homSet in homSets)
			WarnOnSuperfluousHoms(homSet);
	}

	private void WarnOnSuperfluousHoms(ISet<ConstraintDeclNode> homSet)
	{
		ISet<ConstraintDeclNode> alreadyProcessed = new LinkedHashSet<ConstraintDeclNode>();

		foreach(ConstraintDeclNode elem1 in homSet)
		{
			InheritanceTypeNode type1 = elem1.DeclInhType;
			foreach(ConstraintDeclNode elem2 in homSet)
			{
				if(elem1 == elem2 || alreadyProcessed.Contains(elem2))
					continue;

				InheritanceTypeNode type2 = elem2.DeclInhType;

				if(InheritanceTypeNode.HasCommonSubtype(type1, type2))
					continue;

				// search hom statement
				HomNode hom = null;
				foreach(HomNode homNode in homs.ChildrenExact)
				{
					ICollection<BaseNode> homChildren = homNode.Children;
					if(homChildren.Contains(elem1) && homChildren.Contains(elem2))
					{
						hom = homNode;
						break;
					}
				}

				if(hom != null)
					hom.ReportWarning("The " + elem1.Kind + " " + elem1.ident + " and the " + elem2.Kind + " " + elem2.ident
							+ " have no common subtype and thus can never match the same element.");
			}

			alreadyProcessed.Add(elem1);
		}
	}

	internal virtual bool NoRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent()
	{
		bool result = true;
		foreach(PatternGraphLhsNode pattern in negs.ChildrenExact)
		{
			foreach(IteratedDeclNode iter in pattern.iters.ChildrenExact)
			{
				if(iter.right != null)
				{
					iter.right.ReportError("An iterated contained within a negative cannot possess a rewrite part"
							+ " (the negative is a pure (negative) application condition).");
					result = false;
				}
			}
			foreach(AlternativeDeclNode alt in pattern.alts.ChildrenExact)
			{
				foreach(AlternativeCaseDeclNode altCase in alt.ChildrenExact)
				{
					if(altCase.right != null)
					{
						altCase.right.ReportError("An alternative case contained within a negative cannot possess a rewrite part"
								+ " (the negative is a pure (negative) application condition).");
						result = false;
					}
				}
			}
		}
		foreach(PatternGraphLhsNode pattern in idpts.ChildrenExact)
		{
			foreach(IteratedDeclNode iter in pattern.iters.ChildrenExact)
			{
				if(iter.right != null)
				{
					iter.right.ReportError("An iterated contained within an independent cannot possess a rewrite part"
								+ " (the independent is a pure (positive) application condition).");
					result = false;
				}
			}
			foreach(AlternativeDeclNode alt in pattern.alts.ChildrenExact)
			{
				foreach(AlternativeCaseDeclNode altCase in alt.ChildrenExact)
				{
					if(altCase.right != null)
					{
						altCase.right.ReportError("An alternative case contained within an independent cannot possess a rewrite part"
								+ " (the independent is a pure (positive) application condition).");
						result = false;
					}
				}
			}
		}
		return result;
	}

	internal virtual bool NoExecStatementInEvalsOfIteratedOrAlternative()
	{
		bool result = true;
		foreach(IteratedDeclNode iter in iters.ChildrenExact)
		{
			if(iter.right != null)
			{
				foreach(EvalStatementsNode evalStmts in iter.right.RhsGraph.evals.ChildrenExact)
					evalStmts.NoExecStatement();
			}
		}
		foreach(AlternativeDeclNode alt in alts.ChildrenExact)
		{
			foreach(AlternativeCaseDeclNode altCase in alt.ChildrenExact)
			{
				if(altCase.right != null)
				{
					foreach(EvalStatementsNode evalStmts in altCase.right.RhsGraph.evals.ChildrenExact)
						evalStmts.NoExecStatement();
				}
			}
		}
		return result;
	}

	protected internal override bool CheckLocal()
	{
		bool expr = true;

		foreach(ExprNode exp in conditions.ChildrenExact)
		{
			if(!exp.Type.IsEqual(BasicTypeNode.booleanType))
			{
				exp.ReportError("An expression in an if condition must be of type boolean (but is of type " + exp.Type.TypeName + ").");
				expr = false;
			}
		}

		bool noReturnInNegOrIdpt = true;
		if((context & CONTEXT_NEGATIVE) == CONTEXT_NEGATIVE)
		{
			if(returns.Size() != 0)
			{
				ReportError("A return is not allowed in a negative block.");
				noReturnInNegOrIdpt = false;
			}
		}
		if((context & CONTEXT_INDEPENDENT) == CONTEXT_INDEPENDENT)
		{
			if(returns.Size() != 0)
			{
				ReportError("A return is not allowed in an independent block.");
				noReturnInNegOrIdpt = false;
			}
		}

		WarnOnSuperfluousHoms();

		return IsEdgeReuseOk() & expr & noReturnInNegOrIdpt
				& NoRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent()
				& NoDefElementOrIteratedReferenceInCondition()
				& NoIteratedReferenceInDefElementInitialization()
				& IteratedNameIsNotAccessedInNestedPattern()
				& NoExecStatementInEvalsOfIteratedOrAlternative();
	}

	private bool NoDefElementOrIteratedReferenceInCondition()
	{
		bool res = true;
		foreach(ExprNode cond in conditions.ChildrenExact)
		{
			res &= cond.NoDefElement("if condition");
			res &= cond.NoIteratedReference("if condition");
		}
		return res;
	}

	private bool NoIteratedReferenceInDefElementInitialization()
	{
		bool res = true;
		foreach(VarDeclNode var in defVariablesToBeYieldedTo.ChildrenExact)
		{
			if(var.initialization != null)
				res &= var.initialization.NoIteratedReference("def variable initialization");
		}
		return res;
	}

	private bool IteratedNameIsNotAccessedInNestedPattern()
	{
		bool res = true;
		foreach(IteratedDeclNode iterForNameToCheck in iters.ChildrenExact)
		{
			string iterName = iterForNameToCheck.Ident.ToString();
			foreach(IteratedDeclNode iter in iters.ChildrenExact)
			{
				res &= iter.pattern.IteratedNotReferenced(iterName);
				if(iter.right != null)
				{
					res &= iter.right.patternGraph.IteratedNotReferenced(iterName);
					res &= iter.right.patternGraph.IteratedNotReferencedInDefElementInitialization(iterName);
				}
			}
			foreach(AlternativeDeclNode alt in alts.ChildrenExact)
			{
				foreach(AlternativeCaseDeclNode altCase in alt.ChildrenExact)
				{
					res &= altCase.pattern.IteratedNotReferenced(iterName);
					if(altCase.right != null)
					{
						res &= altCase.right.patternGraph.IteratedNotReferenced(iterName);
						res &= altCase.right.patternGraph.IteratedNotReferencedInDefElementInitialization(iterName);
					}
				}
			}
			foreach(PatternGraphLhsNode idpt in idpts.ChildrenExact)
				res &= idpt.IteratedNotReferenced(iterName);
		}
		return res;
	}

	protected internal virtual bool IteratedNotReferenced(string iterName)
	{
		bool res = true;
		foreach(EvalStatementsNode yieldStatements in yields.ChildrenExact)
		{
			foreach(EvalStatementNode yieldStatement in yieldStatements.ChildrenExact)
				res &= yieldStatement.IteratedNotReferenced(iterName);
		}
		return res;
	}

	public virtual bool CheckFilterVariable(IdentNode errorTarget, string filterNameWithEntitySuffix, string filterVariable)
	{
		VarDeclNode variable = TryGetVar(filterVariable);
		if(variable == null)
		{
			errorTarget.ReportError("The variable " + filterVariable + " is not known"
					+ FilterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		TypeNode filterVariableType = variable.DeclType;
		if(!filterVariableType.IsOrderableType())
		{
			errorTarget.ReportError("The variable " + filterVariable + " must be of one of the following types: " + TypeNode.OrderableTypesAsString
				+ " (but is of type " + filterVariableType.TypeName + ")"
				+ FilterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		return true;
	}

	public virtual bool CheckFilterEntity(IdentNode errorTarget, string filterNameWithEntitySuffix, string filterEntity)
	{
		DeclNode entity = TryGetNode(filterEntity);
		if(entity == null)
			entity = TryGetEdge(filterEntity);
		if(entity == null)
			entity = TryGetVar(filterEntity);
		if(entity == null)
		{
			errorTarget.ReportError("The entity " + filterEntity + " is not known"
					+ FilterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		TypeNode filterVariableType = entity.DeclType;
		if(!filterVariableType.IsFilterableType())
		{
			errorTarget.ReportError("The entity " + filterEntity + " must be of one of the following types: " + TypeNode.FilterableTypesAsString
					+ " (but is of type " + filterVariableType.TypeName + ")"
					+ FilterSpecification(filterNameWithEntitySuffix) + ".");
			return false;
		}
		return true;
	}

	private string FilterSpecification(string filterNameWithEntitySuffix)
	{
		return " (in filter " + filterNameWithEntitySuffix + " for " + nameOfGraph + ")";
	}

	/// <summary>
	/// Get the correctly casted IR object.
	/// </summary>
	/// <returns> The IR object. </returns>
	public virtual PatternGraphLhs IRPatternGraphLhs
	{
		get
		{
			return CheckIR(typeof(PatternGraphLhs));
		}
	}

	/// <summary>
	/// NOTE: Use this only in DPO-Mode,i.e. if the pattern is part of a rule </summary>
	public virtual RuleDeclNode Rule
	{
		get
		{
			foreach(BaseNode parent in Parents)
			{
				if(parent is RuleDeclNode)
					return (RuleDeclNode)parent;
			}
			Debug.Assert(false);
			return null;
		}
	}

	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet())
			return IR;

		PatternGraphLhs patternGraph = new PatternGraphLhs(nameOfGraph, modifiers);
		patternGraph.DirectlyNestingLHSGraph = patternGraph;

		// mark this node as already visited
		IR = patternGraph;

		if(this == Invalid)
			return patternGraph;

		patternGraph.IterationBreaking = iterationBreaking;

		foreach(ConnectionCharacter connection in connections.ChildrenExact)
			connection.AddToGraph(patternGraph);

		foreach(VarDeclNode varNode in defVariablesToBeYieldedTo.ChildrenExact)
			patternGraph.AddVariable(varNode.CheckIR(typeof(Variable)));

		foreach(BaseNode subpatternUsage in subpatterns.ChildrenExact)
			patternGraph.AddSubpatternUsage(subpatternUsage.CheckIR(typeof(SubpatternUsage)));

		foreach(AlternativeDeclNode alternativeNode in alts.ChildrenExact)
			patternGraph.AddAlternative(alternativeNode.CheckIR(typeof(Alternative)));

		foreach(IteratedDeclNode iteratedNode in iters.ChildrenExact)
			patternGraph.AddIterated(iteratedNode.CheckIR(typeof(Rule)));

		foreach(PatternGraphLhsNode negativeNode in negs.ChildrenExact)
		{
			PatternGraphLhs negative = negativeNode.IRPatternGraphLhs;
			patternGraph.AddNegGraph(negative);
			if(negative.IsIterationBreaking())
				patternGraph.IterationBreaking = true;
		}

		foreach(PatternGraphLhsNode independentNode in idpts.ChildrenExact)
		{
			PatternGraphLhs independent = independentNode.IRPatternGraphLhs;
			patternGraph.AddIdptGraph(independent);
			if(independent.IsIterationBreaking())
				patternGraph.IterationBreaking = true;
		}

		foreach(ExprNode condition in conditions.ChildrenExact)
		{
			ExprNode conditionEvaluated = condition.Evaluate(); // compile time evaluation (constant folding)
			WarnIfConditionIsConstant(conditionEvaluated);
			patternGraph.AddCondition(conditionEvaluated.CheckIR(typeof(Expression)));
		}

		foreach(EvalStatements yields in YieldStatements)
			patternGraph.AddYield(yields);

		foreach(Node node in patternGraph.Nodes)
			PatternGraphBuilder.GenTypeConditionsFromTypeof(patternGraph, node);
		foreach(Edge edge in patternGraph.Edges)
			PatternGraphBuilder.GenTypeConditionsFromTypeof(patternGraph, edge);

		foreach(ISet<ConstraintDeclNode> homEntityNodes in Homs)
			PatternGraphBuilder.AddHoms(patternGraph, homEntityNodes);

		foreach(TotallyHomNode totallyHomNode in totallyHoms.ChildrenExact)
			PatternGraphBuilder.AddTotallyHom(patternGraph, totallyHomNode);

		foreach(Node node in patternGraph.Nodes)
			PatternGraphBuilder.EnsureDefNodesAreHomToAllOthers(patternGraph, node);
		foreach(Edge edge in patternGraph.Edges)
			PatternGraphBuilder.EnsureDefEdgesAreHomToAllOthers(patternGraph, edge);

		foreach(Node node in patternGraph.Nodes)
			PatternGraphBuilder.EnsureRetypedNodeHomToOldNode(patternGraph, node);
		foreach(Edge edge in patternGraph.Edges)
			PatternGraphBuilder.EnsureRetypedEdgeHomToOldEdge(patternGraph, edge);

		PatternGraphBuilder.AddElementsHiddenInUsedConstructs(this, patternGraph);

		return patternGraph;
	}

	private static void WarnIfConditionIsConstant(ExprNode expr)
	{
		if(expr is BoolConstNode)
		{
			if(((bool?)((BoolConstNode)expr).GetValue()).Value)
				expr.ReportWarning("The if condition is always true.");
			else
				expr.ReportWarning("The if condition is always false, thus the pattern will never match.");
		}
	}

	public virtual ICollection<EvalStatements> YieldStatements
	{
		get
		{
			ICollection<EvalStatements> ret = new List<EvalStatements>();

			foreach(EvalStatementsNode evalStatements in yields.ChildrenExact)
				ret.Add(evalStatements.CheckIR(typeof(EvalStatements)));

			return ret;
		}
	}
}

}
