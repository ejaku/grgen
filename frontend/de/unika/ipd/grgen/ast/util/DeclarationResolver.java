/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

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


package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a source AST node into a target AST node of type R,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R otherwise 
 */
public class DeclarationResolver<R extends BaseNode>
{
	private Class<R> cls;
	
	public DeclarationResolver(Class<R> cls) {
		this.cls = cls;
	}
	
	/** resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise 
	 *  returns null if n's declaration or n can't be cast to R */
	public R resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			R resolved = resolve((IdentNode)n);
			parent.becomeParent(resolved);
			return resolved;
		} 
		if(cls.isInstance(n)) {
			return (R) n;
		}
		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
	
	/** resolves n to node of type R, via declaration 
	 *  returns null if n's declaration can't be cast to R */
	public R resolve(IdentNode n) {
		DeclNode resolved = n.getDecl();
		if(cls.isInstance(resolved)) {
			return (R) resolved;
		}
		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
