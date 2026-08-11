/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
	using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ScanExpr = de.unika.ipd.grgen.ir.expr.ScanExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding an object of the specified type derived from scanning the string input parameter.
	/// </summary>
	public class ScanExprNode : BuiltinFunctionInvocationBaseNode
	{
		static ScanExprNode()
		{
			SetClassName(typeof(ScanExprNode), "scan expr");
		}

		private BaseNode typeUnresolved;
		private TypeNode type;
		private ExprNode stringExpr;

		public ScanExprNode(Coords coords, BaseNode type, ExprNode stringExpr)
			: base(coords)
		{
			if(type != null)
			{
				this.typeUnresolved = type;
				BecomeParent(this.typeUnresolved);
			}
			this.stringExpr = stringExpr;
			BecomeParent(this.stringExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(stringExpr);
				if(typeUnresolved != null)
					children.Add(GetValidVersion(typeUnresolved, type));
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
				childrenNames.Add("string expr");
				if(typeUnresolved != null)
					childrenNames.Add("type");
				return childrenNames;
			}
		}

		protected internal static readonly DeclarationTypeResolver<TypeNode> typeResolver =
				new DeclarationTypeResolver<TypeNode>(typeof(TypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			if(typeUnresolved == null)
				type = BasicTypeNode.objectType;
			else
				type = typeResolver.Resolve(typeUnresolved, this);

			return type != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(stringExpr.Type is StringTypeNode))
			{
				if(type != null)
				{
					ReportError("The construct scan<" + type.TypeName + "> expects as argument a value of type string"
							+ " (but is given a value of type " + stringExpr.Type.TypeName + ").");
				}
				else
				{
					ReportError("The construct scan expects as argument a value of type string"
							+ " (but is given a value of type " + stringExpr.Type.TypeName + ").");
				}
				return false;
			}

			if(type != null)
			{
				if(type is InternalObjectTypeNode)
				{
					ReportError("The construct scan<T> disallows a type argument containing a class object type"
							+ " (but is given " + type.Kind + " " + type.TypeName + ").");
				}
				else if(type is InternalTransientObjectTypeNode)
				{
					ReportError("The construct scan<T> disallows a type argument containing a transient class object type"
							+ " (but is given " + type.Kind + " " + type.TypeName + ").");
				}
				if(type is ContainerTypeNode)
				{
					ContainerTypeNode containerType = (ContainerTypeNode)type;
					if(containerType.ElementType is InternalObjectTypeNode)
					{
						ReportError("The construct scan<T> disallows a type argument (of a container type) containing a class object type"
								+ " (but is given type " + type.TypeName + ").");
					}
					else if(containerType.ElementType is InternalTransientObjectTypeNode)
					{
						ReportError("The construct scan<T> disallows a type argument (of a container type) containing a transient class object type"
								+ " (but is given type " + type.TypeName + ").");
					}
					if(type is MapTypeNode)
					{
						MapTypeNode mapType = (MapTypeNode)type;
						if(mapType.keyType is InternalObjectTypeNode)
						{
							ReportError("The construct scan<T> disallows a type argument (of a container type) containing a class object type"
									+ " (but is given type " + type.TypeName + ").");
						}
						else if(mapType.keyType is InternalTransientObjectTypeNode)
						{
							ReportError("The construct scan<T> disallows a type argument (of a container type) containing a transient class object type"
									+ " (but is given type " + type.TypeName + ").");
						}
					}
				}
			}

			return true;
		}

		protected internal override IR ConstructIR()
		{
			stringExpr = stringExpr.Evaluate();
			return new ScanExpr(stringExpr.CheckIR<Expression>(typeof(Expression)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				if(type != null)
					return type;
				else
					return BasicTypeNode.objectType;
			}
		}
	}

}
