/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a source AST node into a target AST node of type R,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R otherwise
 */
public class DeclarationResolver<R extends BaseNode> extends Resolver<R>
{
	private Class<R> cls;
	private Class<? extends R>[] classes;

	public DeclarationResolver(Class<R> cls)
	{
		this.cls = cls;
	}

	@SafeVarargs
	public DeclarationResolver(Class<? extends R>... classes)
	{
		this.classes = classes;
	}

	/** resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise
	 *  returns null if n's declaration or n can't be cast to R */
	@Override
	public R resolve(BaseNode bn, BaseNode parent)
	{
		if(bn instanceof IdentNode) {
			R resolved = resolve((IdentNode)bn);
			parent.becomeParent(resolved);
			return resolved;
		}

		R res = tryCast(bn);
		if(res != null)
			return res;

		bn.reportError(bn + " is a " + bn.getKind() +
				" but a " + getAllowedNames() + " is expected.");
		return null;
	}

	/** resolves n to node of type R, via declaration
	 *  returns null if n's declaration can't be cast to R */
	public R resolve(IdentNode n)
	{
		if(n instanceof PackageIdentNode) {
			if(!resolveOwner((PackageIdentNode)n)) {
				return null;
			}
		}

		DeclNode resolved = n.getDecl();

		R res = tryCast(resolved);
		if(res != null)
			return res;

		n.reportError(n + " is a " + resolved.getKind() +
				" but a " + getAllowedNames() + " is expected.");
		return null;
	}

	private R tryCast(BaseNode bn)
	{
		if(cls == null) {
			for(Class<? extends R> curCls : classes) {
				if(curCls.isInstance(bn))
					return curCls.cast(bn);
			}
		} else if(cls.isInstance(bn)) {
			return cls.cast(bn);
		}
		return null;
	}

	private String getAllowedNames()
	{
		if(cls != null)
			return Util.getStr(cls, BaseNode.class, "getKindStr");
		else
			return Util.getStrListWithOr(classes, BaseNode.class, "getKindStr");
	}
}
