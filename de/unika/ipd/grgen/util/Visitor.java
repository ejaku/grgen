/**
 * @file Visitor.java
 * @author shack
 * @date Jul 20, 2003
 */
package de.unika.ipd.grgen.util;

/**
 * A Visitor
 */
public interface Visitor {
	public abstract void visit(Walkable n);
}
