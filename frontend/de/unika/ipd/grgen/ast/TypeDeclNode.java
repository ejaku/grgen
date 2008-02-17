/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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

	DeclaredTypeNode type;
	
	public TypeDeclNode(IdentNode i, BaseNode t) {
		super(i, t);

		// Set the declaration of the declared type node to this node.
		if(t instanceof DeclaredTypeNode) {
			((DeclaredTypeNode) t).setDecl(this);
		}
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		return childrenNames;
	}

	DeclarationTypeResolver<DeclaredTypeNode> typeResolver = new DeclarationTypeResolver<DeclaredTypeNode>(DeclaredTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this); 

		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return checkNoArbitraryEdgeChildren() & checkNoConflictionEdgeParents();
	}

	/**
	 * Checks whether an edge class extends a directed and an undirected edge
	 * class.
	 * 
	 * @return Check pass without an error.
	 */
	private boolean checkNoConflictionEdgeParents()
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
	    	reportError("An edge class can't extend a directed and a undirected edge class");
	    	return false;
	    }
	    
	    return true;
    }

	/**
	 * Only Edge and UEdge should extends AEdge.
	 * 
	 * @return Whether this type is not an illegal extend of AEdge.
	 */
	private boolean checkNoArbitraryEdgeChildren()
    {
	    if (!(type instanceof EdgeTypeNode)) {
	    	return true;
	    }
	    
	    EdgeTypeNode edgeType = (EdgeTypeNode) type;
		
		boolean extendAEdge = false;
	    for (InheritanceTypeNode inh : edgeType.getDirectSuperTypes()) {
	        if (inh instanceof ArbitraryEdgeTypeNode) {
	        	extendAEdge = true;
	        }
        }
	    
	    if (!extendAEdge) {
	    	return true;
	    }
	    
	    if (!ident.getNodeLabel().equals("UEdge")
	    	&& !ident.getNodeLabel().equals("Edge")) {
	    	reportError("Illegal extension of AEdge");
	    	return false;
	    }
		return true;
    }

	/**
	 * A type declaration returns the declared type
	 * as result.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
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
