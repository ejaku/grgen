/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// TypeExpr.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.type
{

	using System.Collections.Generic;

	using IR = de.unika.ipd.grgen.ir.IR;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;

	public abstract class TypeExpr : IR
	{
		public TypeExpr()
			: base("type expr")
		{
		}

		/// <summary>
		/// Evaluate this type expression by returning a set
		/// of all types that are represented by the expression. </summary>
		/// <returns> A collection of types that correspond to the expression. </returns>
		public abstract ISet<InheritanceType> Evaluate();

		public static readonly TypeExpr EMPTY = new TypeExprAnonymousInnerClass();

		private class TypeExprAnonymousInnerClass : TypeExpr
		{
			private readonly TypeExpr outerInstance;

			public override ISet<InheritanceType> evaluate()
			{
				return Collections.EmptySet();
			}
		}
	}

}
