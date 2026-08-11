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

using System;
using System.Text;

/// <summary>
/// A debug message reporter.
/// </summary>
public class DebugReporter : Reporter
{
	private void InitializeInstanceFields()
	{
		matcher = pattern.Matcher("");
	}

	private Pattern pattern = Pattern.compile(".*");
	private Matcher matcher;
	private bool inclusive = true;
	private bool includeClassName = false;

	private string prefix = "";
	private bool enableStackTrace = true;

	public DebugReporter(int mask)
	{
		InitializeInstanceFields();
		Mask = mask;
	}

	/// <summary>
	/// Set the class filter.
	/// The class filter is a regular expression. Each class calling
	/// this debug reporter is matched against this regex. Only if the
	/// regex matches, the message is reported. </summary>
	/// <param name="regex"> A regular expression. </param>
	public virtual string Filter
	{
		set
		{
			pattern = Pattern.Compile(value);
			matcher = pattern.Matcher("");
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
			StackTraceElement[] st = (new Exception()).GetStackTrace();
			StackTraceElement ste = st[2];
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < st.Length; i++)
				sb.Append(' ');
			string className = ste.GetClassName();

			int lastDot = className.LastIndexOf('.');
			if(lastDot != -1)
				className = className.Substring(lastDot + 1);

			if(includeClassName)
			{
				sb.Append(className);
				sb.Append('.');
			}
			sb.Append(ste.GetMethodName());
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
			bool matches = matcher.Reset(prefix).Matches();
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
