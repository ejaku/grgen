/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{
	/// <summary>
	/// A visitor that returns a boolean value.
	/// They are occurring rather often, so they're an own class.
	/// </summary>
	public abstract class BooleanResultVisitor : ResultVisitor<bool>
	{
		public abstract void Visit(Walkable n);
		private bool result;

		/// <summary>
		/// Make a new one. </summary>
		/// <param name="def"> The value, the result is initialized. </param>
		public BooleanResultVisitor(bool init)
		{
			result = init;
		}

		public virtual bool Result
		{
			set
			{
				result = value;
			}
			get
			{
				return result;
			}
		}


		public virtual bool BooleanResult()
		{
			return result;
		}
	}

}
