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

/**
 * A resolver that always succeeds.
 * This resolver even succeeds, if the resolver passed as an argument 
 * to the constructor doesn't succeed. But the passed resolver is 
 * evaluated in any case, though.
 * This has the meaning of an optional resolver. If the passed resolver
 * resolves anything, then ok, else also.
 */
public class OptionalResolver extends OneOfResolver {

	/** An "always succeed resolver. */
  private static final Resolver alwaysSucceed = new Resolver() {
		public boolean resolve(BaseNode node, int child) {
			return true;
		}
	};

	/**
	 * Make a new optional resolver. 
	 * @param resolver The resolver, that is optional.
	 */
	public OptionalResolver(Resolver resolver) {
		super(new Resolver[] { resolver, alwaysSucceed });
	}
	
}
