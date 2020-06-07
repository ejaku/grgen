/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir.type;

import java.util.Comparator;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;

/**
 * Abstract base class for types.
 * Subclasses distinguished into primitive (string, int, boolean, ...) and compound
 */
public abstract class Type extends Identifiable
{
	/** helper class for comparing objects of type Type, used in compareTo, overwriting comparteTo of Identifiable */
	private static final Comparator<Type> COMPARATOR = new Comparator<Type>() {
		@Override
		public int compare(Type t1, Type t2)
		{
			if(t1.isEqual(t2))
				return 0;

			if((t1 instanceof InheritanceType) && (t2 instanceof InheritanceType)) {
				int distT1 = ((InheritanceType)t1).getMaxDist();
				int distT2 = ((InheritanceType)t2).getMaxDist();

				if(distT1 < distT2)
					return -1;
				else if(distT1 > distT2)
					return 1;
			}

			return t1.getIdent().compareTo(t2.getIdent());
		}
	};

	public enum TypeClass
	{
		IS_UNKNOWN,
		IS_BYTE,
		IS_SHORT,
		IS_INTEGER, // includes ENUM
		IS_LONG,
		IS_FLOAT,
		IS_DOUBLE,
		IS_BOOLEAN,
		IS_STRING,
		IS_TYPE,
		IS_OBJECT,
		IS_SET,
		IS_MAP,
		IS_ARRAY,
		IS_DEQUE,
		IS_UNTYPED_EXEC_VAR_TYPE,
		IS_EXTERNAL_TYPE,
		IS_GRAPH,
		IS_MATCH,
		IS_DEFINED_MATCH,
		IS_NODE,
		IS_EDGE
	}
	
	/**
	 * Make a new type.
	 * @param name The name of the type (test, group, ...).
	 * @param ident The identifier used to declare that type.
	 */
	public Type(String name, Ident ident)
	{
		super(name, ident);
	}

	/**
	 * Decides, if two types are equal.
	 * @param t The other type.
	 * @return true, if the types are equal.
	 */
	public boolean isEqual(Type t)
	{
		return t == this;
	}

	/**
	 * Compute, if this type is castable to another type.
	 * You do not have to check, if <code>t == this</code>.
	 * @param t The other type.
	 * @return true, if this type is castable.
	 */
	protected boolean castableTo(Type t)
	{
		return false;
	}

	/**
	 * Checks, if this type is castable to another type.
	 * This method is final, to implement the castability, overwrite <code>castableTo</code>, which is called by this method.
	 * @param t The other type.
	 * @return true, if this type can be casted to <code>t</code>, false otherwise.
	 */
	public final boolean isCastableTo(Type t)
	{
		return isEqual(t) || castableTo(t);
	}

	/** @return true, if this type is a void type. */
	public boolean isVoid()
	{
		return false;
	}

	/** Return a classification of a type for the IR. */
	public TypeClass classify()
	{
		return TypeClass.IS_UNKNOWN;
	}

	static final Comparator<Type> getComparator()
	{
		return COMPARATOR;
	}

	@Override
	public int compareTo(Identifiable id)
	{
		if(id instanceof Type) {
			return COMPARATOR.compare(this, (Type)id);
		}

		assert false;
		return super.compareTo(id);
	}

	public boolean isOrderableType()
	{
		if(classify() == TypeClass.IS_BYTE)
			return true;
		if(classify() == TypeClass.IS_SHORT)
			return true;
		if(classify() == TypeClass.IS_INTEGER) // includes ENUM
			return true;
		if(classify() == TypeClass.IS_LONG)
			return true;
		if(classify() == TypeClass.IS_FLOAT)
			return true;
		if(classify() == TypeClass.IS_DOUBLE)
			return true;
		if(classify() == TypeClass.IS_STRING)
			return true;
		if(classify() == TypeClass.IS_BOOLEAN)
			return true;
		return false;
	}

	public boolean isFilterableType()
	{
		if(isOrderableType())
			return true;
		if(classify() == TypeClass.IS_NODE)
			return true;
		if(classify() == TypeClass.IS_EDGE)
			return true;
		return false;
	}
	
	/** Add this type to the digest. */
	public void addToDigest(StringBuffer sb)
	{
		// sensible base implementation, to be overwritten selectively
	}
}
