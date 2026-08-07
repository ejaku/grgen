/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{

using System.Collections.Generic;

/// <summary>
/// A collection of annotations.
/// </summary>
public interface Annotations
{
	bool ContainsKey(string key);

	object Get(string key);

	bool IsInteger(string key);

	bool IsBoolean(string key);

	bool IsString(string key);

	bool IsFlagSet(string key);

	void Put(string key, object value);

	ISet<string> KeySet();
}

}
