/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.util.Util;

/**
 * Base class for identifier resolvers,
 * replacing an identifier node with it's corresponding declaration node.
 * Searching of the declaration occurs within the subclasses,
 * which must overwrite resolveIdent for that purpose.
 */
@Deprecated
public abstract class IdentResolver extends Resolver {
	/** The set of classes the resolved node must be an instance of */
	private Class<?>[] classes;

	/** A string with names of the classes, which are expected. */
	private String expectList;


	/**
	 * Make a new ident resolver.
	 * @param classes An array of classes the resolved ident
	 * must be an instance of.
	 */
	protected IdentResolver(Class<?>[] classes) {
		this.classes = classes;

		try {
			expectList = Util.getStrListWithOr(
				classes, BaseNode.class, "getUseStr");
		}
		catch(Exception e) {
		}
	}

	/**
	 * @see de.unika.ipd.grgen.ast.util.Resolver#resolve(de.unika.ipd.grgen.ast.BaseNode)
	 * The function resolves an IdentNode into it's declaration node.
	 * If the node <code>node</code> is not an IdentNode, the method returns it.
	 */
	public BaseNode resolve(BaseNode node) {
		debug.report(NOTE, "child is a: " + node.getName() + " (" + node + ")");
		/*if(!(node instanceof IdentNode)) {
			// if the desired node isn't an identifier everything is fine, return true
			// reportError(node, "Expected an identifier, not a \"" + node.getName() + "\"");
			return node;
		}*/
		
		BaseNode get = node;
		if (node instanceof IdentNode) {
    		IdentNode identNode = (IdentNode)node;
    		get = resolveIdent(identNode);
    		debug.report(NOTE, "resolved to a: " + get.getName());
		}

		// Check, if the class of the resolved node is one of the desired classes.
		for(int i = 0; i < classes.length; i++) {
			if(classes[i].isInstance(get)) {
				debug.report(NOTE, "type of resolved node fits");
				return get;
			}
		}

		reportError(node, "\"" + node + "\" is a " + get.getUseString() +
						" but a " + expectList + " is expected");
		return null;
	}

	/**
	 * Get a default resolution if the resolving fails (error node).
	 */
	protected BaseNode getDefaultResolution() {
		return BaseNode.getErrorNode();
	}

	/**
	 * Get the resolved AST node for an Identifier.
	 * This can be the declaration, the identifier occurs in, for example.
	 * See {@link DeclResolver} as an example.
	 * @param n The identifier.
	 * @return The resolved AST node.
	 */
	protected abstract BaseNode resolveIdent(IdentNode n);
}

