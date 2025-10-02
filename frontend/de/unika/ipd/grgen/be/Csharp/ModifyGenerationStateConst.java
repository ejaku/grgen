/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
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
	String name();

	Collection<Node> commonNodes();

	Collection<Edge> commonEdges();

	Collection<SubpatternUsage> commonSubpatternUsages();

	Collection<Node> newNodes();

	Collection<Edge> newEdges();

	Collection<SubpatternUsage> newSubpatternUsages();

	Collection<Node> delNodes();

	Collection<Edge> delEdges();

	Collection<SubpatternUsage> delSubpatternUsages();

	Collection<Node> yieldedNodes();

	Collection<Edge> yieldedEdges();

	Collection<Variable> yieldedVariables();

	Collection<Node> newOrRetypedNodes();

	Collection<Edge> newOrRetypedEdges();

	Collection<GraphEntity> accessViaInterface();

	Map<GraphEntity, HashSet<Entity>> neededAttributes();

	Map<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete();

	Collection<Variable> neededVariables();

	Collection<Node> nodesNeededAsElements();

	Collection<Edge> edgesNeededAsElements();

	Collection<Node> nodesNeededAsAttributes();

	Collection<Edge> edgesNeededAsAttributes();

	Collection<Node> nodesNeededAsTypes();

	Collection<Edge> edgesNeededAsTypes();

	Map<GraphEntity, HashSet<Entity>> forceAttributeToVar();

	String matchClassName();

	String packagePrefix();
}
