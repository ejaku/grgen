/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.*;

import de.unika.ipd.grgen.util.Util;
import java.util.Map;

/**
 * A resolver, that resolves a declaration node from an identifier (used in a member init).
 */
public class MemberTripleResolver<S extends BaseNode, T extends BaseNode, U extends BaseNode>
	extends Resolver<Triple<S, T, U>>
{
	private Class<S> clsS;
	private Class<T> clsT;
	private Class<U> clsU;
	private Class<?>[] classes;

	/**
 	 * Make a new member triple resolver.
 	 *
	 * @param cls A class, the resolved node must be an instance of.
	 */
	public MemberTripleResolver(Class<S> clsS, Class<T> clsT, Class<U> clsU) {
		this.clsS = clsS;
		this.clsT = clsT;
		this.clsU = clsU;

		classes = new Class[] { this.clsS, this.clsT, this.clsU };
	}

	/**
	 * Resolves n to node of type S or T or U, via member init if n is an identifier, via simple cast otherwise
	 * returns null if n's declaration or n can't be cast to S or T or U.
	 */
	public Triple<S,T,U> resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode)
			return resolve((IdentNode) n, parent);

		return genTriple(n, n);
	}

	/**
	 * Resolves n to node of type S or T or U via member init
	 * returns null if n's declaration can't be cast to S or T or U.
	 */
	private Triple<S,T,U> resolve(IdentNode n, BaseNode parent) {
		DeclNode res = n.getDecl();

		if (res instanceof InvalidDeclNode) {
			DeclNode scopeDecl = n.getScope().getIdentNode().getDecl();
			if(scopeDecl instanceof RuleDeclNode) {
				n.reportError("Undefined identifier \"" + n.toString() + "\"");
				return null;
			}
			else {
				InheritanceTypeNode typeNode = (InheritanceTypeNode) scopeDecl.getDeclType();
				Map<String, DeclNode> allMembers = typeNode.getAllMembers();
				res = allMembers.get(n.toString());
				if(res == null) {
					n.reportError("Undefined member " + n.toString() + " of " + typeNode.getDecl().getIdentNode());
					return null;
				}
			}
		}

		Triple<S,T,U> triple = genTriple(res, n);
		if(triple == null) return null;

		parent.becomeParent(triple.first);
		parent.becomeParent(triple.second);
		parent.becomeParent(triple.third);

		return triple;
	}

	private Triple<S,T,U> genTriple(BaseNode res, BaseNode errorPos) {
		Triple<S,T,U> triple = new Triple<S,T,U>();
		if(clsS.isInstance(res)) {
			triple.first = clsS.cast(res);
		}
		if(clsT.isInstance(res)) {
			triple.second = clsT.cast(res);
		}
		if(clsU.isInstance(res)) {
			triple.third = clsU.cast(res);
		}
		if(triple.first != null || triple.second != null || triple.third != null) {
			assert (triple.first  == null ? 0 : 1)
				 + (triple.second == null ? 0 : 1)
				 + (triple.third  == null ? 0 : 1) == 1;
			return triple;
		}

		errorPos.reportError("\"" + errorPos + "\" is a " + res.getUseString() + " but a "
		        + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr")
		        + " is expected");

		return null;
	}
}
