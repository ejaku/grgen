/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// TypeExprConst.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.type
{

	using System.Collections.Generic;

	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;

	public class TypeExprConst : TypeExpr
	{
		private readonly ISet<InheritanceType> types = new HashSet<InheritanceType>();

		public virtual void AddOperand(InheritanceType t)
		{
			types.Add(t);
		}

		public override ISet<InheritanceType> Evaluate()
		{
			return types; // TODO: Collections.UnmodifiableSet
		}
	}

}
