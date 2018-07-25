/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;

/**
 * Declaration of a variable.
 */
public class VarDeclNode extends DeclNode {
	private TypeNode type;

	public PatternGraphNode directlyNestingLHSGraph;
	public boolean defEntityToBeYieldedTo;

	ExprNode initialization = null;

	public int context;


	public VarDeclNode(IdentNode id, IdentNode type,
			PatternGraphNode directlyNestingLHSGraph, int context, boolean defEntityToBeYieldedTo) {
		super(id, type);
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		this.context = context;
    }

	public VarDeclNode(IdentNode id, IdentNode type,
			PatternGraphNode directlyNestingLHSGraph, int context) {
		this(id, type, directlyNestingLHSGraph, context, false);
    }

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphNode directlyNestingLHSGraph, int context, boolean defEntityToBeYieldedTo) {
		super(id, type);
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		this.context = context;
	}

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphNode directlyNestingLHSGraph, int context) {
		this(id, type, directlyNestingLHSGraph, context, false);
	}

	/** Get an invalid var declaration. */
	public static final VarDeclNode getInvalidVar(PatternGraphNode directlyNestingLHSGraph, int context) {
		return new VarDeclNode(IdentNode.getInvalid(), IdentNode.getInvalid(), directlyNestingLHSGraph, context);
	}

	/** sets an expression to be used to initialize the variable */
	public void setInitialization(ExprNode initialization)
	{
		this.initialization = initialization;
	}

	/** returns children of this node */
	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		if(initialization!=null) children.add(initialization);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		if(initialization!=null) childrenNames.add("initialization expression");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> declOfTypeResolver = 
		new DeclarationResolver<DeclNode>(DeclNode.class);

	@Override
	protected boolean resolveLocal() {
		// Type was already known at construction?
		if(type != null) return true;

		DeclNode typeDecl = declOfTypeResolver.resolve(typeUnresolved, this);
		if(typeDecl instanceof InvalidDeclNode) {
			typeUnresolved.reportError("Unknown type: \"" + typeUnresolved + "\"");
			return false;
		}
		if(!typeDecl.resolve()) {
			return false;
		}
		type = typeDecl.getDeclType();
		return type != null;
	}

	@Override
	protected boolean checkLocal() {
		if(initialization==null)
			return true;

		TypeNode targetType = getDeclType();
		TypeNode exprType = initialization.getType();

		if (exprType.isEqual(targetType))
			return true;

		initialization = becomeParent(initialization.adjustType(targetType, getCoords()));
		return initialization != ConstNode.getInvalid();
	}

	/** @return The type node of the declaration */
	@Override
	public TypeNode getDeclType() {
		assert isResolved() : this + " was not resolved";
		return type;
	}

	public static String getKindStr() {
		return "variable declaration";
	}

	public static String getUseStr() {
		return "variable";
	}

	/**
	 * Get the IR object correctly casted.
	 * @return The Variable IR object.
	 */
	public Variable getVariable() {
		return checkIR(Variable.class);
	}

	@Override
	protected IR constructIR() {
		Variable var = new Variable("Var", getIdentNode().getIdent(), type.getType(), 
				defEntityToBeYieldedTo,
				directlyNestingLHSGraph!=null ? directlyNestingLHSGraph.getGraph() : null,
				context);
		
		setIR(var);
		
		if(initialization!=null) {
			initialization = initialization.evaluate();
			var.setInitialization(initialization.checkIR(Expression.class));
		}

		return var;
	}
}

