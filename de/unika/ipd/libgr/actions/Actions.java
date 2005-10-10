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
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;

import java.util.Iterator;


/**
 * A bunch of actions.
 */
public interface Actions {

	/**
	 * Get the action with a certain name.
	 * @param name The name of the action.
	 * @return The corresponding Action object or <code>null</code> if
	 * nothing is known about an action named <code>name</code>.
	 */
	Action getAction(String name);

	/**
	 * Get an iterator iterating over all known actions.
	 * @return The iterator.
	 */
	Iterator actions();
	
}
