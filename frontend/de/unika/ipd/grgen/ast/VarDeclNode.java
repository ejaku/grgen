/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;

/**
 * Declaration of a variable.
 */
public class VarDeclNode extends DeclNode {
	private static final DeclarationResolver<DeclNode> declOfTypeResolver = new DeclarationResolver<DeclNode>(DeclNode.class);

	private TypeNode type;

	protected PatternGraphNode directlyNestingLHSGraph;
	
	int context;
	

	public VarDeclNode(IdentNode id, IdentNode type, 
			PatternGraphNode directlyNestingLHSGraph, int context) {
		super(id, type);
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.context = context;
    }

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphNode directlyNestingLHSGraph, int context) {
		super(id, type);
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.context = context;
	}

	/** returns children of this node */
	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		return childrenNames;
	}

	/**
	 * local resolving of the current node to be implemented by the subclasses, called from the resolve AST walk
	 * @return true, if resolution of the AST locally finished successfully;
	 * false, if there was some error.
	 */
	@Override
	protected boolean resolveLocal() {
		// Type was already known at construction?
		if(type != null) return true;

		DeclNode typeDecl = declOfTypeResolver.resolve(typeUnresolved, this);
		if(typeDecl instanceof InvalidDeclNode) {
			typeUnresolved.reportError("Unknown type: \"" + typeUnresolved + "\"");
			return false;
		}
		type = typeDecl.getDeclType();
		return type != null;
	}

	/**
	 * local checking of the current node to be implemented by the subclasses, called from the check AST walk
	 * @return true, if checking of the AST locally finished successfully;
	 * false, if there was some error.
	 */
	@Override
	protected boolean checkLocal() {
		return true;
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
	protected Variable getVariable() {
		return checkIR(Variable.class);
	}

	@Override
	protected IR constructIR() {
		return new Variable("Var", getIdentNode().getIdent(), type.getType(),
				directlyNestingLHSGraph.getGraph(), context);
	}
}

