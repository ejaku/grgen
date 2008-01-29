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

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * A class representing a node type
 */
public class NodeTypeNode extends InheritanceTypeNode {
	static {
		setName(NodeTypeNode.class, "node type");
	}
	
	GenCollectNode<NodeTypeNode> extend;

	/**
	 * Create a new node type
	 * @param ext The collect node containing the node types which are extended by this type.
	 * @param body the collect node with body declarations
	 * @param modifiers Type modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeTypeNode(GenCollectNode<IdentNode> ext, CollectNode body,
						int modifiers, String externalName) {
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.body = body;
		becomeParent(this.body);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(body);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver bodyResolver = new DeclResolver(new Class[] {MemberDeclNode.class, MemberInitNode.class});
		DeclarationTypeResolver<NodeTypeNode> typeResolver =
			new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class);
		CollectResolver<NodeTypeNode> extendResolver =
			new CollectResolver<NodeTypeNode>(typeResolver);
		successfullyResolved = body.resolveChildren(bodyResolver) && successfullyResolved;
		extend = extendResolver.resolve(extendUnresolved);
		successfullyResolved = extend!=null && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = (extend!=null ? extend.resolve() : false) && successfullyResolved;
		successfullyResolved = body.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()  {
		return super.checkLocal();
	}

	/**
	 * Get the IR node type for this AST node.
	 * @return The correctly casted IR node type.
	 */
	public NodeType getNodeType() {
		return (NodeType) checkIR(NodeType.class);
	}

	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		NodeType nt = new NodeType(getDecl().getIdentNode().getIdent(),
								   getIRModifiers(), getExternalName());

		constructIR(nt);

		return nt;
	}

	/** @see de.unika.ipd.grgen.ast.ScopeOwner#fixupDefinition(de.unika.ipd.grgen.ast.IdentNode) */
    public boolean fixupDefinition(IdentNode id)
    {
    	assert isResolved();
    	
    	boolean found = super.fixupDefinition(id, false);
    
    	if(!found) {
    		for(InheritanceTypeNode inh : extend.getChildren()) {
    			boolean result = inh.fixupDefinition(id);
    
    			if(found && result) {
    				error.error(getIdentNode().getCoords(), "Identifier " + id + " is ambiguous");
    			}
    			
    			found = found || result;
    		}
    	}
    
    	return found;
    }

	protected void doGetCompatibleToTypes(Collection<TypeNode> coll)
    {
    	assert isResolved();
		
		for(InheritanceTypeNode inh : extend.getChildren()) {
    		coll.add(inh);
    		inh.getCompatibleToTypes(coll);
    	}
    }

	protected void constructIR(InheritanceType inhType)
    {
    	for(BaseNode n : body.getChildren()) {
    		if(n instanceof DeclNode) {
    			DeclNode decl = (DeclNode)n;
    			inhType.addMember(decl.getEntity());
    		}
    		else if(n instanceof MemberInitNode) {
    			MemberInitNode mi = (MemberInitNode)n;
    			inhType.addMemberInit((MemberInit)mi.getIR());
    		}
    	}
    	for(InheritanceTypeNode inh : extend.getChildren()) {
    		inhType.addDirectSuperType((InheritanceType)inh.getType());
    	}
    
    	// to check overwriting of attributes
    	inhType.getAllMembers();
    }

	public static String getKindStr() {
		return "node type";
	}

	public static String getUseStr() {
		return "node type";
	}

	@Override
    public Collection<NodeTypeNode> getDirectSuperTypes()
    {
		assert isResolved();
		
	    return extend.getChildren();
    }
}

