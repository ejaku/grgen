/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using de.unika.ipd.grgen.ir;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// A variable expression node.
	/// </summary>
	public class VariableExpression : Expression
	{
		private Variable var;

		public VariableExpression(Variable var)
			: base("variable", var.Type)
		{
			this.var = var;
		}

		/// <summary>
		/// Returns the variable of this variable expression. </summary>
		public virtual Variable Variable
		{
			get
			{
				return var;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(!IsGlobalVariable(var))
				needs.Add(var);
		}

		public override bool Equals(object other)
		{
			if(!(other is VariableExpression))
				return false;
			return var == ((VariableExpression)other).Variable;
		}

		public override int GetHashCode()
		{
			return var.GetHashCode();
		}
	}

}
