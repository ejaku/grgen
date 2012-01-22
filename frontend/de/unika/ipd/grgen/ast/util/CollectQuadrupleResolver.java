/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;

/**
 * A resolver, that resolves a source AST CollectNode into four target AST
 * CollectNode of type R, S, T  and U by using a given resolver.
 */
public class CollectQuadrupleResolver<R extends BaseNode, S extends BaseNode, T extends BaseNode, U extends BaseNode>
{
	private Resolver<? extends Quadruple<R, S, T, U>> resolver;

	public CollectQuadrupleResolver(Resolver<? extends Quadruple<R, S, T, U>> resolver) {
		this.resolver = resolver;
	}

	/**
	 * resolves the collect node to collect nodes of type R, S, T and U via
	 * the given resolver
	 */
	public Quadruple<CollectNode<R>, CollectNode<S>, CollectNode<T>, CollectNode<U>> resolve(CollectNode<?> collect) {
		CollectNode<R> first = null;
		CollectNode<S> second = null;
		CollectNode<T> third = null;
		CollectNode<U> fourth = null;

		for (BaseNode elem : collect.getChildren()) {
	        Quadruple<R, S, T, U> quadruple = resolver.resolve(elem, collect);
	        if (quadruple == null) {
	        	return null;
	        }
	        if (quadruple.first != null) {
	        	if (first == null) {
	        		first = new CollectNode<R>();
					first.setCoords(collect.getCoords());
	        	}
	        	first.addChild(quadruple.first);
	        }
	        if (quadruple.second != null) {
	        	if (second == null) {
	        		second = new CollectNode<S>();
					second.setCoords(collect.getCoords());
	        	}
	        	second.addChild(quadruple.second);
	        }
	        if (quadruple.third != null) {
	        	if (third == null) {
	        		third = new CollectNode<T>();
					third.setCoords(collect.getCoords());
	        	}
	        	third.addChild(quadruple.third);
	        }
	        if (quadruple.fourth != null) {
	        	if (fourth == null) {
	        		fourth = new CollectNode<U>();
					fourth.setCoords(collect.getCoords());
	        	}
	        	fourth.addChild(quadruple.fourth);
	        }
        }

		Quadruple<CollectNode<R>, CollectNode<S>, CollectNode<T>, CollectNode<U>> res = new Quadruple<CollectNode<R>, CollectNode<S>, CollectNode<T>, CollectNode<U>>();
		res.first = first;
		res.second = second;
		res.third = third;
		res.fourth = fourth;

		return res;
	}
}
