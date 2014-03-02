/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.exprevals.OperatorSignature;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.NeededEntities;

/**
 * Class for accessing an index by ordering, binding a pattern element
 */
public class IndexAccessOrdering extends IndexAccess {
	public boolean ascending;
	int comp;
	Expression expr;
	int comp2;
	Expression expr2;
	
	public IndexAccessOrdering(AttributeIndex index, boolean ascending,
			int comp, Expression expr, int comp2, Expression expr2) {
		super(index);
		this.ascending = ascending;
		this.comp = comp;
		this.expr = expr;
		this.comp2 = comp2;
		this.expr2 = expr2;
	}
	
	public Expression from() {
		if(ascending) {
			if(expr!=null) {
				if(comp==OperatorSignature.GT || comp==OperatorSignature.GE)
					return expr;
				if(expr2!=null) {
					if(comp2==OperatorSignature.GT || comp2==OperatorSignature.GE)
						return expr2;
				}
			}
			return null;
		} else {
			if(expr!=null) {
				if(comp==OperatorSignature.LT || comp==OperatorSignature.LE)
					return expr;
				if(expr2!=null) {
					if(comp2==OperatorSignature.LT || comp2==OperatorSignature.LE)
						return expr2;
				}
			}
			return null;
		}
	}

	public Expression to() {
		if(ascending) {
			if(expr!=null) {
				if(comp==OperatorSignature.LT || comp==OperatorSignature.LE)
					return expr;
				if(expr2!=null) {
					if(comp2==OperatorSignature.LT || comp2==OperatorSignature.LE)
						return expr2;
				}
			}
			return null;
		} else {
			if(expr!=null) {
				if(comp==OperatorSignature.GT || comp==OperatorSignature.GE)
					return expr;
				if(expr2!=null) {
					if(comp2==OperatorSignature.GT || comp2==OperatorSignature.GE)
						return expr2;
				}
			}
			return null;
		}
	}
	
	public boolean includingFrom() {
		if(ascending) {
			if(expr!=null) {
				if(comp==OperatorSignature.GT || comp==OperatorSignature.GE)
					return comp==OperatorSignature.GE;
				if(expr2!=null) {
					if(comp2==OperatorSignature.GT || comp2==OperatorSignature.GE)
						return comp2==OperatorSignature.GE;
				}
			}
			return false; // dummy/don't care
		} else {
			if(expr!=null) {
				if(comp==OperatorSignature.LT || comp==OperatorSignature.LE)
					return comp==OperatorSignature.LE;
				if(expr2!=null) {
					if(comp2==OperatorSignature.LT || comp2==OperatorSignature.LE)
						return comp2==OperatorSignature.LE;
				}
			}
			return false; // dummy/don't care
		}
	}
	
	public boolean includingTo() {
		if(ascending) {
			if(expr!=null) {
				if(comp==OperatorSignature.LT || comp==OperatorSignature.LE)
					return comp==OperatorSignature.LE;
				if(expr2!=null) {
					if(comp2==OperatorSignature.LT || comp2==OperatorSignature.LE)
						return comp2==OperatorSignature.LE;
				}
			}
			return false; // dummy/don't care
		} else {
			if(expr!=null) {
				if(comp==OperatorSignature.GT || comp==OperatorSignature.GE)
					return comp==OperatorSignature.GE;
				if(expr2!=null) {
					if(comp2==OperatorSignature.GT || comp2==OperatorSignature.GE)
						return comp2==OperatorSignature.GE;
				}
			}
			return false; // dummy/don't care
		}
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		if(expr!=null)
			expr.collectNeededEntities(needs);
		if(expr2!=null)
			expr2.collectNeededEntities(needs);
	}
}
