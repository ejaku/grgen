/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Typeof = de.unika.ipd.grgen.ir.expr.Typeof;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node representing the current type of a
	/// certain node/edge.
	/// </summary>
	public class TypeofNode : ExprNode
	{
		static TypeofNode()
		{
			SetClassName(typeof(TypeofNode), "typeof");
		}

		private IdentNode entityUnresolved;
		private EdgeDeclNode entityEdgeDecl = null;
		private NodeDeclNode entityNodeDecl = null;
		private VarDeclNode entityVarDecl = null;

		public TypeofNode(Coords coords, IdentNode entity)
			: base(coords)
		{
			this.entityUnresolved = entity;
			BecomeParent(this.entityUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl, entityVarDecl));
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
				childrenNames.Add("entity");
				return childrenNames;
			}
		}

		private static readonly DeclarationTripleResolver<EdgeDeclNode, NodeDeclNode, VarDeclNode> entityResolver =
				new DeclarationTripleResolver<EdgeDeclNode, NodeDeclNode, VarDeclNode>(typeof(EdgeDeclNode), typeof(NodeDeclNode), typeof(VarDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool res = FixupDefinition(entityUnresolved, entityUnresolved.Scope);

			Triple<EdgeDeclNode, NodeDeclNode, VarDeclNode> resolved = entityResolver.Resolve(entityUnresolved, this);
			if(resolved != null)
			{
				entityEdgeDecl = resolved.first;
				entityNodeDecl = resolved.second;
				entityVarDecl = resolved.third;
			}

			return res && resolved != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
		protected internal override bool CheckLocal()
		{
			if(entityVarDecl != null
					&& !(entityVarDecl.DeclType is NodeTypeNode)
					&& !(entityVarDecl.DeclType is EdgeTypeNode))
			{
				ReportError("The variable in a typeof (" + entityUnresolved + ") must be of node or edge type,"
						+ " but is of type " + entityVarDecl.DeclType.TypeName
						+ " (which is a " + entityVarDecl.DeclType.Kind + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			Entity entity = GetValidResolvedVersion(entityEdgeDecl, entityNodeDecl, entityVarDecl).CheckIR(typeof(Entity));

			return new Typeof(entity);
		}

		public virtual DeclNode Entity
		{
			get
			{
				Debug.Assert(IsResolved());

				return GetValidResolvedVersion(entityEdgeDecl, entityNodeDecl, entityVarDecl);
			}
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.typeType;
			}
		}

		public override bool NoDefElement(string containingConstruct)
		{
			if(entityEdgeDecl != null)
			{
				if(entityEdgeDecl.defEntityToBeYieldedTo)
				{
					entityEdgeDecl.ReportError("A def edge (" + entityUnresolved + ")"
							+ " cannot be accessed from a(n) " + containingConstruct + ".");
					return false;
				}
			}
			if(entityNodeDecl != null)
			{
				if(entityNodeDecl.defEntityToBeYieldedTo)
				{
					entityNodeDecl.ReportError("A def node (" + entityUnresolved + ")"
							+ " cannot be accessed from a(n) " + containingConstruct + ".");
					return false;
				}
			}
			if(entityVarDecl != null)
			{
				if(entityVarDecl.defEntityToBeYieldedTo && !entityVarDecl.lambdaExpressionVariable)
				{
					entityVarDecl.ReportError("A def variable (" + entityUnresolved + ")"
							+ " cannot be accessed from a(n) " + containingConstruct + ".");
					return false;
				}
			}
			return true;
		}
	}

}
