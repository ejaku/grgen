/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.util
{
	using Entity = de.unika.ipd.grgen.ir.Entity;

	/// <summary>
	/// @author adam
	/// Something that is being retyped during the rewrite
	/// </summary>
	public interface Retyped
	{
		Entity OldEntity {get;set;}

	}

}
