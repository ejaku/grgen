/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;

/**
 * Declaration of a type.
 */
public class TypeDeclNode extends DeclNode {
	static {
		setName(TypeDeclNode.class, "type declaration");
	}

	private DeclaredTypeNode type;

	public TypeDeclNode(IdentNode i, BaseNode t) {
		super(i, t);

		// Set the declaration of the declared type node to this node.
		if(t instanceof DeclaredTypeNode) {
			((DeclaredTypeNode) t).setDecl(this);
		}
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
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

	private static DeclarationTypeResolver<DeclaredTypeNode> typeResolver = new DeclarationTypeResolver<DeclaredTypeNode>(DeclaredTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return checkNoConflictingEdgeParents();
	}

	/**
	 * Checks whether an edge class extends a directed and an undirected edge
	 * class.
	 *
	 * @return Check pass without an error.
	 */
	private boolean checkNoConflictingEdgeParents()
    {
		if (!(type instanceof EdgeTypeNode)) {
	    	return true;
	    }

	    EdgeTypeNode edgeType = (EdgeTypeNode) type;

		boolean extendEdge = false;
		boolean extendUEdge = false;
	    for (InheritanceTypeNode inh : edgeType.getDirectSuperTypes()) {
	        if (inh instanceof DirectedEdgeTypeNode) {
	        	extendEdge = true;
	        }
	        if (inh instanceof UndirectedEdgeTypeNode) {
	        	extendUEdge = true;
	        }
        }

	    if (extendEdge && extendUEdge) {
	    	reportError("An edge class cannot extend a directed and an undirected edge class");
	    	return false;
	    }
	    if ((type instanceof ArbitraryEdgeTypeNode) && extendEdge) {
	    	reportError("An arbitrary edge class cannot extend a directed edge class");
	    	return false;
	    }
	    if (type instanceof ArbitraryEdgeTypeNode && extendUEdge) {
	    	reportError("An arbitrary edge class cannot extend an undirected edge class");
	    	return false;
	    }
	    if ((type instanceof UndirectedEdgeTypeNode) && extendEdge) {
	    	reportError("An undirected edge class cannot extend a directed edge class");
	    	return false;
	    }
	    if (type instanceof DirectedEdgeTypeNode && extendUEdge) {
	    	reportError("A directed edge class cannot extend an undirected edge class");
	    	return false;
	    }

	    return true;
    }

	/**
	 * A type declaration returns the declared type
	 * as result.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		return getDeclType().getIR();
	}

	public static String getKindStr() {
		return "type declaration";
	}

	public static String getUseStr() {
		return "type";
	}

	@Override
	public DeclaredTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}
}
