/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A bad IR element.
 * This used in case of an error.
 */
public class Bad extends IR {

  public Bad() {
    super("bad");
  }

  public boolean isBad() {
  	return true;
  }

}
