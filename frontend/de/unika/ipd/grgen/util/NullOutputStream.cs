/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// NullOutputStream.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{

	using System;
	using System.IO;

	// replaced in .NET version by Stream.Null, TODO: purge
	/*public class NullOutputStream : Stream
	{
		public static readonly Stream STREAM = new NullOutputStream();

		public override void Write(int p1)
		{
			Console.WriteLine("write to null stream");
		}

		private NullOutputStream()
		{
		}
	}*/

}
