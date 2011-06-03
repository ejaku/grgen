/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

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

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 * better yet: move it to own pass before resolving
	 */
	public static void tryfixupDefinition(BaseNode elem, Scope scope) {
		if(!(elem instanceof IdentNode)) {
			return;
		}
		IdentNode id = (IdentNode)elem;

		debug.report(NOTE, "try Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else nothing happens as this ident may be referenced in an
		// attribute initialization expression within a node/edge type declaration
		// and attributes from super types are not found in this stage
		// this fixup stuff is crappy as hell
		if(def.isValid()) {
			id.setSymDef(def);
		}
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	@Override
	public TypeNode getType() {
		return decl.getDecl().getDeclType();
	}

	/**
	 * Gets the ConstraintDeclNode this DeclExprNode resolved to, or null if it is something else.
	 */
	protected ConstraintDeclNode getConstraintDeclNode() {
		assert isResolved();
		if(decl instanceof ConstraintDeclNode) return (ConstraintDeclNode)decl;
		return null;
	}

	/** returns the node this DeclExprNode was resolved to. */
	protected BaseNode getResolvedNode() {
		assert isResolved();
		return (BaseNode)decl;
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	protected ExprNode evaluate() {
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
}

