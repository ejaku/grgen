/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

using System;
using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using DummyNodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.DummyNodeDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using EdgeTypeChangeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeTypeChangeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using ArbitraryEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Checker = de.unika.ipd.grgen.ast.util.Checker;
using de.unika.ipd.grgen.ast.util;
using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;

/// <summary>
/// AST node that represents a Connection (an edge connecting two nodes)
/// children: LEFT:NodeDeclNode, EDGE:EdgeDeclNode, RIGHT:NodeDeclNode
/// </summary>
public class ConnectionNode : ConnectionCharacter
{
	static ConnectionNode()
	{
		SetClassName(typeof(ConnectionNode), "connection");
	}

	private ConnectionKind connectionKind;

	/// <summary>
	/// possible redirection kinds </summary>
	public const int NO_REDIRECTION = 0;
	public const int REDIRECT_SOURCE = 1;
	public const int REDIRECT_TARGET = 2;
	public const int REDIRECT_SOURCE_AND_TARGET = REDIRECT_SOURCE | REDIRECT_TARGET;

	private int redirectionKind;

	private NodeDeclNode left;
	private EdgeDeclNode edge;
	private NodeDeclNode right;

	private BaseNode leftUnresolved;
	public BaseNode edgeUnresolved;
	private BaseNode rightUnresolved;

	/// <summary>
	/// Construct a new connection node.
	///  A connection node has two node nodes and one edge node </summary>
	///  <param name="left"> First node </param>
	///  <param name="edge"> Edge that connects n1 with n2 </param>
	///  <param name="right"> Second node. </param>
	///  <param name="direction"> Direction of the connection. </param>
	///  <param name="redirection"> Potential redirection of the edge in the connection. </param>
	public ConnectionNode(BaseNode left, BaseNode edge, BaseNode right, ConnectionKind direction, int redirection)
		: base(edge.Coords)
	{
		leftUnresolved = left;
		BecomeParent(leftUnresolved);
		edgeUnresolved = edge;
		BecomeParent(edgeUnresolved);
		rightUnresolved = right;
		BecomeParent(rightUnresolved);
		connectionKind = direction;
		redirectionKind = redirection;
	}

	/// <summary>
	/// Construct a new already resolved and checked connection node.
	///  A connection node has two node nodes and one edge node </summary>
	///  <param name="left"> First node </param>
	///  <param name="edge"> Edge that connects n1 with n2 </param>
	///  <param name="right"> Second node. </param>
	///  <param name="direction"> Direction of the connection. </param>
	public ConnectionNode(NodeDeclNode left, EdgeDeclNode edge, NodeDeclNode right, ConnectionKind direction, BaseNode parent)
		: this(left, edge, right, direction, NO_REDIRECTION)
	{
		parent.BecomeParent(this);

		Resolve();
		Check();
	}

