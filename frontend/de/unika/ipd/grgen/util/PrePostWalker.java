/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @file PrePostWalker.java
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
