/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.deque
{
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ContainerVarProcedureMethodInvocationBase = de.unika.ipd.grgen.ir.stmt.ContainerVarProcedureMethodInvocationBase;

	public class DequeVarClear : ContainerVarProcedureMethodInvocationBase
	{
		public DequeVarClear(Variable target)
			: base("deque var clear", target)
		{
		}
	}

}
