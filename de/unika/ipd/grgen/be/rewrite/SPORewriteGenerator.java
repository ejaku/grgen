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
import java.util.Set;

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
		Collection commonNodes = r.getCommonNodes();
		Collection commonEdges = r.getCommonEdges();
		Graph right = r.getRight();
		Graph left = r.getLeft();
		Collection w, nodesToInsert;
		
		assert getClass().isAssignableFrom(handler.getRequiredRewriteGenerator());
		
		// Call the start function.
		handler.start(r);
		
		// First of all, add the nodes that have to be inserted.
		// This makes the redirections possible. They can only be applied,
		// if all nodes (the ones to be deleted, and the ones to be inserted)
		// are present.
		nodesToInsert = right.getNodes(new HashSet());
		nodesToInsert.removeAll(commonNodes);
		
		// Only consider redirections and node insertions, if we truly have
		// to insert some nodes, i.e. The nodesToInsert set has elements
		handler.insertNodes(nodesToInsert);
		
		// All edges, that occur only on the left side have to be removed.
		w = left.getEdges(new HashSet());
		w.removeAll(commonEdges);
		handler.deleteEdges(w);
		
		w = left.getNodes(new HashSet());
		Map nodeTypeChangeMap = new HashMap();
		
		// Change types of nodes.
		for (Iterator it = w.iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			if (n.typeChanges())
				nodeTypeChangeMap.put(n, n.getReplaceType());
		}
		handler.changeNodeTypes(nodeTypeChangeMap);

		// Delete all nodes to delete and the incident edges.
		w.removeAll(commonNodes);
		handler.deleteEdgesOfNodes(w);
		handler.deleteNodes(w);
		
		
		w = right.getEdges(new HashSet());
		w.removeAll(commonEdges);
		handler.insertEdges(w);
		
		// ... and the finish function.
		handler.finish();
	}

}
