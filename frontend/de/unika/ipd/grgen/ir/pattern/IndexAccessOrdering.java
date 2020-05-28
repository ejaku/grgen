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
	int comp;
	Expression expr;
	int comp2;
	Expression expr2;

	public IndexAccessOrdering(Index index, boolean ascending,
			int comp, Expression expr, int comp2, Expression expr2)
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
				if(comp == OperatorDeclNode.GT || comp == OperatorDeclNode.GE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.GT || comp2 == OperatorDeclNode.GE)
						return expr2;
				}
			}
			return null;
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.LT || comp == OperatorDeclNode.LE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.LT || comp2 == OperatorDeclNode.LE)
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
				if(comp == OperatorDeclNode.LT || comp == OperatorDeclNode.LE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.LT || comp2 == OperatorDeclNode.LE)
						return expr2;
				}
			}
			return null;
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.GT || comp == OperatorDeclNode.GE)
					return expr;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.GT || comp2 == OperatorDeclNode.GE)
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
				if(comp == OperatorDeclNode.GT || comp == OperatorDeclNode.GE)
					return comp == OperatorDeclNode.GE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.GT || comp2 == OperatorDeclNode.GE)
						return comp2 == OperatorDeclNode.GE;
				}
			}
			return false; // dummy/don't care
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.LT || comp == OperatorDeclNode.LE)
					return comp == OperatorDeclNode.LE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.LT || comp2 == OperatorDeclNode.LE)
						return comp2 == OperatorDeclNode.LE;
				}
			}
			return false; // dummy/don't care
		}
	}

	public boolean includingTo()
	{
		if(ascending) {
			if(expr != null) {
				if(comp == OperatorDeclNode.LT || comp == OperatorDeclNode.LE)
					return comp == OperatorDeclNode.LE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.LT || comp2 == OperatorDeclNode.LE)
						return comp2 == OperatorDeclNode.LE;
				}
			}
			return false; // dummy/don't care
		} else {
			if(expr != null) {
				if(comp == OperatorDeclNode.GT || comp == OperatorDeclNode.GE)
					return comp == OperatorDeclNode.GE;
				if(expr2 != null) {
					if(comp2 == OperatorDeclNode.GT || comp2 == OperatorDeclNode.GE)
						return comp2 == OperatorDeclNode.GE;
				}
			}
			return false; // dummy/don't care
		}
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(expr != null)
			expr.collectNeededEntities(needs);
		if(expr2 != null)
			expr2.collectNeededEntities(needs);
	}
}
