/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
	using SingleNodeConnNode = de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ErrorTypeNode = de.unika.ipd.grgen.ast.type.basic.ErrorTypeNode;
	using FunctionTypeNode = de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using FunctionMethod = de.unika.ipd.grgen.ir.executable.FunctionMethod;

	/// <summary>
	/// AST node class representing function declarations
	/// </summary>
	public class FunctionDeclNode : FunctionDeclBaseNode
	{
		static FunctionDeclNode()
		{
			SetClassName(typeof(FunctionDeclNode), "function declaration");
		}

		protected internal CollectNode<BaseNode> parametersUnresolved;
		protected internal CollectNode<DeclNode> parameters;

		public CollectNode<EvalStatementNode> evalStatements;
		public FunctionAutoNode functionAuto;

		internal bool isMethod;

		protected internal static readonly FunctionTypeNode functionType = new FunctionTypeNode();


		public FunctionDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, FunctionAutoNode functionAuto,
				CollectNode<BaseNode> @params, BaseNode ret, bool isMethod)
			: base(id, functionType)
		{
			this.evalStatements = evals;
			BecomeParent(this.evalStatements);
			this.functionAuto = functionAuto;
			this.parametersUnresolved = @params;
			BecomeParent(this.parametersUnresolved);
			this.resultUnresolved = ret;
			BecomeParent(this.resultUnresolved);
			this.isMethod = isMethod;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ident);
				children.Add(evalStatements);
				children.Add(parametersUnresolved);
				children.Add(GetValidVersion(resultUnresolved, resultType));
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
				childrenNames.Add("ident");
				childrenNames.Add("evals");
				childrenNames.Add("params");
				childrenNames.Add("ret");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			bool result = base.ResolveLocal();

			if(functionAuto != null)
				result &= functionAuto.ResolveLocalBypass();

			return result;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool CheckLocal()
		{
			parameters = new CollectNode<DeclNode>();
			foreach(BaseNode param in parametersUnresolved.ChildrenExact)
			{
				if(param is ConnectionNode)
				{
					ConnectionNode conn = (ConnectionNode)param;
					parameters.AddChild(conn.Edge.Decl);
				}
				else if(param is SingleNodeConnNode)
				{
					NodeDeclNode node = ((SingleNodeConnNode)param).Node;
					parameters.AddChild(node);
				}
				else if(param is VarDeclNode)
					parameters.AddChild((VarDeclNode)param);
				else
					throw new System.NotSupportedException("Unsupported parameter (" + param + ")");
			}

			parameterTypes = new List<TypeNode>();
			foreach(DeclNode decl in parameters.ChildrenExact)
				parameterTypes.Add(decl.DeclType);
			bool res = true;
			foreach(TypeNode parameterType in parameterTypes)
			{
				if(parameterType == null || parameterType is ErrorTypeNode)
					res = false;
			}

			if(functionAuto != null)
			{
				res &= functionAuto.CheckLocalBypass();
				res &= functionAuto.CheckLocal(this);
			}

			return res;
		}

		/// <summary>
		/// Returns the IR object for this function node. </summary>
		public virtual Function IRFunction
		{
			get
			{
				return CheckIR<Function>(typeof(Function));
			}
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());
				return functionType;
			}
		}

		protected internal override IR ConstructIR()
		{
			// return if the IR object was already constructed
			// that may happen in recursive calls
			if(IsIRAlreadySet())
				return IR;

			Function function = isMethod
					? new FunctionMethod(Ident.ToString(), Ident.IRIdent, resultType.CheckIR<Type>(typeof(Type)))
					: new Function(Ident.ToString(), Ident.IRIdent, resultType.CheckIR<Type>(typeof(Type)));

			// mark this node as already visited
			IR = function;

			// add Params to the IR
			foreach(DeclNode decl in parameters.ChildrenExact)
				function.AddParameter(decl.CheckIR<Entity>(typeof(Entity)));

			// add Computation Statements to the IR
			if(functionAuto != null)
				functionAuto.GetStatements(this, function);
			else
			{
				foreach(EvalStatementNode eval in evalStatements.ChildrenExact)
					function.AddStatement(eval.CheckIR<EvalStatement>(typeof(EvalStatement)));
			}

			return function;
		}

		public static string KindStr
		{
			get
			{
				return "function";
			}
		}
	}

}
