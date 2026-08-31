/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.model.type
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using CompoundTypeNode = de.unika.ipd.grgen.ast.type.CompoundTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using PackageType = de.unika.ipd.grgen.ir.model.type.PackageType;

	/// <summary>
	/// A package type AST node.
	/// </summary>
	public class PackageTypeNode : CompoundTypeNode
	{
		static PackageTypeNode()
		{
			SetClassName(typeof(PackageTypeNode), "package type");
		}

		private CollectNode<IdentNode> declsUnresolved;
		protected internal CollectNode<TypeDeclNode> decls;

		public PackageTypeNode(CollectNode<IdentNode> decls)
		{
			this.declsUnresolved = decls;
			BecomeParent(this.declsUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(declsUnresolved, decls));
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
				childrenNames.Add("decls");
				return childrenNames;
			}
		}

		private static CollectResolver<TypeDeclNode> declsResolver = new CollectResolver<TypeDeclNode>(
				new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			decls = declsResolver.Resolve(declsUnresolved, this);
			return decls != null;
		}

		public virtual CollectNode<TypeDeclNode> TypeDecls
		{
			get
			{
				return decls;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			Ident id = Ident.CheckIR<Ident>(typeof(Ident));
			PackageType pt = new PackageType(id);
			foreach(TypeDeclNode typeDecl in decls.ChildrenExact)
				pt.AddType(typeDecl.DeclType.IRType);
			return pt;
		}

		public override string ToString()
		{
			return "package " + Ident;
		}

		public static string KindStr
		{
			get
			{
				return "package";
			}
		}
	}

}
