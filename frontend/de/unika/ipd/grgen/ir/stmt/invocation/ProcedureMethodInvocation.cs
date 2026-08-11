/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.invocation
{
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using ProcedureBase = de.unika.ipd.grgen.ir.executable.ProcedureBase;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// A procedure method invocation.
	/// </summary>
	public class ProcedureMethodInvocation : ProcedureInvocationBase
	{
		/// <summary>
		/// The owner of the procedure method. </summary>
		private Entity owner;

		/// <summary>
		/// The procedure of the procedure method invocation. </summary>
		protected internal Procedure procedure;

		public ProcedureMethodInvocation(Entity owner, Procedure procedure)
			: base("procedure method invocation")
		{

			this.owner = owner;
			this.procedure = procedure;
		}

		public virtual Entity Owner
		{
			get
			{
				return owner;
			}
		}

		public override ProcedureBase ProcedureBase
		{
			get
			{
				return procedure;
			}
		}

		public virtual Procedure Procedure
		{
			get
			{
				return procedure;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(!IsGlobalVariable(owner))
			{
				if(owner is GraphEntity)
					needs.Add((GraphEntity)owner);
				else
					needs.Add((Variable)owner);
			}
			foreach(Expression child in WalkableChildren)
				child.CollectNeededEntities(needs);
		}
	}

}
