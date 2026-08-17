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
				return commonNodes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> CommonEdges
		{
			get
			{
				return commonEdges; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<SubpatternUsage> CommonSubpatternUsages
		{
			get
			{
				return commonSubpatternUsages; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> NewNodes
		{
			get
			{
				return newNodes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> NewEdges
		{
			get
			{
				return newEdges; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<SubpatternUsage> NewSubpatternUsages
		{
			get
			{
				return newSubpatternUsages; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> DelNodes
		{
			get
			{
				return delNodes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> DelEdges
		{
			get
			{
				return delEdges; // TODO: Collections.UnmodifiableSet
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
				return delSubpatternUsages; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> YieldedNodes
		{
			get
			{
				return yieldedNodes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> YieldedEdges
		{
			get
			{
				return yieldedEdges; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Variable> YieldedVariables
		{
			get
			{
				return yieldedVariables; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> NewOrRetypedNodes
		{
			get
			{
				return newOrRetypedNodes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> NewOrRetypedEdges
		{
			get
			{
				return newOrRetypedEdges; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<GraphEntity> AccessViaInterface
		{
			get
			{
				return accessViaInterface; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual IDictionary<GraphEntity, ISet<Entity>> NeededAttributes
		{
			get
			{
				return neededAttributes; // TODO: Collections.UnmodifiableMap => IReadOnlyDictionary
			}
		}

		public virtual IDictionary<GraphEntity, ISet<Entity>> AttributesStoredBeforeDelete
		{
			get
			{
				return attributesStoredBeforeDelete; // TODO: Collections.UnmodifiableMap => IReadOnlyDictionary
			}
		}

		public virtual ICollection<Variable> NeededVariables
		{
			get
			{
				return neededVariables; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> NodesNeededAsElements
		{
			get
			{
				return nodesNeededAsElements; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> EdgesNeededAsElements
		{
			get
			{
				return edgesNeededAsElements; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> NodesNeededAsAttributes
		{
			get
			{
				return nodesNeededAsAttributes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> EdgesNeededAsAttributes
		{
			get
			{
				return edgesNeededAsAttributes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Node> NodesNeededAsTypes
		{
			get
			{
				return nodesNeededAsTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Edge> EdgesNeededAsTypes
		{
			get
			{
				return edgesNeededAsTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual IDictionary<GraphEntity, ISet<Entity>> ForceAttributeToVar
		{
			get
			{
				return forceAttributeToVar; // TODO: Collections.UnmodifiableMap => IReadOnlyDictionary
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
				return mapExprToTempVar; // TODO: Collections.UnmodifiableMap => IReadOnlyDictionary
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

		public ISet<Node> commonNodes = new LinkedHashSet<Node>();
		public ISet<Edge> commonEdges = new LinkedHashSet<Edge>();
		public ISet<SubpatternUsage> commonSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public ISet<Node> newNodes = new LinkedHashSet<Node>();
		public ISet<Edge> newEdges = new LinkedHashSet<Edge>();
		public ISet<SubpatternUsage> newSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public ISet<Node> delNodes = new LinkedHashSet<Node>();
		public ISet<Edge> delEdges = new LinkedHashSet<Edge>();
		public ISet<SubpatternUsage> delSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public ISet<Node> yieldedNodes = new LinkedHashSet<Node>();
		public ISet<Edge> yieldedEdges = new LinkedHashSet<Edge>();
		public ISet<Variable> yieldedVariables = new LinkedHashSet<Variable>();

		public ISet<Node> newOrRetypedNodes = new LinkedHashSet<Node>();
		public ISet<Edge> newOrRetypedEdges = new LinkedHashSet<Edge>();
		public ISet<GraphEntity> accessViaInterface = new LinkedHashSet<GraphEntity>();

		public Dictionary<GraphEntity, ISet<Entity>> neededAttributes;
		public Dictionary<GraphEntity, ISet<Entity>> attributesStoredBeforeDelete = new LinkedHashMap<GraphEntity, ISet<Entity>>();

		public ISet<Variable> neededVariables;

		public ISet<Node> nodesNeededAsElements;
		public ISet<Edge> edgesNeededAsElements;
		public ISet<Node> nodesNeededAsAttributes;
		public ISet<Edge> edgesNeededAsAttributes;

		public ISet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
		public ISet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

		public Dictionary<GraphEntity, ISet<Entity>> forceAttributeToVar = new LinkedHashMap<GraphEntity, ISet<Entity>>();

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

		public virtual void InitNeeds(ISet<Expression> containerExprs)
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
