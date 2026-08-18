/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.type.basic
{
	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using UntypedExecVarType = de.unika.ipd.grgen.ir.type.basic.UntypedExecVarType;


	public class UntypedExecVarTypeNode : BasicTypeNode
	{
		static UntypedExecVarTypeNode()
		{
			SetClassName(typeof(UntypedExecVarTypeNode), "untyped exec variable type");
		}

		// TODO: No instance is ever used! Probably useless...
		public class Value
		{
			public static Value NULL = new ValueAnonymousInnerClass();

			private class ValueAnonymousInnerClass : Value
			{
				public override string ToString()
				{
					return "Untyped null";
				}
			}

			internal Value()
			{
			}
		}

		public UntypedExecVarTypeNode()
		{
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				// no children
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				// no children
				return childrenNames;
			}
		}

		public override bool IsCompatibleTo(TypeNode t)
		{
			// compatible to everything
			return true;
		}

		public override bool IsCastableTo(TypeNode t)
		{
			return IsCompatibleTo(t);
		}

		protected internal override IR ConstructIR()
		{
			return new UntypedExecVarType(Ident.IRIdent);
		}

		public override string ToString()
		{
			return "untyped";
		}
	}

}
