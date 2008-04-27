/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
