/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a source AST node into a target AST node of type R or S,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R/S otherwise
 */
public class DeclarationPairResolver<R extends BaseNode, S extends BaseNode> extends Resolver<Pair<R,S>>
{
	private Class<R> clsR;
	private Class<S> clsS;
	private Class<?>[] classes;

	public DeclarationPairResolver(Class<R> clsR, Class<S> clsS) {
		this.clsR = clsR;
		this.clsS = clsS;

		classes = new Class[] { this.clsR, this.clsS };
	}

	/** resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise
	 *  returns null if n's declaration or n can't be cast to R or S */
	public Pair<R,S> resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			Pair<R,S> pair = resolve((IdentNode)n);
			if (pair != null) {
				assert pair.fst==null || pair.snd==null;
				parent.becomeParent(pair.fst);
				parent.becomeParent(pair.snd);
			}
			return pair;
		}

		Pair<R,S> pair = new Pair<R,S>();
		if(clsR.isInstance(n)) {
			pair.fst = clsR.cast(n);
		}
		if(clsS.isInstance(n)) {
			pair.snd = clsS.cast(n);
		}
		if(pair.fst!=null || pair.snd!=null) {
			assert pair.fst==null || pair.snd==null;
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
			pair.fst = clsR.cast(resolved);
		}
		if(clsS.isInstance(resolved)) {
			pair.snd = clsS.cast(resolved);
		}
		if(pair.fst!=null || pair.snd!=null) {
			return pair;
		}

		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
