/**
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


/**
 * A factory that can make the graph tables.
 */
public interface GraphTableFactory {

	NodeTable nodeTable(Node node);
	EdgeTable edgeTable(Edge edge);
  AttributeTable nodeAttrTable(Node node);
  AttributeTable edgeAttrTable(Edge edge);
	
  NodeTable originalNodeTable();
  EdgeTable originalEdgeTable();
  AttributeTable originalNodeAttrTable();
  AttributeTable originalEdgeAttrTable();
  
}
