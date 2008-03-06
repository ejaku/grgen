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
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a source AST node into a target AST node of type R, S or T,
 * by drawing the declaration node out of the source node if it is an identifier node,
 * or by simply casting source to R/S/T otherwise
 */
public class DeclarationTripelResolver<R extends BaseNode, S extends BaseNode, T extends BaseNode> extends Resolver<Tripel<R, S, T>>
{
	private Class<R> clsR;
	private Class<S> clsS;
	private Class<T> clsT;
	private Class<?>[] classes = new Class[] { clsR, clsS, clsT };

	public DeclarationTripelResolver(Class<R> clsR, Class<S> clsS, Class<T> clsT) {
		this.clsR = clsR;
		this.clsS = clsS;
		this.clsT = clsT;
	}

	/** resolves n to node of type R, S or T, via declaration if n is an identifier, via simple cast otherwise
	 *  returns null if n's declaration or n can't be cast to R, S or T */
	public Tripel<R, S, T> resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			Tripel<R, S, T> tripel = resolve((IdentNode)n);
			if (tripel != null) {
				assert (tripel.first == null && tripel.second == null)
					|| (tripel.first == null && tripel.third == null)
					|| (tripel.second == null && tripel.third == null);
				parent.becomeParent(tripel.first);
				parent.becomeParent(tripel.second);
				parent.becomeParent(tripel.third);
			}
			return tripel;
		}

		Tripel<R, S, T> tripel = new Tripel<R, S, T>();
		if(clsR.isInstance(n)) {
			tripel.first = clsR.cast(n);
		}
		if(clsS.isInstance(n)) {
			tripel.second = clsS.cast(n);
		}
		if(clsT.isInstance(n)) {
			tripel.third = clsT.cast(n);
		}
		if(tripel.first != null || tripel.second != null || tripel.third != null) {
			assert (tripel.first == null && tripel.second == null)
			|| (tripel.first == null && tripel.third == null)
			|| (tripel.second == null && tripel.third == null);

			return tripel;
		}

		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}

	/** resolves n to node of type R, S or T, via declaration
	 *  returns null if n's declaration can't be cast to R/S/T */
	private Tripel<R, S, T> resolve(IdentNode n) {
		Tripel<R, S, T> tripel = new Tripel<R, S, T>();
		DeclNode resolved = n.getDecl();
		if(clsR.isInstance(resolved)) {
			tripel.first = clsR.cast(resolved);
		}
		if(clsS.isInstance(resolved)) {
			tripel.second = clsS.cast(resolved);
		}
		if(clsT.isInstance(resolved)) {
			tripel.third = clsT.cast(resolved);
		}
		if(tripel.first != null || tripel.second != null || tripel.third != null) {
			return tripel;
		}

		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
