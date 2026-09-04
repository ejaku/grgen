/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack (.NET version by eja, the same holds for many files)
/// </summary>

namespace de.unika.ipd.grgen.util.report
{

	using System;
	using System.Text;
	using System.Diagnostics;
	using System.Text.RegularExpressions;

	/// <summary>
	/// A debug message reporter.
	/// </summary>
	public class DebugReporter : Reporter
	{
		private String pattern = ".*";
		private bool inclusive = true;
		private bool includeClassName = false;

		private string prefix = "";
		private bool enableStackTrace = true;

		public DebugReporter(int mask)
		{
			Mask = mask;
		}

		/// <summary>
		/// Set the class filter.
		/// The class filter is a regular expression. Each class calling
		/// this debug reporter is matched against this regex. Only if the
		/// regex matches, the message is reported. </summary>
		/// <param name="value"> A regular expression. </param>
		public virtual string Filter
		{
			set
			{
				pattern = value;
			}
		}

		/// <summary>
		/// Determines the meaning of the filter.
		/// If <code>value</code> is true, than all debug zones matching
		/// the filter are reported, all other are ignored. If set to false,
		/// All debug zones not matching the filter are entered, the others
		/// are ignored. </summary>
		/// <param name="value"> Inclusive or exclusive filtering. </param>
		public virtual bool FilterInclusive
		{
			set
			{
				inclusive = value;
			}
		}

		public virtual bool StackTrace
		{
			set
			{
				enableStackTrace = value;
			}
		}

		protected internal virtual void MakePrefix()
		{
			if(enableStackTrace)
			{
				StackFrame[] st = new StackTrace().GetFrames();
				StackFrame sf = st[2];
				StringBuilder sb = new StringBuilder();
				for(int i = 0; i < st.Length; i++)
					sb.Append(' ');

				if(includeClassName)
				{
					string className = sf.GetMethod().DeclaringType.Name; // maybe runtime type would be preferable...
					sb.Append(className);
					sb.Append('.');
				}

				sb.Append(sf.GetMethod().Name);
				prefix = sb.ToString();
			}
			else
				prefix = "";
		}

		/// <summary>
		/// Checks, whether a message supplied with this level will be reported </summary>
		/// <param name="channel"> The channel to check </param>
		/// <returns> true, if the message would be reported, false if not. </returns>
		public override bool WillReport(int channel)
		{
			int res = inclusive ? 1 : 0;

			if(prefix.Length != 0)
			{
				bool matches = Regex.IsMatch(prefix, pattern);
				res += matches ? 1 : 0;
			}

			return (res == 0 || res == 2) && base.WillReport(channel);
		}

		public override void Report(int level, Location loc, string msg)
		{
			MakePrefix();
			base.Report(level, loc, prefix + ": " + msg);
		}

		public override void Report(int channel, string msg)
		{
			MakePrefix();
			base.Report(channel, EmptyLocation.EmptyLoc,
					prefix + ": " + msg);
		}
	}

}
