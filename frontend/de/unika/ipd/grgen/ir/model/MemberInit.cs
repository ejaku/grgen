/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ir.model
{
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;

	public class MemberInit : IR
	{
		/// <summary>
		/// The lhs of the assignment. </summary>
		private Entity member;

		/// <summary>
		/// The rhs of the assignment. </summary>
		private Expression expr;

		public MemberInit(Entity member, Expression expr)
			: base("memberinit")
		{
			this.member = member;
			this.expr = expr;
		}

		public virtual Entity Member
		{
			get
			{
				return member;
			}
		}

		public virtual Expression Expression
		{
			get
			{
				return expr;
			}
		}

		public override string ToString()
		{
			return Member + " = " + Expression;
		}
	}

}
