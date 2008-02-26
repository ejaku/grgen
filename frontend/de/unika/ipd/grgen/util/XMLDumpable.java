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
 * XMLDumpable.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import java.util.Map;

/**
 * Something that can be serialized to an XML file.
 */
public interface XMLDumpable {

	/**
	 * Get the name of the tag.
	 * @return The tag string.
	 */
	String getTagName();

	/**
	 * Name of the tag that expression a reference to
	 * this object.
	 * @return The ref tag name.
	 */
	String getRefTagName();

	/**
	 * Add the "fields" of this object.
	 * You can associate an object of some type with either
	 * an Iterator that gives all the subobject that should be
	 * included in this object's body or any other object.
	 * If the value object is not of type iterator, a key="value"
	 * pair will be added to the tag's attributes. If the value
	 * object is of type iterator, a new block
	 * <key>
	 *   All elements in the iterator are dumped ...
	 * </key>
	 * will be added to the object body.
	 * @param fields The fields of this object.
	 */
	void addFields(Map<String, Object> fields);

	/**
	 * Get a unique ID for this object.
	 * @return A unique ID.
	 */
	String getXMLId();

}

