/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss, Michael Beck
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.Collection;
import java.util.LinkedList;

public class Evaluation extends IR {
	/**
	 * The evaluations constituting an Evaluation of a rule.
	 * They are organized in a list, since their order is vital.
	 * Applying them in a random order will lead to different results.
	 */
	private LinkedList<IR> evaluations = new LinkedList<IR>();

	Evaluation() {
		super("eval");
	}

	/** Adds an element to the list of evaluations. */
	public void add(IR aeval) {
		evaluations.add(aeval);
	}

	/** @return the list of evaluations as collection */
	public Collection<? extends IR> getWalkableChildren() {
		return evaluations;
	}
}

