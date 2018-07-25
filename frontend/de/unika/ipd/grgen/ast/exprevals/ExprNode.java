/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.awt.Color;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Base class for all AST nodes representing expressions.
 */
public abstract class ExprNode extends BaseNode {
	static {
		setName(ExprNode.class, "expression");
	}

	private static final ExprNode INVALID = new InvalidExprNode();

	/**
	 * Make a new expression
	 */
	public ExprNode(Coords coords) {
		super(coords);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	public static ExprNode getInvalid() {
		return INVALID;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	@Override
	public Color getNodeColor()	{
		return Color.PINK;
	}

	/**
	 * Get the type of the expression.
	 * @return The type of this expression node.
	 */
	public abstract TypeNode getType();

	/**
	 * Adjust the type of the expression.
	 * The type can be adjusted by inserting an implicit cast.
	 * @param type The type the expression should be adjusted to. It must be
	 * compatible with the type of the expression.
	 * @return A new expression, that is of a valid type and represents
	 * this expression, if <code>type</code> was compatible with the type of
	 * this expression, an invalid expression otherwise (one of an error type).
	 */
	protected ExprNode adjustType(TypeNode tgt)	{
		TypeNode src = getType();

		if(src.isEqual(tgt) 
				|| src instanceof NodeTypeNode || src instanceof EdgeTypeNode) {
			return this;
		}
		
		if(tgt instanceof MatchTypeNode && src instanceof NullTypeNode) {
			return this;
		}

		if( src.isCompatibleTo(tgt) ) {
			return new CastNode(getCoords(), tgt, this, this);
		}

		/* in general we would have to compute a shortest path in the conceptual
		 * compatibility graph. But as it is very small we do it shortly
		 * and nicely with this little piece of code finding a compatibility
		 * with only one indirection */
		for (TypeNode t : src.getCompatibleToTypes()) {
			if (t.isCompatibleTo(tgt)) {
				return new CastNode(getCoords(), tgt, new CastNode(getCoords(), t, this, this), this);
			}
		}

		return ConstNode.getInvalid();
	}

	public ExprNode adjustType(TypeNode targetType, Coords errorCoords)
	{
		ExprNode expr = adjustType(targetType);

		if (expr == ConstNode.getInvalid()) {
			String msg;
			if (getType().isCastableTo(targetType)) {
				msg = "Assignment of " + getType() + " to " + targetType + " without a cast";
			} else {
				msg = "Incompatible assignment from " + getType() + " to " + targetType;
			}
			error.error(errorCoords, msg);
			if(getType().toString().equals(targetType.toString()))
				error.warning(errorCoords, "Check package prefix.");
		}
		return expr;
	}

	/**
	 * Tries to simplify this node.
	 * @return The possibly simplified value of the expression.
	 */
	public ExprNode evaluate() {
		return this;
	}

	public boolean noDefElementInCondition() {
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noDefElementInCondition();
			else if(child instanceof CollectNode<?>)
				res &= ((CollectNode<?>)child).noDefElementInCondition();
		}
		return res;
	}
	
	public static IdentNode getEdgeRootOfMatchingDirectedness(ExprNode edgeTypeExpr)
	{
		IdentExprNode ident = (IdentExprNode)edgeTypeExpr;
		TypeNode type = ident.getType();
		if(type.isCompatibleTo(EdgeTypeNode.directedEdgeType))
			return EdgeTypeNode.directedEdgeType.getIdentNode();
		if(type.isCompatibleTo(EdgeTypeNode.undirectedEdgeType))
			return EdgeTypeNode.undirectedEdgeType.getIdentNode();
		return EdgeTypeNode.arbitraryEdgeType.getIdentNode();
	}
	
	public static IdentNode getNodeRoot(ExprNode nodeTypeExpr)
	{
		return NodeTypeNode.nodeType.getIdentNode();
	}
	
	protected boolean checkCopyConstructorTypes(TypeNode declaredType, TypeNode givenType, String containerType, String amendment)
	{
		if(declaredType instanceof NodeTypeNode && !(givenType instanceof NodeTypeNode))
		{
			reportError(containerType + " copy constructor of node type expects " + containerType + " of node type" + amendment);
			return false;
		}
		if(declaredType instanceof EdgeTypeNode && !(givenType instanceof EdgeTypeNode))
		{
			reportError(containerType + " copy constructor of edge type expects " + containerType + " of edge type" + amendment);
			return false;
		}
		if(!(declaredType instanceof NodeTypeNode) && !(declaredType instanceof EdgeTypeNode))
		{
			if(givenType instanceof NodeTypeNode || givenType instanceof EdgeTypeNode) {
				reportError(containerType + " copy constructor of non-node and non-edge type cannot be given a " + containerType + " of node or edge type" + amendment);
				return false;
			}
			if(!declaredType.isEqual(givenType)) {
				reportError(containerType + " copy constructor non-node/edge type not equal to " + containerType + " of given type" + amendment);
				return false;
			}
		}
		return true;
	}
}
