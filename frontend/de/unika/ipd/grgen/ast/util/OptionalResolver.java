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
 * A resolver that applies its subresolver but always succeeds, even if the subresolver failed. 
 * Thus the resolver makes the passed subresolver optional: fine if it succeeds, if not, too.
 */
public class OptionalResolver extends Resolver
{
	private Resolver subResolver;
		
	/**
	 * Make a new optional resolver.
	 * @param resolver The resolver, that is optional.
	 */
	public OptionalResolver(Resolver subResolver)
	{
		this.subResolver = subResolver;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.util.Resolver#resolve(de.unika.ipd.grgen.ast.BaseNode, int)
	 */
	public boolean resolve(BaseNode node, int child)
	{
		subResolver.resolve(node, child);
		return true;
	}
}
