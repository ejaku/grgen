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
import de.unika.ipd.grgen.ast.CollectNode;

/**
 * A resolver that resolves a collection node 
 * by applying its subresolver to all children of the collection node
 * TODO: eliminate that resolver
 */
public class CollectResolver extends Resolver
{
	private Resolver subResolver;

	public CollectResolver(Resolver subResolver)
	{
		this.subResolver = subResolver;
	}

	public boolean resolve(BaseNode node, int pos)
	{
		BaseNode childToResolve = node.getChild(pos);
		if(!(childToResolve instanceof CollectNode))
		{
			reportError(node, "Expecting \""
				+ BaseNode.getName(CollectNode.class) + "\", found \""
				+ childToResolve.getName() + "\" instead.");
				// TODO: this case returns true but reports an error - wanted that way?
			return true;
		}

		boolean res = true;
		CollectNode collectNode = (CollectNode)childToResolve;
		for (int i = 0; i < collectNode.children(); i++) {
			if (!subResolver.resolve(collectNode, i)) {
				res = false;
			}
		}
		return res;
	}
	
	public BaseNode resolve(BaseNode node) {
		assert(false);
		return null;
	}
}
