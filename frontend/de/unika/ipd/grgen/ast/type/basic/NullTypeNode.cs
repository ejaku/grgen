/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.type.basic
{
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ObjectType = de.unika.ipd.grgen.ir.type.basic.ObjectType;

	public class NullTypeNode : BasicTypeNode
	{
		static NullTypeNode()
		{
			SetClassName(typeof(NullTypeNode), "null type");
		}

		public override bool IsCompatibleTo(TypeNode t)
		{
			// null is compatible to all graph element types, object, string, and graph
			if(!(t is BasicTypeNode))
				return true;
			if(t == BasicTypeNode.objectType || t == BasicTypeNode.stringType || t == BasicTypeNode.graphType)
				return true;
			return false;
		}

		public override bool IsCastableTo(TypeNode t)
		{
			return IsCompatibleTo(t);
		}

		protected internal override IR ConstructIR()
		{
			return new ObjectType(Ident.IRIdent);
		}

		public override string ToString()
		{
			return "null";
		}
	}

}
