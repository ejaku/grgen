/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

/**
 * A Handler to handle reporting.
 */
public interface Handler {
	public abstract void report(int level, Location loc, String msg);
	public abstract void entering(String s);
	public abstract void leaving();
}
