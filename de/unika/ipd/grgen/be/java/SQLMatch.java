/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import de.unika.ipd.libgr.actions.Match;


/**
 * An Java/SQL match.
 */
final class SQLMatch implements Match {

	int[] ids;
	
	SQLMatch(int[] ids) {
		this.ids = ids;
	}
	
	public boolean isValid() {
		return true;
	}
	
}
