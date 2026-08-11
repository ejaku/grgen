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

	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

	/// <summary>
	/// A XGRS in an exec statement.
	/// </summary>
	public class Exec : IR, ImperativeStmt
	{
		private ISet<Expression> parameters = new LinkedHashSet<Expression>();
		private ISet<Entity> neededEntities;
		private ISet<Entity> neededEntitiesForComputation;
		private string xgrsString;
		private int lineNr;

		public Exec(string xgrsString, ISet<Expression> parameters, int lineNr)
			: base("exec")
		{
			this.xgrsString = xgrsString;
			this.parameters = parameters;
			this.lineNr = lineNr;
		}

		/// <summary>
		/// Returns XGRS as an String </summary>
		public virtual string XGRSString
		{
			get
			{
				return xgrsString;
			}
		}

		public virtual int LineNr
		{
			get
			{
				return lineNr;
			}
		}

		/// <summary>
		/// Returns Parameters </summary>
		public virtual ISet<Expression> Arguments
		{
			get
			{
				return Collections.UnmodifiableSet(parameters);
			}
		}

		public virtual ISet<Entity> GetNeededEntities(bool forComputation)
		{
			if(forComputation)
			{
				if(neededEntitiesForComputation == null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(Needs.ALL_ENTITIES, Needs.COMPUTATION_CONTEXT));
					foreach(Expression param in Arguments)
						param.CollectNeededEntities(needs);
					neededEntitiesForComputation = needs.entities;
				}
				return neededEntitiesForComputation;
			}
			else
			{
				if(neededEntities == null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(Needs.ALL_ENTITIES));
					foreach(Expression param in Arguments)
						param.CollectNeededEntities(needs);
					neededEntities = needs.entities;
				}
				return neededEntities;
			}
		}
	}

}
