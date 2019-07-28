/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
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

		stream.println((loc.hasLocation() ? "at " + loc.getLocation() + "] ": "at ?] ") + msg);
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
