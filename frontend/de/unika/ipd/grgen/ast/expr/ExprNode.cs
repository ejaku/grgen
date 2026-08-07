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
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using NullTypeNode = de.unika.ipd.grgen.ast.type.basic.NullTypeNode;
using ObjectTypeNode = de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
using TypeTypeNode = de.unika.ipd.grgen.ast.type.basic.TypeTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// Base class for all AST nodes representing expressions.
/// </summary>
public abstract class ExprNode : BaseNode
{
	static ExprNode()
	{
		SetClassName(typeof(ExprNode), "expression");
	}

	private static readonly ExprNode INVALID = new InvalidExprNode();

	/// <summary>
	/// Make a new expression
	/// </summary>
	public ExprNode(Coords coords)
		: base(coords)
	{
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	public static ExprNode Invalid
	{
		get
		{
		return INVALID;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor()"/>
	public override Color NodeColor
	{
		get
		{
		return Color.PINK;
		}
	}

	/// <summary>
	/// Get the type of the expression. </summary>
	/// <returns> The type of this expression node. </returns>
	public abstract TypeNode Type {get;}

	/// <summary>
	/// Adjust the type of the expression.
	/// The type can be adjusted by inserting an implicit cast. </summary>
	/// <param name="type"> The type the expression should be adjusted to. It must be
	/// compatible with the type of the expression. </param>
	/// <returns> A new expression, that is of a valid type and represents
	/// this expression, if <code>type</code> was compatible with the type of
	/// this expression, an invalid expression otherwise (one of an error type). </returns>
	protected internal virtual ExprNode AdjustType(TypeNode tgt)
	{
		TypeNode src = Type;

		if(src.IsEqual(tgt)
				|| src is NodeTypeNode && tgt is TypeTypeNode
				|| src is EdgeTypeNode && tgt is TypeTypeNode)
			return this;

		if(tgt is MatchTypeNode
				&& src is NullTypeNode)
			return this;

		if(src.IsCompatibleTo(tgt))
			return new CastNode(Coords, tgt, this, this);

		/* in general we would have to compute a shortest path in the conceptual
		 * compatibility graph. But as it is very small we do it shortly
		 * and nicely with this little piece of code finding a compatibility
		 * with only one indirection */
		foreach(TypeNode t in src.CompatibleToTypes)
		{
			if(t.IsCompatibleTo(tgt) && t != BasicTypeNode.untypedType)
				return new CastNode(Coords, tgt, new CastNode(Coords, t, this, this), this);
		}

		if(src is ExternalObjectTypeNode && tgt is ObjectTypeNode)
			return new CastNode(Coords, tgt, this, this);

		return ConstNode.Invalid;
	}

	public virtual ExprNode AdjustType(TypeNode targetType, Coords errorCoords)
	{
		ExprNode expr = AdjustType(targetType);

		if(expr == ConstNode.Invalid)
		{
			TypeNode src = Type;
			string msg;
			if(src.IsCastableTo(targetType))
			{
				msg = "Assignment of " + src.ToStringWithDeclarationCoords()
							+ " to " + targetType.ToStringWithDeclarationCoords()
							+ " without a cast.";
			}
			else
			{
				msg = "Incompatible assignment from " + src.ToStringWithDeclarationCoords()
							+ " to " + targetType.ToStringWithDeclarationCoords() + ".";
			}
			error.Error(errorCoords, msg);
			if(src.ToString().Equals(targetType.ToString()))
				error.Warning(errorCoords, "Check package prefix.");
		}
		return expr;
	}

	/// <summary>
	/// Tries to simplify this node. </summary>
	/// <returns> The possibly simplified value of the expression. </returns>
	public virtual ExprNode Evaluate()
	{
		return this;
	}

	public virtual bool NoDefElement(string containingConstruct)
	{
		bool res = true;
		foreach(BaseNode child in Children)
		{
			if(child is ExprNode)
				res &= ((ExprNode)child).NoDefElement(containingConstruct);
			else if(child is CollectBaseNode)
				res &= ((CollectBaseNode)child).NoDefElement(containingConstruct);
		}
		return res;
	}

	public virtual bool NoIteratedReference(string containingConstruct)
	{
		bool res = true;
		foreach(BaseNode child in Children)
		{
			if(child is ExprNode)
				res &= ((ExprNode)child).NoIteratedReference(containingConstruct);
			else if(child is CollectBaseNode)
				res &= ((CollectBaseNode)child).NoIteratedReference(containingConstruct);
		}
		return res;
	}

	public virtual bool IteratedNotReferenced(string iterName)
	{
		bool res = true;
		foreach(BaseNode child in Children)
		{
			if(child is ExprNode)
				res &= ((ExprNode)child).IteratedNotReferenced(iterName);
			else if(child is CollectBaseNode)
				res &= ((CollectBaseNode)child).IteratedNotReferenced(iterName);
		}
		return res;
	}

	public static IdentNode GetEdgeRootOfMatchingDirectedness(ExprNode edgeTypeExpr)
	{
		IdentExprNode ident = (IdentExprNode)edgeTypeExpr;
		TypeNode type = ident.Type;
		if(type.IsCompatibleTo(EdgeTypeNode.directedEdgeType))
			return EdgeTypeNode.directedEdgeType.GetIdent();
		if(type.IsCompatibleTo(EdgeTypeNode.undirectedEdgeType))
			return EdgeTypeNode.undirectedEdgeType.GetIdent();
		return EdgeTypeNode.arbitraryEdgeType.GetIdent();
	}

	public static IdentNode EdgeRoot
	{
		get
		{
		return EdgeTypeNode.arbitraryEdgeType.GetIdent();
		}
	}

	public static IdentNode GetNodeRoot(ExprNode nodeTypeExpr)
	{
		return NodeTypeNode.nodeType.GetIdent();
	}

	public static IdentNode NodeRoot
	{
		get
		{
		return NodeTypeNode.nodeType.GetIdent();
		}
	}

	protected internal virtual bool CheckCopyConstructorTypes(TypeNode declaredType, TypeNode givenType,
			string containerType, bool isKeyType)
	{
		string containerCompartmentAmendment = "";
		if(containerType.Equals("map"))
			containerCompartmentAmendment = isKeyType ? " key" : " value";

		string errorMessage = "The " + containerType + " copy constructor expects a(n) " + containerType + containerCompartmentAmendment + " of type " + declaredType.TypeName
				+ " but is given a(n) " + containerType + containerCompartmentAmendment + " of type " + givenType.TypeName;

		if(declaredType is NodeTypeNode && !(givenType is NodeTypeNode))
		{
			ReportError(errorMessage + " (which is not a node type).");
			return false;
		}
		if(declaredType is EdgeTypeNode && !(givenType is EdgeTypeNode))
		{
			ReportError(errorMessage + " (which is not an edge type).");
			return false;
		}
		if(!(declaredType is NodeTypeNode) && !(declaredType is EdgeTypeNode))
		{
			if(givenType is NodeTypeNode || givenType is EdgeTypeNode)
			{
				ReportError(errorMessage + ".");
				return false;
			}
			if(!declaredType.IsEqual(givenType))
			{
				ReportError(errorMessage + ".");
				return false;
			}
		}
		return true;
	}

	// returns elements used/referenced by the expression
	public virtual void CollectElements(ISet<ConstraintDeclNode> elements)
	{
		Debug.Assert(IsResolved());
		if(this is DeclExprNode)
		{
			ConstraintDeclNode decl = ((DeclExprNode)this).ConstraintDecl;
			if(decl != null)
				elements.Add(decl);
		}
		foreach(BaseNode child in Children)
		{
			if(child is ExprNode)
				((ExprNode)child).CollectElements(elements);
			else if(child is CollectBaseNode)
			{
				CollectBaseNode collectNode = (CollectBaseNode)child;
				foreach(BaseNode grandchild in collectNode.Children)
				{
					if(grandchild is ExprNode)
						((ExprNode)grandchild).CollectElements(elements);
				}
			}
		}
	}

	// returns elements that are potentially resulting from the expression
	// (not all potentially resulting elements are returned, this is only an approximation
	// by the directly resulting element and elements resulting from the condition operator cases)
	public virtual void GetPotentiallyResultingElements(ISet<ConstraintDeclNode> elements)
	{
		Debug.Assert(IsResolved());
		if(this is DeclExprNode)
		{
			ConstraintDeclNode decl = ((DeclExprNode)this).ConstraintDecl;
			if(decl != null)
				elements.Add(decl);
		}
		if(this is ArithmeticOperatorNode)
		{
			ArithmeticOperatorNode @operator = (ArithmeticOperatorNode)this;
			if(@operator.Operator == Operator.COND)
			{
				@operator.ChildrenAsList[1].GetPotentiallyResultingElements(elements);
				@operator.ChildrenAsList[2].GetPotentiallyResultingElements(elements);
			}
		}
	}
}

}
