/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// VCGDUmperFactory.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{

	using System.IO;

	using Sys = de.unika.ipd.grgen.Sys;

	public class VCGDumperFactory : GraphDumperFactory
	{
		private Sys sys;

		public VCGDumperFactory(Sys sys)
		{
			this.sys = sys;
		}

		public virtual GraphDumper Get(string fileNamePart)
		{
			string fileName = fileNamePart + ".vcg";
			Stream os = sys.CreateDebugFile(new File(fileName));
			PrintStream ps = new PrintStream(os);
			return new VCGDumper(ps);
		}
	}

}
