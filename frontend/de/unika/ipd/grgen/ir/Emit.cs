/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;

	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

	/// <summary>
	/// An emit statement.
	/// </summary>
	public class Emit : IR, ImperativeStmt, OrderedReplacement
	{
		private List<Expression> arguments;
		private bool isDebug;

		public Emit(List<Expression> arguments, bool isDebug)
			: base("emit")
		{
			this.arguments = arguments;
			this.isDebug = isDebug;
		}

		public virtual bool IsDebug()
		{
			return isDebug;
		}

		/// <summary>
		/// Returns Arguments
		/// </summary>
		public virtual IList<Expression> Arguments
		{
			get
			{
				return arguments.AsReadOnly();
			}
		}

		public virtual void CollectNeededEntities(NeededEntities needs)
		{
			foreach(Expression expr in arguments)
				expr.CollectNeededEntities(needs);
		}
	}

}
