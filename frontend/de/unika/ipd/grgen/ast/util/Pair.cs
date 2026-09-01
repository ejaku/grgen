/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;

	public class Pair<R, S> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode
	{
		public R fst = null;
		public S snd = null;

		public override int GetHashCode()
		{
			return fst.GetHashCode() * 31 + snd.GetHashCode();
		}

		public override bool Equals(object that)
		{
			if(that == null)
				return false;
			if(this == that)
				return true;
			if(!(that is Pair<R, S>))
				return false;
			try
			{
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: @SuppressWarnings("unchecked") Pair<R,S> that_ = (Pair<R,S>)that;
				Pair<R, S> that_ = (Pair<R, S>)that;
				return fst.Equals(that_.fst) && snd.Equals(that_.snd);
			}
			catch(System.InvalidCastException)
			{
				return false;
			}
		}
	}

}
