/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.List;
import java.util.Vector;

/**
 * A graph rewrite sequence definition.
 */
public class Sequence extends Identifiable {
	private Exec exec;
	private List<ExecVariable> inParams = new Vector<ExecVariable>();
	private List<ExecVariable> outParams = new Vector<ExecVariable>();

	public Sequence(Ident ident, Exec exec) {
		super("sequence", ident);
		this.exec = exec;
	}

	public Exec getExec() {
		return exec;
	}

	public void addInParam(ExecVariable inParam) {
		inParams.add(inParam);
	}

	public List<ExecVariable> getInParameters() {
		return Collections.unmodifiableList(inParams);
	}

	public void addOutParam(ExecVariable outParam) {
		outParams.add(outParam);
	}

	public List<ExecVariable> getOutParameters() {
		return Collections.unmodifiableList(outParams);
	}
}
