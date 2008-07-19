/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * MemberAnyResolver.java
 *
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.InvalidDeclNode;
import de.unika.ipd.grgen.ast.ActionDeclNode;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.Util;
import java.util.Map;
import java.util.Vector;

/**
 * A resolver, that resolves a declaration node from an identifier.
 */
public class MemberResolver<T> extends Base
{
	// for error message
	private BaseNode orginalNode;

	private BaseNode unresolvedNode;
	private T resolvedNode;
	private Vector<Class<? extends T>> triedClasses = new Vector<Class<? extends T>>();
	private int validClasses;

	/**
	 * Tries to resolve the given BaseNode.
	 * @returns True, if the BaseNode was resolved.
	 *          False, when an error occurred (the error is reported).
	 */
	public boolean resolve(BaseNode node) {
		triedClasses.clear();
		validClasses = 0;

		orginalNode = node;
		if(!(orginalNode instanceof IdentNode)) {
			unresolvedNode = orginalNode;
			return true;
		}

		IdentNode identNode = (IdentNode) orginalNode;
		unresolvedNode = identNode.getDecl();

		if (unresolvedNode instanceof InvalidDeclNode) {
			DeclNode scopeDecl = identNode.getScope().getIdentNode().getDecl();
			if(scopeDecl instanceof ActionDeclNode || scopeDecl instanceof InvalidDeclNode) {
				identNode.reportError("Undefined identifier \"" + identNode.toString() + "\"");
				return false;
			}
			else {
				InheritanceTypeNode typeNode = (InheritanceTypeNode) scopeDecl.getDeclType();
				Map<String, DeclNode> allMembers = typeNode.getAllMembers();
				unresolvedNode = allMembers.get(identNode.toString());
				if(unresolvedNode == null) {
					identNode.reportError("Undefined member " + identNode.toString()
							+ " of " + typeNode.getDecl().getIdentNode());
					return false;
				}
			}
		}
		return true;
	}

	public T getResult() {
		return resolvedNode;
	}

	/**
	 * Returns the last resolved BaseNode, if it has the given type.
	 * Otherwise it returns null.
	 */
	public <S extends T> S getResult(Class<S> cls) {
		triedClasses.add(cls);
		if(cls.isInstance(unresolvedNode))
		{
			validClasses++;
			resolvedNode = cls.cast(unresolvedNode);
			return cls.cast(unresolvedNode);
		}

		return null;
	}

	/**
	 * Reports an error with all failed classes for the last resolved BaseNode.
	 */
	public void failed() {
		Class<?>[] classes = new Class<?>[triedClasses.size()];
		orginalNode.reportError("\"" + orginalNode + "\" is a " + orginalNode.getUseString() + " but a "
		        + Util.getStrListWithOr(triedClasses.toArray(classes), BaseNode.class, "getUseStr")
		        + " is expected");
	}

	/**
	 * Returns true, if exactly one valid result was returned for the last resolved BaseNode.
	 * Otherwise it reports an error with all expected classes.
	 */
	public boolean finish() {
		if(validClasses == 1) return true;
		failed();
		return false;
	}
}
