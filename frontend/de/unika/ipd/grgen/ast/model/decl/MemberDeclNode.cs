/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>
namespace de.unika.ipd.grgen.ast.model.decl
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
	using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
	using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
	using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using Checker = de.unika.ipd.grgen.ast.util.Checker;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using SimpleChecker = de.unika.ipd.grgen.ast.util.SimpleChecker;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// A compound type member declaration.
	/// </summary>
	public class MemberDeclNode : DeclNode
	{
		static MemberDeclNode()
		{
			SetClassName(typeof(MemberDeclNode), "member declaration");
		}

		public TypeNode type;
		private bool isConst;
		private BaseNode constInitializer;

		/// <param name="n"> Identifier which declared the member. </param>
		/// <param name="t"> Type with which the member was declared. </param>
		public MemberDeclNode(IdentNode n, BaseNode t, bool isConst)
			: base(n, t)
		{
			this.isConst = isConst;
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
				return childrenNames;
			}
		}

		public virtual bool IsConst()
		{
			return isConst;
		}

		public virtual BaseNode ConstInitializer
		{
			get
			{
				return constInitializer;
			}
			set
			{
				constInitializer = value;
			}
		}


		private static readonly DeclarationTypeResolver<TypeNode> typeResolver =
				new DeclarationTypeResolver<TypeNode>(typeof(TypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			if(typeUnresolved is PackageIdentNode)
				Resolver.ResolveOwner((PackageIdentNode)typeUnresolved);
			else if(typeUnresolved is IdentNode)
				FixupDefinition((IdentNode)typeUnresolved, ((IdentNode)typeUnresolved).Scope.Ident.Scope);
			type = typeResolver.Resolve(typeUnresolved, this);
			return type != null;
		}

		/// <returns> The type node of the declaration </returns>
		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return type;
			}
		}

		private static readonly Checker typeChecker = new SimpleChecker(
				new Type[] {typeof(BasicTypeNode), typeof(EnumTypeNode),
						typeof(InternalObjectTypeNode), typeof(InternalTransientObjectTypeNode), typeof(ExternalObjectTypeNode),
						typeof(NodeTypeNode), typeof(EdgeTypeNode),
						typeof(MapTypeNode), typeof(SetTypeNode), typeof(ArrayTypeNode), typeof(DequeTypeNode)});

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return typeChecker.Check(type, error);
		}

		protected internal override IR ConstructIR()
		{
			Type type = DeclType.CheckIR(typeof(Type));
			return new Entity("entity", Ident.IRIdent, type, isConst, false, 0);
		}

		public static string KindStr
		{
			get
			{
				return "member";
			}
		}
	}

}
