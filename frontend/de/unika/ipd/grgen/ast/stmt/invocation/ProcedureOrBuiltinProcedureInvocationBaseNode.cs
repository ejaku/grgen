/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.invocation
{

	using System.Collections.Generic;

	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// base class for builtin and real procedures calls </summary>
	public abstract class ProcedureOrBuiltinProcedureInvocationBaseNode : EvalStatementNode
	{
		static ProcedureOrBuiltinProcedureInvocationBaseNode()
		{
			SetClassName(typeof(ProcedureOrBuiltinProcedureInvocationBaseNode), "procedure or builtin procedure invocation base");
		}

		private static readonly IList<TypeNode> emptyReturn = new List<TypeNode>();

		public ProcedureOrBuiltinProcedureInvocationBaseNode(Coords coords)
			: base(coords)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool res = true;
			foreach(TypeNode typeNode in Type)
				res &= typeNode.Resolve();
			return res;
		}

		// default is a procedure without returns, overwrite if return is not empty
		public virtual IList<TypeNode> Type
		{
			get
			{
				return emptyReturn;
			}
		}
	}

}
