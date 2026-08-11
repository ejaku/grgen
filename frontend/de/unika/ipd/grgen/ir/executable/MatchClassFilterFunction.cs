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
using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// Base type for match class filter functions (internal and external).
/// </summary>
public abstract class MatchClassFilterFunction : Identifiable, MatchClassFilter, ContainedInPackage
{
	private string packageContainedIn;

	/// <summary>
	/// A list of the parameters </summary>
	protected internal List<Entity> @params = new List<Entity>();

	/// <summary>
	/// A list of the parameter types, computed from the parameters </summary>
	protected internal List<Type> parameterTypes = null;

	/// <summary>
	/// The match class we're a filter for </summary>
	protected internal DefinedMatchType matchClass;

	public MatchClassFilterFunction(string name, Ident ident)
		: base(name, ident)
	{
	}

	public virtual DefinedMatchType MatchClass
	{
		set
		{
			this.matchClass = value;
		}
		get
		{
			return matchClass;
		}
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


	public virtual string FilterName
	{
		get
		{
			return Ident.ToString();
		}
	}

	/// <summary>
	/// Add a parameter to the match class filter function. </summary>
	public virtual void AddParameter(Entity entity)
	{
		@params.Add(entity);
	}

	/// <summary>
	/// Get all parameters of this match class filter function. </summary>
	public virtual IList<Entity> Parameters
	{
		get
		{
			return @params.AsReadOnly();
		}
	}

	/// <summary>
	/// Get all parameter types of this match class filter function. </summary>
	public virtual IList<Type> ParameterTypes
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
