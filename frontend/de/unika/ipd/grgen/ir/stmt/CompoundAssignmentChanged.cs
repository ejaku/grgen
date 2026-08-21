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

	using de.unika.ipd.grgen.ir;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// Represents a compound assignment changed statement in the IR.
	/// </summary>
	public class CompoundAssignmentChanged : CompoundAssignment
	{
		/// <summary>
		/// The change assignment. </summary>
		private Qualification changedTarget;

		/// <summary>
		/// The operation of the change assignment </summary>
		private CompoundAssignmentType changedOperation;

		public CompoundAssignmentChanged(Qualification target,
				CompoundAssignmentType compoundAssignmentType, Expression expr,
				CompoundAssignmentType changedAssignmentType, Qualification changedTarget)
			: base(target, compoundAssignmentType, expr)
		{
			this.changedOperation = changedAssignmentType;
			this.changedTarget = changedTarget;
		}

		public virtual Qualification ChangedTarget
		{
			get
			{
				return changedTarget;
			}
		}

		public virtual CompoundAssignmentType ChangedOperation
		{
			get
			{
				return changedOperation;
			}
		}

		public override string ToString()
		{
			return base.ToString()
					+ (changedOperation == CompoundAssignmentType.UNION ? " |> "
							: changedOperation == CompoundAssignmentType.INTERSECTION ? " &> " : " => ")
					+ changedTarget.ToString();
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			base.CollectNeededEntities(needs);

			Entity entity = changedTarget.Owner;
			if(!IsGlobalVariable(entity))
				needs.Add((GraphEntity)entity);

			// Temporarily do not collect variables for changed target
			ISet<Variable> varSet = needs.variables;
			needs.variables = null;
			changedTarget.CollectNeededEntities(needs);
			needs.variables = varSet;
		}
	}

}
