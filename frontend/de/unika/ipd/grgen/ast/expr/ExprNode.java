/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import java.awt.Color;
import java.util.Set;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.NullTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Base class for all AST nodes representing expressions.
 */
public abstract class ExprNode extends BaseNode
{
	static {
		setName(ExprNode.class, "expression");
	}

	private static final ExprNode INVALID = new InvalidExprNode();

	/**
	 * Make a new expression
	 */
	public ExprNode(Coords coords)
	{
		super(coords);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	public static ExprNode getInvalid()
	{
		return INVALID;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	@Override
	public Color getNodeColor()
	{
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
	protected ExprNode adjustType(TypeNode tgt)
	{
		TypeNode src = getType();

		if(src.isEqual(tgt)
				|| src instanceof NodeTypeNode || src instanceof EdgeTypeNode) {
			return this;
		}

		if((tgt instanceof MatchTypeNode || tgt instanceof DefinedMatchTypeNode)
				&& src instanceof NullTypeNode) {
			return this;
		}

		if(src.isCompatibleTo(tgt)) {
			return new CastNode(getCoords(), tgt, this, this);
		}

		/* in general we would have to compute a shortest path in the conceptual
		 * compatibility graph. But as it is very small we do it shortly
		 * and nicely with this little piece of code finding a compatibility
		 * with only one indirection */
		for(TypeNode t : src.getCompatibleToTypes()) {
			if(t.isCompatibleTo(tgt) && t != BasicTypeNode.untypedType) {
				return new CastNode(getCoords(), tgt, new CastNode(getCoords(), t, this, this), this);
			}
		}

		return ConstNode.getInvalid();
	}

	public ExprNode adjustType(TypeNode targetType, Coords errorCoords)
	{
		ExprNode expr = adjustType(targetType);

		if(expr == ConstNode.getInvalid()) {
			String msg;
			if(getType().isCastableTo(targetType)) {
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
	public ExprNode evaluate()
	{
		return this;
	}

	public boolean noDefElement(String containingConstruct)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noDefElement(containingConstruct);
			else if(child instanceof CollectNode<?>)
				res &= ((CollectNode<?>)child).noDefElement(containingConstruct);
		}
		return res;
	}

	public boolean noIteratedReference(String containingConstruct)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noIteratedReference(containingConstruct);
			else if(child instanceof CollectNode<?>)
				res &= ((CollectNode<?>)child).noIteratedReference(containingConstruct);
		}
		return res;
	}

	public boolean iteratedNotReferenced(String iterName)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).iteratedNotReferenced(iterName);
			else if(child instanceof CollectNode<?>)
				res &= ((CollectNode<?>)child).iteratedNotReferenced(iterName);
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

	protected boolean checkCopyConstructorTypes(TypeNode declaredType, TypeNode givenType, String containerType,
			String amendment)
	{
		if(declaredType instanceof NodeTypeNode && !(givenType instanceof NodeTypeNode)) {
			reportError(containerType + " copy constructor of node type expects " + containerType + " of node type"
					+ amendment);
			return false;
		}
		if(declaredType instanceof EdgeTypeNode && !(givenType instanceof EdgeTypeNode)) {
			reportError(containerType + " copy constructor of edge type expects " + containerType + " of edge type"
					+ amendment);
			return false;
		}
		if(!(declaredType instanceof NodeTypeNode) && !(declaredType instanceof EdgeTypeNode)) {
			if(givenType instanceof NodeTypeNode || givenType instanceof EdgeTypeNode) {
				reportError(containerType + " copy constructor of non-node and non-edge type cannot be given a "
						+ containerType + " of node or edge type" + amendment);
				return false;
			}
			if(!declaredType.isEqual(givenType)) {
				reportError(containerType + " copy constructor non-node/edge type not equal to " + containerType
						+ " of given type" + amendment);
				return false;
			}
		}
		return true;
	}

	// returns elements used/referenced by the expression
	public void collectElements(Set<ConstraintDeclNode> elements)
	{
		assert isResolved();
		if(this instanceof DeclExprNode) {
			ConstraintDeclNode decl = ((DeclExprNode)this).getConstraintDeclNode();
			if(decl != null)
				elements.add(decl);
		}
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode) {
				((ExprNode)child).collectElements(elements);
			} else if(child instanceof CollectNode<?>) {
				CollectNode<? extends BaseNode> collectNode = (CollectNode<?>)child;
				for(BaseNode grandchild : collectNode.getChildren()) {
					if(grandchild instanceof ExprNode)
						((ExprNode)grandchild).collectElements(elements);
				}
			}
		}
	}

	// returns elements that are potentially resulting from the expression
	// (not all potentially resulting elements are returned, this is only an approximation
	// by the directly resulting element and elements resulting from the condition operator cases)
	public void getPotentiallyResultingElements(Set<ConstraintDeclNode> elements)
	{
		assert isResolved();
		if(this instanceof DeclExprNode) {
			ConstraintDeclNode decl = ((DeclExprNode)this).getConstraintDeclNode();
			if(decl != null)
				elements.add(decl);
		}
		if(this instanceof ArithmeticOperatorNode) {
			ArithmeticOperatorNode operator = (ArithmeticOperatorNode)this;
			if(operator.getOperator() == OperatorDeclNode.Operator.COND) {
				operator.getChildren().get(1).getPotentiallyResultingElements(elements);
				operator.getChildren().get(2).getPotentiallyResultingElements(elements);
			}
		}
	}
}
