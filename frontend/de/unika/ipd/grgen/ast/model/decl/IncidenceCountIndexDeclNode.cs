/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.model.decl
{
	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
	using FunctionInvocationDecisionNode = de.unika.ipd.grgen.ast.expr.invocation.FunctionInvocationDecisionNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using IncidenceCountIndexTypeNode = de.unika.ipd.grgen.ast.model.type.IncidenceCountIndexTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using IncidenceCountIndex = de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;
	using Direction = de.unika.ipd.grgen.util.Direction;


	/// <summary>
	/// AST node class representing incidence count index declarations
	/// </summary>
	public class IncidenceCountIndexDeclNode : IndexDeclNode
	{
		static IncidenceCountIndexDeclNode()
		{
			SetClassName(typeof(IncidenceCountIndexDeclNode), "incidence count index declaration");
		}

		private string functionName; // input string, "resolved" to direction
		private Direction direction;
		private IdentNode startNodeTypeUnresolved;
		private InheritanceTypeNode startNodeType;
		private IdentNode incidentEdgeTypeUnresolved;
		private InheritanceTypeNode incidentEdgeType;
		private IdentNode adjacentNodeTypeUnresolved;
		private InheritanceTypeNode adjacentNodeType;

		private static readonly IncidenceCountIndexTypeNode incidenceCountIndexType =
				new IncidenceCountIndexTypeNode();

		private static readonly DeclarationTypeResolver<InheritanceTypeNode> typeResolver =
				new DeclarationTypeResolver<InheritanceTypeNode>(typeof(InheritanceTypeNode));

		public IncidenceCountIndexDeclNode(IdentNode id, string functionName,
				IdentNode startNodeType, IdentNode incidentEdgeType, IdentNode adjacentNodeType,
				ParserEnvironment env)
			: base(id, incidenceCountIndexType)
		{
			this.functionName = functionName;
			this.startNodeTypeUnresolved = BecomeParent(startNodeType);
			this.incidentEdgeTypeUnresolved = BecomeParent(incidentEdgeType != null ? incidentEdgeType : env.DirectedEdgeRoot);
			this.adjacentNodeTypeUnresolved = BecomeParent(adjacentNodeType != null ? adjacentNodeType : env.NodeRoot);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ident);
				children.Add(GetValidVersion(startNodeTypeUnresolved, startNodeType));
				children.Add(GetValidVersion(incidentEdgeTypeUnresolved, incidentEdgeType));
				children.Add(GetValidVersion(adjacentNodeTypeUnresolved, adjacentNodeType));
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
				childrenNames.Add("startNodeType");
				childrenNames.Add("incidentEdgeType");
				childrenNames.Add("adjacentNodeType");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			if(startNodeTypeUnresolved == null)
			{
				ReportError(functionName + "() expects 1-3 parameters (but already the start node type is missing).");
				return false;
			}

			if(startNodeTypeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)startNodeTypeUnresolved);
			else
				FixupDefinition(startNodeTypeUnresolved, startNodeTypeUnresolved.Scope);
			startNodeType = typeResolver.Resolve(startNodeTypeUnresolved, this);
			if(startNodeType == null)
				return false;

			if(incidentEdgeTypeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)incidentEdgeTypeUnresolved);
			else
				FixupDefinition(incidentEdgeTypeUnresolved, incidentEdgeTypeUnresolved.Scope);
			incidentEdgeType = typeResolver.Resolve(incidentEdgeTypeUnresolved, this);
			if(incidentEdgeType == null)
				return false;

			if(adjacentNodeTypeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)adjacentNodeTypeUnresolved);
			else
				FixupDefinition(adjacentNodeTypeUnresolved, adjacentNodeTypeUnresolved.Scope);
			adjacentNodeType = typeResolver.Resolve(adjacentNodeTypeUnresolved, this);
			if(adjacentNodeType == null)
				return false;

			direction = FunctionInvocationDecisionNode.GetDirection(functionName);
			if(direction == Direction.INVALID)
			{
				ReportError(functionName
						+ "() is not a valid incidence count index declaration, expected is one of the count incidence function names countIncoming|countOutgoing|countIncident.");
				return false;
			}

			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(startNodeType is NodeTypeNode))
			{
				ReportError("The incidence count function specification " + functionName + "()"
						+ " in the incidende count index " + ident + " declaration"
						+ " expects as 1. type (start node type) a node type, but is given type " + startNodeType.TypeName + ".");
				return false;
			}
			if(!(incidentEdgeType is EdgeTypeNode))
			{
				ReportError("The incidence count function specification " + functionName + "()"
						+ " in the incidende count index " + ident + " declaration"
						+ " expects as 2. type (incident edge type) an edge type, but is given type " + incidentEdgeType.TypeName + ".");
				return false;
			}
			if(!(adjacentNodeType is NodeTypeNode))
			{
				ReportError("The incidence count function specification " + functionName + "()"
						+ " in the incidende count index " + ident + " declaration"
						+ " expects as 3. type (adjacent node type) a node type, but is given type " + adjacentNodeType.TypeName + ".");
				return false;
			}
			return true;
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return incidenceCountIndexType;
			}
		}

		public override InheritanceTypeNode Type
		{
			get
			{
				Debug.Assert(IsResolved());

				return startNodeType;
			}
		}

		public override TypeNode ExpectedAccessType
		{
			get
			{
				Debug.Assert(IsResolved());

				return IntTypeNode.intType;
			}
		}

		protected internal override IR ConstructIR()
		{
			IncidenceCountIndex incidenceCountIndex = new IncidenceCountIndex(Ident.ToString(),
					Ident.IRIdent, startNodeType.CheckIR<NodeType>(typeof(NodeType)),
					incidentEdgeType.CheckIR<EdgeType>(typeof(EdgeType)), direction,
					adjacentNodeType.CheckIR<NodeType>(typeof(NodeType)));
			return incidenceCountIndex;
		}

		public static string KindStr
		{
			get
			{
				return "incidence count index";
			}
		}
	}

}
