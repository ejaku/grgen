/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Node;

/**
 * Something, that produces an IR node.
 */
public interface NodeProducer {

	Node getNode();

}