	public virtual ConnectionNode CloneForAuto(PatternGraphLhsNode parent)
	{
		return new ConnectionNode(this.left.CloneForAuto(parent),
				this.edge.CloneForAuto(parent),
				this.right.CloneForAuto(parent),
				this.connectionKind, parent);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(GetValidVersion(leftUnresolved, left));
		children.Add(GetValidVersion(edgeUnresolved, edge));
		children.Add(GetValidVersion(rightUnresolved, right));
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
		childrenNames.Add("src");
		childrenNames.Add("edge");
		childrenNames.Add("tgt");
		return childrenNames;
		}
	}

	private static DeclarationResolver<NodeDeclNode> nodeResolver =
			new DeclarationResolver<NodeDeclNode>(typeof(NodeDeclNode));
	private static DeclarationResolver<EdgeDeclNode> edgeResolver =
			new DeclarationResolver<EdgeDeclNode>(typeof(EdgeDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool res = FixupDefinition(leftUnresolved, leftUnresolved.Scope);
		res &= FixupDefinition(edgeUnresolved, edgeUnresolved.Scope);
		res &= FixupDefinition(rightUnresolved, rightUnresolved.Scope);
		if(!res)
			return false;

		left = nodeResolver.Resolve(leftUnresolved, this);
		edge = edgeResolver.Resolve(edgeUnresolved, this);
		right = nodeResolver.Resolve(rightUnresolved, this);

		return left != null && edge != null && right != null;
	}

	private static Checker nodeTypeChecker = new TypeChecker(typeof(NodeTypeNode));
	private static Checker edgeTypeChecker = new TypeChecker(typeof(EdgeTypeNode));

	/// <summary>
	/// Check, if the AST node is correctly built. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		bool sucess = nodeTypeChecker.Check(left, error)
				& edgeTypeChecker.Check(edge, error)
				& nodeTypeChecker.Check(right, error)
				& CheckEdgeRootType()
				& AreDanglingEdgesInReplacementDeclaredInPattern()
				& NoDefNonDefMixedConnection()
				& CheckDeclaredOnLHS();

		if(!sucess)
			return false;
		WarnArbitraryRootType();

		return true;
	}

	private void WarnArbitraryRootType()
	{
		if(connectionKind != de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY)
			return;

		if(!(edge.DeclType is ArbitraryEdgeTypeNode))
			edge.ReportWarning("The type of edge" + edge.EmptyWhenAnonymousPostfix(" ") + " differs from "
					+ ArbitraryEdgeRootTypeDecl.Ident
					+ ", please use another edge kind instead of ?--? (e.g. -->).");

		return;
	}

	private bool CheckEdgeRootType()
	{
		TypeDeclNode rootDecl = null;
		switch(connectionKind)
		{
		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY:
			rootDecl = ArbitraryEdgeRootTypeDecl;
			break;

		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY_DIRECTED:
			rootDecl = DirectedEdgeRootTypeDecl;
			break;

		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.DIRECTED:
			rootDecl = DirectedEdgeRootTypeDecl;
			break;

		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.UNDIRECTED:
			rootDecl = UndirectedEdgeRootTypeDecl;
			break;

		default:
			Debug.Assert(false);
			break;
		}

		TypeNode rootType = rootDecl != null ? rootDecl.DeclType : null;

		if(!edge.DeclType.IsCompatibleTo(rootType))
		{
			ReportError("The connection kind of the edge" + edge.EmptyWhenAnonymousPostfix(" ") + " is incompatible with the type of the edge"
					+ " (" + toString(connectionKind) + " with " + edge.DeclType.ToStringWithDeclarationCoords() + ").");
			return false;
		}

		return true;
	}

	private bool AreDanglingEdgesInReplacementDeclaredInPattern()
	{
		if(!(left is DummyNodeDeclNode) && !(right is DummyNodeDeclNode))
			return true; // edge not dangling

		// edge dangling
		if(left is DummyNodeDeclNode)
		{
			if((left.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
				return true; // we're within the pattern, not the replacement
		}
		if(right is DummyNodeDeclNode)
		{
			if((right.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
				return true; // we're within the pattern, not the replacement
		}

		// edge dangling and located within the replacement
		if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			return true; // edge was declared in the pattern
		if(edge is EdgeTypeChangeDeclNode)
			return true; // edge is a type change edge of an edge declared within the pattern
		if(edge.defEntityToBeYieldedTo)
			return true; // edge is a def to be yielded to, i.e. output variable

		edge.ReportError("Dangling edges in the rewrite part must have been declared in the pattern part"
				+ edge.EmptyWhenAnonymous(" (a declaration in the pattern part is missing for " + edge.Ident + ")") + ".");
		return false;
	}

	private bool NoDefNonDefMixedConnection()
	{
		if(left.defEntityToBeYieldedTo)
		{
			if((left.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
					&& (edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			{
				left.ReportError("A pattern part def node cannot connect to a pattern part non-def edge"
					+ " (as is the case with " + left.Ident + " and " + edge.ToStringWithDeclarationCoords() + ").");
				return false;
			}
		}
		if(right.defEntityToBeYieldedTo)
		{
			if((right.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS
					&& (edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			{
				right.ReportError("A pattern part def node cannot connect to a pattern part non-def edge"
					+ " (as is the case with " + right.Ident + " and " + edge.ToStringWithDeclarationCoords() + ").");
				return false;
			}
		}
		if(edge.defEntityToBeYieldedTo)
		{
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			{
				if(!(left is DummyNodeDeclNode && right is DummyNodeDeclNode))
				{
					edge.ReportError("A pattern part def edge cannot connect to nodes at all"
						+ " (as is the case with " + edge.Ident
						+ (left is DummyNodeDeclNode ? "" : " and " + left.Ident)
						+ (right is DummyNodeDeclNode ? "" : " and " + right.Ident)
						+ ").");
					return false;
				}
			}
		}

		return true;
	}

	private bool CheckDeclaredOnLHS()
	{
		if(redirectionKind != NO_REDIRECTION)
		{
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			{
				edge.ReportError("An edge to be redirected must have been declared in the pattern (thus matched)"
						+ " (but edge " + edge.Ident + " is declared in the rewrite part).");
				return false;
			}
			if(connectionKind != de.unika.ipd.grgen.ast.pattern.ConnectionKind.DIRECTED)
			{
				edge.ReportError("Only directed edges may be redirected (to other nodes)"
						+ " (this is not the case for edge " + edge.Ident + " of connection kind " + toString(connectionKind) + ").");
				return false;
			}
		}
		if(connectionKind == de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY)
		{
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			{
				edge.ReportError("New instances of ?--? are not allowed in the rewrite part"
						+ edge.EmptyWhenAnonymous(" (this is the case for edge " + edge.Ident + ")") + ".");
				return false;
			}
		}
		if(connectionKind == de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY_DIRECTED)
		{
			if((edge.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			{
				edge.ReportError("New instances of <--> are not allowed in the rewrite part"
						+ edge.EmptyWhenAnonymous(" (this is the case for edge " + edge.Ident + ")") + ".");
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// This adds the connection to an IR pattern graph.
	/// This method should only be used by <seealso cref="PatternGraphLhsNode.constructIR()"/>. </summary>
	/// <param name="patternGraph"> The IR pattern graph. </param>
	public override void AddToGraph(PatternGraphBase patternGraph)
	{
		patternGraph.AddConnection(left.IRNode, edge.IREdge, right.IRNode, connectionKind == de.unika.ipd.grgen.ast.pattern.ConnectionKind.DIRECTED,
				(redirectionKind & REDIRECT_SOURCE) == REDIRECT_SOURCE,
				(redirectionKind & REDIRECT_TARGET) == REDIRECT_TARGET);
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.pattern.ConnectionCharacter.addEdges(java.util.Set)"/>
	public override void AddEdge(ISet<EdgeDeclNode> set)
	{
		Debug.Assert(IsResolved());

		set.Add(edge);
	}

	public virtual ConnectionKind ConnectionKind
	{
		get
		{
		return connectionKind;
		}
	}

	public virtual int RedirectionKind
	{
		get
		{
		return redirectionKind;
		}
	}

	public override EdgeDeclNode Edge
	{
		get
		{
		return edge;
		}
	}

	public override NodeDeclNode Src
	{
		get
		{
		return left;
		}
		set
		{
		Debug.Assert((value != null));
		SwitchParenthood(left, value);
		left = value;
		}
	}


	public override NodeDeclNode Tgt
	{
		get
		{
		return right;
		}
		set
		{
		Debug.Assert((value != null));
		SwitchParenthood(right, value);
		right = value;
		}
	}


	/// <seealso cref="de.unika.ipd.grgen.ast.pattern.ConnectionCharacter.addNodes(java.util.Set)"/>
	public override void AddNodes(ISet<NodeDeclNode> set)
	{
		Debug.Assert(IsResolved());

		set.Add(left);
		set.Add(right);
	}

	public static string KindStr
	{
		get
		{
		return "connection node";
		}
	}

	public static string toString(ConnectionKind connectionKind)
	{
		switch(connectionKind)
		{
		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY:
			return "?--?";
		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.ARBITRARY_DIRECTED:
			return "<-->";
		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.DIRECTED:
			return "-->";
		case de.unika.ipd.grgen.ast.pattern.ConnectionKind.UNDIRECTED:
			return "--";
		default:
			throw new Exception("Internal compiler error -- unkonwn connection kind.");
		}
	}
}

}
