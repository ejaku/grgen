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
	using de.unika.ipd.grgen.ir;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// Represents a compound assignment var statement in the IR.
	/// </summary>
	public class CompoundAssignmentVar : EvalStatement
	{
		public enum CompoundAssignmentType
		{
			NONE,
			UNION,
			INTERSECTION,
			WITHOUT,
			CONCATENATE,
			ASSIGN
		}

		/// <summary>
		/// The lhs of the assignment. </summary>
		private Variable target;

		/// <summary>
		/// The operation of the compound assignment </summary>
		private CompoundAssignmentType operation;

		/// <summary>
		/// The rhs of the assignment. </summary>
		private Expression expr;

		public CompoundAssignmentVar(Variable target, CompoundAssignmentType compoundAssignmentType, Expression expr)
			: base("compound assignment var")
		{
			this.target = target;
			this.operation = compoundAssignmentType;
			this.expr = expr;
		}

		public virtual Variable Target
		{
			get
			{
				return target;
			}
		}

		public virtual Expression Expression
		{
			get
			{
				return expr;
			}
		}

		public virtual CompoundAssignmentType Operation
		{
			get
			{
				return operation;
			}
		}

		public override string ToString()
		{
			return Target + (operation == CompoundAssignmentType.UNION ?
					" |= " : operation == CompoundAssignmentType.INTERSECTION ? " &= " : " \\= ")
					+ Expression;
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(!IsGlobalVariable(target))
				needs.Add(target);

			Expression.CollectNeededEntities(needs);
		}
	}

}
