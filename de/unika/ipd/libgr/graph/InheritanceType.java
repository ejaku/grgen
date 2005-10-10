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
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A type.
 */
public interface InheritanceType extends Type {

	/**
	 * Check if this type is a root type.
	 * @return true if this type is a root type.
	 */
	boolean isRoot();
	
	/**
	 * Get all direct super types of this one.
	 * @return An iterator.
	 */
	Iterator<Object> getSuperTypes();
	
	/**
	 * Get all types that inherit from this one.
	 * @return An iterator.
	 */
	Iterator<Object> getSubTypes();
	
	/**
	 * Checks, if this type is also of type <code>t</type>
	 * @param t The type to check for.
	 * @return true, if this type is also of type <code>t</code>
	 */
	boolean isA(InheritanceType t);
	
	
}
