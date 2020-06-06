/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.HashSet;
import java.util.Set;

/**
 * Empty annotations.
 */
public class EmptyAnnotations implements Annotations
{
	private static final Annotations EMPTY = new EmptyAnnotations();

	public static Annotations get()
	{
		return EMPTY;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#containsKey(java.lang.String) */
	@Override
	public boolean containsKey(String key)
	{
		return false;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#get(java.lang.String) */
	@Override
	public Object get(String key)
	{
		return null;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#put(java.lang.String, java.lang.Object) */
	@Override
	public void put(String key, Object value)
	{
		// nothing to do
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isInteger(java.lang.String) */
	@Override
	public boolean isInteger(String key)
	{
		return false;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isBoolean(java.lang.String) */
	@Override
	public boolean isBoolean(String key)
	{
		return false;
	}

	/** @see de.unika.ipd.grgen.util.Annotations#isString(java.lang.String) */
	@Override
	public boolean isString(String key)
	{
		return false;
	}

	@Override
	public boolean isFlagSet(String key)
	{
		return false;
	}

	@Override
	public Set<String> keySet()
	{
		return new HashSet<String>();
	}
}
