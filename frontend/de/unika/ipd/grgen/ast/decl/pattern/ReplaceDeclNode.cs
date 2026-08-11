/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Buchwald, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{
using System.Collections.Generic;

using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using ConnectionCharacter = de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
using PatternGraphRhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;


/// <summary>
/// AST node for a replacement right-hand side.
/// </summary>
public class ReplaceDeclNode : RhsDeclNode
{
	static ReplaceDeclNode()
	{
		SetClassName(typeof(ReplaceDeclNode), "replace declaration");
	}

	/// <summary>
	/// Make a new replace right-hand side. </summary>
	/// <param name="id"> The identifier of this RHS. </param>
	/// <param name="patternGraph"> The right hand side graph. </param>
	public ReplaceDeclNode(IdentNode id, PatternGraphRhsNode patternGraph)
		: base(id, patternGraph)
	{
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, type));
			children.Add(patternGraph);
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
			childrenNames.Add("ident");
			childrenNames.Add("type");
			childrenNames.Add("right");
			return childrenNames;
		}
	}

	public override PatternGraphRhs GetIRPatternGraph(PatternGraphLhs left)
	{
		PatternGraphRhs right = patternGraph.IRPatternGraphRhs;
		InsertElementsFromEvalsIntoRhs(left, right);
		InsertElementsFromOrderedReplacementsIntoRhs(left, right);
		InsertElementsFromLeftToRightIfTheyAreFromNestingPattern(left, right);
		return right;
	}

	public override bool CheckAgainstLhsPattern(PatternGraphLhsNode pattern)
	{
		return true; // nothing to do as of now
	}

	protected internal override ISet<ConstraintDeclNode> GetElementsToDeleteImpl(PatternGraphLhsNode pattern)
	{
		LinkedHashSet<ConstraintDeclNode> elementsToDelete = new LinkedHashSet<ConstraintDeclNode>();

		ISet<EdgeDeclNode> rhsEdges = new LinkedHashSet<EdgeDeclNode>();
		ISet<NodeDeclNode> rhsNodes = new LinkedHashSet<NodeDeclNode>();

		foreach(EdgeDeclNode rhsEdge in patternGraph.Edges)
		{
			EdgeDeclNode originalRhsEdge = rhsEdge;
			while(originalRhsEdge is EdgeTypeChangeDeclNode)
				originalRhsEdge = ((EdgeTypeChangeDeclNode)originalRhsEdge).OldEdge;
			rhsEdges.Add(originalRhsEdge);
		}
		foreach(EdgeDeclNode lhsEdge in pattern.Edges)
		{
			if(!rhsEdges.Contains(lhsEdge))
				elementsToDelete.Add(lhsEdge);
		}

		foreach(NodeDeclNode rhsNode in patternGraph.Nodes)
		{
			NodeDeclNode originalRhsNode = rhsNode;
			while(originalRhsNode is NodeTypeChangeDeclNode)
				originalRhsNode = ((NodeTypeChangeDeclNode)originalRhsNode).OldNode;
			rhsNodes.Add(originalRhsNode);
		}
		foreach(NodeDeclNode lhsNode in pattern.Nodes)
		{
			if(!rhsNodes.Contains(lhsNode) && !lhsNode.IsDummy())
				elementsToDelete.Add(lhsNode);
		}

		// parameters are no special case, since they are treated like normal graph elements
		return elementsToDelete;
	}

	protected internal override ISet<ConnectionNode> GetConnectionsToReuseImpl(PatternGraphLhsNode pattern)
	{
		ISet<ConnectionNode> connectionsToReuse = new LinkedHashSet<ConnectionNode>();

		ISet<EdgeDeclNode> lhsEdges = pattern.Edges;
		foreach(ConnectionCharacter connectionCharacter in patternGraph.Connections)
		{
			if(connectionCharacter is ConnectionNode)
			{
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode rhsEdge = connection.Edge;
				while(rhsEdge is EdgeTypeChangeDeclNode)
					rhsEdge = ((EdgeTypeChangeDeclNode)rhsEdge).OldEdge;
				if(lhsEdges.Contains(rhsEdge))
					connectionsToReuse.Add(connection);
			}
		}

		return connectionsToReuse;
	}

	protected internal override ISet<NodeDeclNode> GetNodesToReuseImpl(PatternGraphLhsNode pattern)
	{
		ISet<NodeDeclNode> nodesToReuse = new LinkedHashSet<NodeDeclNode>();

		ISet<NodeDeclNode> lhsNodes = pattern.Nodes;
		ISet<NodeDeclNode> rhsNodes = patternGraph.Nodes;
		foreach(NodeDeclNode lhsNode in lhsNodes)
		{
			if(rhsNodes.Contains(lhsNode))
				nodesToReuse.Add(lhsNode);
		}

		return nodesToReuse;
	}

	protected internal override ISet<ConnectionNode> GetConnectionsNotDeleted(PatternGraphLhsNode pattern)
	{
		ISet<ConnectionNode> connectionsNotDeleted = new LinkedHashSet<ConnectionNode>();

		foreach(ConnectionCharacter connectionCharacter in patternGraph.Connections)
		{
			if(connectionCharacter is ConnectionNode)
			{
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				connectionsNotDeleted.Add(connection);
			}
		}

		return connectionsNotDeleted;
	}

	private static void InsertElementsFromLeftToRightIfTheyAreFromNestingPattern(PatternGraphLhs left, PatternGraphBase right)
	{
		foreach(Node lhsNode in left.Nodes)
		{
			if(lhsNode.directlyNestingLHSGraph != left && !right.HasNode(lhsNode))
				right.AddSingleNode(lhsNode);
		}
		foreach(Edge lhsEdge in left.Edges)
		{
			if(lhsEdge.directlyNestingLHSGraph != left && !right.HasEdge(lhsEdge))
				right.AddSingleEdge(lhsEdge);
		}
	}
}

}
