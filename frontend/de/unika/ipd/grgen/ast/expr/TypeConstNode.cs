/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{
	using de.unika.ipd.grgen.ast;
	using StringConstNode = de.unika.ipd.grgen.ast.expr.@string.StringConstNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;

	/// <summary>
	/// A type const value.
	/// </summary>
	public class TypeConstNode : ConstNode
	{
		/// <summary>
		/// The name of the type. </summary>
		private IdentNode id;

		/// <param name="coords"> The source code coordinates. </param>
		/// <param name="id"> The name of the enum item. </param>
		/// <param name="value"> The value of the enum item. </param>
		public TypeConstNode(IdentNode id)
			: base(id.Coords, "type const", "DO NOT USE")
		{
			this.id = id;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ConstNode.doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) "/>
		protected internal override ConstNode DoCastTo(TypeNode type)
		{
			if(type.IsEqual(BasicTypeNode.stringType))
				return new StringConstNode(Coords, id.ToString());
			else
				throw new System.NotSupportedException();
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			return new Constant(Type.IRType, id.Decl.GetDeclType().GetIR());
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.getType() "/>
		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.typeType;
			}
		}

		public override object Value
		{
			get
			{
				return id.Decl.GetDeclType();
			}
		}
	}

}
