/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ast.decl.executable.Operator;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;

/**
 * Class for accessing an index by ordering, binding a pattern element
 * input: lower and upper bounds (each or both may be optional, output: from/to (each or both may be optional)
 * when ascending, lower bound is from and upper bound is to, when descending, lower bound is to and upper bound is from
 */
public class IndexAccessOrdering extends IndexAccess
{
	public boolean ascending;
	Operator comp;
	Expression expr;
	Operator comp2;
	Expression expr2;

	public IndexAccessOrdering(Index index, boolean ascending,
			Operator comp, Expression expr, Operator comp2, Expression expr2)
	{
		super(index);
		this.ascending = ascending;
		this.comp = comp;
		this.expr = expr;
		this.comp2 = comp2;
		this.expr2 = expr2;
	}

	public Expression from()
	{
		if(ascending) {
			if(expr != null) { // return lower bound from expr or expr2
				if(comp == Operator.GT || comp == Operator.GE)
					return expr;
			}
			if(expr2 != null) {
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return expr2;
			}
			return null;
		} else {
			if(expr != null) { // return upper bound from expr or expr2
				if(comp == Operator.LT || comp == Operator.LE)
					return expr;
			}
			if(expr2 != null) {
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return expr2;
			}
			return null;
		}
	}

	public Expression to()
	{
		if(ascending) {
			if(expr != null) { // return upper bound from expr or expr2
				if(comp == Operator.LT || comp == Operator.LE)
					return expr;
			}
			if(expr2 != null) {
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return expr2;
			}
			return null;
		} else {
			if(expr != null) { // return lower bound from expr or expr2
				if(comp == Operator.GT || comp == Operator.GE)
					return expr;
			}
			if(expr2 != null) {
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return expr2;
			}
			return null;
		}
	}

	public boolean includingFrom()
	{
		if(ascending) {
			if(expr != null) {
				if(comp == Operator.GT || comp == Operator.GE)
					return comp == Operator.GE;
			}
			if(expr2 != null) {
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return comp2 == Operator.GE;
			}
			return false; // dummy/don't care
		} else {
			if(expr != null) {
				if(comp == Operator.LT || comp == Operator.LE)
					return comp == Operator.LE;
			}
			if(expr2 != null) {
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return comp2 == Operator.LE;
			}
			return false; // dummy/don't care
		}
	}

	public boolean includingTo()
	{
		if(ascending) {
			if(expr != null) {
				if(comp == Operator.LT || comp == Operator.LE)
					return comp == Operator.LE;
			}
			if(expr2 != null) {
				if(comp2 == Operator.LT || comp2 == Operator.LE)
					return comp2 == Operator.LE;
			}
			return false; // dummy/don't care
		} else {
			if(expr != null) {
				if(comp == Operator.GT || comp == Operator.GE)
					return comp == Operator.GE;
			}
			if(expr2 != null) {
				if(comp2 == Operator.GT || comp2 == Operator.GE)
					return comp2 == Operator.GE;
			}
			return false; // dummy/don't care
		}
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(expr != null)
			expr.collectNeededEntities(needs);
		if(expr2 != null)
			expr2.collectNeededEntities(needs);
	}
}
