/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{

	using System.Collections.Generic;

	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;

	/// <summary>
	/// Represents a switch statement in the IR.
	/// </summary>
	public class SwitchStatement : EvalStatement
	{
		private Expression switchExpr;
		private List<CaseStatement> statements = new List<CaseStatement>();

		public SwitchStatement(Expression switchExpr)
			: base("switch statement")
		{
			this.switchExpr = switchExpr;
		}

		public virtual void AddStatement(CaseStatement statement)
		{
			statements.Add(statement);
		}

		public virtual Expression SwitchExpr
		{
			get
			{
				return switchExpr;
			}
		}

		public virtual ICollection<CaseStatement> Statements
		{
			get
			{
				return statements.AsReadOnly();
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			switchExpr.CollectNeededEntities(needs);
			foreach(EvalStatement statement in statements)
				statement.CollectNeededEntities(needs);
		}
	}

}
