/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author G. Veit Batz
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ir;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class Cast : Expression
	{
		protected internal Expression expr;

		public Cast(Type type, Expression expr)
			: base("cast", type)
		{
			this.expr = expr;
		}

		public override string NodeLabel
		{
			get
			{
				return "Cast to " + type;
			}
		}

		public virtual Expression Expression
		{
			get
			{
				return expr;
			}
		}

		public virtual ICollection<Expression> WalkableChildren
		{
			get
			{
				IList<Expression> vec = new List<Expression>();
				vec.Add(expr);
				return vec;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			Expression.CollectNeededEntities(needs);
		}
	}

}
