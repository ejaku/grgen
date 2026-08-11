/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.stmt.graph
{
	using System.Diagnostics;

	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class GraphAddNodeProc : BuiltinProcedureInvocationBase
	{
		private readonly Expression nodeType;

		private readonly Type returnType;

		public GraphAddNodeProc(Expression nodeType, Type returnType)
			: base("graph add node procedure")
		{
			this.nodeType = nodeType;
			this.returnType = returnType;
		}

		public virtual Expression NodeTypeExpr
		{
			get
			{
				return nodeType;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.NeedsGraph();
			nodeType.CollectNeededEntities(needs);
		}

		public override int ReturnArity()
		{
			return 1;
		}

		public override Type GetReturnType(int index)
		{
			Debug.Assert((index == 0));
			return returnType;
		}
	}

}
