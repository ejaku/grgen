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

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using FilterCharacter = de.unika.ipd.grgen.ast.FilterCharacter;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
	using SingleNodeConnNode = de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using FilterFunctionTypeNode = de.unika.ipd.grgen.ast.type.executable.FilterFunctionTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using FilterFunction = de.unika.ipd.grgen.ir.executable.FilterFunction;
	using FilterFunctionExternal = de.unika.ipd.grgen.ir.executable.FilterFunctionExternal;
	using FilterFunctionInternal = de.unika.ipd.grgen.ir.executable.FilterFunctionInternal;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;

	/// <summary>
	/// AST node class representing filter function declarations
	/// </summary>
	public class FilterFunctionDeclNode : DeclNode, FilterCharacter
	{
		static FilterFunctionDeclNode()
		{
			SetClassName(typeof(FilterFunctionDeclNode), "filter function declaration");
		}

		protected internal CollectNode<BaseNode> paramsUnresolved;
		protected internal CollectNode<DeclNode> @params;

		public CollectNode<EvalStatementNode> evalStatements;
		internal static readonly FilterFunctionTypeNode filterFunctionType = new FilterFunctionTypeNode();

		protected internal IdentNode actionUnresolved;
		public ActionDeclNode action;

		public FilterFunctionDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, CollectNode<BaseNode> @params,
				IdentNode action)
			: base(id, filterFunctionType)
		{
			this.evalStatements = evals;
			BecomeParent(this.evalStatements);
			this.paramsUnresolved = @params;
			BecomeParent(this.paramsUnresolved);
			this.actionUnresolved = action;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ident);
				if(evalStatements != null)
					children.Add(evalStatements);
				children.Add(paramsUnresolved);
				children.Add(actionUnresolved);
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
				if(evalStatements != null)
					childrenNames.Add("evals");
				childrenNames.Add("params");
				childrenNames.Add("action");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<ActionDeclNode> actionResolver =
				new DeclarationResolver<ActionDeclNode>(typeof(ActionDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			if(!(actionUnresolved is PackageIdentNode))
				FixupDefinition(actionUnresolved, actionUnresolved.Scope);
			action = actionResolver.Resolve(actionUnresolved, this);
			return action != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool CheckLocal()
		{
			@params = new CollectNode<DeclNode>();
			foreach(BaseNode param in paramsUnresolved.ChildrenExact)
			{
				if(param is ConnectionNode)
				{
					ConnectionNode conn = (ConnectionNode)param;
					@params.AddChild(conn.Edge.Decl);
				}
				else if(param is SingleNodeConnNode)
				{
					NodeDeclNode node = ((SingleNodeConnNode)param).Node;
					@params.AddChild(node);
				}
				else if(param is VarDeclNode)
					@params.AddChild((VarDeclNode)param);
				else
					throw new System.NotSupportedException("Unsupported parameter (" + param + ")");
			}

			return true;
		}

		public virtual string FilterName
		{
			get
			{
				return Ident.ToString();
			}
		}

		public virtual ActionDeclNode Action
		{
			get
			{
				return action;
			}
		}

		/// <summary>
		/// Returns the IR object for this function filter node. </summary>
		public virtual FilterFunction IRFilterFunction
		{
			get
			{
				return CheckIR(typeof(FilterFunction));
			}
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return filterFunctionType;
			}
		}

		public virtual IList<TypeNode> ParameterTypes
		{
			get
			{
				Debug.Assert(IsChecked());

				IList<TypeNode> types = new List<TypeNode>();
				foreach(DeclNode decl in @params.ChildrenExact)
					types.Add(decl.DeclType);

				return types;
			}
		}

		protected internal override IR ConstructIR()
		{
			// return if the IR object was already constructed
			// that may happen in recursive calls
			if(IsIRAlreadySet())
				return IR;

			FilterFunction filterFunction;
			if(evalStatements != null)
				filterFunction = new FilterFunctionInternal(Ident.ToString(), Ident.IRIdent);
			else
				filterFunction = new FilterFunctionExternal(Ident.ToString(), Ident.IRIdent);

			// mark this node as already visited
			IR = filterFunction;

			filterFunction.Action = action.IRMatcher;
			action.IRMatcher.AddFilter(filterFunction);

			// add Params to the IR
			foreach(DeclNode decl in @params.ChildrenExact)
				filterFunction.AddParameter(decl.CheckIR(typeof(Entity)));

			if(evalStatements != null)
			{
				// add Computation Statements to the IR
				foreach(EvalStatementNode eval in evalStatements.ChildrenExact)
					((FilterFunctionInternal)filterFunction).AddStatement(eval.CheckIR(typeof(EvalStatement)));
			}

			return filterFunction;
		}

		public static string KindStr
		{
			get
			{
				return "filter function";
			}
		}
	}

}
