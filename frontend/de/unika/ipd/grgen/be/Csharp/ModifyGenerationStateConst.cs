/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Interface giving access to the state needed for generating eval statements.
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ir;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	public interface ModifyGenerationStateConst : ExpressionGenerationState
	{
		string Name {get;}

		ICollection<Node> CommonNodes {get;}

		ICollection<Edge> CommonEdges {get;}

		ICollection<SubpatternUsage> CommonSubpatternUsages {get;}

		ICollection<Node> NewNodes {get;}

		ICollection<Edge> NewEdges {get;}

		ICollection<SubpatternUsage> NewSubpatternUsages {get;}

		ICollection<Node> DelNodes {get;}

		ICollection<Edge> DelEdges {get;}

		bool IsDeleted(Entity entity);

		ICollection<SubpatternUsage> DelSubpatternUsages {get;}

		ICollection<Node> YieldedNodes {get;}

		ICollection<Edge> YieldedEdges {get;}

		ICollection<Variable> YieldedVariables {get;}

		ICollection<Node> NewOrRetypedNodes {get;}

		ICollection<Edge> NewOrRetypedEdges {get;}

		ICollection<GraphEntity> AccessViaInterface {get;}

		IDictionary<GraphEntity, HashSet<Entity>> NeededAttributes {get;}

		IDictionary<GraphEntity, HashSet<Entity>> AttributesStoredBeforeDelete {get;}

		ICollection<Variable> NeededVariables {get;}

		ICollection<Node> NodesNeededAsElements {get;}

		ICollection<Edge> EdgesNeededAsElements {get;}

		ICollection<Node> NodesNeededAsAttributes {get;}

		ICollection<Edge> EdgesNeededAsAttributes {get;}

		ICollection<Node> NodesNeededAsTypes {get;}

		ICollection<Edge> EdgesNeededAsTypes {get;}

		IDictionary<GraphEntity, HashSet<Entity>> ForceAttributeToVar {get;}

		string MatchClassName {get;}

		string PackagePrefix {get;}
	}

}
