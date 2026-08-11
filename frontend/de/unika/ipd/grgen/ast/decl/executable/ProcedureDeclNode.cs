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
	using ProcedureTypeNode = de.unika.ipd.grgen.ast.type.executable.ProcedureTypeNode;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using ProcedureMethod = de.unika.ipd.grgen.ir.executable.ProcedureMethod;

	/// <summary>
	/// AST node class representing procedure declarations
	/// </summary>
	public class ProcedureDeclNode : ProcedureDeclBaseNode
	{
		static ProcedureDeclNode()
		{
			SetClassName(typeof(ProcedureDeclNode), "procedure declaration");
		}

		protected internal CollectNode<BaseNode> parametersUnresolved;
		protected internal CollectNode<DeclNode> parameters;

		public CollectNode<EvalStatementNode> evalStatements;

		internal bool isMethod;

		internal static readonly ProcedureTypeNode procedureType = new ProcedureTypeNode();


		public ProcedureDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, CollectNode<BaseNode> @params,
				CollectNode<BaseNode> rets, bool isMethod)
			: base(id, procedureType)
		{
			this.evalStatements = evals;
			BecomeParent(this.evalStatements);
			this.parametersUnresolved = @params;
			BecomeParent(this.parametersUnresolved);
			this.resultsUnresolved = rets;
			BecomeParent(this.resultsUnresolved);
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
				children.Add(GetValidVersionCollectNode(resultsUnresolved, resultTypesCollectNode));
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

			return res;
		}

		/// <summary>
		/// Returns the IR object for this procedure node. </summary>
		public virtual Procedure IRProcedure
		{
			get
			{
				return CheckIR<Procedure>(typeof(Procedure));
			}
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return procedureType;
			}
		}

		protected internal override IR ConstructIR()
		{
			// return if the IR object was already constructed
			// that may happen in recursive calls
			if(IsIRAlreadySet())
				return IR;

			Procedure procedure = isMethod ? new ProcedureMethod(Ident.ToString(), Ident.IRIdent)
					: new Procedure(Ident.ToString(), Ident.IRIdent);

			// mark this node as already visited
			IR = procedure;

			// add return types to the IR
			foreach(TypeNode retType in resultTypesCollectNode.ChildrenExact)
				procedure.AddReturnType(retType.CheckIR<Type>(typeof(Type)));

			// add Params to the IR
			foreach(DeclNode decl in parameters.ChildrenExact)
				procedure.AddParameter(decl.CheckIR<Entity>(typeof(Entity)));

			// add Computation Statements to the IR
			foreach(EvalStatementNode eval in evalStatements.ChildrenExact)
				procedure.AddStatement(eval.CheckIR<EvalStatement>(typeof(EvalStatement)));

			return procedure;
		}

		public static string KindStr
		{
			get
			{
				return "procedure";
			}
		}
	}

}
