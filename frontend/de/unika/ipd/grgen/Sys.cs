/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Sys.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen
{

	using System.IO;

	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	public interface Sys
	{
		DirectoryInfo ModelPath {get;}

		ErrorReporter ErrorReporter {get;}

		Stream CreateDebugFile(FileInfo file);

		bool MayFireEvents();

		bool MayFireDebugEvents();

		bool EmitProfilingInstrumentation();
	}

}
