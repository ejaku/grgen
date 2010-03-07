/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Comparator;


/**
 * Abstract base class for types.
 * Subclasses distinguished into primitive (string, int, boolean, ...) and compound
 */
public abstract class Type extends Identifiable {
	/** helper class for comparing objects of type Type, used in compareTo, overwriting comparteTo of Identifiable */
	private static final Comparator<Type> COMPARATOR = new Comparator<Type>() {

		public int compare(Type t1, Type t2) {
			if(t1.isEqual(t2))
				return 0;

			if((t1 instanceof InheritanceType) && (t2 instanceof InheritanceType)) {
				int distT1 = ((InheritanceType) t1).getMaxDist();
				int distT2 = ((InheritanceType) t2).getMaxDist();

				if(distT1 < distT2)
					return -1;
				else if(distT1 > distT2)
					return 1;
			}

			return t1.getIdent().compareTo(t2.getIdent());
		}
	};

	public static final int IS_UNKNOWN = 0;
	public static final int IS_INTEGER = 1;
	public static final int IS_FLOAT = 2;
	public static final int IS_DOUBLE = 3;
	public static final int IS_BOOLEAN = 4;
	public static final int IS_STRING  = 5;
	public static final int IS_TYPE  = 6;
	public static final int IS_OBJECT = 7;
	public static final int IS_SET = 8;
	public static final int IS_MAP = 9;
	public static final int IS_UNTYPED_EXEC_VAR_TYPE = 10;

	/**
	 * Make a new type.
	 * @param name The name of the type (test, group, ...).
	 * @param ident The identifier used to declare that type.
	 */
	public Type(String name, Ident ident) {
		super(name, ident);
	}

	/**
	 * Decides, if two types are equal.
	 * @param t The other type.
	 * @return true, if the types are equal.
	 */
	public boolean isEqual(Type t) {
		return t == this;
	}

	/**
	 * Compute, if this type is castable to another type.
	 * You do not have to check, if <code>t == this</code>.
	 * @param t The other type.
	 * @return true, if this type is castable.
	 */
	protected boolean castableTo(Type t) {
		return false;
	}

	/**
	 * Checks, if this type is castable to another type.
	 * This method is final, to implement the castability, overwrite <code>castableTo</code>, which is called by this method.
	 * @param t The other type.
	 * @return true, if this type can be casted to <code>t</code>, false otherwise.
	 */
	public final boolean isCastableTo(Type t) {
		return isEqual(t) || castableTo(t);
	}

	/** @return true, if this type is a void type. */
	public boolean isVoid() {
		return false;
	}

	/** Return a classification of a type for the IR. */
	public int classify() {
		return IS_UNKNOWN;
	}

	static final Comparator<Type> getComparator() {
		return COMPARATOR;
	}

	public int compareTo(Identifiable id) {
		if (id instanceof Type) {
			return COMPARATOR.compare(this, (Type) id);
		}

		assert false;
		return super.compareTo(id);
	}
}
