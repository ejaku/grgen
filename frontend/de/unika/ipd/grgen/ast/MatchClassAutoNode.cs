/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast
{

using System;
using System.Collections.Generic;

using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using AlternativeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
using DummyNodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.DummyNodeDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using ConnectionCharacter = de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
using ExactNode = de.unika.ipd.grgen.ast.pattern.ExactNode;
using HomNode = de.unika.ipd.grgen.ast.pattern.HomNode;
using InducedNode = de.unika.ipd.grgen.ast.pattern.InducedNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using SingleNodeConnNode = de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
using SubpatternReplNode = de.unika.ipd.grgen.ast.pattern.SubpatternReplNode;
using TotallyHomNode = de.unika.ipd.grgen.ast.pattern.TotallyHomNode;
using EvalStatementsNode = de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;


/// <summary>
/// AST node class representing auto-generated match classes / match class bodies
/// (combining matches under name merging, to be used as results of "natural joins" of matches)
/// </summary>
public class MatchClassAutoNode : BaseNode
{
	static MatchClassAutoNode()
	{
		SetClassName(typeof(MatchClassAutoNode), "match class auto");
	}

	protected internal string nameOfGraph;
	protected internal Coords coords;
	protected internal int modifiers;
	protected internal int context;

	protected internal CollectNode<IdentNode> matchTypesUnresolved;
	protected internal CollectNode<MatchTypeActionNode> matchTypes;

	internal CollectNode<BaseNode> connections;
	internal CollectNode<BaseNode> @params;

	public MatchClassAutoNode(string nameOfGraph, Coords coords, int modifiers, int context,
			CollectNode<IdentNode> matchTypes)
		: base(coords)
	{
		this.nameOfGraph = nameOfGraph;
		this.modifiers = modifiers;
		this.context = context;
		this.matchTypesUnresolved = BecomeParent(matchTypes);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersionCollectNode(matchTypesUnresolved, matchTypes));
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
			childrenNames.Add("matchTypes");
			return childrenNames;
		}
	}

	private static readonly CollectResolver<MatchTypeActionNode> matchTypesResolver =
			new CollectResolver<MatchTypeActionNode>(new DeclarationTypeResolver<MatchTypeActionNode>(typeof(MatchTypeActionNode)));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		foreach(IdentNode mtid in matchTypesUnresolved.ChildrenExact)
		{
			if(!(mtid is PackageIdentNode))
				FixupDefinition(mtid, mtid.Scope);
		}
		matchTypes = matchTypesResolver.Resolve(matchTypesUnresolved, this);

		return matchTypes != null;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(matchTypes.ChildrenExact.Count != 2)
		{
			ReportError("The auto(match<T> | match<S>) construct is only supported on two types (given are " + matchTypes.ChildrenExact.Count + ").");
			return false;
		}

		return true;
	}

	public virtual PatternGraphLhsNode PatternGraph
	{
		get
		{
			connections = new CollectNode<BaseNode>();
			@params = new CollectNode<BaseNode>();

			CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
			CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
			CollectNode<AlternativeDeclNode> alts = new CollectNode<AlternativeDeclNode>();
			CollectNode<IteratedDeclNode> iters = new CollectNode<IteratedDeclNode>();
			CollectNode<PatternGraphLhsNode> negs = new CollectNode<PatternGraphLhsNode>();
			CollectNode<PatternGraphLhsNode> idpts = new CollectNode<PatternGraphLhsNode>();
			CollectNode<ExprNode> conds = new CollectNode<ExprNode>();
			CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
			CollectNode<HomNode> homs = new CollectNode<HomNode>();
			CollectNode<TotallyHomNode> totallyhoms = new CollectNode<TotallyHomNode>();
			CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
			CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
			PatternGraphLhsNode res = new PatternGraphLhsNode(nameOfGraph, coords,
					connections, @params, subpatterns, subpatternRepls,
					alts, iters, negs, idpts, conds,
					returnz, homs, totallyhoms, exact, induced, modifiers, context);

			return res;
		}
	}

	public virtual bool FillPatternGraph(PatternGraphLhsNode patternGraph)
	{
		bool result = true;

		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();

		IDictionary<string, TypeNode> entitiesToTypes = new Dictionary<string, TypeNode>();

		foreach(MatchTypeActionNode matchType in matchTypes.ChildrenExact)
		{
			PatternGraphLhsNode lhsPattern = matchType.Action.pattern;
			foreach(ConnectionCharacter cc in lhsPattern.Connections)
			{
				if(cc is ConnectionNode)
				{
					ConnectionNode connection = (ConnectionNode)cc;
					EdgeDeclNode edge = connection.Edge;
					string edgeName = edge.Ident.Symbol.Text;
					if(edgeName.StartsWith("$", StringComparison.Ordinal))
					{
						result &= AddSourceAndTargetIfNotYetAddedOrTypeCheckIfDuplicate(connection,
							entitiesToTypes, connections, patternGraph);
					}
					else
					{
						if(!entitiesToTypes.ContainsKey(edgeName))
						{
							ConnectionNode connectionClone = connection.CloneForAuto(patternGraph);
							connections.AddChild(connectionClone);
							entitiesToTypes[edgeName] = edge.DeclType;
							result &= ReplaceSourceAndTargetIfAlreadyAdded(connection,
									connectionClone, entitiesToTypes, patternGraph);
						}
						else
						{
							result &= AddSourceAndTargetIfNotYetAddedOrTypeCheckIfDuplicate(connection,
									entitiesToTypes, connections, patternGraph);
							result &= IsTypeMatching(edge, entitiesToTypes);
						}
					}
				}
				else
				{
					SingleNodeConnNode singleNode = (SingleNodeConnNode)cc;
					NodeDeclNode node = singleNode.Node;
					result &= AddIfNotYetAddedOrTypeCheckIfDuplicate(node, entitiesToTypes,
							connections, () => singleNode.CloneForAuto(patternGraph));
				}
			}

			foreach(VarDeclNode defVar in lhsPattern.DefVariablesToBeYieldedTo.ChildrenExact)
			{
				result &= AddIfNotYetAddedOrTypeCheckIfDuplicate(defVar, entitiesToTypes,
						@params, () => defVar.CloneForAuto(patternGraph));
			}

			foreach(BaseNode param in lhsPattern.@params.ChildrenExact)
			{
				if(param is VarDeclNode)
				{
					VarDeclNode var = (VarDeclNode)param;
					result &= AddIfNotYetAddedOrTypeCheckIfDuplicate(var, entitiesToTypes,
							@params, () => var.CloneForAuto(patternGraph));
				}
			}
		}

		patternGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
		patternGraph.AddYieldings(evals);

		return result;
	}

	private bool AddSourceAndTargetIfNotYetAddedOrTypeCheckIfDuplicate(ConnectionNode connection,
			IDictionary<string, TypeNode> entitiesToTypes, CollectNode<BaseNode> connections,
			PatternGraphLhsNode patternGraph)
	{
		bool result = true;
		NodeDeclNode source = connection.Src;
		if(!(source is DummyNodeDeclNode))
		{
			result &= AddIfNotYetAddedOrTypeCheckIfDuplicate(source, entitiesToTypes,
					connections, () => new SingleNodeConnNode(source.CloneForAuto(patternGraph)));
		}
		NodeDeclNode target = connection.Tgt;
		if(!(target is DummyNodeDeclNode))
		{
			result &= AddIfNotYetAddedOrTypeCheckIfDuplicate(target, entitiesToTypes,
					connections, () => new SingleNodeConnNode(target.CloneForAuto(patternGraph)));
		}
		return result;
	}

	private bool ReplaceSourceAndTargetIfAlreadyAdded(ConnectionNode connection,
			ConnectionNode connectionClone, IDictionary<string, TypeNode> entitiesToTypes,
			PatternGraphLhsNode patternGraph)
	{
		NodeDeclNode source = connection.Src;
		string sourceName = source.Ident.Symbol.Text;
		if(entitiesToTypes.ContainsKey(sourceName))
		{
			NodeDeclNode newSource = new DummyNodeDeclNode(source.Ident,
					source.DeclType, source.context, patternGraph);
			connectionClone.Src = newSource;
		}
		NodeDeclNode target = connection.Tgt;
		string targetName = target.Ident.Symbol.Text;
		if(entitiesToTypes.ContainsKey(targetName))
		{
			NodeDeclNode newTarget = new DummyNodeDeclNode(target.Ident,
					target.DeclType, target.context, patternGraph);
			connectionClone.Tgt = newTarget;
		}
		return IsTypeMatching(source, entitiesToTypes) & IsTypeMatching(target, entitiesToTypes);
	}

	private bool AddIfNotYetAddedOrTypeCheckIfDuplicate(DeclNode entity, IDictionary<string, TypeNode> entitiesToTypes,
			CollectNode<BaseNode> connections, Func<BaseNode> entityForAuto)
	{
		string nodeName = entity.Ident.Symbol.Text;
		if(nodeName.StartsWith("$", StringComparison.Ordinal))
			return true;
		if(!entitiesToTypes.ContainsKey(nodeName))
		{
			connections.AddChild(entityForAuto());
			entitiesToTypes[nodeName] = entity.DeclType;
			return true;
		}
		else
			return IsTypeMatching(entity, entitiesToTypes);
	}

	private bool IsTypeMatching(DeclNode decl, IDictionary<string, TypeNode> entitiesToTypes)
	{
		string entity = decl.Ident.Symbol.Text;
		TypeNode type = entitiesToTypes[entity];
		if(!decl.DeclType.IsEqual(type))
		{
			ReportError("Ambiguous resulting type: the entity " + entity
					+ " is declared with type " + type.ToStringWithDeclarationCoords()
					+ " and with type + " + decl.DeclType.ToStringWithDeclarationCoords() + ".");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		throw new Exception("Not implemented");
	}
}

}
