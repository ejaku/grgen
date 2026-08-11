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
	using ScopeOwner = de.unika.ipd.grgen.ast.ScopeOwner;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using AttributeIndexTypeNode = de.unika.ipd.grgen.ast.model.type.AttributeIndexTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using AttributeIndex = de.unika.ipd.grgen.ir.model.AttributeIndex;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;


	/// <summary>
	/// AST node class representing attribute index declarations
	/// </summary>
	public class AttributeIndexDeclNode : IndexDeclNode
	{
		static AttributeIndexDeclNode()
		{
			SetClassName(typeof(AttributeIndexDeclNode), "attribute index declaration");
		}

		public InheritanceTypeNode type;
		protected internal IdentNode memberUnresolved;
		public MemberDeclNode member;

		private static readonly AttributeIndexTypeNode attributeIndexType =
			new AttributeIndexTypeNode();

		public AttributeIndexDeclNode(IdentNode id, IdentNode type, IdentNode member)
			: base(id, attributeIndexType)
		{
			this.typeUnresolved = type;
			BecomeParent(this.typeUnresolved);
			this.memberUnresolved = member;
			BecomeParent(this.memberUnresolved);
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
				children.Add(GetValidVersion(memberUnresolved, member));
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
				childrenNames.Add("member");
				return childrenNames;
			}
		}

		private static DeclarationResolver<TypeDeclNode> typeResolver =
			new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode));
		private static readonly DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(typeof(MemberDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			TypeDeclNode resolved = typeResolver.Resolve(typeUnresolved, this);
			if(resolved == null)
				return false;
			//if(!resolved.resolve()) return false;

			TypeNode type = resolved.DeclType;

			if(!(type is InheritanceTypeNode))
			{
				typeUnresolved.ReportError("The attribute index " + Ident + " expects a node or edge type"
						+ "(but is given type " + type.TypeName + " as owner of attribute " + memberUnresolved + ").");
				return false;
			}
			else
				this.type = (InheritanceTypeNode)type;

			ScopeOwner o = (ScopeOwner)type;
			o.FixupDefinition(memberUnresolved);
			member = memberResolver.Resolve(memberUnresolved, this);

			return member != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return attributeIndexType;
			}
		}

		public override InheritanceTypeNode Type
		{
			get
			{
				Debug.Assert(IsResolved());

				return type;
			}
		}

		public override TypeNode ExpectedAccessType
		{
			get
			{
				Debug.Assert(IsResolved());

				return member.DeclType;
			}
		}

		protected internal override IR ConstructIR()
		{
			AttributeIndex attributeIndex = new AttributeIndex(Ident.ToString(), Ident.IRIdent,
					type.CheckIR<InheritanceType>(typeof(InheritanceType)), member.CheckIR<Entity>(typeof(Entity)));
			return attributeIndex;
		}

		public static string KindStr
		{
			get
			{
				return "attribute index";
			}
		}
	}

}
