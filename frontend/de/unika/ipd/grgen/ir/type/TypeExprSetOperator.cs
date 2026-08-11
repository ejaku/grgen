/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// TypeExprOp.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.type
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;

	public class TypeExprSetOperator : TypeExpr
	{
		public enum SetOperator
		{
			UNION,
			DIFFERENCE,
			INTERSECT
		}

		private readonly SetOperator op;

		private readonly IList<TypeExpr> children = new List<TypeExpr>();

		public TypeExprSetOperator(SetOperator op)
		{
			this.op = op;
		}

		public void AddOperand(TypeExpr operand)
		{
			children.Add(operand);
		}

		public override ISet<InheritanceType> Evaluate()
		{
			ISet<InheritanceType> res = new HashSet<InheritanceType>();
			Debug.Assert(children.Count == 2, "Arity 2 required"); // it could make sense to model this explicitly as a binary tree

			ICollection<InheritanceType> lhs = children[0].Evaluate();
			ICollection<InheritanceType> rhs = children[1].Evaluate();

			res.AddAll(lhs);

			switch(op)
			{ // note that types are taken literally and are not resolved to the union of their subtypes
			case de.unika.ipd.grgen.ir.type.TypeExprSetOperator.SetOperator.UNION:
				res.AddAll(rhs); // entity:T1\(T2+T2+T3) is evaluated/optimized to entity:T1\(T2+T3)
				break;
			case de.unika.ipd.grgen.ir.type.TypeExprSetOperator.SetOperator.DIFFERENCE:
				Debug.Assert((false)); // not used yet, entity:T1\T2 is mapped to entity:T1 and a type constraint T2, entity:T1\(T2+T3) is mapped to entity:T1 and a type constraint T2+T3 (union) (note that it is checked that T is not contained in the constraints if entity:T)
				res.RemoveAll(rhs);
				break;
			case de.unika.ipd.grgen.ir.type.TypeExprSetOperator.SetOperator.INTERSECT:
				Debug.Assert((false)); // not used yet
				res.RetainAll(rhs);
				break;
			}

			return res;
		}
	}

}
