/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


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
