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
using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using NestingStatement = de.unika.ipd.grgen.ir.NestingStatement;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// A function (has a return type and parameters,
/// is a top-level object that contains nested statements, and may be contained in a package).
/// </summary>
public class Function : FunctionBase, ContainedInPackage, NestingStatement
{
	private string packageContainedIn;

	/// <summary>
	/// A list of the parameters </summary>
	private List<Entity> @params = new List<Entity>();

	/// <summary>
	/// A list of the parameter types, computed from the parameters </summary>
	private List<Type> parameterTypes = null;

	/// <summary>
	/// The computation statements </summary>
	private List<EvalStatement> computationStatements = new List<EvalStatement>();

	public Function(string name, Ident ident, Type retType)
		: base(name, ident, retType)
	{
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


	/// <summary>
	/// Add a parameter to the function. </summary>
	public virtual void AddParameter(Entity entity)
	{
		@params.Add(entity);
	}

	/// <summary>
	/// Get all parameters of this function. </summary>
	public virtual IList<Entity> Parameters
	{
		get
		{
			return @params.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a computation statement to the function. </summary>
	public virtual void AddStatement(EvalStatement eval)
	{
		computationStatements.Add(eval);
	}

	/// <summary>
	/// Get all computation statements of this function. </summary>
	public virtual ICollection<EvalStatement> Statements
	{
		get
		{
			return computationStatements.AsReadOnly();
		}
	}

	/// <summary>
	/// Get all parameter types of this function. </summary>
	public override IList<Type> ParameterTypes
	{
		get
		{
			if(parameterTypes == null)
			{
				parameterTypes = new List<Type>();
				foreach(Entity entity in Parameters)
					parameterTypes.Add(entity.Type);
			}
			return parameterTypes.AsReadOnly();
		}
	}
}

}
