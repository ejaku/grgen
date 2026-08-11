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

using Ident = de.unika.ipd.grgen.ir.Ident;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// An external procedure.
/// </summary>
public class ExternalProcedure : ProcedureBase
{
	/// <summary>
	/// A list of the pattern parameters </summary>
	private readonly List<Type> paramTypes = new List<Type>();

	/// <param name="name"> The name of the external procedure. </param>
	/// <param name="ident"> The identifier that identifies this object. </param>
	public ExternalProcedure(string name, Ident ident)
		: base(name, ident)
	{
	}

	/// <summary>
	/// Add a parameter type to the external procedure. </summary>
	public virtual void AddParameterType(Type paramType)
	{
		paramTypes.Add(paramType);
	}

	/// <summary>
	/// Get all parameter types of this external procedure. </summary>
	public override IList<Type> ParameterTypes
	{
		get
		{
		return paramTypes.AsReadOnly();
		}
	}
}

}
