/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.procenv
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ImportExpr = de.unika.ipd.grgen.ir.expr.procenv.ImportExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding a graph imported from a file.
	/// </summary>
	public class ImportExprNode : BuiltinFunctionInvocationBaseNode
	{
		static ImportExprNode()
		{
			SetClassName(typeof(ImportExprNode), "import expr");
		}

		private ExprNode pathExpr;

		public ImportExprNode(Coords coords, ExprNode pathExpr)
			: base(coords)
		{
			this.pathExpr = pathExpr;
			BecomeParent(this.pathExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(pathExpr);
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
				childrenNames.Add("pathExpr");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(pathExpr.Type is StringTypeNode))
			{
				ReportError("The function File::import() expects as argument (filePath) a value of type string"
						+ " (but is given a value of type " + pathExpr.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			pathExpr = pathExpr.Evaluate();
			return new ImportExpr(pathExpr.CheckIR<Expression>(typeof(Expression)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.graphType;
			}
		}
	}

}
