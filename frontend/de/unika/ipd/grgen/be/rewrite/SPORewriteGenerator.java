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
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.rewrite;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Rule;

/**
 * An abstract rewriter.
 * This rewriter implements the default SPO behaviour.
 *
 * This rewrite generator ensures following calling sequence for the methods
 * of the {@link de.unika.ipd.grgen.be.rewrite.RewriteHandler}:
 *
 * <ul>
 * <li>{@link RewriteHandler#start(Rule, Class)}</li>
 * <li>{@link RewriteHandler#insertNodes(Collection)}</li>
 * <li>{@link RewriteHandler#deleteEdges(Collection)}</li>
 * <li>{@link RewriteHandler#changeNodeTypes(Map)}</li>
 * <li>{@link RewriteHandler#deleteEdgesOfNodes(Collection)}</li>
 * <li>{@link RewriteHandler#deleteNodes(Collection)}</li>
 * <li>{@link RewriteHandler#insertEdges(Collection)\</li>
 * </ul>
 */
public class SPORewriteGenerator implements RewriteGenerator {
	
	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteGenerator#rewrite(de.unika.ipd.grgen.ir.Rule, de.unika.ipd.grgen.be.spo.RewriteHandler)
	 */
	public void rewrite(Rule r, RewriteHandler handler) {
		Collection<Node> commonNodes = r.getCommonNodes();
		Collection<Edge> commonEdges = r.getCommonEdges();
		Graph right = r.getRight();
		Graph left = r.getLeft();
		Collection<Edge> es = new HashSet<Edge>();
		
		
		assert getClass().isAssignableFrom(handler.getRequiredRewriteGenerator());
		
		// Call the start function.
		handler.start(r);
		
		// First of all, add the nodes that have to be inserted.
		// This makes the redirections possible. They can only be applied,
		// if all nodes (the ones to be deleted, and the ones to be inserted)
		// are present.
		Collection<Node> nodesToInsert = new HashSet<Node>(right.getNodes());
		nodesToInsert.removeAll(commonNodes);
		
		// Only consider redirections and node insertions, if we truly have
		// to insert some nodes, i.e. The nodesToInsert set has elements
		handler.insertNodes(nodesToInsert);
		
		es.clear();
		right.putEdges(es);
		es.removeAll(commonEdges);
		handler.insertEdges(es);
		
		
		// All edges, that occur only on the left side have to be removed.
		es.clear();
		left.putEdges(es);
		es.removeAll(commonEdges);
		handler.deleteEdges(es);
		
		Collection<Node> ns = new HashSet<Node>();
		ns.clear();
		left.putNodes(ns);
		Map<Node, Object> nodeTypeChangeMap = new HashMap<Node, Object>();
		
		// Change types of nodes.
		for (Iterator<Node> it = ns.iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			if (n.changesType()) {
				nodeTypeChangeMap.put(n, n.getRetypedNode().getType());
			}
		}
		handler.changeNodeTypes(nodeTypeChangeMap);
		
		
//				// add retyped nodes of R
//		if(a instanceof Rule)
//			for(Node n : ((Rule)a).getRight().getNodes())
//				if(n.isRetypedNode())
//					nodes.add(n);
		
		// Finally the evaluations.
		handler.generateEvals(r.getEvals());
		
		// Delete all nodes to delete and the incident edges.
		ns.removeAll(commonNodes);
		handler.deleteEdgesOfNodes(ns);
		handler.deleteNodes(ns);
		
		// ... and the finish function.
		handler.finish();
	}
	
}
