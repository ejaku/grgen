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
 * A resolver, that resolves a source AST node into a target AST node of type R, S or T,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R/S/T otherwise
 */
public class DeclarationTripleResolver<R extends BaseNode, S extends BaseNode, T extends BaseNode>
		extends Resolver<Triple<R, S, T>>
{
	private Class<R> clsR;
	private Class<S> clsS;
	private Class<T> clsT;
	private Class<?>[] classes;

	public DeclarationTripleResolver(Class<R> clsR, Class<S> clsS, Class<T> clsT)
	{
		this.clsR = clsR;
		this.clsS = clsS;
		this.clsT = clsT;

		classes = new Class[] { this.clsR, this.clsS, this.clsT };
	}

	/** resolves n to node of type R, S or T, via declaration if n is an identifier, via simple cast otherwise
	 *  returns null if n's declaration or n can't be cast to R, S or T */
	@Override
	public Triple<R, S, T> resolve(BaseNode bn, BaseNode parent)
	{
		Triple<R, S, T> triple;
		if(bn instanceof IdentNode) {
			triple = resolve((IdentNode)bn);
			if(triple != null) {
				assert(triple.first == null ? 0 : 1)
						+ (triple.second == null ? 0 : 1)
						+ (triple.third == null ? 0 : 1) == 1;
				parent.becomeParent(triple.first);
				parent.becomeParent(triple.second);
				parent.becomeParent(triple.third);
			}
			return triple;
		}

		triple = new Triple<R, S, T>();
		if(clsR.isInstance(bn)) {
			triple.first = clsR.cast(bn);
		}
		if(clsS.isInstance(bn)) {
			triple.second = clsS.cast(bn);
		}
		if(clsT.isInstance(bn)) {
			triple.third = clsT.cast(bn);
		}
		if(triple.first != null || triple.second != null || triple.third != null) {
			assert(triple.first == null ? 0 : 1)
					+ (triple.second == null ? 0 : 1)
					+ (triple.third == null ? 0 : 1) == 1;

			return triple;
		}

		bn.reportError(bn + " is a " + bn.getKind() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getKindStr") + " is expected.");
		return null;
	}

	/** resolves n to node of type R, S or T, via declaration
	 *  returns null if n's declaration can't be cast to R/S/T */
	private Triple<R, S, T> resolve(IdentNode n)
	{
		if(n instanceof PackageIdentNode) {
			if(!resolveOwner((PackageIdentNode)n)) {
				return null;
			}
		}

		Triple<R, S, T> triple = new Triple<R, S, T>();
		DeclNode resolved = n.getDecl();
		if(clsR.isInstance(resolved)) {
			triple.first = clsR.cast(resolved);
		}
		if(clsS.isInstance(resolved)) {
			triple.second = clsS.cast(resolved);
		}
		if(clsT.isInstance(resolved)) {
			triple.third = clsT.cast(resolved);
		}
		if(triple.first != null || triple.second != null || triple.third != null) {
			return triple;
		}

		n.reportError(n + " is a " + resolved.getKind() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getKindStr") + " is expected.");
		return null;
	}
}
