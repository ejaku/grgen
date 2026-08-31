/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
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
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using ContainerInitNode = de.unika.ipd.grgen.ast.expr.ContainerInitNode;
	using ArrayInitNode = de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
	using DequeInitNode = de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using MemberInitNode = de.unika.ipd.grgen.ast.model.MemberInitNode;
	using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;

	/// <summary>
	/// A class representing a node type
	/// </summary>
	public class NodeTypeNode : InheritanceTypeNode
	{
		static NodeTypeNode()
		{
			SetClassName(typeof(NodeTypeNode), "node type");
		}

		public static NodeTypeNode nodeType;

		private CollectNode<NodeTypeNode> extend;

		/// <summary>
		/// Create a new node type </summary>
		/// <param name="ext"> The collect node containing the node types which are extended by this type. </param>
		/// <param name="body"> the collect node with body declarations </param>
		/// <param name="modifiers"> Type modifiers for this type. </param>
		/// <param name="externalName"> The name of the external implementation of this type or null. </param>
		public NodeTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body, int modifiers, string externalName)
		{
			this.extendUnresolved = ext;
			BecomeParent(this.extendUnresolved);
			this.bodyUnresolved = body;
			BecomeParent(this.bodyUnresolved);
			Modifiers = modifiers;
			ExternalName = externalName;
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

		private static readonly CollectResolver<NodeTypeNode> extendResolver =
				new CollectResolver<NodeTypeNode>(new DeclarationTypeResolver<NodeTypeNode>(typeof(NodeTypeNode)));

		private static readonly CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
				new DeclarationResolver<BaseNode>(typeof(MemberDeclNode), typeof(MemberInitNode), typeof(ConstructorDeclNode),
						typeof(MapInitNode), typeof(SetInitNode), typeof(ArrayInitNode), typeof(DequeInitNode),
						typeof(FunctionDeclNode), typeof(ProcedureDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			OperatorDeclNode.MakeOp(Operator.COND, this, new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.SE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);

			body = bodyResolver.Resolve(bodyUnresolved, this);
			extend = extendResolver.Resolve(extendUnresolved, this);

			// Initialize direct sub types
			if(extend != null)
			{
				foreach(InheritanceTypeNode type in extend.ChildrenExact)
					type.AddDirectSubType(this);
			}

			return body != null && extend != null;
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
							+ " in node class " + Ident + ").");
					res &= false;
				}
			}

			return res;
		}

		/// <summary>
		/// Get the IR node type for this AST node. </summary>
		/// <returns> The correctly casted IR node type. </returns>
		public virtual NodeType IRNodeType
		{
			get
			{
				return CheckIR<NodeType>(typeof(NodeType));
			}
		}

		/// <summary>
		/// Construct IR object for this AST node. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			if(IsIRAlreadySet()) // break endless recursion in case of a member of node or container of node type
				return IR;

			NodeType nt = new NodeType(Decl.Ident.IRIdent, IRModifiers, ExternalName);

			IR = nt;

			ConstructIR(nt);

			return nt;
		}

		public override void DoGetCompatibleToTypes(ICollection<TypeNode> coll)
		{
			Debug.Assert(IsResolved());

			foreach(NodeTypeNode inh in extend.ChildrenExact)
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
				return "node class";
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
