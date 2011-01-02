/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Map;
import java.util.Vector;
import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

public abstract class EdgeTypeNode extends InheritanceTypeNode {
	static {
		setName(EdgeTypeNode.class, "edge type");
	}

	@SuppressWarnings("unchecked")
	private static final CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
			new DeclarationResolver<BaseNode>(MemberDeclNode.class,
					MemberInitNode.class, MapInitNode.class, SetInitNode.class,
					ConstructorDeclNode.class));

	private static final CollectResolver<EdgeTypeNode> extendResolver = new CollectResolver<EdgeTypeNode>(
    		new DeclarationTypeResolver<EdgeTypeNode>(EdgeTypeNode.class));

	private CollectNode<EdgeTypeNode> extend;
	private CollectNode<ConnAssertNode> cas;

	/**
	 * Make a new edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public EdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas, CollectNode<BaseNode> body,
						int modifiers, String externalName) {
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);
		this.cas = cas;
		becomeParent(this.cas);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(getValidVersion(bodyUnresolved, body));
		children.add(cas);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		childrenNames.add("cas");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		body = bodyResolver.resolve(bodyUnresolved, this);
		extend = extendResolver.resolve(extendUnresolved, this);

		// Initialize direct sub types
		if (extend != null) {
			for (InheritanceTypeNode type : extend.getChildren()) {
				type.addDirectSubType(this);
			}
		}

		return body != null && extend != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		
		// check all super types to ensure their copy extends are resolved
    	for(EdgeTypeNode parent : extend.getChildren()) {
    		if(!parent.visitedDuringCheck()) { // only if not already visited
    			parent.check();
    		}
    	}
		
		// "resolve" connection assertion inheritance, 
		// after resolve to ensure everything is available, before IR building
		// remember connection assertions to copy and copy after iteration to prevent iterator from becoming stale
		Vector<ConnAssertNode> connAssertsToCopy = new Vector<ConnAssertNode>(); 
		boolean alreadyCopiedExtends = false;
		for(Iterator<ConnAssertNode> it = cas.getChildren().iterator(); it.hasNext(); ) {
			ConnAssertNode ca = it.next();
	        if(ca.copyExtends) {
	        	if(alreadyCopiedExtends) {
	        		reportWarning("more than one copy extends only causes double work without benefit");
	        	}
	        	
	        	for(EdgeTypeNode parent : extend.getChildren()) {
	        		for(ConnAssertNode caToCopy : parent.cas.getChildren()) {
	        			if(caToCopy.copyExtends) {
	        				reportError("internal error: copy extends in parent while copying connection assertions from parent");
	        				res = false;
	        				assert false;
	        			}
	        			connAssertsToCopy.add(caToCopy);
	        		}
	        	}
	        	
	            it.remove();
	            alreadyCopiedExtends = true;
	        }
		}
		
		for(ConnAssertNode caToCopy : connAssertsToCopy) {
			cas.addChild(caToCopy);
		}

		// todo: check for duplicate connection assertions and issue warning about being senseless
		
		return res;
	}
	
	/**
	 * Get the edge type IR object.
	 * @return The edge type IR object for this AST node.
	 */
	protected final EdgeType getEdgeType() {
		return checkIR(EdgeType.class);
	}

	@Override
	protected CollectNode<? extends InheritanceTypeNode> getExtends() {
		return extend;
	}

	@Override
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		assert isResolved();

		for(EdgeTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
    }

	@Override
	protected Collection<EdgeTypeNode> getDirectSuperTypes() {
		assert isResolved();

	    return extend.getChildren();
    }

	@Override
    protected void getMembers(Map<String, DeclNode> members)
    {
    	assert isResolved();

    	for(BaseNode n : body.getChildren()) {
    		if(n instanceof DeclNode) {
    			DeclNode decl = (DeclNode)n;

    			DeclNode old=members.put(decl.getIdentNode().toString(), decl);
    			if(old!=null && !(old instanceof AbstractMemberDeclNode)) {
    				// TODO this should be part of a check (that return false)
    				error.error(decl.getCoords(), "member " + decl.toString() +" of " +
    								getUseString() + " " + getIdentNode() +
    								" already defined in " + old.getParents() + "." // TODO improve error message
    						   );
    			}
    		}
    	}
    }

	protected abstract void setDirectednessIR(EdgeType inhType);

	/**
     * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
     */
	@Override
    protected IR constructIR()
    {
    	EdgeType et = new EdgeType(getDecl().getIdentNode().getIdent(),
    							   getIRModifiers(), getExternalName());

    	constructIR(et); // from InheritanceTypeNode

    	setDirectednessIR(et); // from Undirected/Arbitrary/Directed-EdgeTypeNode

    	for(ConnAssertNode can : cas.getChildren()) {
    		et.addConnAssert(can.checkIR(ConnAssert.class));
    	}

    	return et;
    }
}
