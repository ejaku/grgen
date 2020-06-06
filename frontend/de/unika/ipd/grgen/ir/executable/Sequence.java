/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.Collections;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.ExecVariable;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;

/**
 * A graph rewrite sequence definition.
 */
public class Sequence extends Identifiable implements ContainedInPackage
{
	private String packageContainedIn;

	private Exec exec;

	private List<ExecVariable> inParams = new Vector<ExecVariable>();
	private List<ExecVariable> outParams = new Vector<ExecVariable>();

	public Sequence(Ident ident, Exec exec)
	{
		super("sequence", ident);
		this.exec = exec;
	}

	@Override
	public String getPackageContainedIn()
	{
		return packageContainedIn;
	}

	public void setPackageContainedIn(String packageContainedIn)
	{
		this.packageContainedIn = packageContainedIn;
	}

	public Exec getExec()
	{
		return exec;
	}

	public void addInParam(ExecVariable inParam)
	{
		inParams.add(inParam);
	}

	public List<ExecVariable> getInParameters()
	{
		return Collections.unmodifiableList(inParams);
	}

	public void addOutParam(ExecVariable outParam)
	{
		outParams.add(outParam);
	}

	public List<ExecVariable> getOutParameters()
	{
		return Collections.unmodifiableList(outParams);
	}
}
