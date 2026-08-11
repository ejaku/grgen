/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util
{
using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;
using Reporter = de.unika.ipd.grgen.util.report.Reporter;

/// <summary>
/// Base class for all grgen facilities.
/// This class defines basic facilities and behaviour for all grgen classes.
/// </summary>
public class Base : Id
{
	/// <summary>
	/// static id counter </summary>
	private static long currId = 1;

	/// <summary>
	/// The id of this object </summary>
	private string id;

	/// <summary>
	/// constants for debug reporting </summary>
	public const int NOTE = 4;

	/// <summary>
	/// The debug reporter for debugging </summary>
	public static Reporter debug;

	/// <summary>
	/// The error reporter for error reporting </summary>
	public static ErrorReporter error;

	/// <summary>
	/// Set the reporting facilities of the base class </summary>
	/// <param name="debug"> The debug reporter </param>
	/// <param name="error"> The error reporter </param>
	public static void SetReporters(Reporter debug, ErrorReporter error)
	{
		Base.debug = debug;
		Base.error = error;
	}

	/// <summary>
	/// Get a new ID for this object.
	/// </summary>
	public Base()
	{
		id = "" + currId++;
	}

	/// <seealso cref="de.unika.ipd.grgen.util.ID.getId()"/>
	public virtual string Id
	{
		get
		{
			return id;
		}
	}
}

}
