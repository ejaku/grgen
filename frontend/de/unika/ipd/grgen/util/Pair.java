/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Pair.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

public class Pair<T, S>
{
	public T first;
	public S second;

	public Pair()
	{
		first = null;
		second = null;
	}

	public Pair(T first, S second)
	{
		this.first = first;
		this.second = second;
	}
	
	@Override
	public int hashCode()
	{
		return first.hashCode() * 31 + second.hashCode();
	}
	
	@Override
	public boolean equals(Object that)
	{
		if(that == null)
			return false;
		if(this == that)
			return true;
		if(!(that instanceof Pair<?,?>))
			return false;
		Pair<?,?> that_ = (Pair<?,?>)that;
		return first.equals(that_.first) && second.equals(that_.second);
	}
}
