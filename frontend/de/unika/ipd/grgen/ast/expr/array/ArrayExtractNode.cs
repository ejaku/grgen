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
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayExtract = de.unika.ipd.grgen.ir.expr.array.ArrayExtract;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayExtractNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayExtractNode()
		{
			SetClassName(typeof(ArrayExtractNode), "array extract");
		}

		private IdentNode attribute;
		private DeclNode member;

		private ArrayTypeNode extractedArrayType;

		public ArrayExtractNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
			: base(coords, targetExpr)
		{
			this.attribute = attribute;
		}

		protected internal override bool ResolveLocal()
		{
			bool ownerResolveResult = targetExpr.Resolve();
			if(!ownerResolveResult)
			{
				// member can not be resolved due to inaccessible owner
				return false;
			}

			// target type already checked during resolving into this node
			ArrayTypeNode arrayType = TargetTypeExact;
			if(!(arrayType.valueType is InheritanceTypeNode)
					&& !(arrayType.valueType is MatchTypeNode))
			{
				targetExpr.ReportError("The array function method extract can only be employed on an object of type array<match<T>, match<T.S>, match<class T>, array<T> where T extends node/edge>"
						+ " (but is employed on an object of type " + arrayType.TypeName + ").");
				return false;
			}

			TypeNode valueType = arrayType.valueType;

			member = Resolver.ResolveMember(valueType, attribute);
			if(member == null)
				return false;

			TypeNode type = TypeOfElementToBeExtracted;
			if(!(type is DeclaredTypeNode)
					|| type is ContainerTypeNode
					|| type is MatchTypeNode)
			{
				ReportError("The type " + type.TypeName + " of the element to be extracted"
						+ " is not an allowed type (basic type or node or edge class - set, map, array, deque, and match are forbidden).");
				return false;
			}

			DeclaredTypeNode declType = (DeclaredTypeNode)type;
			extractedArrayType = new ArrayTypeNode(declType.Ident);

			return extractedArrayType.Resolve();
		}

		public override TypeNode Type
		{
			get
			{
				Debug.Assert((IsResolved()));
				return extractedArrayType;
			}
		}

		private TypeNode TypeOfElementToBeExtracted
		{
			get
			{
				if(member != null)
					return member.DeclType;
				return null;
			}
		}

		protected internal override IR ConstructIR()
		{
			Entity accessedMember = null;
			if(member != null)
				accessedMember = member.CheckIR(typeof(Entity));
			targetExpr = targetExpr.Evaluate();
			return new ArrayExtract(targetExpr.CheckIR(typeof(Expression)), extractedArrayType.CheckIR(typeof(ArrayType)),
					accessedMember);
		}
	}

}
