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
import de.unika.ipd.grgen.util.Base;

/**
 * something, that resolves a node to another node.
 */
public abstract class Resolver extends Base {
	/**
	 * Resolves a node to another node. (but doesn't replace the node in the AST)
	 * @param node The original node to resolve.
	 * @return The node the original node was resolved to (which might be the original node itself),
	 *         or null if the resolving failed
	 */
	public abstract BaseNode resolve(BaseNode node);

	/**
	 * Report an error during resolution.
	 * Some resolvers might want to overwrite this method, so
	 * {@link BaseNode#reportError(String)} is not used directly.
	 * @param node The node that caused the error.
	 * @param msg The error message to be printed.
	 */
	protected void reportError(BaseNode node, String msg) {
		node.reportError(msg);
	}
}

