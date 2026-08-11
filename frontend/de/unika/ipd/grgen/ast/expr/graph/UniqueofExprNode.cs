/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Uniqueof = de.unika.ipd.grgen.ir.expr.graph.Uniqueof;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the name of some node/edge or the graph or an internal class object.
	/// </summary>
	public class UniqueofExprNode : BuiltinFunctionInvocationBaseNode
	{
		static UniqueofExprNode()
		{
			SetClassName(typeof(UniqueofExprNode), "uniqueof");
		}

		private ExprNode entity;

		public UniqueofExprNode(Coords coords, ExprNode entity)
			: base(coords)
		{
			this.entity = entity;
			BecomeParent(this.entity);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				if(entity != null)
					children.Add(entity);
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
				if(entity != null)
					childrenNames.Add("entity");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
		protected internal override bool CheckLocal()
		{
			if(entity != null)
			{
				if(entity.Type.IsEqual(BasicTypeNode.graphType))
					return true;
				if(entity.Type is EdgeTypeNode)
				{
					if(!UnitNode.Root.Model.IsUniqueDefined() && !UnitNode.Root.Model.IsUniqueIndexDefined())
					{
						ReportError("The function uniqueof applied to an argument of edge type expects a model with uniqueId support, but the required node edge unique; declaration is missing in the model specification.");
						return false;
					}
					return true;
				}
				if(entity.Type is NodeTypeNode)
				{
					if(!UnitNode.Root.Model.IsUniqueDefined() && !UnitNode.Root.Model.IsUniqueIndexDefined())
					{
						ReportError("The function uniqueof applied to an argument of node type expects a model with uniqueId support, but the required node edge unique; declaration is missing in the model specification.");
						return false;
					}
					return true;
				}
				if(entity.Type is InternalObjectTypeNode)
				{
					if(!UnitNode.Root.Model.IsUniqueClassDefined())
					{
						ReportError("The function uniqueof applied to an argument of (object) class type expects a model with uniqueId support, but the required object class unique; declaration is missing in the model specification.");
						return false;
					}
					return true;
				}

				ReportError("The function uniqueof expects as argument (entityToFetchUniqueIdOf) a value of type node or edge or graph or internal class object"
						+ " (but is given a value of type " + entity.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			if(entity == null)
				return new Uniqueof(null, Type.IRType);
			entity = entity.Evaluate();
			return new Uniqueof(entity.CheckIR(typeof(Expression)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				if(entity != null && entity.Type is InternalObjectTypeNode)
					return BasicTypeNode.longType;
				else
					return BasicTypeNode.intType;
			}
		}
	}

}
