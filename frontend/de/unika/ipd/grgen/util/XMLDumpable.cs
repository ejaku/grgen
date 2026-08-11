/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// XMLDumpable.java
/// 
/// @author Created by Omnicore CodeGuide
/// </summary>

namespace de.unika.ipd.grgen.util
{

using System.Collections.Generic;

/// <summary>
/// Something that can be serialized to an XML file.
/// </summary>
public interface XMLDumpable
{
	/// <summary>
	/// Get the name of the tag. </summary>
	/// <returns> The tag string. </returns>
	string TagName {get;}

	/// <summary>
	/// Name of the tag that expression a reference to
	/// this object. </summary>
	/// <returns> The ref tag name. </returns>
	string RefTagName {get;}

	/// <summary>
	/// Add the "fields" of this object.
	/// You can associate an object of some type with either
	/// an Iterator that gives all the subobject that should be
	/// included in this object's body or any other object.
	/// If the value object is not of type iterator, a key="value"
	/// pair will be added to the tag's attributes. If the value
	/// object is of type iterator, a new block
	/// <key>
	///   All elements in the iterator are dumped ...
	/// </key>
	/// will be added to the object body. </summary>
	/// <param name="fields"> The fields of this object. </param>
	void AddFields(IDictionary<string, object> fields);

	/// <summary>
	/// Get a unique ID for this object. </summary>
	/// <returns> A unique ID. </returns>
	string XMLId {get;}
}

}
