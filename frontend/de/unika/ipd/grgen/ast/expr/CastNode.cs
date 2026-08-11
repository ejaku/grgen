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

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
	using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ObjectTypeNode = de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Cast = de.unika.ipd.grgen.ir.expr.Cast;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A cast operator for expressions.
	/// </summary>
	public class CastNode : ExprNode
	{
		static CastNode()
		{
			SetClassName(typeof(CastNode), "cast expression");
		}

		// target type of the cast
		private BaseNode typeUnresolved;
		private TypeNode type;

		// expression to be casted
		private ExprNode expr;

		/// <summary>
		/// Make a new cast node. </summary>
		/// <param name="coords"> The source code coordinates. </param>
		public CastNode(Coords coords)
			: base(coords)
		{
		}

		/// <summary>
		/// Make a new cast node with a target type and an expression </summary>
		/// <param name="coords"> The source code coordinates. </param>
		/// <param name="targetType"> The target type. </param>
		/// <param name="expr"> The expression to be casted. </param>
		public CastNode(Coords coords, BaseNode targetType, ExprNode expr)
			: base(coords)
		{
			this.typeUnresolved = targetType;
			BecomeParent(this.typeUnresolved);
			this.expr = expr;
			BecomeParent(this.expr);
		}

		/// <summary>
		/// Make a new cast node with a target type and an expression, which is immediately marked as resolved
		/// Only to be called by type adjusting, after tree was already resolved </summary>
		/// <param name="coords"> The source code coordinates. </param>
		/// <param name="targetType"> The target type. </param>
		/// <param name="expr"> The expression to be casted. </param>
		/// <param name="resolveResult"> Resolution result (should be true) </param>
		public CastNode(Coords coords, TypeNode targetType, ExprNode expr, BaseNode parent)
			: this(coords, targetType, expr)
		{
			parent.BecomeParent(this);

			Resolve();
			Check();
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(typeUnresolved, type));
				children.Add(expr);
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
				childrenNames.Add("type");
				childrenNames.Add("expr");
				return childrenNames;
			}
		}

		private static DeclarationTypeResolver<TypeNode> typeResolver =
				new DeclarationTypeResolver<TypeNode>(typeof(TypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;
			if(typeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)typeUnresolved);
			else
				FixupDefinition(typeUnresolved, typeUnresolved.Scope);
			type = typeResolver.Resolve(typeUnresolved, this);
			successfullyResolved = type != null && successfullyResolved;
			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"
		/// A cast node is valid, if the second child is an expression node
		/// and the first node is a type node identifier./>
		protected internal override bool CheckLocal()
		{
			return TypeCheckLocal();
		}

		/// <summary>
		/// Check the types of this cast.
		/// Check if the expression can be casted to the given type. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.typeCheckLocal()"/>
		private bool TypeCheckLocal()
		{
			TypeNode fromType = expr.Type;
			if(fromType is NodeTypeNode && type is NodeTypeNode
					|| fromType is EdgeTypeNode && type is EdgeTypeNode
					|| fromType is InternalObjectTypeNode && type is InternalObjectTypeNode
					|| fromType is InternalTransientObjectTypeNode && type is InternalTransientObjectTypeNode)
			{
				// we support up- and down-casts, but no cross-casts of nodes/edges/class objects/transient class objects
				HashSet<TypeNode> supertypesOfFrom = new HashSet<TypeNode>();
				((InheritanceTypeNode)fromType).DoGetCompatibleToTypes(supertypesOfFrom);
				HashSet<TypeNode> supertypesOfTo = new HashSet<TypeNode>();
				((InheritanceTypeNode)type).DoGetCompatibleToTypes(supertypesOfTo);
				bool castable = fromType.Equals(type) || supertypesOfFrom.Contains(type) || supertypesOfTo.Contains(fromType);
				if(castable)
					return true;
			}
			if(fromType is ObjectTypeNode)
				return true; // object is castable to anything (at least to external object types) -- in a real OO language, everything should be statically castable into an object and out of an object (but could of course fail at runtime) -- TODO: make sure this really holds everywhere, it may very well be this does not hold (or define the exact relationship)
			if(type is ObjectTypeNode)
				return true; // anything can be casted into an object
			if(fromType is ExternalObjectTypeNode && type is ExternalObjectTypeNode)
			{
				// we support up- and down-casts, but no cross-casts of external object types
				HashSet<TypeNode> supertypesOfFrom = new HashSet<TypeNode>();
				((ExternalObjectTypeNode)fromType).DoGetCompatibleToTypes(supertypesOfFrom);
				HashSet<TypeNode> supertypesOfTo = new HashSet<TypeNode>();
				((ExternalObjectTypeNode)type).DoGetCompatibleToTypes(supertypesOfTo);
				bool castable = fromType.Equals(type) || supertypesOfFrom.Contains(type) || supertypesOfTo.Contains(fromType);
				if(castable)
					return true;
			}

			// assumption: when the castable checks above are failing, they cause also the castable check here to fail / they only prevent a fail in this place when the cast should succeed
			bool result = fromType.IsCastableTo(type);
			if(!result)
				ReportError("A cast from " + expr.Type.ToStringWithDeclarationCoords() + " to " + type.ToStringWithDeclarationCoords() + " is not supported.");

			return result;
		}

		/// <summary>
		/// Tries to simplify this node by simplifying the target expression and,
		/// if the expression is a constant, applying the cast. </summary>
		/// <returns> The possibly simplified value of the expression. </returns>
		public override ExprNode Evaluate()
		{
			Debug.Assert(IsResolved());

			expr = expr.Evaluate();
			if(expr is ConstNode)
			{
				ConstNode constExprEvaluated = ((ConstNode)expr).CastTo(type);
				if(constExprEvaluated is InvalidConstNode)
				{
					ReportError("The cast from " + expr.ToString() + " of type " + expr.Type.ToStringWithDeclarationCoords() + " to type " + type.ToStringWithDeclarationCoords() + " is failing.");
					return this;
				}
				return constExprEvaluated;
			}
			else
				return this;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.getType()"/>
		public override TypeNode Type
		{
			get
			{
				Debug.Assert(IsResolved());

				return type;
			}
		}

		protected internal override IR ConstructIR()
		{
			Type type = this.type.CheckIR<Type>(typeof(Type));
			this.expr = this.expr.Evaluate();
			Expression expr = this.expr.CheckIR<Expression>(typeof(Expression));

			return new Cast(type, expr);
		}
	}

}
