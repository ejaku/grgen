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
 * Created on May 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;


/**
 * Empty annotations.
 */
public class EmptyAnnotations implements Annotations {

	private static final Annotations EMPTY = new EmptyAnnotations();

	public static Annotations get() {
		return EMPTY;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#containsKey(java.lang.String)
	 */
	public boolean containsKey(String key) {
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#get(java.lang.String)
	 */
	public Object get(String key) {
		return null;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#put(java.lang.String, java.lang.Object)
	 */
	public void put(String key, Object value) {
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#isInteger(java.lang.String)
	 */
	public boolean isInteger(String key) {
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#isBoolean(java.lang.String)
	 */
	public boolean isBoolean(String key) {
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Annotations#isString(java.lang.String)
	 */
	public boolean isString(String key) {
		return false;
	}

}
