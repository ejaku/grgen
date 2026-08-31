/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExecVarDeclNode = de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
	using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using DeclExprNode = de.unika.ipd.grgen.ast.expr.DeclExprNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using BooleanTypeNode = de.unika.ipd.grgen.ast.type.basic.BooleanTypeNode;
	using ByteTypeNode = de.unika.ipd.grgen.ast.type.basic.ByteTypeNode;
	using DoubleTypeNode = de.unika.ipd.grgen.ast.type.basic.DoubleTypeNode;
	using FloatTypeNode = de.unika.ipd.grgen.ast.type.basic.FloatTypeNode;
	using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
	using LongTypeNode = de.unika.ipd.grgen.ast.type.basic.LongTypeNode;
	using NullTypeNode = de.unika.ipd.grgen.ast.type.basic.NullTypeNode;
	using ShortTypeNode = de.unika.ipd.grgen.ast.type.basic.ShortTypeNode;
	using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Exec = de.unika.ipd.grgen.ir.Exec;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
	using VariableExpression = de.unika.ipd.grgen.ir.expr.VariableExpression;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;
	using Color = de.unika.ipd.grgen.util.Color;

	public class ExecNode : BaseNode
	{
		static ExecNode()
		{
			SetClassName(typeof(ExecNode), "exec");
		}

		private static readonly CollectQuadrupleResolver<ExecVarDeclNode, NodeDeclNode, EdgeDeclNode, VarDeclNode> graphElementUsageOutsideOfCallResolver =
			new CollectQuadrupleResolver<ExecVarDeclNode, NodeDeclNode, EdgeDeclNode, VarDeclNode>(
			new DeclarationQuadrupleResolver<ExecVarDeclNode, NodeDeclNode, EdgeDeclNode, VarDeclNode>(typeof(ExecVarDeclNode), typeof(NodeDeclNode), typeof(EdgeDeclNode), typeof(VarDeclNode)));

		private StringBuilder sb = new StringBuilder(); // if sb.length()==0 this is an external exec implemented externally

		protected internal CollectNode<MultiCallActionNode> multiCallActions = new CollectNode<MultiCallActionNode>();
		public CollectNode<CallActionNode> callActions = new CollectNode<CallActionNode>();
		private CollectNode<ExecVarDeclNode> varDecls = new CollectNode<ExecVarDeclNode>();
		private CollectNode<IdentNode> usageUnresolved = new CollectNode<IdentNode>();
		private CollectNode<IdentNode> writeUsageUnresolved = new CollectNode<IdentNode>();
		private CollectNode<DeclNode> usage = new CollectNode<DeclNode>();
		private CollectNode<DeclNode> writeUsage = new CollectNode<DeclNode>();

		private bool xgrsStringBuildingDisabled = false;

		public ExecNode(Coords coords)
			: base(coords)
		{
			BecomeParent(multiCallActions);
			BecomeParent(callActions);
		}

		public virtual void Append(object n)
		{
			Debug.Assert(!IsResolved());

			if(xgrsStringBuildingDisabled)
				return;

			if(n is ConstNode)
			{
				ConstNode constant = (ConstNode)n;
				TypeNode type = constant.Type;
				object value = constant.Value;

				if(type is StringTypeNode)
				{
					if(value == null)
						sb.Append("null");
					else
						sb.Append("\"" + value + "\"");
				}
				else if(type is IntTypeNode || type is DoubleTypeNode
						|| type is ByteTypeNode || type is ShortTypeNode)
					sb.Append(value);
				else if(type is FloatTypeNode)
					sb.Append(value + "f");
				else if(type is LongTypeNode)
					sb.Append(value + "L");
				else if(type is BooleanTypeNode)
					sb.Append(((bool?)value).Value ? "true" : "false");
				else if(type is NullTypeNode)
					sb.Append("null");
				else
					throw new System.NotSupportedException("unsupported type");
			}
			else if(n is IdentExprNode)
			{
				IdentExprNode identExpr = (IdentExprNode)n;
				sb.Append(identExpr.Ident);
			}
			else if(n is DeclExprNode)
			{
				DeclExprNode declExpr = (DeclExprNode)n;
				sb.Append(declExpr.declUnresolved);
			}
			else
				sb.Append(n);
		}

		private string XGRSString
		{
			get
			{
				return sb.ToString();
			}
		}

		public virtual void EnableXgrsStringBuilding()
		{
			xgrsStringBuildingDisabled = false;
		}

		public virtual void DisableXgrsStringBuilding()
		{
			xgrsStringBuildingDisabled = true;
		}

		public virtual void AddMultiCallAction(MultiCallActionNode m)
		{
			Debug.Assert(!IsResolved());
			BecomeParent(m);
			multiCallActions.AddChild(m);
		}

		public virtual void AddCallAction(CallActionNode n)
		{
			Debug.Assert(!IsResolved());
			BecomeParent(n);
			callActions.AddChild(n);
		}

		/// <summary>
		/// Registers an explicit sequence-local variable declaration
		/// </summary>
		public virtual void AddVarDecl(ExecVarDeclNode varDecl)
		{
			Debug.Assert(!IsResolved());
			BecomeParent(varDecl);
			varDecls.AddChild(varDecl);
		}

		/// <summary>
		/// Registers an identifier usage which might denote
		/// a) the use of a declared pattern graph element (node/edge)
		/// b) the use of a graph-global or sequence-local variable
		/// c) the implicit declaration of a graph-global variable at the first occurance
		/// which appears outside of a call (i.e. is not a rule call (input) parameter)
		/// </summary>
		public virtual void AddUsage(IdentNode id)
		{
			Debug.Assert(!IsResolved());
			BecomeParent(id);
			usageUnresolved.AddChild(id);
		}

		public virtual void AddWriteUsage(IdentNode id)
		{
			Debug.Assert(!IsResolved());
			BecomeParent(id);
			writeUsageUnresolved.AddChild(id);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> res = new List<BaseNode>();
				res.Add(multiCallActions);
				res.Add(callActions);
				res.Add(varDecls);
				res.Add(GetValidVersionCollectNode(usageUnresolved, usage));
				res.Add(GetValidVersionCollectNode(writeUsageUnresolved, writeUsage));
				return res;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("multi call actions");
				childrenNames.Add("call actions");
				childrenNames.Add("var decls");
				childrenNames.Add("graph element usage outside of a call");
				childrenNames.Add("writing graph element usage (outside of a call)");
				return childrenNames;
			}
		}

		/*
		 * This introduces an ExecVar definition if an identifier is not defined
		 * to support the usage-is-definition policy of the graph global variables in the sequences.
		 * Note: an (x)=r() & (x:A)=r() error will not be found due to the grgen symbol table and the fixupDefinition
		 * not taking care of the position of the definition compared to the uses
		 * (which makes sense for every other construct of the grgen language);
		 * this error will be caught later on when the xgrs is processed by the libgr sequence parser and symbol table.
		 */
		public virtual void AddImplicitDefinitions()
		{
			foreach(IdentNode id in usageUnresolved.ChildrenExact)
			{
				debug.Report(NOTE, "Implicit definition for " + id + " in scope " + Scope);

				// Get the definition of the ident's symbol local to the owned scope.
				Symbol.Definition def = Scope.GetCurrDef(id.Symbol);
				debug.Report(NOTE, "definition is: " + def);

				// If this definition is valid, i.e. it exists, it will be used
				// else, an ExecVarDeclNode of this name is added to the scope
				if(def.IsValid())
					id.SymDef = def;
				else
				{
					Symbol.Definition vdef = Scope.Define(id.Symbol, id.Coords);
					id.SymDef = vdef;
					vdef.Node = id;
					Scope.LeaveScope();
					ExecVarDeclNode evd = new ExecVarDeclNode(id, BasicTypeNode.untypedType);
					id.Decl = evd;
					AddVarDecl(evd);
				}
			}

			foreach(IdentNode id in writeUsageUnresolved.ChildrenExact)
			{
				debug.Report(NOTE, "Implicit definition for " + id + " in scope " + Scope);

				// Get the definition of the ident's symbol local to the owned scope.
				Symbol.Definition def = Scope.GetCurrDef(id.Symbol);
				debug.Report(NOTE, "definition is: " + def);

				// If this definition is valid, i.e. it exists, it will be used
				// else, an ExecVarDeclNode of this name is added to the scope
				if(def.IsValid())
					id.SymDef = def;
				else
				{
					Symbol.Definition vdef = Scope.Define(id.Symbol, id.Coords);
					id.SymDef = vdef;
					vdef.Node = id;
					Scope.LeaveScope();
					ExecVarDeclNode evd = new ExecVarDeclNode(id, BasicTypeNode.untypedType);
					id.Decl = evd;
					AddVarDecl(evd);
				}
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			AddImplicitDefinitions();
			Quadruple<CollectNode<ExecVarDeclNode>, CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<VarDeclNode>> resolve = graphElementUsageOutsideOfCallResolver.Resolve(usageUnresolved);

			if(resolve != null)
			{
				if(resolve.first != null)
				{
					foreach(ExecVarDeclNode execVar in resolve.first.ChildrenExact)
						usage.AddChild(execVar);
				}

				if(resolve.second != null)
				{
					foreach(NodeDeclNode node in resolve.second.ChildrenExact)
						usage.AddChild(node);
				}

				if(resolve.third != null)
				{
					foreach(EdgeDeclNode edge in resolve.third.ChildrenExact)
						usage.AddChild(edge);
				}

				if(resolve.fourth != null)
				{
					foreach(VarDeclNode var in resolve.fourth.ChildrenExact)
						usage.AddChild(var);
				}

				BecomeParent(usage);
			}

			Quadruple<CollectNode<ExecVarDeclNode>, CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<VarDeclNode>> writeResolve =
					graphElementUsageOutsideOfCallResolver.Resolve(writeUsageUnresolved);

			if(writeResolve != null)
			{
				if(writeResolve.first != null)
				{
					foreach(ExecVarDeclNode execVar in writeResolve.first.ChildrenExact)
						writeUsage.AddChild(execVar);
				}

				if(writeResolve.second != null)
				{
					foreach(NodeDeclNode node in writeResolve.second.ChildrenExact)
					{
						if(!node.defEntityToBeYieldedTo)
						{
							ReportError("Only a def (to be yielded to) node is allowed to be written from an exec statement"
									+ " (this does not hold for " + node.Ident + ").");
						}
						writeUsage.AddChild(node);
					}
				}

				if(writeResolve.third != null)
				{
					foreach(EdgeDeclNode edge in writeResolve.third.ChildrenExact)
					{
						if(!edge.defEntityToBeYieldedTo)
						{
							ReportError("Only a def (to be yielded to) edge is allowed to be written from an exec statement"
									+ " (this does not hold for " + edge.Ident + ").");
						}
						writeUsage.AddChild(edge);
					}
				}

				if(writeResolve.fourth != null)
				{
					foreach(VarDeclNode var in writeResolve.fourth.ChildrenExact)
					{
						if(!var.defEntityToBeYieldedTo)
						{
							ReportError("Only a def (to be yielded to) variable is allowed to be written from an exec statement"
									+ " (this does not hold for " + var.Ident + ").");
						}
						writeUsage.AddChild(var);
					}
				}

				BecomeParent(writeUsage);
			}

			return resolve != null && writeResolve != null;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override Color NodeColor
		{
			get
			{
				return Color.PINK;
			}
		}

		protected internal override IR ConstructIR()
		{
			ISet<Expression> parameters = new LinkedHashSet<Expression>();
			foreach(DeclNode dn in usage.ChildrenExact)
			{
				if(dn is ConstraintDeclNode)
					parameters.Add(new GraphEntityExpression(dn.CheckIR<GraphEntity>(typeof(GraphEntity))));
				else if(dn is VarDeclNode)
					parameters.Add(new VariableExpression(dn.CheckIR<Variable>(typeof(Variable))));
			}
			foreach(DeclNode dn in writeUsage.ChildrenExact)
			{
				if(dn is ConstraintDeclNode)
					parameters.Add(new GraphEntityExpression(dn.CheckIR<GraphEntity>(typeof(GraphEntity))));
				else if(dn is VarDeclNode)
					parameters.Add(new VariableExpression(dn.CheckIR<Variable>(typeof(Variable))));
			}
			foreach(CallActionNode callActionNode in callActions.ChildrenExact)
			{
				callActionNode.CheckPost();
				foreach(ExprNode param in callActionNode.Params.ChildrenExact)
				{
					ExprNode paramEvaluated = param.Evaluate();
					parameters.Add(paramEvaluated.CheckIR<Expression>(typeof(Expression)));
				}
			}
			foreach(MultiCallActionNode multiCallActionNode in multiCallActions.ChildrenExact)
				multiCallActionNode.CheckPost();
			Exec res = new Exec(XGRSString, parameters, Coords.Line);
			return res;
		}
	}

}
