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

/**
 * A reporter that eats every thing up
 */
public class NullReporter extends Reporter {

  /**
   * Do nothing here
   * @see de.unika.ipd.grgen.util.report.Reporter#entering(String)
   */
  public void entering(String s) {
  }

  /**
   * Do nothing here
   * @see de.unika.ipd.grgen.util.report.Reporter#leaving()
   */
  public void leaving() {
  }

  /**
   * Do nothing here
   * @see de.unika.ipd.grgen.util.report.Reporter#report(int, de.unika.ipd.grgen.util.report.Location, java.lang.String)
   */
  public void report(int channel, Location loc, String msg) {
  }

  /**
   * Do nothing here
   * @see de.unika.ipd.grgen.util.report.Reporter#report(int, java.lang.String)
   */
  public void report(int channel, String msg) {
  }

}
