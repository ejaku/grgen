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
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// Represents an accumulation yielding of a container variable in the IR.
	/// </summary>
	public class ContainerAccumulationYield : BlockNestingStatement
	{
		private Variable iterationVar;
		private Variable indexVar;
		private Variable containerVar;

		public ContainerAccumulationYield(Variable iterationVar, Variable indexVar,
				Variable containerVar)
			: base("container accumulation yield")
		{
			this.iterationVar = iterationVar;
			this.indexVar = indexVar;
			this.containerVar = containerVar;
		}

		public virtual Variable IterationVar
		{
			get
			{
				return iterationVar;
			}
		}

		public virtual Variable IndexVar
		{
			get
			{
				return indexVar;
			}
		}

		public virtual Variable Container
		{
			get
			{
				return containerVar;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(!IsGlobalVariable(containerVar))
				needs.Add(containerVar);
			foreach(EvalStatement accumulationStatement in statements)
				accumulationStatement.CollectNeededEntities(needs);
			if(needs.variables != null)
				needs.variables.Remove(iterationVar);
			if(indexVar != null)
			{
				if(needs.variables != null)
					needs.variables.Remove(indexVar);
			}
		}
	}

}
