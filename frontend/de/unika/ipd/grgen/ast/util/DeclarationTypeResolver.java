/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
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
	public DeclarationTypeResolver(Class<T> cls)
	{
		this.cls = cls;
	}

	/**
	 * Resolves n to node of type R, via declaration type if n is an identifier, via simple cast otherwise
	 * returns null if n's declaration or n can't be cast to R.
	 */
	@Override
	public T resolve(BaseNode bn, BaseNode parent)
	{
		if(bn instanceof IdentNode) {
			T resolved = resolve((IdentNode)bn);
			parent.becomeParent(resolved);
			return resolved;
		}
		if(cls.isInstance(bn)) {
			return cls.cast(bn);
		}
		bn.reportError(bn + " is a " + bn.getKind() +
				" but a " + Util.getStr(cls, BaseNode.class, "getKindStr") + " is expected.");
		return null;
	}

	/**
	 * Resolves n to node of type R, via declaration type
	 * returns null if n's declaration can't be cast to R.
	 */
	public T resolve(IdentNode n)
	{
		// ensure that the used types are resolved
		DeclarationResolver<DeclNode> declResolver = new DeclarationResolver<DeclNode>(DeclNode.class);
		DeclNode decl = declResolver.resolve(n);
		if(decl != null) {
			decl.resolve();

			TypeNode resolved = decl.getDeclType();
			if(cls.isInstance(resolved)) {
				return cls.cast(resolved);
			}
			n.reportError(n + " is a " + resolved.getKind() +
					" but a " + Util.getStr(cls, BaseNode.class, "getKindStr") + " is expected.");
		}
		
		return null;
	}
}
