/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr;


/**
 * Somthing that has a numerical id.
 */
public interface IntegerId {

	/** The invalid ID. */
	int INVALID = -1;
	
	int getId();
	
}
