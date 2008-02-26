/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * A debug message reporter.
 */
public class DebugReporter extends Reporter {

	private Pattern pattern = Pattern.compile(".*");
	private Matcher matcher = pattern.matcher("");
	private boolean inclusive = true;
	private boolean includeClassName = false;

	private String prefix = "";
	private boolean enableStackTrace = true;

	public DebugReporter(int mask) {
		setMask(mask);
	}

	/**
	 * Set the class filter.
	 * The class filter is a regular expression. Each class calling
	 * this debug reporter is matched against this regex. Only if the
	 * regex matches, the message is reported.
	 * @param regex A regular expression.
	 */
	public void setFilter(String regex) {
		pattern = Pattern.compile(regex);
		matcher = pattern.matcher("");
	}

	/**
	 * Determines the meaning of the filter.
	 * If <code>value</code> is true, than all debug zones matching
	 * the filter are reported, all other are ignored. If set to false,
	 * All debug zones not matching the filter are entered, the others
	 * are ignored.
	 * @param value Inclusive or exclusive filtering.
	 */
	public void setFilterInclusive(boolean value) {
		inclusive = value;
	}

	public void setStackTrace(boolean enabled) {
		enableStackTrace = enabled;
	}

	protected void makePrefix() {
		if(enableStackTrace) {
			StackTraceElement[] st = (new Exception()).getStackTrace();
			StackTraceElement ste = st[2];
			StringBuffer sb = new StringBuffer();
			for(int i = 0; i < st.length; i++)
				sb.append(' ');
			String className = ste.getClassName();

			int lastDot = className.lastIndexOf('.');
			if(lastDot != -1)
				className = className.substring(lastDot + 1);

			if(includeClassName) {
				sb.append(className);
				sb.append('.');
			}
			sb.append(ste.getMethodName());
			prefix = sb.toString();
		} else
			prefix = "";
	}

	/**
	 * Checks, whether a message supplied with this level will be reported
	 * @param channel The channel to check
	 * @return true, if the message would be reported, false if not.
	 */
	public boolean willReport(int channel) {
		int res = inclusive ? 1 : 0;

		if(prefix.length() != 0) {
			boolean matches = matcher.reset(prefix).matches();
			res += matches ? 1 : 0;
		}

		return (res == 0 || res == 2) && super.willReport(channel);
	}

	public void report(int level, Location loc, String msg) {
		makePrefix();
		super.report(level, loc, prefix + ": " + msg);
	}

	public void report(int channel, String msg) {
		makePrefix();
		super.report(channel, EmptyLocation.getEmptyLocation(),
								 prefix + ": " + msg);

	}
}
