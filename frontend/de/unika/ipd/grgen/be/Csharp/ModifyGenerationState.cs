/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Class holding the state needed for generating the rewrite part of an action.
/// @author Edgar Jakumeit, Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
using Model = de.unika.ipd.grgen.ir.model.Model;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;

public class ModifyGenerationState : ModifyGenerationStateConst
{
	public virtual string Name
	{
		get
		{
		return !string.ReferenceEquals(functionOrProcedureName, null) ? functionOrProcedureName : actionName;
		}
	}

	public virtual ICollection<Node> CommonNodes
	{
		get
		{
		return Collections.UnmodifiableSet(commonNodes);
		}
	}

	public virtual ICollection<Edge> CommonEdges
	{
		get
		{
		return Collections.UnmodifiableSet(commonEdges);
		}
	}

	public virtual ICollection<SubpatternUsage> CommonSubpatternUsages
	{
		get
		{
		return Collections.UnmodifiableSet(commonSubpatternUsages);
		}
	}

	public virtual ICollection<Node> NewNodes
	{
		get
		{
		return Collections.UnmodifiableSet(newNodes);
		}
	}

	public virtual ICollection<Edge> NewEdges
	{
		get
		{
		return Collections.UnmodifiableSet(newEdges);
		}
	}

	public virtual ICollection<SubpatternUsage> NewSubpatternUsages
	{
		get
		{
		return Collections.UnmodifiableSet(newSubpatternUsages);
		}
	}

	public virtual ICollection<Node> DelNodes
	{
		get
		{
		return Collections.UnmodifiableSet(delNodes);
		}
	}

	public virtual ICollection<Edge> DelEdges
	{
		get
		{
		return Collections.UnmodifiableSet(delEdges);
		}
	}

	public virtual bool IsDeleted(Entity entity)
	{
		if(entity is Node)
			return delNodes.Contains((Node)entity);
		else if(entity is Edge)
			return delEdges.Contains((Edge)entity);
		else
			return false;
	}

	public virtual ICollection<SubpatternUsage> DelSubpatternUsages
	{
		get
		{
		return Collections.UnmodifiableSet(delSubpatternUsages);
		}
	}

	public virtual ICollection<Node> YieldedNodes
	{
		get
		{
		return Collections.UnmodifiableSet(yieldedNodes);
		}
	}

	public virtual ICollection<Edge> YieldedEdges
	{
		get
		{
		return Collections.UnmodifiableSet(yieldedEdges);
		}
	}

	public virtual ICollection<Variable> YieldedVariables
	{
		get
		{
		return Collections.UnmodifiableSet(yieldedVariables);
		}
	}

	public virtual ICollection<Node> NewOrRetypedNodes
	{
		get
		{
		return Collections.UnmodifiableSet(newOrRetypedNodes);
		}
	}

	public virtual ICollection<Edge> NewOrRetypedEdges
	{
		get
		{
		return Collections.UnmodifiableSet(newOrRetypedEdges);
		}
	}

	public virtual ICollection<GraphEntity> AccessViaInterface
	{
		get
		{
		return Collections.UnmodifiableSet(accessViaInterface);
		}
	}

	public virtual IDictionary<GraphEntity, HashSet<Entity>> NeededAttributes
	{
		get
		{
		return Collections.UnmodifiableMap(neededAttributes);
		}
	}

	public virtual IDictionary<GraphEntity, HashSet<Entity>> AttributesStoredBeforeDelete
	{
		get
		{
		return Collections.UnmodifiableMap(attributesStoredBeforeDelete);
		}
	}

	public virtual ICollection<Variable> NeededVariables
	{
		get
		{
		return Collections.UnmodifiableSet(neededVariables);
		}
	}

	public virtual ICollection<Node> NodesNeededAsElements
	{
		get
		{
		return Collections.UnmodifiableSet(nodesNeededAsElements);
		}
	}

	public virtual ICollection<Edge> EdgesNeededAsElements
	{
		get
		{
		return Collections.UnmodifiableSet(edgesNeededAsElements);
		}
	}

	public virtual ICollection<Node> NodesNeededAsAttributes
	{
		get
		{
		return Collections.UnmodifiableSet(nodesNeededAsAttributes);
		}
	}

	public virtual ICollection<Edge> EdgesNeededAsAttributes
	{
		get
		{
		return Collections.UnmodifiableSet(edgesNeededAsAttributes);
		}
	}

	public virtual ICollection<Node> NodesNeededAsTypes
	{
		get
		{
		return Collections.UnmodifiableSet(nodesNeededAsTypes);
		}
	}

	public virtual ICollection<Edge> EdgesNeededAsTypes
	{
		get
		{
		return Collections.UnmodifiableSet(edgesNeededAsTypes);
		}
	}

	public virtual IDictionary<GraphEntity, HashSet<Entity>> ForceAttributeToVar
	{
		get
		{
		return Collections.UnmodifiableMap(forceAttributeToVar);
		}
	}

	public virtual string MatchClassName
	{
		get
		{
		return matchClassName;
		}
	}

