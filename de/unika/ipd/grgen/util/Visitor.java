/**
 * @date Jul 20, 2003
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A Visitor
 */
public interface Visitor {
	public abstract void visit(Walkable n);
}
