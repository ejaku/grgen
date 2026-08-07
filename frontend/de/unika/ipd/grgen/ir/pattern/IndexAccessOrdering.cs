/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Index = de.unika.ipd.grgen.ir.model.Index;

/// <summary>
/// Class for accessing an index by ordering, binding a pattern element
/// input: lower and upper bounds (each or both may be optional, output: from/to (each or both may be optional)
/// when ascending, lower bound is from and upper bound is to, when descending, lower bound is to and upper bound is from
/// </summary>
public class IndexAccessOrdering : IndexAccess
{
	public bool ascending;
	internal Operator comp;
	internal Expression expr;
	internal Operator comp2;
	internal Expression expr2;

	public IndexAccessOrdering(Index index, bool ascending,
			Operator comp, Expression expr, Operator comp2, Expression expr2)
		: base(index)
	{
		this.ascending = ascending;
		this.comp = comp;
		this.expr = expr;
		this.comp2 = comp2;
		this.expr2 = expr2;
	}

	public virtual Expression From()
	{
		if(ascending)
		{
			if(expr != null)
			{ // return lower bound from expr or expr2
				if(comp == Operator.GT || comp == Operator.GE)
					return expr;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return expr2;
			}
			return null;
		}
		else
		{
			if(expr != null)
			{ // return upper bound from expr or expr2
				if(comp == Operator.LT || comp == Operator.LE)
					return expr;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return expr2;
			}
			return null;
		}
	}

	public virtual Expression To()
	{
		if(ascending)
		{
			if(expr != null)
			{ // return upper bound from expr or expr2
				if(comp == Operator.LT || comp == Operator.LE)
					return expr;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return expr2;
			}
			return null;
		}
		else
		{
			if(expr != null)
			{ // return lower bound from expr or expr2
				if(comp == Operator.GT || comp == Operator.GE)
					return expr;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return expr2;
			}
			return null;
		}
	}

	public virtual bool IncludingFrom()
	{
		if(ascending)
		{
			if(expr != null)
			{
				if(comp == Operator.GT || comp == Operator.GE)
					return comp == Operator.GE;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return comp2 == Operator.GE;
			}
			return false; // dummy/don't care
		}
		else
		{
			if(expr != null)
			{
				if(comp == Operator.LT || comp == Operator.LE)
					return comp == Operator.LE;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return comp2 == Operator.LE;
			}
			return false; // dummy/don't care
		}
	}

	public virtual bool IncludingTo()
	{
		if(ascending)
		{
			if(expr != null)
			{
				if(comp == Operator.LT || comp == Operator.LE)
					return comp == Operator.LE;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return comp2 == Operator.LE;
			}
			return false; // dummy/don't care
		}
		else
		{
			if(expr != null)
			{
				if(comp == Operator.GT || comp == Operator.GE)
					return comp == Operator.GE;
			}
			if(expr2 != null)
			{
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return comp2 == Operator.GE;
			}
			return false; // dummy/don't care
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(expr != null)
			expr.CollectNeededEntities(needs);
		if(expr2 != null)
			expr2.CollectNeededEntities(needs);
	}
}

}
