/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.stmt.procenv
{

	using System.Collections.Generic;

	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

	public class AssertProc : BuiltinProcedureInvocationBase
	{
		private ICollection<Expression> exprs;
		private bool isAlways;

		public AssertProc(ICollection<Expression> expressions, bool isAlways)
			: base("assert procedure")
		{
			this.exprs = expressions;
			this.isAlways = isAlways;
		}

		public virtual ICollection<Expression> Expressions
		{
			get
			{
				return exprs;
			}
		}

		public virtual bool IsAlways()
		{
			return isAlways;
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.NeedsGraph();
			foreach(Expression expr in exprs)
				expr.CollectNeededEntities(needs);
		}
	}

}