	public virtual string PackagePrefix
	{
		get
		{
		return packagePrefix;
		}
	}

	public virtual IDictionary<Expression, string> MapExprToTempVar
	{
		get
		{
		return Collections.UnmodifiableMap(mapExprToTempVar);
		}
	}

	public virtual bool UseVarForResult()
	{
		return useVarForResult_;
	}

	public virtual bool SwitchToVarForResultAfterFirstVarUsage()
	{
		return switchToVarForResultAfterFirstVarUsage_;
	}

	public virtual void SwitchToVarForResult()
	{
		useVarForResult_ = true;
	}

	public virtual Model Model
	{
		get
		{
		return model;
		}
	}

	public virtual bool IsToBeParallelizedActionExisting()
	{
		return isToBeParallelizedActionExisting_;
	}

	public virtual bool EmitProfilingInstrumentation()
	{
		return emitProfiling_;
	}

	public virtual SourceBuilder PerElementMethodSourceBuilder
	{
		get
		{
		return perElementMethodSourceBuilder;
		}
	}

	// --------------------

	// if not null this is the generation state of a function or procedure (with all entries empty)
	public string functionOrProcedureName;
	// otherwise it is the generation state of the modify of an action
	public string actionName;

	public HashSet<Node> commonNodes = new LinkedHashSet<Node>();
	public HashSet<Edge> commonEdges = new LinkedHashSet<Edge>();
	public HashSet<SubpatternUsage> commonSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

	public HashSet<Node> newNodes = new LinkedHashSet<Node>();
	public HashSet<Edge> newEdges = new LinkedHashSet<Edge>();
	public HashSet<SubpatternUsage> newSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

	public HashSet<Node> delNodes = new LinkedHashSet<Node>();
	public HashSet<Edge> delEdges = new LinkedHashSet<Edge>();
	public HashSet<SubpatternUsage> delSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

	public HashSet<Node> yieldedNodes = new LinkedHashSet<Node>();
	public HashSet<Edge> yieldedEdges = new LinkedHashSet<Edge>();
	public HashSet<Variable> yieldedVariables = new LinkedHashSet<Variable>();

	public HashSet<Node> newOrRetypedNodes = new LinkedHashSet<Node>();
	public HashSet<Edge> newOrRetypedEdges = new LinkedHashSet<Edge>();
	public HashSet<GraphEntity> accessViaInterface = new LinkedHashSet<GraphEntity>();

	public Dictionary<GraphEntity, HashSet<Entity>> neededAttributes;
	public Dictionary<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

	public HashSet<Variable> neededVariables;

	public HashSet<Node> nodesNeededAsElements;
	public HashSet<Edge> edgesNeededAsElements;
	public HashSet<Node> nodesNeededAsAttributes;
	public HashSet<Edge> edgesNeededAsAttributes;

	public HashSet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
	public HashSet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

	public Dictionary<GraphEntity, HashSet<Entity>> forceAttributeToVar = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

	public Dictionary<Expression, string> mapExprToTempVar = new LinkedHashMap<Expression, string>();
	public bool useVarForResult_;
	public bool switchToVarForResultAfterFirstVarUsage_;

	private Model model;
	private string matchClassName;
	private string packagePrefix;
	private bool isToBeParallelizedActionExisting_;
	private bool emitProfiling_;

	private SourceBuilder perElementMethodSourceBuilder;


	public virtual void InitNeeds(NeededEntities needs)
	{
		neededAttributes = needs.attrEntityMap;
		nodesNeededAsElements = needs.nodes;
		edgesNeededAsElements = needs.edges;
		nodesNeededAsAttributes = needs.attrNodes;
		edgesNeededAsAttributes = needs.attrEdges;
		neededVariables = needs.variables;

		int i = 0;
		foreach(Expression expr in needs.containerExprs)
		{
			if(expr is MapInit || expr is SetInit
					|| expr is ArrayInit || expr is DequeInit)
				continue;
			mapExprToTempVar[expr] = "tempcontainervar_" + i;
			i++;
		}
	}

	public virtual void InitNeeds(HashSet<Expression> containerExprs)
	{
		int i = 0;
		foreach(Expression expr in containerExprs)
		{
			if(expr is MapInit || expr is SetInit
					|| expr is ArrayInit || expr is DequeInit)
				continue;
			mapExprToTempVar[expr] = "tempcontainervar_" + i;
			i++;
		}
	}

	public virtual void ClearContainerExprs()
	{
		mapExprToTempVar.Clear();
	}

	public ModifyGenerationState(Model model,
			string matchClassName, string packagePrefix,
			bool isToBeParallelizedActionExisting,
			bool emitProfiling)
	{
		this.model = model;
		this.matchClassName = matchClassName;
		this.packagePrefix = packagePrefix;
		this.isToBeParallelizedActionExisting_ = isToBeParallelizedActionExisting;
		this.emitProfiling_ = emitProfiling;
		this.perElementMethodSourceBuilder = new SourceBuilder();
	}
}

}
