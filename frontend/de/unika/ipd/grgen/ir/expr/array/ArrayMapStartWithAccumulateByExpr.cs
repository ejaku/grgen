/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ir.expr.array
{
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

	public class ArrayMapStartWithAccumulateByExpr : ArrayFunctionMethodInvocationBaseExpr, ArrayPerElementMethod
	{
		private Variable initArrayAccessVar;
		private Expression initExpr;

		private Variable arrayAccessVar;
		private Variable previousAccumulationAccessVar;
		private Variable indexVar;
		private Variable elementVar;
		private Expression mappingExpr;

		public ArrayMapStartWithAccumulateByExpr(Expression targetExpr, Variable initArrayAccessVar, Expression initExpr, Variable arrayAccessVar, Variable previousAccumulationAccessVar, Variable indexVar, Variable elementVar, Expression mappingExpr, ArrayType resultingType)
			: base("array map start with accumulate by expr", resultingType, targetExpr)
		{
			this.initArrayAccessVar = initArrayAccessVar;
			this.initExpr = initExpr;
			this.arrayAccessVar = arrayAccessVar;
			this.previousAccumulationAccessVar = previousAccumulationAccessVar;
			this.indexVar = indexVar;
			this.elementVar = elementVar;
			this.mappingExpr = mappingExpr;
		}

		public virtual Variable InitArrayAccessVar
		{
			get
			{
				return initArrayAccessVar;
			}
		}

		public virtual Expression InitExpr
		{
			get
			{
				return initExpr;
			}
		}

		public virtual Variable ArrayAccessVar
		{
			get
			{
				return arrayAccessVar;
			}
		}

		public virtual Variable PreviousAccumulationAccessVar
		{
			get
			{
				return previousAccumulationAccessVar;
			}
		}

		public virtual Variable IndexVar
		{
			get
			{
				return indexVar;
			}
		}

		public virtual Variable ElementVar
		{
			get
			{
				return elementVar;
			}
		}

		public virtual Expression MappingExpr
		{
			get
			{
				return mappingExpr;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			base.CollectNeededEntities(needs);
			needs.Add(this);
			initExpr.CollectNeededEntities(needs);
			mappingExpr.CollectNeededEntities(needs);
			if(needs.variables != null)
			{
				if(initArrayAccessVar != null)
					needs.variables.Remove(initArrayAccessVar);
				if(arrayAccessVar != null)
					needs.variables.Remove(arrayAccessVar);
				needs.variables.Remove(previousAccumulationAccessVar);
				if(indexVar != null)
					needs.variables.Remove(indexVar);
				needs.variables.Remove(elementVar);
			}
		}
	}

}
