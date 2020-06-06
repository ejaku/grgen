/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Comparator;
import java.util.Map;

import de.unika.ipd.grgen.util.Annotated;
import de.unika.ipd.grgen.util.Annotations;

/**
 * Identifiable with an identifier.
 * This is a super class for all classes which are associated with an identifier.
 */
public abstract class Identifiable extends IR implements Annotated, Comparable<Identifiable>
{
	/** helper class for comparing objects of type Identifiable, used in compareTo */
	protected static final Comparator<Identifiable> COMPARATOR = new Comparator<Identifiable>() {
		@Override
		public int compare(Identifiable lt, Identifiable rt)
		{
			return lt.getIdent().compareTo(rt.getIdent());
		}
	};

	/** The identifier */
	private Ident ident;

	/** @param name The name of the IR class
	 *  @param ident The identifier associated with this IR object */
	public Identifiable(String name, Ident ident)
	{
		super(name);
		this.ident = ident;
	}

	/** @return The identifier that identifies this IR structure. */
	public Ident getIdent()
	{
		return ident;
	}

	/** Set the identifier for this object. */
	public void setIdent(Ident ident)
	{
		this.ident = ident;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	@Override
	public String getNodeLabel()
	{
		return toString();
	}

	@Override
	public String getNodeInfo()
	{
		return ident.getNodeInfo();
	}

	@Override
	public String toString()
	{
		return getName() + " " + ident;
	}

	@Override
	public void addFields(Map<String, Object> fields)
	{
		fields.put("ident", ident.toString());
	}

	@Override
	public int hashCode()
	{
		return getIdent().hashCode();
	}

	@Override
	public int compareTo(Identifiable id)
	{
		return COMPARATOR.compare(this, id);
	}

	/** @return The annotations. */
	@Override
	public Annotations getAnnotations()
	{
		return getIdent().getAnnotations();
	}
}
