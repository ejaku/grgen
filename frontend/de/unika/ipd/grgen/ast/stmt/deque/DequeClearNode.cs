/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.deque
{
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using DequeClear = de.unika.ipd.grgen.ir.stmt.deque.DequeClear;
	using DequeVarClear = de.unika.ipd.grgen.ir.stmt.deque.DequeVarClear;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class DequeClearNode : DequeProcedureMethodInvocationBaseNode
	{
		static DequeClearNode()
		{
			SetClassName(typeof(DequeClearNode), "deque clear statement");
		}

		public DequeClearNode(Coords coords, QualIdentNode target)
			: base(coords, target)
		{
		}

		public DequeClearNode(Coords coords, VarDeclNode targetVar)
			: base(coords, targetVar)
		{
		}

		protected internal override IR ConstructIR()
		{
			if(target != null)
				return new DequeClear(target.CheckIR<Qualification>(typeof(Qualification)));
			else
				return new DequeVarClear(targetVar.CheckIR<Variable>(typeof(Variable)));
		}
	}

}
