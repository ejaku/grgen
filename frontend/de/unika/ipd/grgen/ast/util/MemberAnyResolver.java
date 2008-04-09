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
import de.unika.ipd.grgen.ast.RuleDeclNode;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.Util;
import java.util.Map;
import java.util.Vector;

/**
 * A resolver, that resolves a declaration node from an identifier.
 */
public class MemberAnyResolver extends Base
{
	private BaseNode unresolvedNode;
	private BaseNode resolvedNode;
	private Vector<Class> triedClasses = new Vector<Class>();
	private int validClasses;

	/**
	 * Tries to resolve the given BaseNode.
	 * @returns True, if the BaseNode was resolved.
	 *          False, when an error occurred (the error is reported).
	 */
	public boolean resolve(BaseNode node) {
		triedClasses.clear();
		validClasses = 0;

		unresolvedNode = node;
		if(!(unresolvedNode instanceof IdentNode)) {
			resolvedNode = unresolvedNode;
			return true;
		}

		IdentNode identNode = (IdentNode) unresolvedNode;
		resolvedNode = identNode.getDecl();

		if (resolvedNode instanceof InvalidDeclNode) {
			DeclNode scopeDecl = identNode.getScope().getIdentNode().getDecl();
			if(scopeDecl instanceof RuleDeclNode) {
				identNode.reportError("Undefined identifier \"" + identNode.toString() + "\"");
				return false;
			}
			else {
				InheritanceTypeNode typeNode = (InheritanceTypeNode) scopeDecl.getDeclType();
				Map<String, DeclNode> allMembers = typeNode.getAllMembers();
				resolvedNode = allMembers.get(identNode.toString());
				if(resolvedNode == null) {
					identNode.reportError("Undefined member " + identNode.toString()
							+ " of " + typeNode.getDecl().getIdentNode());
					return false;
				}
			}
		}
		return true;
	}

	public BaseNode getResult() {
		return resolvedNode;
	}

	/**
	 * Returns the last resolved BaseNode, if it has the given type.
	 * Otherwise it returns null.
	 */
	public <T> T getResult(Class<T> cls) {
		triedClasses.add(cls);
		if(cls.isInstance(resolvedNode))
		{
			validClasses++;
			return cls.cast(resolvedNode);
		}

		return null;
	}

	/**
	 * Reports an error with all failed classes for the last resolved BaseNode.
	 */
	public void failed() {
		Class<?>[] classes = new Class<?>[triedClasses.size()];
		unresolvedNode.reportError("\"" + unresolvedNode + "\" is a " + resolvedNode.getUseString() + " but a "
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
