/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
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
			return cls.cast(n);
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
		// ensure that the used types are resolved
		DeclarationResolver<DeclNode> declResolver =
			new DeclarationResolver<DeclNode>(DeclNode.class);
		DeclNode decl = declResolver.resolve(n);
		if (decl != null) {
			decl.resolve();
		}

		TypeNode resolved = decl.getDeclType();
		if(cls.isInstance(resolved)) {
			return cls.cast(resolved);
		}
		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
