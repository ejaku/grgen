/**
 * Created on Mar 6, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr;


/**
 * A default implementation for the {@link de.unika.ipd.libgr.IntegerId} interface.
 * Equality of two IntegerId object is implemented by comparison of their IDs.
 */
public class DefaultIntegerId implements IntegerId {

	protected int id;
	
	/**
	 * Make a new object with and ID.
	 * @param id An ID.
	 */
	protected DefaultIntegerId(int id) {
		this.id = id;
	}
	
	/**
	 * Get the ID of this object.
	 * @return The ID.
	 */
	public int getId() {
		return id;
	}
	
	/**
	 * Check, if this object represents the same as <code>obj</code>.
	 * @param obj Another object.
	 * @return true, if both objects represent the same (they have the same IDs).
	 */
	public boolean equals(Object obj) {
		return obj instanceof IntegerId ? ((IntegerId) obj).getId() == id : false;
	}
	
	/**
	 * Get the string representation of this id.
	 * @return A string identifying this id.
	 */
	public String toString() {
		return "" + id;
	}

}
