/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using ProcedureBase = de.unika.ipd.grgen.ir.executable.ProcedureBase;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class ProjectionExpr : Expression
	{
		private int index;
		private ProcedureBase procedure;
		private string projectedValueVarName;

		public ProjectionExpr(int index, ProcedureBase procedure, Type type)^
			: base("projection expr", type)
		{
			this.index = index;
			this.procedure = procedure;
		}

		public virtual int Index
		{
			get
			{
				return index;
			}
		}

		public virtual ProcedureBase Procedure
		{
			get
			{
				return procedure;
			}
		}

		public virtual string ProjectedValueVarName
		{
			get
			{
				return projectedValueVarName;
			}
			set
			{
				this.projectedValueVarName = value;
			}
		}

	}

}
