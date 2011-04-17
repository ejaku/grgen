/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A visitor that takes a parameter array.
 */
public abstract class ParamVisitor implements Visitor {

	private Object[] parameters;

	/**
	 * Get the i-th parameter.
	 * @param i The number of the parameter.
	 * @return The i-th parameter, null, if i was greater than the number of
	 * parameters.
	 */
	protected Object getParameter(int i) {
		return i < parameters.length ? parameters[i] : null;
	}

  /**
   * Make a new parameter visitor.
   * @param params The parameter for the visitor.
   */
  public ParamVisitor(Object[] params) {
    parameters = params;
  }

  /**
   * Make a new parameter visitor with one parameter.
   * @param param The parameter.
   */
  public ParamVisitor(Object param) {
  	this(new Object[] { param });
  }

}
