/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
	using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node that represents a qualified identifier
	/// i.e. expressions like this one: a.b.c.d
	/// </summary>
	public class QualIdentNode : BaseNode, DeclaredCharacter
	{
		static QualIdentNode()
		{
			SetClassName(typeof(QualIdentNode), "Qualification");
		}

		protected internal IdentNode ownerUnresolved;
		private DeclNode owner;

		protected internal IdentNode memberUnresolved;
		private DeclNode member;

		/// <summary>
		/// Make a new identifier qualify node. </summary>
		/// <param name="coords"> The coordinates. </param>
		public QualIdentNode(Coords coords, IdentNode owner, IdentNode member)
			: base(coords)
		{
			this.ownerUnresolved = owner;
			ownerUnresolved.Coords;
			BecomeParent(this.ownerUnresolved);
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
				children.Add(GetValidVersion(ownerUnresolved, owner));
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
				childrenNames.Add("owner");
				childrenNames.Add("member");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<DeclNode> ownerResolver =
				new DeclarationResolver<DeclNode>(typeof(DeclNode));
		private static readonly DeclarationResolver<MemberDeclNode> memberResolver =
				new DeclarationResolver<MemberDeclNode>(typeof(MemberDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
			 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
			 * 3) resolve now complete/correct right hand side identifier into its declaration */
			bool res = FixupDefinition(ownerUnresolved, ownerUnresolved.Scope);
			if(!res)
				return false;

			bool successfullyResolved = true;
			owner = ownerResolver.Resolve(ownerUnresolved, this);
			successfullyResolved = owner != null && successfullyResolved;
			bool ownerResolveResult = owner != null && owner.Resolve();

			if(!ownerResolveResult)
			{
				// member can not be resolved due to inaccessible owner
				return false;
			}

			TypeNode ownerType = owner.DeclType;

			if(owner is NodeDeclNode || owner is EdgeDeclNode)
			{
				if(ownerType is ScopeOwner)
				{
					ScopeOwner o = (ScopeOwner)ownerType;
					o.FixupDefinition(memberUnresolved);
					member = memberResolver.Resolve(memberUnresolved, this);
					successfullyResolved = member != null && successfullyResolved;
				}
				else
				{
					ReportError("Left hand side of '.' does not own a scope (in " + this + ").");
					successfullyResolved = false;
				}
			}
			else if(owner is VarDeclNode)
			{
				member = Resolver.ResolveMember(ownerType, memberUnresolved);
				if(member == null)
					successfullyResolved = false;
			}
			else
			{
				ReportError("Left hand side of '.' is neither a node nor an edge, nor a variable (in " + this + ").");
				successfullyResolved = false;
			}

			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.DeclaredCharacter.getDecl() "/>
		public virtual DeclNode Decl
		{
			get
			{
				return MemberDecl;
			}
		}

		public virtual MemberDeclNode MemberDecl
		{
			get
			{
				Debug.Assert(IsResolved());

				return member is MemberDeclNode ? (MemberDeclNode)member : null;
			}
		}

		public virtual DeclNode Owner
		{
			get
			{
				Debug.Assert(IsResolved());

				return owner;
			}
		}

		public virtual bool IsMatchAssignment()
		{
			Debug.Assert(IsResolved());

			return !(member is MemberDeclNode);
		}

		public virtual bool IsTransientObjectAssignment()
		{
			Debug.Assert(IsResolved());

			return owner.DeclType is InternalTransientObjectTypeNode;
		}

		public virtual DeclNode Member
		{
			get
			{
				Debug.Assert(IsResolved());

				return member;
			}
		}

		protected internal override IR ConstructIR()
		{
			Entity ownerIR = owner.CheckIR(typeof(Entity));
			Entity memberIR = member.CheckIR(typeof(Entity));
			return new Qualification(ownerIR, memberIR);
		}

		public static string KindStr
		{
			get
			{
				return "qualified identifier";
			}
		}

		public override string ToString()
		{
			return ownerUnresolved + "." + memberUnresolved;
		}
	}

}
