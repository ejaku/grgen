/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A basic walker
 */
public interface Walker {
	public abstract void reset();
	public abstract void walk(Walkable w);
}
