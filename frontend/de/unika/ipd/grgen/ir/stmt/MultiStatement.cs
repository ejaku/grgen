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

	/// <summary>
	/// Represents a multi statement in the IR.
	/// </summary>
	public class MultiStatement : EvalStatement
	{
		private List<EvalStatement> statements = new List<EvalStatement>();

		public MultiStatement()
			: base("multi statement")
		{
		}

		public virtual void AddStatement(EvalStatement loopedStatement)
		{
			statements.Add(loopedStatement);
		}

		public virtual ICollection<EvalStatement> Statements
		{
			get
			{
				return statements.AsReadOnly();
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			foreach(EvalStatement statement in statements)
				statement.CollectNeededEntities(needs);
		}
	}

}
