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
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

	public class FunctionAutoKeepOneForEachAccumulateBy : EvalStatement
	{
		protected internal Variable targetVar;

		private Entity member;
		private Variable accumulationMember;
		private string accumulationMethod;

		public FunctionAutoKeepOneForEachAccumulateBy(Variable targetVar, Entity member,
				Variable accumulationMember, string accumulationMethod)
			: base("function auto keep one for each accumulate by stmt")
		{
			this.targetVar = targetVar;
			this.member = member;
			this.accumulationMember = accumulationMember;
			this.accumulationMethod = accumulationMethod;
		}

		public virtual Entity Member
		{
			get
			{
				return member;
			}
		}

		public virtual Variable AccumulationMember
		{
			get
			{
				return accumulationMember;
			}
		}

		public virtual string AccumulationMethod
		{
			get
			{
				return accumulationMethod;
			}
		}

		public virtual Variable TargetVar
		{
			get
			{
				return targetVar;
			}
		}

		public virtual ArrayType TargetType
		{
			get
			{
				return (ArrayType)targetVar.Type;
			}
		}
	}

}
