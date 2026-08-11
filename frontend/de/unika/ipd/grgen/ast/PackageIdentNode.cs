/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast
{

	using System;
	using System.Collections.Generic;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;

	/// <summary>
	/// AST node that represents an Identifier in a package (name that appears within the specification)
	/// </summary>
	public class PackageIdentNode : IdentNode
	{
		static PackageIdentNode()
		{
			SetClassName(typeof(PackageIdentNode), "package identifier");
		}

		/// <summary>
		/// Occurrence of the package identifier owning the base identifier. </summary>
		public Symbol.Occurrence owningPackage;

		/// <summary>
		/// The declaration of the package owning the base identifier. </summary>
		protected internal DeclNode ownerDecl = DeclNode.Invalid;

		/// <summary>
		/// Make a new identifier node at a symbol's occurrence. </summary>
		/// <param name="owningPackage"> The occurrence of the symbol of the package owning the identifier. </param>
		/// <param name="occ"> The occurrence of the symbol of the identifier. </param>
		public PackageIdentNode(Symbol.Occurrence owningPackage, Symbol.Occurrence occ)
			: base(occ)
		{
			this.owningPackage = owningPackage;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				// no children
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
				// no children
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			// there must be exactly one definition
			return base.CheckLocal() && (OwnerSymbol.ToString().Equals("global") || OwnerSymDef.IsValid());
		}

		private Symbol.Definition OwnerSymDef
		{
			get
			{
				if(owningPackage.Definition == null
					|| !owningPackage.Definition.IsValid())
				{
					Symbol.Definition def = owningPackage.Scope.GetCurrDef(OwnerSymbol);
					if(def.IsValid())
						OwnerSymDef = def;
				}
				return owningPackage.Definition;
			}
			set
			{
				owningPackage.Definition = value;
			}
		}


		public virtual DeclNode OwnerDecl
		{
			get
			{
				Symbol.Definition def = OwnerSymDef;

				if(def.IsValid())
				{
					if(def.Node == this)
						return decl;
					else
						return def.Node.Decl;
				}
				else
					return DeclNode.GetInvalid(this);
			}
		}

		public virtual Symbol OwnerSymbol
		{
			get
			{
				return owningPackage.Symbol;
			}
		}

		public override DeclNode Decl
		{
			get
			{
				Resolver<BaseNode>.ResolveOwner(this);
				if(OwnerSymbol.ToString().Equals("global"))
					FixupDefinition(this, Scope.Root, true);
				return base.Decl;
			}
		}

		public override string ToString()
		{
			return owningPackage.Symbol.ToString() + "::" + occ.Symbol.ToString();
		}

		public static string KindStr
		{
			get
			{
				return "package-prefixed identifier";
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeInfo()"/>
		protected internal override string ExtraNodeInfo()
		{
			return "package: " + owningPackage + "occurrence: " + occ + "\ndefinition: " + SymDef;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"
		/// Package ident nodes are resolved to their targeted concept, the owning package is ignored thereafter./>
		protected internal override IR ConstructIR()
		{
			throw new Exception("internal compiler error");
		}
	}

}
