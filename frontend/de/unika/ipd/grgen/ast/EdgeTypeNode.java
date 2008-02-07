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
 * @version $Id: DirectedEdgeTypeNode.java 17629 2008-02-07 18:44:43Z buchwald $
 */
package de.unika.ipd.grgen.ast;

import java.util.Map;

import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;

public abstract class EdgeTypeNode extends InheritanceTypeNode {
	static {
		setName(EdgeTypeNode.class, "edge type");
	}

	protected CollectNode<BaseNode> body;
	protected CollectNode<ConnAssertNode> cas;

	@Override
    protected void getMembers(Map<String, DeclNode> members)
    {
    	assert isResolved();
    
    	for(BaseNode n : body.getChildren()) {
    		if(n instanceof DeclNode) {
    			DeclNode decl = (DeclNode)n;
    
    			DeclNode old=members.put(decl.getIdentNode().toString(), decl);
    			if(old!=null && !(old instanceof AbstractMemberDeclNode)) {
    				error.error(decl.getCoords(), "member " + decl.toString() +" of " +
    								getUseString() + " " + getIdentNode() +
    								" already defined in " + old.getParents() + "." // TODO improve error message
    						   );
    			}
    		}
    	}
    }
	
	protected abstract void constructIR(InheritanceType inhType);

	/**
     * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
     */
    protected IR constructIR()
    {
    	EdgeType et = new EdgeType(getDecl().getIdentNode().getIdent(),
    							   getIRModifiers(), getExternalName());
    
    	constructIR(et);
    
    	for(BaseNode n : cas.getChildren()) {
    		ConnAssertNode can = (ConnAssertNode)n;
    		et.addConnAssert((ConnAssert)can.checkIR(ConnAssert.class));
    	}
    
    	return et;
    }
}
