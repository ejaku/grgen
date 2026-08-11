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
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayMapStartWithAccumulateByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMapStartWithAccumulateByExpr;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayMapStartWithAccumulateByNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayMapStartWithAccumulateByNode()
		{
			SetClassName(typeof(ArrayMapStartWithAccumulateByNode), "array map start with accumulate by");
		}

		private IdentNode resultValueTypeUnresolved;
		private TypeNode resultValueType;

		private VarDeclNode initArrayAccessVar;
		private ExprNode initExpr;

		private VarDeclNode arrayAccessVar;
		private VarDeclNode previousAccumulationAccessVar;
		private VarDeclNode indexVar;
		private VarDeclNode elementVar;
		private ExprNode mappingExpr;

		private ArrayTypeNode resultArrayType;

		public ArrayMapStartWithAccumulateByNode(Coords coords, ExprNode targetExpr, IdentNode resultValueType,
				VarDeclNode initArrayAccessVar, ExprNode initExpr,
				VarDeclNode arrayAccessVar, VarDeclNode previousAccumulationAccessVar, VarDeclNode indexVar, VarDeclNode elementVar, ExprNode mappingExpr)
			: base(coords, targetExpr)
		{
			this.resultValueTypeUnresolved = resultValueType;
			this.initArrayAccessVar = initArrayAccessVar;
			this.initExpr = initExpr;
			this.arrayAccessVar = arrayAccessVar;
			this.previousAccumulationAccessVar = previousAccumulationAccessVar;
			this.indexVar = indexVar;
			this.elementVar = elementVar;
			this.mappingExpr = mappingExpr;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(targetExpr);
				if(initArrayAccessVar != null)
					children.Add(initArrayAccessVar);
				children.Add(initExpr);
				if(arrayAccessVar != null)
					children.Add(arrayAccessVar);
				children.Add(previousAccumulationAccessVar);
				if(indexVar != null)
					children.Add(indexVar);
				children.Add(elementVar);
				children.Add(mappingExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("targetExpr");
				if(initArrayAccessVar != null)
					childrenNames.Add("initArrayAccessVar");
				childrenNames.Add("initExpr");
				if(arrayAccessVar != null)
					childrenNames.Add("arrayAccessVar");
				childrenNames.Add("previousAccumulationAccessVar");
				if(indexVar != null)
					childrenNames.Add("indexVar");
				childrenNames.Add("elementVar");
				childrenNames.Add("mappingExpr");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<TypeDeclNode> typeResolver =
				new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode));

		protected internal override bool ResolveLocal()
		{
			bool ownerResolveResult = targetExpr.Resolve();
			if(!ownerResolveResult)
			{
				// member can not be resolved due to inaccessible owner
				return false;
			}

			TargetType;

			TypeDeclNode resultValueTypeDecl = typeResolver.Resolve(resultValueTypeUnresolved, this);
			if(resultValueTypeDecl == null)
				return false;
			if(!resultValueTypeDecl.Resolve())
				return false;

			// maybe move to type checking
			resultValueType = resultValueTypeDecl.DeclType;
			if(!(resultValueType is DeclaredTypeNode)
					|| resultValueType is ContainerTypeNode)
			{
				ReportError("The type " + resultValueType.TypeName
						+ " is not an allowed type - set, map, array, deque are forbidden.");
				return false;
			}

			DeclaredTypeNode declResultValueType = (DeclaredTypeNode)resultValueType;

			resultArrayType = new ArrayTypeNode(declResultValueType.Ident);
			if(!resultArrayType.Resolve())
				return false;

			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			TypeNode resultType = resultArrayType.valueType;
			TypeNode initExprType = initExpr.Type;
			TypeNode exprType = mappingExpr.Type;

			if(initArrayAccessVar != null)
			{
				TypeNode initArrayAccessVarType = initArrayAccessVar.DeclType;
				if(!(initArrayAccessVarType is ArrayTypeNode))
				{
					ReportError("The init array access variable of the array mapStartWithAccumulateBy function method must be of array type"
							+ " (but is of type " + initArrayAccessVarType.TypeName + ").");
					return false;
				}
				if(!initArrayAccessVarType.IsEqual(targetExpr.GetType()))
				{
					ReportError("The init array access variable of the array mapStartWithAccumulateBy function method must be of type " + targetExpr.GetType().TypeName
							+ " (but is of type " + initArrayAccessVarType.TypeName + ").");
					return false;
				}
			}

			if(!initExprType.IsEqual(resultType))
			{
				initExpr = BecomeParent(initExpr.AdjustType(resultValueType, Coords));
				if(initExpr == ConstNode.Invalid)
					return false;

				if(resultType is NodeTypeNode && initExprType is NodeTypeNode
						|| resultType is EdgeTypeNode && initExprType is EdgeTypeNode)
				{
					ICollection<TypeNode> superTypes = new HashSet<TypeNode>();
					initExprType.DoGetCompatibleToTypes(superTypes);
					if(!superTypes.Contains(resultType))
					{
						ReportError("Type mismatch in the array mapStartWithAccumulateBy function method between the init lambda expression value of type " + initExprType.ToStringWithDeclarationCoords()
								+ " and the expected element type " + resultType.ToStringWithDeclarationCoords() + " of the resulting array.");
						return false;
					}
				}
				if(resultType is NodeTypeNode && initExprType is EdgeTypeNode
						|| resultType is EdgeTypeNode && initExprType is NodeTypeNode)
				{
					ReportError("Type mismatch in the array mapStartWithAccumulateBy function method between the init lambda expression value of " + initExprType.Kind + " " + initExprType.TypeName
							+ " and the expected " + resultType.Kind + " element type " + resultType.TypeName + " of the resulting array.");
					return false;
				}
			}

			if(arrayAccessVar != null)
			{
				TypeNode arrayAccessVarType = arrayAccessVar.DeclType;
				if(!(arrayAccessVarType is ArrayTypeNode))
				{
					ReportError("The array access variable of the array mapStartWithAccumulateBy function method must be of array type"
							+ " (but is of type " + arrayAccessVarType.TypeName + ").");
					return false;
				}
				if(!arrayAccessVarType.IsEqual(targetExpr.GetType()))
				{
					ReportError("The array access variable of the array mapStartWithAccumulateBy function method must be of type " + targetExpr.GetType().TypeName
							+ " (but is of type " + arrayAccessVarType.TypeName + ").");
					return false;
				}
			}

			TypeNode previousAccumulationAccessVarType = previousAccumulationAccessVar.DeclType;
			if(!previousAccumulationAccessVarType.IsEqual(resultValueType))
			{
				ReportError("The previous accumulation access variable of the array mapStartWithAccumulateBy function method must be of type " + resultValueType.TypeName
						+ " (but is of type " + previousAccumulationAccessVarType.TypeName + ").");
				return false;
			}

			if(indexVar != null)
			{
				TypeNode indexVarType = indexVar.DeclType;
				if(!indexVarType.IsEqual(BasicTypeNode.intType))
				{
					ReportError("The index variable of the array mapStartWithAccumulateBy function method must be of int type"
							+ " (but is of type " + indexVarType.TypeName + ").");
					return false;
				}
			}

			if(!exprType.IsEqual(resultType))
			{
				mappingExpr = BecomeParent(mappingExpr.AdjustType(resultValueType, Coords));
				if(mappingExpr == ConstNode.Invalid)
					return false;

				if(resultType is NodeTypeNode && exprType is NodeTypeNode
						|| resultType is EdgeTypeNode && exprType is EdgeTypeNode)
				{
					ICollection<TypeNode> superTypes = new HashSet<TypeNode>();
					exprType.DoGetCompatibleToTypes(superTypes);
					if(!superTypes.Contains(resultType))
					{
						ReportError("Type mismatch in the array mapStartWithAccumulateBy function method between the lambda expression value of type " + exprType.ToStringWithDeclarationCoords()
								+ " and the expected element type " + resultType.ToStringWithDeclarationCoords() + " of the resulting array.");
						return false;
					}
				}
				if(resultType is NodeTypeNode && exprType is EdgeTypeNode
						|| resultType is EdgeTypeNode && exprType is NodeTypeNode)
				{
					ReportError("Type mismatch in the array mapStartWithAccumulateBy function method between the lambda expression value of " + exprType.Kind + " " + exprType.TypeName
							+ " and the expected " + resultType.Kind + " element type " + resultType.TypeName + " of the resulting array.");
					return false;
				}
			}

			TypeNode elementVarType = elementVar.DeclType;
			TypeNode targetType = ((ArrayTypeNode)targetExpr.GetType()).valueType;

			if(targetType is NodeTypeNode && elementVarType is EdgeTypeNode
					|| targetType is EdgeTypeNode && elementVarType is NodeTypeNode)
			{
				ReportError("Cannot bind the element variable of " + elementVarType.Kind + " " + elementVarType.TypeName
						+ " to a value of " + targetType.Kind + " " + targetType.TypeName + " in the array mapStartWithAccumulateBy function method.");
				return false;
			}
			if(!targetType.IsCompatibleTo(elementVarType))
			{
				ReportError("Cannot bind the element variable of type " + elementVarType.ToStringWithDeclarationCoords()
						+ " to a value of type " + targetType.ToStringWithDeclarationCoords() + " in the array mapStartWithAccumulateBy function method.");
				return false;
			}

			return true;
		}

		public override TypeNode Type
		{
			get
			{
				Debug.Assert((IsResolved()));
				return resultArrayType;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			mappingExpr = mappingExpr.Evaluate();
			return new ArrayMapStartWithAccumulateByExpr(targetExpr.CheckIR<Expression>(typeof(Expression)),
					initArrayAccessVar != null ? initArrayAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					initExpr.CheckIR<Expression>(typeof(Expression)), arrayAccessVar != null ? arrayAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					previousAccumulationAccessVar != null ? previousAccumulationAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					indexVar != null ? indexVar.CheckIR<Variable>(typeof(Variable)) : null,
					elementVar.CheckIR<Variable>(typeof(Variable)),
					mappingExpr.CheckIR<Expression>(typeof(Expression)),
					resultArrayType.CheckIR<ArrayType>(typeof(ArrayType)));
		}
	}

}
