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
	using ProcedureOrBuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;

	/// <summary>
	/// Represents an assignment of procedure invocation return values statement in the IR.
	/// </summary>
	public class ReturnAssignment : EvalStatement
	{
		internal ProcedureOrBuiltinProcedureInvocationBase procedureInvocation;
		internal IList<AssignmentBase> targets = new List<AssignmentBase>();

		public ReturnAssignment(ProcedureOrBuiltinProcedureInvocationBase procedureInvocation)
			: base("return assignment")
		{

			this.procedureInvocation = procedureInvocation;
		}

		public virtual void AddAssignment(AssignmentBase target)
		{
			targets.Add(target);
		}

		public virtual ProcedureOrBuiltinProcedureInvocationBase ProcedureInvocation
		{
			get
			{
				return procedureInvocation;
			}
		}

		public virtual IList<AssignmentBase> Targets
		{
			get
			{
				return targets;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			foreach(EvalStatement target in targets)
				target.CollectNeededEntities(needs);
			procedureInvocation.CollectNeededEntities(needs);
		}
	}

}
