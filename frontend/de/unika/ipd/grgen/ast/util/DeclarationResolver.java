/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a source AST node into a target AST node of type R,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R otherwise
 */
public class DeclarationResolver<R extends BaseNode> extends Resolver<R>
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
			return cls.cast(n);
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
			return cls.cast(resolved);
		}
		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
