/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.model.type
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ConstructorDeclNode = de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using MemberInitNode = de.unika.ipd.grgen.ast.model.MemberInitNode;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using ContainerInitNode = de.unika.ipd.grgen.ast.expr.ContainerInitNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;

	/// <summary>
	/// A class representing an (internal non-node/edge) object type (i.e. a class)
	/// </summary>
	public class InternalObjectTypeNode : BaseInternalObjectTypeNode
	{
		static InternalObjectTypeNode()
		{
			SetClassName(typeof(InternalObjectTypeNode), "internal object type");
		}

		public static InternalObjectTypeNode internalObjectType;

		private CollectNode<InternalObjectTypeNode> extend;

		/// <summary>
		/// Create a new (internal) object type (i.e. class) </summary>
		/// <param name="ext"> The collect node containing the object types which are extended by this type. </param>
		/// <param name="body"> the collect node with body declarations </param>
		/// <param name="modifiers"> Type modifiers for this type. </param>
		public InternalObjectTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body, int modifiers)
			: base(ext, body, modifiers)
		{
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(extendUnresolved, extend));
				children.Add(GetValidVersionCollectNode(bodyUnresolved, body));
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
				childrenNames.Add("extends");
				childrenNames.Add("body");
				return childrenNames;
			}
		}

		private static readonly CollectResolver<InternalObjectTypeNode> extendResolver =
				new CollectResolver<InternalObjectTypeNode>(new DeclarationTypeResolver<InternalObjectTypeNode>(typeof(InternalObjectTypeNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			OperatorDeclNode.MakeOp(Operator.COND, this, new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.SE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);

			bool bodyOk = base.ResolveLocal();
			extend = extendResolver.Resolve(extendUnresolved, this);

			// Initialize direct sub types
			if(extend != null)
			{
				foreach(InheritanceTypeNode type in extend.ChildrenExact)
					type.AddDirectSubType(this);
			}

			return bodyOk && extend != null;
		}

		protected internal override bool CheckLocal()
		{
			bool res = base.CheckLocal();

			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is ConstructorDeclNode
						|| child is MemberInitNode
						|| child is ContainerInitNode
						|| child is FunctionDeclNode
						|| child is ProcedureDeclNode)
					continue;
				DeclNode decl = (DeclNode)child;
				if(decl.DeclType is InternalTransientObjectTypeNode)
				{
					decl.ReportError("Only transient object classes may contain attributes of transient object class types"
							+ " (but the attribute " + decl.Ident
							+ " is of transient object class type " + decl.DeclType.ToStringWithDeclarationCoords()
							+ " in object class " + Ident + ").");
					res &= false;
				}
			}

			return res;
		}

		/// <summary>
		/// Get the IR internal type for this AST node. </summary>
		/// <returns> The correctly casted IR internal type. </returns>
		public virtual InternalObjectType IRInternalObjectType
		{
			get
			{
				return CheckIR(typeof(InternalObjectType));
			}
		}

		/// <summary>
		/// Construct IR object for this AST node. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			if(IsIRAlreadySet()) // break endless recursion in case of a member of class or container of class type
				return IR;

			InternalObjectType ot = new InternalObjectType(Decl.GetIdent().GetIRIdent(), IRModifiers);

			IR = ot;

			ConstructIR(ot);

			return ot;
		}

		public override void DoGetCompatibleToTypes(ICollection<TypeNode> coll)
		{
			Debug.Assert(IsResolved());

			foreach(InternalObjectTypeNode inh in extend.ChildrenExact)
			{
				coll.Add(inh);
				coll.AddAll(inh.CompatibleToTypes);
			}

			coll.Add(BasicTypeNode.typeType); // ~~ addCompatibility(this, BasicTypeNode.typeType);
		}

		public static string KindStr
		{
			get
			{
				return "internal object class";
			}
		}

		public override ICollection<InheritanceTypeNode> DirectSuperTypes
		{
			get
			{
				Debug.Assert(IsResolved());

				return new List<InheritanceTypeNode>(extend.ChildrenExact);
			}
		}
	}

}
