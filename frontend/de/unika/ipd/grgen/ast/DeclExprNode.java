/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.MemberAnyResolver;
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

	BaseNode declUnresolved;
	MemberDeclNode declMember;
	QualIdentNode qualIdent;
	VarDeclNode declVar;
	ExecVarDeclNode declExecVar;
	ConstraintDeclNode declElem;

	DeclaredCharacter validVersion;

	/**
	 * Make a new declaration expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(BaseNode declCharacter) {
		super(declCharacter.getCoords());
		this.declUnresolved = declCharacter;
		this.validVersion = (DeclaredCharacter) declCharacter;
		becomeParent(this.declUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add((BaseNode) validVersion);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("decl");
		return childrenNames;
	}

	private static MemberAnyResolver<DeclaredCharacter> memberResolver = new MemberAnyResolver<DeclaredCharacter>();

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		if(!memberResolver.resolve(declUnresolved)) return false;

		declMember    = memberResolver.getResult(MemberDeclNode.class);
		qualIdent     = memberResolver.getResult(QualIdentNode.class);
		declVar       = memberResolver.getResult(VarDeclNode.class);
		declExecVar   = memberResolver.getResult(ExecVarDeclNode.class);
		declElem      = memberResolver.getResult(ConstraintDeclNode.class);

		validVersion  = memberResolver.getResult();

		return memberResolver.finish();
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	public TypeNode getType() {
		return validVersion.getDecl().getDeclType();
	}

	/**
	 * Gets the ConstraintDeclNode this DeclExprNode resolved to, or null if it is something else.
	 */
	public ConstraintDeclNode getConstraintDeclNode() {
		assert isResolved();
		return declElem;
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	public ExprNode evaluate() {
		ExprNode res = this;
		DeclNode decl = validVersion.getDecl();

		if(decl instanceof EnumItemNode)
			res = ((EnumItemNode) decl).getValue();

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		BaseNode decl = (BaseNode) validVersion;
		if(decl instanceof MemberDeclNode)
			return new MemberExpression(decl.checkIR(Entity.class));
		else if(decl instanceof VarDeclNode)
			return new VariableExpression(decl.checkIR(Variable.class));
		else if(decl instanceof ExecVarDeclNode)
			return new ExecVariableExpression(decl.checkIR(ExecVariable.class));
		else if(decl instanceof ConstraintDeclNode)
			return new GraphEntityExpression((GraphEntity) decl.getIR());
		else
			return decl.getIR();
	}
}

