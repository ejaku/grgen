/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.util.SourceBuilder;

class ModifyGenerationState implements ModifyGenerationStateConst
{
	@Override
	public String getName()
	{
		return functionOrProcedureName != null ? functionOrProcedureName : actionName;
	}

	@Override
	public Collection<Node> getCommonNodes()
	{
		return Collections.unmodifiableCollection(commonNodes);
	}

	@Override
	public Collection<Edge> getCommonEdges()
	{
		return Collections.unmodifiableCollection(commonEdges);
	}

	@Override
	public Collection<SubpatternUsage> getCommonSubpatternUsages()
	{
		return Collections.unmodifiableCollection(commonSubpatternUsages);
	}

	@Override
	public Collection<Node> getNewNodes()
	{
		return Collections.unmodifiableCollection(newNodes);
	}

	@Override
	public Collection<Edge> getNewEdges()
	{
		return Collections.unmodifiableCollection(newEdges);
	}

	@Override
	public Collection<SubpatternUsage> getNewSubpatternUsages()
	{
		return Collections.unmodifiableCollection(newSubpatternUsages);
	}

	@Override
	public Collection<Node> getDelNodes()
	{
		return Collections.unmodifiableCollection(delNodes);
	}

	@Override
	public Collection<Edge> getDelEdges()
	{
		return Collections.unmodifiableCollection(delEdges);
	}

	@Override
	public Collection<SubpatternUsage> getDelSubpatternUsages()
	{
		return Collections.unmodifiableCollection(delSubpatternUsages);
	}

	@Override
	public Collection<Node> getYieldedNodes()
	{
		return Collections.unmodifiableCollection(yieldedNodes);
	}

	@Override
	public Collection<Edge> getYieldedEdges()
	{
		return Collections.unmodifiableCollection(yieldedEdges);
	}

	@Override
	public Collection<Variable> getYieldedVariables()
	{
		return Collections.unmodifiableCollection(yieldedVariables);
	}

	@Override
	public Collection<Node> getNewOrRetypedNodes()
	{
		return Collections.unmodifiableCollection(newOrRetypedNodes);
	}

	@Override
	public Collection<Edge> getNewOrRetypedEdges()
	{
		return Collections.unmodifiableCollection(newOrRetypedEdges);
	}

	@Override
	public Collection<GraphEntity> getAccessViaInterface()
	{
		return Collections.unmodifiableCollection(accessViaInterface);
	}

	@Override
	public Map<GraphEntity, HashSet<Entity>> getNeededAttributes()
	{
		return Collections.unmodifiableMap(neededAttributes);
	}

	@Override
	public Map<GraphEntity, HashSet<Entity>> getAttributesStoredBeforeDelete()
	{
		return Collections.unmodifiableMap(attributesStoredBeforeDelete);
	}

	@Override
	public Collection<Variable> getNeededVariables()
	{
		return Collections.unmodifiableCollection(neededVariables);
	}

	@Override
	public Collection<Node> getNodesNeededAsElements()
	{
		return Collections.unmodifiableCollection(nodesNeededAsElements);
	}

	@Override
	public Collection<Edge> getEdgesNeededAsElements()
	{
		return Collections.unmodifiableCollection(edgesNeededAsElements);
	}

	@Override
	public Collection<Node> getNodesNeededAsAttributes()
	{
		return Collections.unmodifiableCollection(nodesNeededAsAttributes);
	}

	@Override
	public Collection<Edge> getEdgesNeededAsAttributes()
	{
		return Collections.unmodifiableCollection(edgesNeededAsAttributes);
	}

	@Override
	public Collection<Node> getNodesNeededAsTypes()
	{
		return Collections.unmodifiableCollection(nodesNeededAsTypes);
	}

	@Override
	public Collection<Edge> getEdgesNeededAsTypes()
	{
		return Collections.unmodifiableCollection(edgesNeededAsTypes);
	}

	@Override
	public Map<GraphEntity, HashSet<Entity>> getForceAttributeToVar()
	{
		return Collections.unmodifiableMap(forceAttributeToVar);
	}

	@Override
	public String getMatchClassName()
	{
		return matchClassName;
	}

	@Override
	public String getPackagePrefix()
	{
		return packagePrefix;
	}

	@Override
	public Map<Expression, String> getMapExprToTempVar()
	{
		return Collections.unmodifiableMap(mapExprToTempVar);
	}

	@Override
	public boolean useVarForResult()
	{
		return useVarForResult_;
	}

	@Override
	public boolean switchToVarForResultAfterFirstVarUsage()
	{
		return switchToVarForResultAfterFirstVarUsage_;
	}

	@Override
	public void switchToVarForResult()
	{
		useVarForResult_ = true;
	}

	@Override
	public Model getModel()
	{
		return model;
	}

	@Override
	public boolean isToBeParallelizedActionExisting()
	{
		return isToBeParallelizedActionExisting_;
	}

	@Override
	public boolean emitProfilingInstrumentation()
	{
		return emitProfiling_;
	}
	
	@Override
	public SourceBuilder getPerElementMethodSourceBuilder()
	{
		return perElementMethodSourceBuilder;
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
	public boolean useVarForResult_;
	public boolean switchToVarForResultAfterFirstVarUsage_;

	private Model model;
	private String matchClassName;
	private String packagePrefix;
	private boolean isToBeParallelizedActionExisting_;
	private boolean emitProfiling_;

	private SourceBuilder perElementMethodSourceBuilder;


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
		this.isToBeParallelizedActionExisting_ = isToBeParallelizedActionExisting;
		this.emitProfiling_ = emitProfiling;
		this.perElementMethodSourceBuilder = new SourceBuilder();
	}
}
