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
 * @file PostWalker.java
 * @author shack
 * @date Jul 20, 2003
 */
package de.unika.ipd.grgen.util;

import java.util.HashSet;
import java.util.Set;

/**
 * A walker calling visitors
 * pre before descending to the first child
 * post after ascending from the last child.
 */
public class PrePostWalker extends Base implements Walker {
	private Set<Walkable> visited;
	private Visitor pre, post;

	/**
	 * Creates PrePostWalker
	 * @param pre Visitor called before descending to the first child
	 * @param post Visitor called after ascending from the last child
	 */
	public PrePostWalker(Visitor pre, Visitor post) {
		this.pre = pre;
		this.post = post;
		visited = new HashSet<Walkable>();
	}

	public void reset() {
		visited.clear();
	}

	public void walk(Walkable node) {

		if (!visited.contains(node)) {
			if(node!=null) {
				visited.add(node);

				if (pre != null) {
					pre.visit(node);
				}

				for (Walkable p : node.getWalkableChildren()) {
					walk(p);
				}

				if (post != null) {
					post.visit(node);
				}
			} else
				Base.error.error("Node was null, while walking");
		}
	}
}
