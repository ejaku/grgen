/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

interface ModifyGenerationStateConst extends ExpressionGenerationState {
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
