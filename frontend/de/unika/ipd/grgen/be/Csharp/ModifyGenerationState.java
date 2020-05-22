/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Class holding the state needed for generating the rewrite part of an action.
 * @author Edgar Jakumeit, Moritz Kroll
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.Map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.expr.set.SetInit;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;

class ModifyGenerationState implements ModifyGenerationStateConst
{
	public String name()
	{
		return functionOrProcedureName != null ? functionOrProcedureName : actionName;
	}

	public Collection<Node> commonNodes()
	{
		return Collections.unmodifiableCollection(commonNodes);
	}

	public Collection<Edge> commonEdges()
	{
		return Collections.unmodifiableCollection(commonEdges);
	}

	public Collection<SubpatternUsage> commonSubpatternUsages()
	{
		return Collections.unmodifiableCollection(commonSubpatternUsages);
	}

	public Collection<Node> newNodes()
	{
		return Collections.unmodifiableCollection(newNodes);
	}

	public Collection<Edge> newEdges()
	{
		return Collections.unmodifiableCollection(newEdges);
	}

	public Collection<SubpatternUsage> newSubpatternUsages()
	{
		return Collections.unmodifiableCollection(newSubpatternUsages);
	}

	public Collection<Node> delNodes()
	{
		return Collections.unmodifiableCollection(delNodes);
	}

	public Collection<Edge> delEdges()
	{
		return Collections.unmodifiableCollection(delEdges);
	}

	public Collection<SubpatternUsage> delSubpatternUsages()
	{
		return Collections.unmodifiableCollection(delSubpatternUsages);
	}

	public Collection<Node> yieldedNodes()
	{
		return Collections.unmodifiableCollection(yieldedNodes);
	}

	public Collection<Edge> yieldedEdges()
	{
		return Collections.unmodifiableCollection(yieldedEdges);
	}

	public Collection<Variable> yieldedVariables()
	{
		return Collections.unmodifiableCollection(yieldedVariables);
	}

	public Collection<Node> newOrRetypedNodes()
	{
		return Collections.unmodifiableCollection(newOrRetypedNodes);
	}

	public Collection<Edge> newOrRetypedEdges()
	{
		return Collections.unmodifiableCollection(newOrRetypedEdges);
	}

	public Collection<GraphEntity> accessViaInterface()
	{
		return Collections.unmodifiableCollection(accessViaInterface);
	}

	public Map<GraphEntity, HashSet<Entity>> neededAttributes()
	{
		return Collections.unmodifiableMap(neededAttributes);
	}

	public Map<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete()
	{
		return Collections.unmodifiableMap(attributesStoredBeforeDelete);
	}

	public Collection<Variable> neededVariables()
	{
		return Collections.unmodifiableCollection(neededVariables);
	}

	public Collection<Node> nodesNeededAsElements()
	{
		return Collections.unmodifiableCollection(nodesNeededAsElements);
	}

	public Collection<Edge> edgesNeededAsElements()
	{
		return Collections.unmodifiableCollection(edgesNeededAsElements);
	}

	public Collection<Node> nodesNeededAsAttributes()
	{
		return Collections.unmodifiableCollection(nodesNeededAsAttributes);
	}

	public Collection<Edge> edgesNeededAsAttributes()
	{
		return Collections.unmodifiableCollection(edgesNeededAsAttributes);
	}

	public Collection<Node> nodesNeededAsTypes()
	{
		return Collections.unmodifiableCollection(nodesNeededAsTypes);
	}

	public Collection<Edge> edgesNeededAsTypes()
	{
		return Collections.unmodifiableCollection(edgesNeededAsTypes);
	}

	public Map<GraphEntity, HashSet<Entity>> forceAttributeToVar()
	{
		return Collections.unmodifiableMap(forceAttributeToVar);
	}

	public String matchClassName()
	{
		return matchClassName;
	}

	public String packagePrefix()
	{
		return packagePrefix;
	}

	public Map<Expression, String> mapExprToTempVar()
	{
		return Collections.unmodifiableMap(mapExprToTempVar);
	}

	public boolean useVarForResult()
	{
		return useVarForResult;
	}

	public Model model()
	{
		return model;
	}

	public boolean isToBeParallelizedActionExisting()
	{
		return isToBeParallelizedActionExisting;
	}

	public boolean emitProfilingInstrumentation()
	{
		return emitProfiling;
	}

	// --------------------

	// if not null this is the generation state of a function or procedure (with all entries empty)
	public String functionOrProcedureName;
	// otherwise it is the generation state of the modify of an action
	public String actionName;

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

	public HashMap<GraphEntity, HashSet<Entity>> neededAttributes;
	public HashMap<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

	public HashSet<Variable> neededVariables;

	public HashSet<Node> nodesNeededAsElements;
	public HashSet<Edge> edgesNeededAsElements;
	public HashSet<Node> nodesNeededAsAttributes;
	public HashSet<Edge> edgesNeededAsAttributes;

	public HashSet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
	public HashSet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

	public HashMap<GraphEntity, HashSet<Entity>> forceAttributeToVar = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

	public HashMap<Expression, String> mapExprToTempVar = new LinkedHashMap<Expression, String>();
	public boolean useVarForResult;

	private Model model;
	private String matchClassName;
	private String packagePrefix;
	private boolean isToBeParallelizedActionExisting;
	private boolean emitProfiling;

	public void InitNeeds(NeededEntities needs)
	{
		neededAttributes = needs.attrEntityMap;
		nodesNeededAsElements = needs.nodes;
		edgesNeededAsElements = needs.edges;
		nodesNeededAsAttributes = needs.attrNodes;
		edgesNeededAsAttributes = needs.attrEdges;
		neededVariables = needs.variables;

		int i = 0;
		for(Expression expr : needs.containerExprs) {
			if(expr instanceof MapInit || expr instanceof SetInit
					|| expr instanceof ArrayInit || expr instanceof DequeInit)
				continue;
			mapExprToTempVar.put(expr, "tempcontainervar_" + i);
			i++;
		}
	}

	public void InitNeeds(HashSet<Expression> containerExprs)
	{
		int i = 0;
		for(Expression expr : containerExprs) {
			if(expr instanceof MapInit || expr instanceof SetInit
					|| expr instanceof ArrayInit || expr instanceof DequeInit)
				continue;
			mapExprToTempVar.put(expr, "tempcontainervar_" + i);
			i++;
		}
	}

	public void ClearContainerExprs()
	{
		mapExprToTempVar.clear();
	}

	public ModifyGenerationState(Model model,
			String matchClassName, String packagePrefix,
			boolean isToBeParallelizedActionExisting,
			boolean emitProfiling)
	{
		this.model = model;
		this.matchClassName = matchClassName;
		this.packagePrefix = packagePrefix;
		this.isToBeParallelizedActionExisting = isToBeParallelizedActionExisting;
		this.emitProfiling = emitProfiling;
	}
}
