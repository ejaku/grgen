/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
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
public class DeclarationQuadrupleResolver<R extends BaseNode, S extends BaseNode, T extends BaseNode, U extends BaseNode>
		extends Resolver<Quadruple<R, S, T, U>>
{
	private Class<R> clsR;
	private Class<S> clsS;
	private Class<T> clsT;
	private Class<U> clsU;
	private Class<?>[] classes;

	public DeclarationQuadrupleResolver(Class<R> clsR, Class<S> clsS, Class<T> clsT, Class<U> clsU)
	{
		this.clsR = clsR;
		this.clsS = clsS;
		this.clsT = clsT;
		this.clsU = clsU;

		classes = new Class[] { this.clsR, this.clsS, this.clsT };
	}

	/** resolves n to node of type R, S, T or U, via declaration if n is an identifier, via simple cast otherwise
	 *  returns null if n's declaration or n can't be cast to R, S, T or U */
	@Override
	public Quadruple<R, S, T, U> resolve(BaseNode bn, BaseNode parent)
	{
		Quadruple<R, S, T, U> quadruple;
		if(bn instanceof IdentNode) {
			quadruple = resolve((IdentNode)bn);
			if(quadruple != null) {
				assert(quadruple.first == null ? 0 : 1)
						+ (quadruple.second == null ? 0 : 1)
						+ (quadruple.third == null ? 0 : 1)
						+ (quadruple.fourth == null ? 0 : 1) == 1;
				parent.becomeParent(quadruple.first);
				parent.becomeParent(quadruple.second);
				parent.becomeParent(quadruple.third);
				parent.becomeParent(quadruple.fourth);
			}
			return quadruple;
		}

		quadruple = new Quadruple<R, S, T, U>();
		if(clsR.isInstance(bn)) {
			quadruple.first = clsR.cast(bn);
		}
		if(clsS.isInstance(bn)) {
			quadruple.second = clsS.cast(bn);
		}
		if(clsT.isInstance(bn)) {
			quadruple.third = clsT.cast(bn);
		}
		if(clsU.isInstance(bn)) {
			quadruple.fourth = clsU.cast(bn);
		}
		if(quadruple.first != null || quadruple.second != null || quadruple.third != null || quadruple.fourth != null) {
			assert(quadruple.first == null ? 0 : 1)
					+ (quadruple.second == null ? 0 : 1)
					+ (quadruple.third == null ? 0 : 1)
					+ (quadruple.fourth == null ? 0 : 1) == 1;

			return quadruple;
		}

		bn.reportError(bn + " is a " + bn.getKind() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getKindStr") + " is expected.");
		return null;
	}

	/** resolves n to node of type R, S, T or U, via declaration
	 *  returns null if n's declaration can't be cast to R/S/T/U */
	private Quadruple<R, S, T, U> resolve(IdentNode n)
	{
		if(n instanceof PackageIdentNode) {
			if(!resolveOwner((PackageIdentNode)n)) {
				return null;
			}
		}

		Quadruple<R, S, T, U> quadruple = new Quadruple<R, S, T, U>();
		DeclNode resolved = n.getDecl();
		if(clsR.isInstance(resolved)) {
			quadruple.first = clsR.cast(resolved);
		}
		if(clsS.isInstance(resolved)) {
			quadruple.second = clsS.cast(resolved);
		}
		if(clsT.isInstance(resolved)) {
			quadruple.third = clsT.cast(resolved);
		}
		if(clsU.isInstance(resolved)) {
			quadruple.fourth = clsU.cast(resolved);
		}
		if(quadruple.first != null || quadruple.second != null || quadruple.third != null || quadruple.fourth != null) {
			return quadruple;
		}

		n.reportError(n + " is a " + resolved.getKind() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getKindStr") + " is expected.");
		return null;
	}
}
