/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;

/**
 * A resolver, that resolves a source AST CollectNode into three target AST
 * CollectNode of type R, S and T by using a given resolver.
 */
public class CollectTripleResolver<R extends BaseNode, S extends BaseNode, T extends BaseNode>
{
	private Resolver<? extends Triple<R, S, T>> resolver;

	public CollectTripleResolver(Resolver<? extends Triple<R, S, T>> resolver) {
		this.resolver = resolver;
	}

	/**
	 * resolves the collect node to collect nodes of type R, S and T via
	 * the given resolver
	 */
	public Triple<CollectNode<R>, CollectNode<S>, CollectNode<T>> resolve(CollectNode<?> collect) {
		CollectNode<R> first = null;
		CollectNode<S> second = null;
		CollectNode<T> third = null;

		for (BaseNode elem : collect.getChildren()) {
	        Triple<R, S, T> triple = resolver.resolve(elem, collect);
	        if (triple == null) {
	        	return null;
	        }
	        if (triple.first != null) {
	        	if (first == null) {
	        		first = new CollectNode<R>();
					first.setCoords(collect.getCoords());
	        	}
	        	first.addChild(triple.first);
	        }
	        if (triple.second != null) {
	        	if (second == null) {
	        		second = new CollectNode<S>();
					second.setCoords(collect.getCoords());
	        	}
	        	second.addChild(triple.second);
	        }
	        if (triple.third != null) {
	        	if (third == null) {
	        		third = new CollectNode<T>();
					third.setCoords(collect.getCoords());
	        	}
	        	third.addChild(triple.third);
	        }
        }

		Triple<CollectNode<R>, CollectNode<S>, CollectNode<T>> res = new Triple<CollectNode<R>, CollectNode<S>, CollectNode<T>>();
		res.first = first;
		res.second = second;
		res.third = third;

		return res;
	}
}
