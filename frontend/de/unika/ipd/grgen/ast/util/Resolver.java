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

import java.util.LinkedList;
import java.util.Collection;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.Base;

/**
 * something, that resolves a node to another node.
 */
public abstract class Resolver extends Base {
	
	protected static class ErrorMessage {
		
		private BaseNode node;
		
		private String msg;
		
		private Resolver resolver;
		
		public ErrorMessage(Resolver resolver, BaseNode node, String msg) {
			this.resolver = resolver;
			this.node = node;
			this.msg = msg;
		}
		
		public void print() {
			node.reportError(msg);
		}
		
		public Resolver getResolver() {
			return resolver;
		}
		
	}
	
	/** A collection holding all error messages, this resolver produced. */
	private Collection<ErrorMessage> errorMessages = new LinkedList<ErrorMessage>();

	/**
	 * Resolve a node.
	 * @param node The parent node of the node to resolve.
	 * @param child The index of the node to resolve in <code>node</code>'s
	 * children.
	 * @return true, if the resolving was successful, false, if not.
	 */
	public abstract boolean resolve(BaseNode node, int child);
	
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
	
	/**
	 * Set the place, where error messages are put to.
	 * This can be used by resolvers, which call other resolvers to collect
	 * the error messages of these subresolvers. 
	 * @param c A collection, where objects instance of {@link ErrorMessage}
	 * are inserted.
	 */
	protected final void setErrorQueue(Collection<ErrorMessage> c) {
		errorMessages = c;
	}
	
	/**
	 * Print all error messages concerning this resolver.
	 */
	public void printErrors() {
	}

}
