/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using BooleanType = de.unika.ipd.grgen.ir.type.basic.BooleanType;

	public class Visited : Expression
	{
		private Expression visitorID;
		private Expression entity;

		public Visited(Expression visitorID, Expression entity)
			: base("visited", BooleanType.Type)
		{
			this.visitorID = visitorID;
			this.entity = entity;
		}

		public virtual Expression VisitorID
		{
			get
			{
				return visitorID;
			}
		}

		public virtual Expression Entity
		{
			get
			{
				return entity;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.NeedsGraph();
			entity.CollectNeededEntities(needs);
			visitorID.CollectNeededEntities(needs);
		}
	}

}
