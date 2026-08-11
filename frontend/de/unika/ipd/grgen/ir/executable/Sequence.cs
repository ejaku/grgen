/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.executable
{

using System.Collections.Generic;

using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
using Exec = de.unika.ipd.grgen.ir.Exec;
using ExecVariable = de.unika.ipd.grgen.ir.ExecVariable;
using Ident = de.unika.ipd.grgen.ir.Ident;
using Identifiable = de.unika.ipd.grgen.ir.Identifiable;

/// <summary>
/// A graph rewrite sequence definition.
/// </summary>
public class Sequence : Identifiable, ContainedInPackage
{
	private string packageContainedIn;

	private Exec exec;

	private List<ExecVariable> inParams = new List<ExecVariable>();
	private List<ExecVariable> outParams = new List<ExecVariable>();

	public Sequence(Ident ident, Exec exec)
		: base("sequence", ident)
	{
		this.exec = exec;
	}

	public virtual string PackageContainedIn
	{
		get
		{
			return packageContainedIn;
		}
		set
		{
			this.packageContainedIn = value;
		}
	}


	public virtual Exec Exec
	{
		get
		{
			return exec;
		}
	}

	public virtual void AddInParam(ExecVariable inParam)
	{
		inParams.Add(inParam);
	}

	public virtual IList<ExecVariable> InParameters
	{
		get
		{
			return inParams.AsReadOnly();
		}
	}

	public virtual void AddOutParam(ExecVariable outParam)
	{
		outParams.Add(outParam);
	}

	public virtual IList<ExecVariable> OutParameters
	{
		get
		{
			return outParams.AsReadOnly();
		}
	}
}

}
