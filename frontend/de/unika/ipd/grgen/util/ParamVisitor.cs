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
	/// A visitor that takes a parameter array.
	/// </summary>
	public abstract class ParamVisitor : Visitor
	{
		public abstract void visit(Walkable n);
		private object[] parameters;

		/// <summary>
		/// Get the i-th parameter. </summary>
		/// <param name="i"> The number of the parameter. </param>
		/// <returns> The i-th parameter, null, if i was greater than the number of
		/// parameters. </returns>
		protected internal virtual object GetParameter(int i)
		{
			return i < parameters.Length ? parameters[i] : null;
		}

		/// <summary>
		/// Make a new parameter visitor. </summary>
		/// <param name="params"> The parameter for the visitor. </param>
		public ParamVisitor(object[] @params)
		{
			parameters = @params;
		}

		/// <summary>
		/// Make a new parameter visitor with one parameter. </summary>
		/// <param name="param"> The parameter. </param>
		public ParamVisitor(object param) : this(new object[] {param})
		{
		}
	}

}
