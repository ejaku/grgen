/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.ExecVariable;
import de.unika.ipd.grgen.ir.ExecVariableExpression;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberExpression;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.VariableExpression;
import java.util.Collection;
import java.util.Vector;

/**
 * An expression that results from a declared identifier.
 */
public class DeclExprNode extends ExprNode {
	static {
		setName(DeclExprNode.class, "decl expression");
	}

	protected BaseNode declUnresolved; // either EnumExprNode if constructed locally, or IdentNode if constructed from IdentExprNode
	protected DeclaredCharacter decl;

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
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add((BaseNode) decl);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("decl");
		return childrenNames;
	}

	private static MemberResolver<DeclaredCharacter> memberResolver = new MemberResolver<DeclaredCharacter>();

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
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

	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	public ExprNode evaluate() {
		ExprNode res = this;
		DeclNode declNode = decl.getDecl();

		if(declNode instanceof EnumItemNode)
			res = ((EnumItemNode) declNode).getValue();

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
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
}

