/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectBaseNode;
import de.unika.ipd.grgen.ast.CollectNode;

/**
 * A resolver, that resolves a source AST CollectNode into (one of) two target AST CollectNode of types S and T,
 * by using a given resolver.
 */
public class CollectPairResolver<S extends BaseNode, T extends BaseNode>
{
	private Resolver<Pair<S, T>> resolver;

	public CollectPairResolver(Resolver<Pair<S, T>> resolver)
	{
		this.resolver = resolver;
	}

	/**
	 * resolves the collect node to collect nodes of type S, T via the given resolver
	 */
	public Pair<CollectNode<S>, CollectNode<T>> resolve(CollectBaseNode collect)
	{
		CollectNode<S> first = null;
		CollectNode<T> second = null;

		for(BaseNode child : collect.getChildren()) {
			Pair<S, T> pair = resolver.resolve(child, collect);
			if(pair == null) {
				return null;
			}
			if(pair.fst != null) {
				if(first == null) {
					first = new CollectNode<S>();
					first.setCoords(collect.getCoords());
				}
				first.addChild(pair.fst);
			}
			if(pair.snd != null) {
				if(second == null) {
					second = new CollectNode<T>();
					second.setCoords(collect.getCoords());
				}
				second.addChild(pair.snd);
			}
		}

		Pair<CollectNode<S>, CollectNode<T>> res = new Pair<CollectNode<S>, CollectNode<T>>();
		res.fst = first;
		res.snd = second;

		return res;
	}
}
