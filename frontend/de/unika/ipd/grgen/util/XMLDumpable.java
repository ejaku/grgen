/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
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

