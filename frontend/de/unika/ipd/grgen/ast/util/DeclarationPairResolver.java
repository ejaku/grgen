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
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a source AST node into a target AST node of type R or S,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R/S otherwise 
 */
public class DeclarationPairResolver<R extends BaseNode, S extends BaseNode>
{
	private Class<R> clsR;
	private Class<S> clsS;
	private Class<?>[] classes = new Class[] { clsR, clsS };
	
	public DeclarationPairResolver(Class<R> clsR, Class<S> clsS) {
		this.clsR = clsR;
		this.clsS = clsS;
	}
	
	/** resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise 
	 *  returns null if n's declaration or n can't be cast to R */
	public Pair<R,S> resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			Pair<R,S> pair = resolve((IdentNode)n);
			if (pair != null) {
				parent.becomeParent(pair.fst);
				parent.becomeParent(pair.snd);
			}
			return pair;
		}
		
		Pair<R,S> pair = new Pair<R,S>();
		if(clsR.isInstance(n)) {
			pair.fst = (R) n;
		}
		if(clsS.isInstance(n)) {
			pair.snd = (S) n;
		}		
		if(pair.fst!=null || pair.snd!=null) {
			return pair;
		}
		
		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
	
	/** resolves n to node of type R or S, via declaration 
	 *  returns null if n's declaration can't be cast to R/S */
	private Pair<R,S> resolve(IdentNode n) {
		Pair<R,S> pair = new Pair<R,S>();
		DeclNode resolved = n.getDecl();
		if(clsR.isInstance(resolved)) {
			pair.fst = (R) resolved;
		}
		if(clsS.isInstance(resolved)) {
			pair.snd = (S) resolved;
		}
		if(pair.fst!=null || pair.snd!=null) {
			return pair;
		}
		
		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
