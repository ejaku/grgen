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
