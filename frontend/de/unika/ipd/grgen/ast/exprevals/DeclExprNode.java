/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.ExecVariable;
import de.unika.ipd.grgen.ir.ExecVariableExpression;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.exprevals.GraphEntityExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.MemberExpression;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.exprevals.VariableExpression;

/**
 * An expression that results from a declared identifier.
 */
public class DeclExprNode extends ExprNode {
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
	public DeclExprNode(BaseNode declCharacter) {
		super(declCharacter.getCoords());
		this.declUnresolved = declCharacter;
		this.decl = (DeclaredCharacter) declCharacter;
		becomeParent(this.declUnresolved);
	}

	/**
	 * Make a new declaration expression from an enum expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(EnumExprNode declCharacter) {
		super(declCharacter.getCoords());
		this.declUnresolved = declCharacter;
		this.decl = (DeclaredCharacter) declCharacter;
		becomeParent(this.declUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add((BaseNode) decl);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("decl");
		return childrenNames;
	}

	private static MemberResolver<DeclaredCharacter> memberResolver = new MemberResolver<DeclaredCharacter>();

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		tryfixupDefinition(declUnresolved, declUnresolved.getScope());
		
		if(!memberResolver.resolve(declUnresolved)) return false;

		memberResolver.getResult(MemberDeclNode.class);
		memberResolver.getResult(QualIdentNode.class);
		memberResolver.getResult(VarDeclNode.class);
		memberResolver.getResult(ExecVarDeclNode.class);
		memberResolver.getResult(ConstraintDeclNode.class);
		decl = memberResolver.getResult();

		return memberResolver.finish();
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	@Override
	public TypeNode getType() {
		return decl.getDecl().getDeclType();
	}

	/**
	 * Gets the ConstraintDeclNode this DeclExprNode resolved to, or null if it is something else.
	 */
	public ConstraintDeclNode getConstraintDeclNode() {
		assert isResolved();
		if(decl instanceof ConstraintDeclNode) return (ConstraintDeclNode)decl;
		return null;
	}

	/** returns the node this DeclExprNode was resolved to. */
	public BaseNode getResolvedNode() {
		assert isResolved();
		return (BaseNode)decl;
	}
	
	public boolean isEnumValue() {
		return declUnresolved instanceof EnumExprNode;
	}


	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	public ExprNode evaluate() {
		ExprNode res = this;
		DeclNode declNode = decl.getDecl();

		if(declNode instanceof EnumItemNode)
			res = ((EnumItemNode) declNode).getValue();

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		BaseNode declNode = (BaseNode) decl;
		if(declNode instanceof MemberDeclNode)
			return new MemberExpression(declNode.checkIR(Entity.class));
		else if(declNode instanceof VarDeclNode)
			return new VariableExpression(declNode.checkIR(Variable.class));
		else if(declNode instanceof ExecVarDeclNode)
			return new ExecVariableExpression(declNode.checkIR(ExecVariable.class));
		else if(declNode instanceof ConstraintDeclNode)
			return new GraphEntityExpression((GraphEntity) declNode.getIR());
		else
			return declNode.getIR();
	}
	
	public boolean noDefElementInCondition() {
		if(decl instanceof ConstraintDeclNode) {
			ConstraintDeclNode entity = (ConstraintDeclNode)decl;
			if(entity.defEntityToBeYieldedTo) {
				declUnresolved.reportError("A def entity ("+entity+") can't be accessed from an if");
				return false;
			}
		}
		if(decl instanceof VarDeclNode) {
			VarDeclNode entity = (VarDeclNode)decl;
			if(entity.defEntityToBeYieldedTo) {
				declUnresolved.reportError("A def variable ("+entity+") can't be accessed from an if");
				return false;
			}
		}
		return true;
	}
}

