/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>
namespace de.unika.ipd.grgen.ast
{

using System.Collections.Generic;

using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Ident = de.unika.ipd.grgen.ir.Ident;
using Symbol = de.unika.ipd.grgen.parser.Symbol;
using Annotated = de.unika.ipd.grgen.util.Annotated;
using Annotations = de.unika.ipd.grgen.util.Annotations;
using EmptyAnnotations = de.unika.ipd.grgen.util.EmptyAnnotations;

/// <summary>
/// AST node that represents an Identifier (name that appears within the specification)
/// children: none
/// </summary>
public class IdentNode : BaseNode, DeclaredCharacter, Annotated
{
	static IdentNode()
	{
		SetClassName(typeof(IdentNode), "identifier");
	}

	/// <summary>
	/// The annotations. </summary>
	protected internal Annotations annotations = EmptyAnnotations.Get();

	/// <summary>
	/// Occurrence of the identifier. </summary>
	public Symbol.Occurrence occ;

	/// <summary>
	/// The declaration associated with this identifier. </summary>
	protected internal DeclNode decl = DeclNode.Invalid;

	protected internal static readonly IdentNode INVALID = new IdentNode(Symbol.Definition.Invalid);

	/// <summary>
	/// Get an invalid ident node. </summary>
	/// <returns> An invalid ident node. </returns>
	public static IdentNode Invalid
	{
		get
		{
			return INVALID;
		}
	}

	/// <summary>
	/// Make a new identifier node at a symbols's definition. </summary>
	/// <param name="def"> The definition of the symbol. </param>
	public IdentNode(Symbol.Definition def)
		: this((Symbol.Occurrence)def)
	{
		def.Node = this;
	}

	/// <summary>
	/// Make a new identifier node at a symbol's occurrence. </summary>
	/// <param name="occ"> The occurrence of the symbol. </param>
	public IdentNode(Symbol.Occurrence occ)
		: base(occ.Coords)
	{
		this.occ = occ;
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

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		// there must be exactly one definition
		bool isValid = SymDef.IsValid();
		return isValid;
	}

	/// <summary>
	/// Get the symbol definition of this identifier </summary>
	/// <seealso cref="Symbol.Definition"/>
	/// <returns> The symbol definition. </returns>
	public virtual Symbol.Definition SymDef
	{
		get
		{
			if(occ.Definition == null)
			{
				// I don't now why this is needed, it feels like a hack, but it works
				Symbol.Definition def = occ.Scope.GetCurrDef(Symbol);
				if(def.IsValid())
					SymDef = def;
			}
			return occ.Definition;
		}
		set
		{
			occ.Definition = value;
		}
	}


	/// <summary>
	/// set the declaration node for this ident node. Each ident node
	/// declares an entity. To resolve this declared entity from the name,
	/// an ident node (which gets the name from the symbol defined
	/// by the symbol definition) has a declaration as its only child. </summary>
	/// <param name="n"> The declaration this ident represents. </param>
	/// <returns> For convenience, this method returns <code>this</code>. </returns>
	public virtual IdentNode setDecl(DeclNode n)
	{
		decl = n;
		return this;
	}

	/// <summary>
	/// Get the declaration corresponding to this node. </summary>
	/// <seealso cref=".setDecl() for a detailed description."/>
	/// <returns> The declaration this node represents </returns>
	public virtual DeclNode Decl
	{
		get
		{
			Symbol.Definition def = SymDef;

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

	/// <summary>
	/// Get the symbol of the identifier. </summary>
	/// <returns> The symbol. </returns>
	public virtual Symbol Symbol
	{
		get
		{
			return occ.Symbol;
		}
	}

	public override string NodeLabel
	{
		get
		{
			return ToString();
		}
	}

	/// <summary>
	/// The string representation for this node.
	/// For an identifier, this is the string of the symbol, the identifier represents.
	/// </summary>
	public override string ToString()
	{
		return occ.Symbol.ToString();
	}

	public static string KindStr
	{
		get
		{
			return "identifier";
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor()"/>
	public override Color NodeColor
	{
		get
		{
			return Color.ORANGE;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeInfo()"/>
	protected internal override string ExtraNodeInfo()
	{
		return "occurrence: " + occ + "\ndefinition: " + SymDef;
	}

	/// <summary>
	/// Get the current occurrence of this identifier.
	/// Each time this ident node is reused in the parser (rule identUse)
	/// the current occurrence changes. </summary>
	/// <returns> The current occurrence. </returns>
	public virtual Symbol.Occurrence CurrOcc
	{
		get
		{
			return occ;
		}
	}

	/// <summary>
	/// Get the IR object.
	/// This is an ident here. </summary>
	/// <returns> The IR object. </returns>
	public virtual Ident IRIdent
	{
		get
		{
			return CheckIR(typeof(Ident));
		}
	}

	/// <summary>
	/// Construct the ir object. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		Symbol.Definition def = SymDef;
		return Ident.Get(ToString(), def, Annotations);
	}

	/// <summary>
	/// Get the annotations of this identifier. </summary>
	/// <returns> The annotations of this identifier. </returns>
	public virtual Annotations Annotations
	{
		get
		{
			return annotations;
		}
		set // Set annotations for this ident node.
		{
		annotations = value;
		}
	}

}

}
