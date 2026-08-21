/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.decl
{

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using DeclaredCharacter = de.unika.ipd.grgen.ast.DeclaredCharacter;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using ArbitraryEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
	using DirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
	using UndirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Color = de.unika.ipd.grgen.util.Color;

	/// <summary>
	/// Base class for all AST nodes representing declarations.
	/// children: IDENT:IdentNode TYPE:
	/// </summary>
	public abstract class DeclNode : BaseNode, DeclaredCharacter
	{
		static DeclNode()
		{
			SetClassName(typeof(DeclNode), "declaration");
			invalidDecl = new InvalidDeclNode(IdentNode.Invalid);
		}

		public IdentNode ident;

		public BaseNode typeUnresolved;

		/// <summary>
		/// An invalid declaration. </summary>
		private static readonly DeclNode invalidDecl;

		/// <summary>
		/// Get an invalid declaration. </summary>
		public static DeclNode Invalid
		{
			get
			{
				return invalidDecl;
			}
		}

		/// <summary>
		/// Get an invalid declaration for an IdentNode. </summary>
		public static DeclNode GetInvalid(IdentNode id)
		{
			return new InvalidDeclNode(id);
		}

		/// <summary>
		/// Create a new declaration node </summary>
		/// <param name="n"> The identifier that is declared </param>
		/// <param name="t"> The type with which it is declared </param>
		protected internal DeclNode(IdentNode n, BaseNode t)
			: base(n.Coords)
		{
			n.Decl = this;
			this.ident = n;
			BecomeParent(this.ident);
			this.typeUnresolved = t;
			BecomeParent(this.typeUnresolved);
		}

		/// <returns> The ident node of the declaration </returns>
		public virtual IdentNode Ident
		{
			get
			{
				return ident;
			}
		}

		/// <returns> The type node of the declaration </returns>
		public abstract TypeNode DeclType {get;}

		/// <seealso cref="de.unika.ipd.grgen.ast.DeclaredCharacter.getDecl() "/>
		public virtual DeclNode Decl
		{
			get
			{
				return this;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeColor() "/>
		public override Color NodeColor
		{
			get
			{
				return Color.BLUE;
			}
		}

		public string EmptyWhenAnonymous(string str)
		{
			return Ident.CurrOcc.IsAnonymous() ? "" : str;
		}

		public string EmptyWhenAnonymousPostfix(string prefix)
		{
			return Ident.CurrOcc.IsAnonymous() ? "" : prefix + Ident;
		}

		public string EmptyWhenAnonymousInParenthesis(string prefix)
		{
			return Ident.CurrOcc.IsAnonymous() ? "" : prefix + "(" + Ident + ")";
		}

		public string DotOrArrowWhenAnonymous()
		{
			if(Ident.CurrOcc.IsAnonymous() && this is EdgeDeclNode)
			{
				EdgeDeclNode edge = (EdgeDeclNode)this;
				if(edge.DeclType is ArbitraryEdgeTypeNode)
					return "?--?";
				else if(edge.DeclType is DirectedEdgeTypeNode)
					return "-->";
				else if(edge.DeclType is UndirectedEdgeTypeNode)
					return "--";
			}
			return Ident.CurrOcc.IsAnonymous() ? "." : Ident.ToString();
		}

		public virtual Entity IREntity
		{
			get
			{
				return CheckIR<Entity>(typeof(Entity));
			}
		}

		public static string KindStr
		{
			get
			{
				return "declaration";
			}
		}

		public override string ToString()
		{
			return ident.ToString();
		}
	}

}
