/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;

public class Pair<R extends BaseNode, S extends BaseNode>
{
	public R fst = null;
	public S snd = null;
	
	@Override
	public int hashCode()
	{
		return fst.hashCode() * 31 + snd.hashCode();
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
		return fst.equals(that_.fst) && snd.equals(that_.snd);
	}
}
