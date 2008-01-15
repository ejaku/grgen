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
 * @author Rubino Geiss, Michael Beck
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.Collection;
import java.util.LinkedList;

public class Evaluation extends IR {
	/**
	 * The evaluations constituting an Evaluation of a rule.
	 * They are organized in a list, since their order is vital.
	 * Applying them in a random order will lead to different results.
	 */
	private LinkedList<IR> evaluations = new LinkedList<IR>();
	
	Evaluation() {
		super("eval");
	}
	
	/** Adds an element to the list of evaluations. */
	public void add(IR aeval) {
		evaluations.add(aeval);
	}
	
	/** @return the list of evaluations as collection */
	public Collection<? extends IR> getWalkableChildren() {
		return evaluations;
	}
}

