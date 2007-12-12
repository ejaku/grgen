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
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;


import de.unika.ipd.grgen.ast.*;

import java.util.Map;

/**
 * A resolver, that resolves a declaration node from an identifier (used in a member init).
 */
public class MemberInitResolver extends IdentResolver {

	/**
	 * Make a new declaration resolver.
	 * @param classes A list of classes, the resolved node must be
	 * instance of.
	 */
	public MemberInitResolver(Class<?>[] classes) {
		super(classes);
	}

	/**
	 * Just a convenience constructor for {@link #DeclResolver(Class[])}
	 */
	public MemberInitResolver(Class<?> cls) {
		super(new Class[] { cls });
	}

	/**
	 * @see de.unika.ipd.grgen.ast.check.Resolver#resolve()
	 */
	protected BaseNode resolveIdent(IdentNode n) {
		DeclNode res = DeclNode.getInvalid();

		//System.out.println("resolveIdent:" + n);
		//System.out.println("resolveIdent getDecl():" + n.getDecl());

		if(!(n.getDecl() instanceof DeclNode.InvalidDeclNode))
			return n.getDecl();

		InheritanceTypeNode typeNode = (InheritanceTypeNode)n.getScope().getIdentNode().getDecl().getDeclType();

		//System.out.println("resolveIdent: (InheritanceTypeNode)n.getScope().getIdentNode().getDecl().getDeclType(): " + typeNode);

		Map<String, DeclNode> allMembers = typeNode.getAllMembers();

		//System.out.println("resolveIdent: allMembers: " + allMembers);

		res = allMembers.get(n.toString());

		//System.out.println("=== resolveIdent: res: " + res);

		if(res==null) {
			error.error(n.getCoords(), "Undefined member " + n.toString() + " of "+ typeNode.getDecl().getIdentNode());
			res = DeclNode.getInvalid();
		}
		/*
		 System.out.println("resolveIdent: n.getParents()" + n.getParents());

		 ret:for(BaseNode p : n.getParents())
		 if(p instanceof MemberInitNode)
		 for(BaseNode p2 : p.getParents()) {
		 System.out.println("resolveIdent: p.getParents()" + p.getParents());
		 assert p2 instanceof CollectNode;
		 for(BaseNode p3 : p2.getParents()) {
		 InheritanceTypeNode typeNode = (InheritanceTypeNode)p3;

		 System.out.println("resolveIdent: typeNode: " + typeNode);

		 Map<String, DeclNode> allMembers = typeNode.getAllMembers();

		 System.out.println("resolveIdent: allMembers: " + allMembers);

		 res = allMembers.get(n.toString());

		 System.out.println("=== resolveIdent: res: " + res);

		 if(res==null) {
		 error.error(n.getCoords(), "Undefined member " + n.toString() + " of "+ typeNode.getDecl().getIdentNode());
		 res = DeclNode.getInvalid();
		 }

		 break ret;
		 }
		 }
		 */
		return res;
	}


	protected BaseNode getDefaultResolution() {
		return DeclNode.getInvalid();
	}

}

