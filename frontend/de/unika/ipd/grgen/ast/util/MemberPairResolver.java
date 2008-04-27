/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import java.util.Map;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.InvalidDeclNode;
import de.unika.ipd.grgen.ast.RuleDeclNode;
import de.unika.ipd.grgen.util.Util;

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
	private Pair<S,T> resolve(IdentNode n) {
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
