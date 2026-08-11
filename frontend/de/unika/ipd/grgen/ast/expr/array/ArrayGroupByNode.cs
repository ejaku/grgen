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
	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayGroupBy = de.unika.ipd.grgen.ir.expr.array.ArrayGroupBy;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayGroupByNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayGroupByNode()
		{
			SetClassName(typeof(ArrayGroupByNode), "array group by");
		}

		private IdentNode attribute;
		private DeclNode member;

		public ArrayGroupByNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
			: base(coords, targetExpr)
		{
			this.attribute = attribute;
		}

		protected internal override bool CheckLocal()
		{
			// target type already checked during resolving into this node
			ArrayTypeNode arrayType = TargetTypeExact;
			if(!(arrayType.valueType is InheritanceTypeNode)
					&& !(arrayType.valueType is MatchTypeNode))
			{
				targetExpr.ReportError("The array function method groupBy can only be employed on an object of type array<nodes, edges, class objects, transient class objects, match types, match class types>"
						+ " (but is employed on an object of type " + arrayType.TypeName + ").");
				return false;
			}

			TypeNode valueType = arrayType.valueType;
			member = Resolver.ResolveMember(valueType, attribute);
			if(member == null)
				return false;

			TypeNode memberType = TypeOfElementToBeExtracted;
			if(!memberType.IsFilterableType())
			{
				targetExpr.ReportError("The array function method groupBy is only available for attributes of type "
						+ TypeNode.FilterableTypesAsString + " (but is of type " + memberType.TypeName + ").");
				return false;
			}

			return true;
		}

		public override TypeNode Type
		{
			get
			{
				return TargetType;
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
			return new ArrayGroupBy(targetExpr.CheckIR(typeof(Expression)),
					accessedMember);
		}
	}

}
