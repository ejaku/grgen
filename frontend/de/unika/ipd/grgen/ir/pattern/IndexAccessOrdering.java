/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;

/**
 * Class for accessing an index by ordering, binding a pattern element
 */
public class IndexAccessOrdering extends IndexAccess
{
	public boolean ascending;
	OperatorDeclNode.Operator comp;
	Expression expr;
	OperatorDeclNode.Operator comp2;
	Expression expr2;

	public IndexAccessOrdering(Index index, boolean ascending,
			OperatorDeclNode.Operator comp, Expression expr, OperatorDeclNode.Operator comp2, Expression expr2)
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
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.GT || comp == OperatorDeclNode.Operator.GE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.GT || comp2 == OperatorDeclNode.Operator.GE)
						return expr2;
				}
			}
			return null;
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.LT || comp == OperatorDeclNode.Operator.LE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.LT || comp2 == OperatorDeclNode.Operator.LE)
						return expr2;
				}
			}
			return null;
		}
	}

	public Expression to()
	{
		if(ascending) {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.LT || comp == OperatorDeclNode.Operator.LE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.LT || comp2 == OperatorDeclNode.Operator.LE)
						return expr2;
				}
			}
			return null;
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.GT || comp == OperatorDeclNode.Operator.GE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.GT || comp2 == OperatorDeclNode.Operator.GE)
						return expr2;
				}
			}
			return null;
		}
	}

	public boolean includingFrom()
	{
		if(ascending) {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.GT || comp == OperatorDeclNode.Operator.GE)
					return comp == OperatorDeclNode.Operator.GE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.GT || comp2 == OperatorDeclNode.Operator.GE)
						return comp2 == OperatorDeclNode.Operator.GE;
				}
			}
			return false; // dummy/don't care
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.LT || comp == OperatorDeclNode.Operator.LE)
					return comp == OperatorDeclNode.Operator.LE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.LT || comp2 == OperatorDeclNode.Operator.LE)
						return comp2 == OperatorDeclNode.Operator.LE;
				}
			}
			return false; // dummy/don't care
		}
	}

	public boolean includingTo()
	{
		if(ascending) {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.LT || comp == OperatorDeclNode.Operator.LE)
					return comp == OperatorDeclNode.Operator.LE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.LT || comp2 == OperatorDeclNode.Operator.LE)
						return comp2 == OperatorDeclNode.Operator.LE;
				}
			}
			return false; // dummy/don't care
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.Operator.GT || comp == OperatorDeclNode.Operator.GE)
					return comp == OperatorDeclNode.Operator.GE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.Operator.GT || comp2 == OperatorDeclNode.Operator.GE)
						return comp2 == OperatorDeclNode.Operator.GE;
				}
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
