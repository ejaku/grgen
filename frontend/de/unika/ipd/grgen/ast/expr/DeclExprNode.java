/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.model.decl.EnumItemDeclNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.ExecVariable;
import de.unika.ipd.grgen.ir.ExecVariableExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.MemberExpression;
import de.unika.ipd.grgen.ir.expr.VariableExpression;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * An expression that results from a declared identifier.
 */
public class DeclExprNode extends ExprNode
{
	static {
		setName(DeclExprNode.class, "decl expression");
	}

	public BaseNode declUnresolved; // either EnumExprNode if constructed locally, or IdentNode if constructed from IdentExprNode
	public DeclaredCharacter decl;

	/**
	 * Make a new declaration expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(BaseNode declCharacter)
	{
		super(declCharacter.getCoords());
		this.declUnresolved = declCharacter;
		this.decl = (DeclaredCharacter)declCharacter;
		becomeParent(this.declUnresolved);
	}

	/**
	 * Make a new declaration expression from an enum expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(EnumExprNode declCharacter)
	{
		super(declCharacter.getCoords());
		this.declUnresolved = declCharacter;
		this.decl = declCharacter;
		becomeParent(this.declUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add((BaseNode)decl);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("decl");
		return childrenNames;
	}

	private static MemberResolver<DeclaredCharacter> memberResolver = new MemberResolver<DeclaredCharacter>();

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(!(declUnresolved instanceof PackageIdentNode)) {
			tryFixupDefinition(declUnresolved, declUnresolved.getScope());
		}

		if(!memberResolver.resolve(declUnresolved))
			return false;

		memberResolver.getResult(MemberDeclNode.class);
		memberResolver.getResult(EnumExprNode.class);
		memberResolver.getResult(VarDeclNode.class);
		memberResolver.getResult(ExecVarDeclNode.class);
		memberResolver.getResult(ConstraintDeclNode.class);
		decl = memberResolver.getResult();

		return memberResolver.finish();
	}

	/** @see de.unika.ipd.grgen.ast.expr.ExprNode#getType() */
	@Override
	public TypeNode getType()
	{
		return decl.getDecl().getDeclType();
	}

	/**
	 * Gets the ConstraintDeclNode this DeclExprNode resolved to, or null if it is something else.
	 */
	public ConstraintDeclNode getConstraintDeclNode()
	{
		assert isResolved();
		if(decl instanceof ConstraintDeclNode)
			return (ConstraintDeclNode)decl;
		return null;
	}

	/** returns the node this DeclExprNode was resolved to. */
	public BaseNode getResolvedNode()
	{
		assert isResolved();
		return (BaseNode)decl;
	}

	public boolean isEnumValue()
	{
		return declUnresolved instanceof EnumExprNode;
	}

	/** @see de.unika.ipd.grgen.ast.expr.ExprNode#evaluate() */
	@Override
	public ExprNode evaluate()
	{
		ExprNode res = this;
		DeclNode declNode = decl.getDecl();

		if(declNode instanceof EnumItemDeclNode)
			res = ((EnumItemDeclNode)declNode).getValue();

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		BaseNode declNode = (BaseNode)decl;
		if(declNode instanceof MemberDeclNode)
			return new MemberExpression(declNode.checkIR(Entity.class));
		else if(declNode instanceof VarDeclNode)
			return new VariableExpression(declNode.checkIR(Variable.class));
		else if(declNode instanceof ExecVarDeclNode)
			return new ExecVariableExpression(declNode.checkIR(ExecVariable.class));
		else if(declNode instanceof ConstraintDeclNode)
			return new GraphEntityExpression((GraphEntity)declNode.getIR());
		else
			return declNode.getIR();
	}

	@Override
	public boolean noDefElement(String containingConstruct)
	{
		if(decl instanceof NodeDeclNode) {
			NodeDeclNode node = (NodeDeclNode)decl;
			if(node.defEntityToBeYieldedTo) {
				declUnresolved.reportError("A def node (" + node + ")"
						+ " cannot be accessed from a(n) " + containingConstruct + ".");
				return false;
			}
		}
		if(decl instanceof EdgeDeclNode) {
			EdgeDeclNode edge = (EdgeDeclNode)decl;
			if(edge.defEntityToBeYieldedTo) {
				declUnresolved.reportError("A def edge (" + edge + ")"
						+ " cannot be accessed from a(n) " + containingConstruct + ".");
				return false;
			}
		}
		if(decl instanceof VarDeclNode) {
			VarDeclNode entity = (VarDeclNode)decl;
			if(entity.defEntityToBeYieldedTo && !entity.lambdaExpressionVariable) {
				declUnresolved.reportError("A def variable (" + entity + ")"
						+ " cannot be accessed from a(n) " + containingConstruct + ".");
				return false;
			}
		}
		return true;
	}
}
