/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util.report;

import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

/**
 * Base class for a reporting facility
 */
public abstract class Reporter {
	private int mask = 0;

	protected final Set<Handler> handlers = new HashSet<Handler>();


	/**
	 * Add a handler to this reporter
	 * @param handler The handler to add
	 */
	public void addHandler(Handler handler) {
		handlers.add(handler);
	}

	/**
	 * Remove a handler from this reporter
	 * @param handler The handler to remove
	 */
	public void removeHandler(Handler handler) {
		handlers.remove(handler);
	}

	/**
	 * Set the reporting level.
	 * Setting it to 0 will disable all reporting. Basically, all messages
	 * with reporting level smaller than <code>level</code> will be displayed.
	 * @param level The new level for the reporter.
	 */
	public void setMask(int mask) {
		this.mask = mask;
	}

	public void enableChannel(int channel) {
		mask |= channel;
	}

	public void disableChannel(int channel) {
		mask &= ~channel;
	}

	/**
	 * Disables reporting on this reporter.
	 * Re-enable it by setting the level to some value > 0
	 */
	public void disable() {
		mask = 0;
	}

	/**
	 * Check whether this reporter is disabled
	 * @return true, if no message will be reported, false otherwise.
	 */
	public boolean isDisabled() {
		return mask == 0;
	}


	/**
	 * Checks, whether a message supplied with this level will be reported
	 * @param channel The channel to check
	 * @return true, if the message would be reported, false if not.
	 */
	public boolean willReport(int channel) {
		return (channel & mask) != 0;
	}

	public void report(int level, Location loc, String msg) {
		if(willReport(level)) {
			Iterator<Handler> it = handlers.iterator();
			while(it.hasNext()) {
				Handler h = it.next();
				h.report(level, loc, msg);
			}
		}
	}

	public void report(int channel, String msg) {
		report(channel, EmptyLocation.getEmptyLocation(), msg);
	}
}

