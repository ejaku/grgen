/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.invocation
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
using ProcedureBase = de.unika.ipd.grgen.ir.executable.ProcedureBase;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// An external procedure method invocation.
/// </summary>
public class ExternalProcedureMethodInvocation : ProcedureInvocationBase
{
	/// <summary>
	/// The owner of the procedure method. </summary>
	private Qualification ownerQual;
	private Variable ownerVar;

	/// <summary>
	/// The procedure of the procedure method invocation expression. </summary>
	protected internal ExternalProcedure externalProcedure;

	public ExternalProcedureMethodInvocation(Qualification ownerQual, ExternalProcedure externalProcedure)
		: base("external procedure method invocation")
	{

		this.ownerQual = ownerQual;
		this.externalProcedure = externalProcedure;
	}

	public ExternalProcedureMethodInvocation(Variable ownerVar, ExternalProcedure externalProcedure)
		: base("external procedure method invocation")
	{

		this.ownerVar = ownerVar;
		this.externalProcedure = externalProcedure;
	}

	public virtual Qualification OwnerQual
	{
		get
		{
			return ownerQual;
		}
	}

	public virtual Variable OwnerVar
	{
		get
		{
			return ownerVar;
		}
	}

	public override ProcedureBase ProcedureBase
	{
		get
		{
			return externalProcedure;
		}
	}

	public virtual ExternalProcedure ExternalProc
	{
		get
		{
			return externalProcedure;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(ownerQual != null)
		{
			ownerQual.CollectNeededEntities(needs);
			if(ownerQual.Owner != null)
			{
				if(ownerQual.Owner is GraphEntity)
					needs.Add((GraphEntity)ownerQual.Owner);
			}
		}
		else
		{
			if(!IsGlobalVariable(ownerVar))
				needs.Add(ownerVar);
		}
		foreach(Expression child in WalkableChildren)
			child.CollectNeededEntities(needs);
	}
}

}
