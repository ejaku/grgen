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

import java.util.Map;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.InvalidDeclNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a declaration node from an identifier (used in a member init).
 */
public class MemberResolver<T extends BaseNode> extends Resolver<T>
{
	private Class<T> cls;

	/**
 	 * Make a new member resolver.
 	 *
	 * @param cls A class, the resolved node must be an instance of.
	 */
	public MemberResolver(Class<T> cls) {
		this.cls = cls;
	}

	/**
	 * Resolves n to node of type T, via member init if n is an identifier, via simple cast otherwise
	 * returns null if n's declaration or n can't be cast to R.
	 */
	public T resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			T resolved = resolve((IdentNode)n);
			parent.becomeParent(resolved);
			return resolved;
		}
		if(cls.isInstance(n)) {
			return cls.cast(n);
		}
		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}

	/**
	 * Resolves n to node of type R, via member init
	 * returns null if n's declaration can't be cast to R.
	 */
	public T resolve(IdentNode n) {
		DeclNode res = n.getDecl();

		if(!(res instanceof InvalidDeclNode)) {
			if (cls.isInstance(res)) {
				return cls.cast(res);
			}
			n.reportError("\"" + n + "\" is a " + res.getUseString() +
					" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		}

		InheritanceTypeNode typeNode = (InheritanceTypeNode)n.getScope().getIdentNode().getDecl().getDeclType();

		Map<String, DeclNode> allMembers = typeNode.getAllMembers();

		res = allMembers.get(n.toString());

		if(res==null) {
			n.reportError("Undefined member " + n.toString() + " of "+ typeNode.getDecl().getIdentNode());
			return null;
		}

		if (cls.isInstance(res)) {
			return cls.cast(res);
		}
		n.reportError("\"" + n + "\" is a " + res.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");

		return null;
	}
}
