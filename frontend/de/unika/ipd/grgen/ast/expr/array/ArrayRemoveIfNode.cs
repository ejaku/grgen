/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.array
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayRemoveIfExpr = de.unika.ipd.grgen.ir.expr.array.ArrayRemoveIfExpr;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayRemoveIfNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayRemoveIfNode()
		{
			SetClassName(typeof(ArrayRemoveIfNode), "array removeIf");
		}

		private VarDeclNode arrayAccessVar;

		private VarDeclNode indexVar;
		private VarDeclNode elementVar;
		private ExprNode conditionExpr;

		public ArrayRemoveIfNode(Coords coords, ExprNode targetExpr, VarDeclNode arrayAccessVar,
				VarDeclNode indexVar, VarDeclNode elementVar, ExprNode conditionExpr)
			: base(coords, targetExpr)
		{
			this.arrayAccessVar = arrayAccessVar;
			this.indexVar = indexVar;
			this.elementVar = elementVar;
			this.conditionExpr = conditionExpr;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(targetExpr);
				if(arrayAccessVar != null)
					children.Add(arrayAccessVar);
				if(indexVar != null)
					children.Add(indexVar);
				children.Add(elementVar);
				children.Add(conditionExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("targetExpr");
				if(arrayAccessVar != null)
					childrenNames.Add("arrayAccessVar");
				if(indexVar != null)
					childrenNames.Add("indexVar");
				childrenNames.Add("elementVar");
				childrenNames.Add("conditionExpr");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			bool ownerResolveResult = targetExpr.Resolve();
			if(!ownerResolveResult)
			{
				// member can not be resolved due to inaccessible owner
				return false;
			}

			type.container.ContainerTypeNode temp = TargetType;

			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			TypeNode exprType = conditionExpr.Type;

			if(arrayAccessVar != null)
			{
				TypeNode arrayAccessVarType = arrayAccessVar.DeclType;
				if(!(arrayAccessVarType is ArrayTypeNode))
				{
					ReportError("The array access variable of the array removeIf function method must be of array type"
							+ " (but is of type " + arrayAccessVarType.TypeName + ").");
					return false;
				}
				if(!arrayAccessVarType.IsEqual(targetExpr.Type))
				{
					ReportError("The array access variable of the array removeIf function method must be of type " + targetExpr.Type.TypeName
							+ " (but is of type " + arrayAccessVarType.TypeName + ").");
					return false;
				}
			}

			if(indexVar != null)
			{
				TypeNode indexVarType = indexVar.DeclType;
				if(!indexVarType.IsEqual(BasicTypeNode.intType))
				{
					ReportError("The index variable of the array removeIf function method must be of int type"
							+ " (but is of type " + indexVarType.TypeName + ").");
					return false;
				}
			}

			if(!exprType.IsEqual(BasicTypeNode.booleanType))
			{
				ReportError("Type mismatch in the array removeIf function method between the lambda expression value of type " + exprType.TypeName
						+ " and the expected boolean type.");
				return false;
			}

			TypeNode elementVarType = elementVar.DeclType;
			TypeNode targetType = ((ArrayTypeNode)targetExpr.Type).valueType;

			if(targetType is NodeTypeNode && elementVarType is EdgeTypeNode
					|| targetType is EdgeTypeNode && elementVarType is NodeTypeNode)
			{
				ReportError("Cannot bind the element variable of " + elementVarType.Kind + " " + elementVarType.TypeName
						+ " to a value of " + targetType.Kind + " " + targetType.TypeName + " in the array removeIf function method.");
				return false;
			}
			if(!targetType.IsCompatibleTo(elementVarType))
			{
				ReportError("Cannot bind the element variable of type " + elementVarType.ToStringWithDeclarationCoords()
						+ " to a value of type " + targetType.ToStringWithDeclarationCoords() + " in the array removeIf function method.");
				return false;
			}

			return true;
		}

		public override TypeNode Type
		{
			get
			{
				Debug.Assert((IsResolved()));
				return TargetType;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			conditionExpr = conditionExpr.Evaluate();
			return new ArrayRemoveIfExpr(targetExpr.CheckIR<Expression>(typeof(Expression)),
					arrayAccessVar != null ? arrayAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					indexVar != null ? indexVar.CheckIR<Variable>(typeof(Variable)) : null,
					elementVar.CheckIR<Variable>(typeof(Variable)),
					conditionExpr.CheckIR<Expression>(typeof(Expression)),
					TargetType.CheckIR<ArrayType>(typeof(ArrayType)));
		}
	}

}
