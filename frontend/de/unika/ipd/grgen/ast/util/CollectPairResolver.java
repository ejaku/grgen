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
 * A resolver, that resolves a source AST CollectNode into a target AST CollectNode of type T,
 * by using a given resolver. 
 */
public class CollectPairResolver<T extends BaseNode>
{
	private Resolver<? extends Pair<? extends T, ? extends T>> resolver;
	
	public CollectPairResolver(Resolver<? extends Pair<? extends T, ? extends T>> resolver) {
		this.resolver = resolver;
	}
	
	/** resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise 
	 *  returns null if n's declaration or n can't be cast to R */
	public CollectNode<T> resolve(CollectNode<?> collect) {
		CollectNode<T> res = new CollectNode<T>();
		for (BaseNode elem : collect.getChildren()) {
	        Pair<? extends T, ? extends T> pair = resolver.resolve(elem, collect);
	        if (pair == null) {
	        	return null;
	        }
	        if (pair.fst != null) {
	        	res.addChild(pair.fst);
	        }
	        if (pair.snd != null) {
	        	res.addChild(pair.snd);
	        }
        }
		return res;
	}
}
