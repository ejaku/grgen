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
	/// The error reported class.
	/// </summary>
	public class ErrorReporter : Reporter
	{
		public const int ERROR = 1;
		public const int WARNING = 2;
		public const int NOTE = 4;

		protected internal static int errCount = 0;
		protected internal static int warnCount = 0;

		/// <summary>
		/// Create a new error reporter.
		/// </summary>
		public ErrorReporter()
		{
			Mask = ERROR | WARNING | NOTE;
		}

		/// <summary>
		/// Report an error at a given location.
		/// </summary>
		/// <param name="loc"> The location. </param>
		/// <param name="msg"> The error message. </param>
		public virtual void Error(Location loc, string msg)
		{
			if(msg.Equals("mismatched input '$' expecting RPAREN"))
				Report(ERROR, loc, msg + " -- forgot \"@\"?");
			else
				Report(ERROR, loc, msg);
			++errCount;
		}

		/// <summary>
		/// Report an error. </summary>
		/// <param name="msg"> </param>
		public virtual void Error(string msg)
		{
			Report(ERROR, msg);
			++errCount;
		}

		/// <summary>
		/// Report a warning at a given location.
		/// </summary>
		/// <param name="loc"> The location. </param>
		/// <param name="msg"> The warning message. </param>
		public virtual void Warning(Location loc, string msg)
		{
			Report(WARNING, loc, msg);
			++warnCount;
		}

		/// <summary>
		/// report a warning. </summary>
		/// <param name="msg"> The warning message. </param>
		public virtual void Warning(string msg)
		{
			Report(WARNING, msg);
			++warnCount;
		}

		/// <summary>
		/// Report a note at a given location.
		/// </summary>
		/// <param name="loc"> The location. </param>
		/// <param name="msg"> The note message. </param>
		public virtual void Note(Location loc, string msg)
		{
			Report(NOTE, loc, msg);
		}

		/// <summary>
		/// Report a note. </summary>
		/// <param name="msg"> The note message. </param>
		public virtual void Note(string msg)
		{
			Report(NOTE, msg);
		}

		/// <summary>
		/// Returns the number of occured errors.
		/// @return
		/// </summary>
		public static int ErrorCount
		{
			get
			{
				return errCount;
			}
		}

		/// <summary>
		/// Returns the number of occured warnings.
		/// @return
		/// </summary>
		public static int WarnCount
		{
			get
			{
				return warnCount;
			}
		}

		public static void ResetCounters()
		{
			errCount = 0;
			warnCount = 0;
		}
	}

}
