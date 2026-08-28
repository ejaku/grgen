/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Pair.java
/// 
/// @author Created by Omnicore CodeGuide
/// </summary>

namespace de.unika.ipd.grgen.util.collection
{
	public class Pair<T, S>
	{
		public T first;
		public S second;

		public Pair()
		{
			first = default(T);
			second = default(S);
		}

		public Pair(T first, S second)
		{
			this.first = first;
			this.second = second;
		}

		public override int GetHashCode()
		{
			return first.GetHashCode() * 31 + second.GetHashCode();
		}

		public override bool Equals(object that)
		{
			if(that == null)
				return false;
			if(this == that)
				return true;
			if(!(that is Pair<T, S>))
				return false;
			try
			{
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: @SuppressWarnings("unchecked") Pair<T,S> that_ = (Pair<T,S>)that;
				Pair<T, S> that_ = (Pair<T, S>)that;
				return first.Equals(that_.first) && second.Equals(that_.second);
			}
			catch(System.InvalidCastException)
			{
				return false;
			}
		}
	}

}
