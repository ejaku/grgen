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

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.TypeNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves an identifier into it's AST type node.
 */
public class DeclarationTypeResolver<T extends BaseNode> extends Resolver<T>
{
	private Class<T> cls;

	/**
 	 * Make a new type declaration resolver.
 	 *
	 * @param cls A class, the resolved node must be an instance of.
	 */
	public DeclarationTypeResolver(Class<T> cls) {
		this.cls = cls;
	}

	/**
	 * Resolves n to node of type R, via declaration type if n is an identifier, via simple cast otherwise
	 * returns null if n's declaration or n can't be cast to R.
	 */
	public T resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			T resolved = resolve((IdentNode)n);
			parent.becomeParent(resolved);
			return resolved;
		}
		if(cls.isInstance(n)) {
			return (T) n;
		}
		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}

	/**
	 * Resolves n to node of type R, via declaration type
	 * returns null if n's declaration can't be cast to R.
	 */
	public T resolve(IdentNode n) {
		TypeNode resolved = n.getDecl().getDeclType();
		if(cls.isInstance(resolved)) {
			return (T) resolved;
		}
		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
