/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using de.unika.ipd.grgen.ir;

	/// <summary>
	/// A member expression node.
	/// </summary>
	public class MemberExpression : Expression
	{
		private Entity member;

		public MemberExpression(Entity member)
			: base("member", member.Type)
		{
			this.member = member;
		}

		/// <summary>
		/// Returns the member entity of this member expression. </summary>
		public virtual Entity Member
		{
			get
			{
				return member;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.Add(this);
		}
	}

}
