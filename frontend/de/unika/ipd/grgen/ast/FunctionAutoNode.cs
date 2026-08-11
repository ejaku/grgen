/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast
{
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node that represents a function auto node
	/// </summary>
	public abstract class FunctionAutoNode : BaseNode
	{
		static FunctionAutoNode()
		{
			SetClassName(typeof(FunctionAutoNode), "function auto");
		}

		protected internal string function;

		public FunctionAutoNode(Coords coords, string function)
			: base(coords)
		{
			this.function = function;
		}

		public abstract bool ResolveLocalBypass();

		public abstract bool CheckLocalBypass();

		public abstract bool CheckLocal(FunctionDeclNode functionDecl);

		public abstract void GetStatements(FunctionDeclNode functionDecl, Function function);
	}

}
