/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.decl.executable
{
	using de.unika.ipd.grgen.ast;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;

	public abstract class FunctionDeclBaseNode : FunctionOrOperatorDeclBaseNode
	{
		protected internal BaseNode resultUnresolved;


		public FunctionDeclBaseNode(IdentNode ident, BaseNode type)
			: base(ident, type)
		{
		}

		private static readonly Resolver<TypeNode> resultTypeResolver =
				new DeclarationTypeResolver<TypeNode>(typeof(TypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			resultType = resultTypeResolver.Resolve(resultUnresolved, this);
			return resultType != null;
		}
	}

}
