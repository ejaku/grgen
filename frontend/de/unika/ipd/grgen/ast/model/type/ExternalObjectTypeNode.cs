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

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExternalFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ExternalFunctionDeclNode;
	using ExternalProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ExternalProcedureDeclNode;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ExternalFunctionMethod = de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
	using ExternalProcedureMethod = de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
	using ExternalObjectType = de.unika.ipd.grgen.ir.model.type.ExternalObjectType;

	/// <summary>
	/// A class representing an external object type
	/// </summary>
	public class ExternalObjectTypeNode : InheritanceTypeNode
	{
		static ExternalObjectTypeNode()
		{
			SetClassName(typeof(ExternalObjectTypeNode), "external object type");
		}

		private CollectNode<ExternalObjectTypeNode> extend;

		/// <summary>
		/// Create a new external object type </summary>
		/// <param name="ext"> The collect node containing the types which are extended by this type. </param>
		public ExternalObjectTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body)
		{
			this.extendUnresolved = ext;
			BecomeParent(this.extendUnresolved);
			this.bodyUnresolved = body;
			BecomeParent(this.bodyUnresolved);

			// allow the conditional operator on the external type
			OperatorDeclNode.MakeOp(Operator.COND, this,
					new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);
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

		private static readonly CollectResolver<ExternalObjectTypeNode> extendResolver =
				new CollectResolver<ExternalObjectTypeNode>(new DeclarationTypeResolver<ExternalObjectTypeNode>(typeof(ExternalObjectTypeNode)));

		private static readonly CollectResolver<BaseNode> bodyResolver =
				new CollectResolver<BaseNode>(new DeclarationResolver<BaseNode>(typeof(ExternalFunctionDeclNode), typeof(ExternalProcedureDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			OperatorDeclNode.MakeOp(Operator.COND, this, new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.GE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.GT, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LT, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);

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

		/// <summary>
		/// Get the IR external object type for this AST node. </summary>
		/// <returns> The correctly casted IR external object type. </returns>
		protected internal virtual ExternalObjectType IRExternalObjectType
		{
			get
			{
				return CheckIR<ExternalObjectType>(typeof(ExternalObjectType));
			}
		}

		/// <summary>
		/// Construct IR object for this AST node. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			if(IsIRAlreadySet()) // break endless recursion in case of a member of node/edge type
				return IR;

			ExternalObjectType et = new ExternalObjectType(Decl.GetIdent().GetIRIdent());

			IR = et;

			ConstructIR(et);

			return et;
		}

		protected internal virtual void ConstructIR(ExternalObjectType extType)
		{
			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is ExternalFunctionDeclNode)
					extType.AddExternalFunctionMethod(child.CheckIR<ExternalFunctionMethod>(typeof(ExternalFunctionMethod)));
				else
					extType.AddExternalProcedureMethod(child.CheckIR<ExternalProcedureMethod>(typeof(ExternalProcedureMethod)));
			}
			foreach(InheritanceTypeNode inh in DirectSuperTypes)
				extType.AddDirectSuperType(inh.InheritanceIRType);
		}

		public override void DoGetCompatibleToTypes(ICollection<TypeNode> coll)
		{
			Debug.Assert(IsResolved());

			foreach(ExternalObjectTypeNode inh in extend.ChildrenExact)
			{
				coll.Add(inh);
				coll.AddAll(inh.CompatibleToTypes);
			}
		}

		public static string KindStr
		{
			get
			{
				return "external class";
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

		protected internal override void GetMembers(IDictionary<string, DeclNode> members)
		{
			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is ExternalFunctionDeclNode)
				{
					ExternalFunctionDeclNode function = (ExternalFunctionDeclNode)child;
					CheckExternalFunctionOverride(function);
				}
				else if(child is ExternalProcedureDeclNode)
				{
					ExternalProcedureDeclNode procedure = (ExternalProcedureDeclNode)child;
					CheckExternalProcedureOverride(procedure);
				}
			}
		}

		private void CheckExternalFunctionOverride(ExternalFunctionDeclNode function)
		{
			foreach(InheritanceTypeNode @base in AllSuperTypes)
			{
				foreach(BaseNode baseChild in @base.Body.ChildrenExact)
				{
					if(baseChild is ExternalFunctionDeclNode)
					{
						ExternalFunctionDeclNode functionBase = (ExternalFunctionDeclNode)baseChild;
						if(function.ident.ToString().Equals(functionBase.ident.ToString()))
							CheckSignatureAdhered(functionBase, function);
					}
				}
			}
		}

		private void CheckExternalProcedureOverride(ExternalProcedureDeclNode procedure)
		{
			foreach(InheritanceTypeNode @base in AllSuperTypes)
			{
				foreach(BaseNode baseChild in @base.Body.ChildrenExact)
				{
					if(baseChild is ExternalProcedureDeclNode)
					{
						ExternalProcedureDeclNode procedureBase = (ExternalProcedureDeclNode)baseChild;
						if(procedure.ident.ToString().Equals(procedureBase.ident.ToString()))
							CheckSignatureAdhered(procedureBase, procedure);
					}
				}
			}
		}
	}

}
