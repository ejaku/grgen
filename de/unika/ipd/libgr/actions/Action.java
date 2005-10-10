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
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;

import de.unika.ipd.libgr.Named;
import de.unika.ipd.libgr.graph.Graph;


/**
 * A graph action.
 */
public interface Action extends Named {

	/**
	 * Apply this action to a graph.
	 * @param graph The graph to apply this action to.
	 * @return The found matches.
	 */
	Matches apply(Graph graph);
	
	/**
	 * Finish this action.
	 * This method needs to be called to free all resources associated with it.
	 * @param match A match contained in the matches obtained by {@link #apply(Graph)}.
	 */
	void finish(Match m);

}
