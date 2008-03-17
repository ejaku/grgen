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
