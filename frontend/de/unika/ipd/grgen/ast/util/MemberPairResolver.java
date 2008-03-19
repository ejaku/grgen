/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

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


/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.*;

import de.unika.ipd.grgen.util.Util;
import java.util.Map;

/**
 * A resolver, that resolves a declaration node from an identifier (used in a member init).
 */
public class MemberPairResolver<S extends BaseNode, T extends BaseNode> extends Resolver<Pair<S,T>>
{
	private Class<S> clsS;
	private Class<T> clsT;
	private Class<?>[] classes;

	/**
 	 * Make a new member pair resolver.
 	 *
	 * @param cls A class, the resolved node must be an instance of.
	 */
	public MemberPairResolver(Class<S> clsS, Class<T> clsT) {
		this.clsS = clsS;
		this.clsT = clsT;

		classes = new Class[] { this.clsS, this.clsT };
	}

	/**
	 * Resolves n to node of type S or T, via member init if n is an identifier, via simple cast otherwise
	 * returns null if n's declaration or n can't be cast to S or T.
	 */
	public Pair<S,T> resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			Pair<S,T> pair = resolve((IdentNode)n);
			if (pair != null) {
				assert pair.fst==null || pair.snd==null;
				parent.becomeParent(pair.fst);
				parent.becomeParent(pair.snd);
			}
			return pair;
		}

		Pair<S,T> pair = new Pair<S,T>();
		if(clsS.isInstance(n)) {
			pair.fst = clsS.cast(n);
		}
		if(clsT.isInstance(n)) {
			pair.snd = clsT.cast(n);
		}
		if(pair.fst!=null || pair.snd!=null) {
			assert pair.fst==null || pair.snd==null;
			return pair;
		}

		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}

	/**
	 * Resolves n to node of type S or T, via member init
	 * returns null if n's declaration can't be cast to S or T.
	 */
	public Pair<S,T> resolve(IdentNode n) {
		DeclNode res = n.getDecl();

		if (res instanceof InvalidDeclNode) {
			DeclNode scopeDecl = n.getScope().getIdentNode().getDecl();
			if(scopeDecl instanceof RuleDeclNode) {
				n.reportError("Undefined identifier \"" + n.toString() + "\"");
				return null;
			}
			else {
				InheritanceTypeNode typeNode = (InheritanceTypeNode) scopeDecl.getDeclType();
				Map<String, DeclNode> allMembers = typeNode.getAllMembers();
				res = allMembers.get(n.toString());
				if(res == null) {
					n.reportError("Undefined member " + n.toString() + " of " + typeNode.getDecl().getIdentNode());
					return null;
				}
			}
		}

		Pair<S,T> pair = new Pair<S,T>();
		if (clsS.isInstance(res)) {
			pair.fst = clsS.cast(res);
		}
		if (clsT.isInstance(res)) {
			pair.snd = clsT.cast(res);
		}
		if(pair.fst!=null || pair.snd!=null) {
			return pair;
		}

		n.reportError("\"" + n + "\" is a " + res.getUseString() + " but a "
		        + Util.getStrListWithOr(classes, BaseNode.class, "getUseStr")
		        + " is expected");

		return null;
	}
}
