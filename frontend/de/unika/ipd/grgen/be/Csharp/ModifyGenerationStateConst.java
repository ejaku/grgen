/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * Interface giving access to the state needed for generating eval statements.
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.HashSet;
import java.util.Map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;

interface ModifyGenerationStateConst extends ExpressionGenerationState
{
	String getName();

	Collection<Node> getCommonNodes();

	Collection<Edge> getCommonEdges();

	Collection<SubpatternUsage> getCommonSubpatternUsages();

	Collection<Node> getNewNodes();

	Collection<Edge> getNewEdges();

	Collection<SubpatternUsage> getNewSubpatternUsages();

	Collection<Node> getDelNodes();

	Collection<Edge> getDelEdges();

	Collection<SubpatternUsage> getDelSubpatternUsages();

	Collection<Node> getYieldedNodes();

	Collection<Edge> getYieldedEdges();

	Collection<Variable> getYieldedVariables();

	Collection<Node> getNewOrRetypedNodes();

	Collection<Edge> getNewOrRetypedEdges();

	Collection<GraphEntity> getAccessViaInterface();

	Map<GraphEntity, HashSet<Entity>> getNeededAttributes();

	Map<GraphEntity, HashSet<Entity>> getAttributesStoredBeforeDelete();

	Collection<Variable> getNeededVariables();

	Collection<Node> getNodesNeededAsElements();

	Collection<Edge> getEdgesNeededAsElements();

	Collection<Node> getNodesNeededAsAttributes();

	Collection<Edge> getEdgesNeededAsAttributes();

	Collection<Node> getNodesNeededAsTypes();

	Collection<Edge> getEdgesNeededAsTypes();

	Map<GraphEntity, HashSet<Entity>> getForceAttributeToVar();

	String getMatchClassName();

	String getPackagePrefix();
}
