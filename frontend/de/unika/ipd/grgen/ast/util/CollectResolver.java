/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;

/**
 * A resolver, that resolves a source AST CollectNode into a target AST CollectNode of type T,
 * by using a given resolver.
 */
public class CollectResolver<T extends BaseNode>
{
	private Resolver<T> resolver;

	public CollectResolver(Resolver<T> resolver)
	{
		this.resolver = resolver;
	}

	/** resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise
	 *  returns null if n's declaration or n can't be cast to R */
	public CollectNode<T> resolve(CollectNode<?> collect, BaseNode parent)
	{
		CollectNode<T> res = new CollectNode<T>();
		res.setCoords(collect.getCoords());

		for(BaseNode child : collect.getChildren()) {
			T resolved = resolver.resolve(child, collect);
			if(resolved == null) {
				return null;
			}
			res.addChild(resolved);
		}
		parent.becomeParent(res);
		return res;
	}
}
