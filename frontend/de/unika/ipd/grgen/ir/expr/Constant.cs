/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class Constant : Expression
	{
		/// <summary>
		/// The value of the constant. </summary>
		public object value;

		/// <param name="type"> The type of the constant. </param>
		/// <param name="value"> The value of the constant. </param>
		public Constant(Type type, object value)
			: base("constant", type)
		{
			this.value = value;
		}

		/// <returns> The value of the constant. </returns>
		public virtual object Value
		{
			get
			{
				return value;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel() "/>
		public override string NodeLabel
		{
			get
			{
				return Name + " " + value;
			}
		}
	}

}
