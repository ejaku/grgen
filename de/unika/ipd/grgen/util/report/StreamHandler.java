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

import java.io.PrintStream;

/**
 * A stream handler for message reporting
 */
public class StreamHandler implements Handler {

	/** The output stream */
	private PrintStream stream;
	
	/** level of indentation */
	private int indent;
	
	/**
	 * Make a new stream report handler
	 * @param stream The stream all messages shall go to.
	 */
	public StreamHandler(PrintStream stream) {
		this.stream = stream;
		indent = 0;
	}
	
	private void doIndent() {
		for(int i = 0; i < indent; i++)
			stream.print("  ");
	}
	
	/**
	* @see de.unika.ipd.grgen.util.report.Handler#report(int, de.unika.ipd.grgen.util.report.Location, java.lang.String)
	*/
	public void report(int level, Location loc, String msg) {

		doIndent();
		stream.print("GrGen: [");

		if (level == ErrorReporter.ERROR)
			stream.print("ERROR ");
		else if (level == ErrorReporter.WARNING)
			stream.print("WARNING ");
		else if (level == ErrorReporter.NOTE)
			stream.print("NOTE ");

		stream.println((loc.hasLocation() ? "at " + loc.getLocation() + "] ": "") + msg);
	}

  /**
   * @see de.unika.ipd.grgen.util.report.Handler#entering(java.lang.String)
   */
  public void entering(String s) {
  	doIndent();
    stream.println(s + " {");
    indent++;
  }

  /**
   * @see de.unika.ipd.grgen.util.report.Handler#leaving()
   */
  public void leaving() {
		indent = indent > 0 ? indent - 1 : 0;
		doIndent();
		stream.println("}");
  }

}
