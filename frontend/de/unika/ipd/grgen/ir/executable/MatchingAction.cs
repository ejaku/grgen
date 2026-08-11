/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.executable
{

using System.Collections.Generic;
using System.Diagnostics;

using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;

/// <summary>
/// An action that represents something that does graph matching.
/// </summary>
public abstract class MatchingAction : Action
{
	/// <summary>
	/// Children names of this node. </summary>
	private static readonly string[] childrenNames = new string[] { "pattern" };

	/// <summary>
	/// The graph pattern to match against. </summary>
	public PatternGraphLhs pattern;

	/// <summary>
	/// A list of the pattern parameters </summary>
	private readonly List<Entity> @params = new List<Entity>();

	/// <summary>
	/// A list of the pattern def parameters which get yielded </summary>
	private readonly List<Entity> defParams = new List<Entity>();

	/// <summary>
	/// A list of the return-parameters </summary>
	private readonly List<Expression> returns = new List<Expression>();

	/// <summary>
	/// A list of the filters </summary>
	private readonly List<Filter> filters = new List<Filter>();

	/// <param name="name"> The name of this action. </param>
	/// <param name="ident"> The identifier that identifies this object. </param>
	protected internal MatchingAction(string name, Ident ident)
		: base(name, ident)
	{
		ChildrenNames = childrenNames;
	}

	/// <param name="pattern"> The graph pattern to match against. </param>
	protected internal virtual PatternGraphLhs Pattern
	{
		set
		{
			Debug.Assert((value != null));
			this.pattern = value;
			value.NameSuffix = "pattern";
		}
		get
		{
			return pattern;
		}
	}


	/// <summary>
	/// Add a parameter to the graph. </summary>
	public virtual void AddParameter(Entity id)
	{
		@params.Add(id);
	}

	/// <summary>
	/// Get all Parameters of this graph. </summary>
	public virtual IList<Entity> Parameters
	{
		get
		{
			return @params.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a def parameter which gets yielded to the graph. </summary>
	public virtual void AddDefParameter(Entity id)
	{
		defParams.Add(id);
	}

	/// <summary>
	/// Get all def Parameters which get yielded of this graph. </summary>
	public virtual IList<Entity> DefParameters
	{
		get
		{
			return defParams.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a return-value to the graph. </summary>
	public virtual void AddReturn(Expression expr)
	{
		returns.Add(expr);
	}

	/// <summary>
	/// Get all Returns of this graph. </summary>
	public virtual IList<Expression> Returns
	{
		get
		{
			return returns.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a filter to the action. </summary>
	public virtual void AddFilter(Filter filter)
	{
		filters.Add(filter);
	}

	/// <summary>
	/// Get all filters of this action. </summary>
	public virtual IList<Filter> Filters
	{
		get
		{
			return filters.AsReadOnly();
		}
	}
}

}
