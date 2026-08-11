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

using System.Collections.Generic;

/// <summary>
/// Base class for a reporting facility
/// </summary>
public abstract class Reporter
{
	private int mask = 0;

	protected internal readonly ISet<Handler> handlers = new HashSet<Handler>();

	/// <summary>
	/// Add a handler to this reporter </summary>
	/// <param name="handler"> The handler to add </param>
	public virtual void AddHandler(Handler handler)
	{
		handlers.Add(handler);
	}

	/// <summary>
	/// Remove a handler from this reporter </summary>
	/// <param name="handler"> The handler to remove </param>
	public virtual void RemoveHandler(Handler handler)
	{
		handlers.Remove(handler);
	}

	/// <summary>
	/// Set the reporting level.
	/// Setting it to 0 will disable all reporting. Basically, all messages
	/// with reporting level smaller than <code>level</code> will be displayed. </summary>
	/// <param name="level"> The new level for the reporter. </param>
	public virtual int Mask
	{
		set
		{
			this.mask = value;
		}
	}

	public virtual void EnableChannel(int channel)
	{
		mask |= channel;
	}

	public virtual void DisableChannel(int channel)
	{
		mask &= ~channel;
	}

	/// <summary>
	/// Disables reporting on this reporter.
	/// Re-enable it by setting the level to some value > 0
	/// </summary>
	public virtual void Disable()
	{
		mask = 0;
	}

	/// <summary>
	/// Check whether this reporter is disabled </summary>
	/// <returns> true, if no message will be reported, false otherwise. </returns>
	public virtual bool IsDisabled()
	{
		return mask == 0;
	}

	/// <summary>
	/// Checks, whether a message supplied with this level will be reported </summary>
	/// <param name="channel"> The channel to check </param>
	/// <returns> true, if the message would be reported, false if not. </returns>
	public virtual bool WillReport(int channel)
	{
		return (channel & mask) != 0;
	}

	public virtual void Report(int level, Location loc, string msg)
	{
		if(WillReport(level))
		{
			foreach(Handler h in handlers)
				h.Report(level, loc, msg);
		}
	}

	public virtual void Report(int channel, string msg)
	{
		Report(channel, EmptyLocation.EmptyLoc, msg);
	}
}

}
