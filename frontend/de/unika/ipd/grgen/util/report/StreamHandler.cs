/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util.report
{

/// <summary>
/// A stream handler for message reporting
/// </summary>
public class StreamHandler : Handler
{
	/// <summary>
	/// The output stream </summary>
	private PrintStream stream;

	/// <summary>
	/// level of indentation </summary>
	private int indent;

	/// <summary>
	/// Make a new stream report handler </summary>
	/// <param name="stream"> The stream all messages shall go to. </param>
	public StreamHandler(PrintStream stream)
	{
		this.stream = stream;
		indent = 0;
	}

	private void DoIndent()
	{
		for(int i = 0; i < indent; i++)
			stream.Print("  ");
	}

	/// <seealso cref="de.unika.ipd.grgen.util.report.Handler.report(int, de.unika.ipd.grgen.util.report.Location, java.lang.String)"/>
	public virtual void Report(int level, Location loc, string msg)
	{
		DoIndent();
		stream.Print("GrGen: [");

		if(level == ErrorReporter.ERROR)
			stream.Print("ERROR ");
		else if(level == ErrorReporter.WARNING)
			stream.Print("WARNING ");
		else if(level == ErrorReporter.NOTE)
			stream.Print("NOTE ");

		stream.Println((loc.HasLocation() ? "at " + loc.Location + "] " : "at ?] ") + msg);
	}

	/// <seealso cref="de.unika.ipd.grgen.util.report.Handler.entering(java.lang.String)"/>
	public virtual void Entering(string s)
	{
		DoIndent();
		stream.Println(s + " {");
		indent++;
	}

	/// <seealso cref="de.unika.ipd.grgen.util.report.Handler.leaving()"/>
	public virtual void Leaving()
	{
		indent = indent > 0 ? indent - 1 : 0;
		DoIndent();
		stream.Println("}");
	}
}

}
