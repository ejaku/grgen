/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public abstract class ContainerInitNode : ExprNode
	{
		static ContainerInitNode()
		{
			SetClassName(typeof(ContainerInitNode), "container init");
		}

		public ContainerInitNode(Coords coords)
			: base(coords)
		{
		}

		public override TypeNode Type
		{
			get
			{
				return ContainerType;
			}
		}

		public abstract ContainerTypeNode ContainerType {get;}

		public abstract bool IsInitInModel();

		protected internal static bool IsEnumValue(ExprNode expr)
		{
			if(!(expr is DeclExprNode))
				return false;
			if(!(((DeclExprNode)expr).IsEnumValue()))
				return false;
			return true;
		}
	}

}
