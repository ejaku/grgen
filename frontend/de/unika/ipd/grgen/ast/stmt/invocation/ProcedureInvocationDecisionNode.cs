/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.invocation
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using GraphAddCopyEdgeProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphAddCopyEdgeProcNode;
	using GraphAddCopyNodeProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphAddCopyNodeProcNode;
	using GraphAddEdgeProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphAddEdgeProcNode;
	using GraphAddNodeProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphAddNodeProcNode;
	using GraphClearProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphClearProcNode;
	using GraphMergeProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphMergeProcNode;
	using GraphRedirectSourceAndTargetProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphRedirectSourceAndTargetProcNode;
	using GraphRedirectSourceProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphRedirectSourceProcNode;
	using GraphRedirectTargetProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphRedirectTargetProcNode;
	using GraphRemoveProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphRemoveProcNode;
	using GraphRetypeProcNode = de.unika.ipd.grgen.ast.stmt.graph.GraphRetypeProcNode;
	using InsertCopyProcNode = de.unika.ipd.grgen.ast.stmt.graph.InsertCopyProcNode;
	using InsertDefinedSubgraphProcNode = de.unika.ipd.grgen.ast.stmt.graph.InsertDefinedSubgraphProcNode;
	using InsertInducedSubgraphProcNode = de.unika.ipd.grgen.ast.stmt.graph.InsertInducedSubgraphProcNode;
	using InsertProcNode = de.unika.ipd.grgen.ast.stmt.graph.InsertProcNode;
	using VAllocProcNode = de.unika.ipd.grgen.ast.stmt.graph.VAllocProcNode;
	using VFreeNonResetProcNode = de.unika.ipd.grgen.ast.stmt.graph.VFreeNonResetProcNode;
	using VFreeProcNode = de.unika.ipd.grgen.ast.stmt.graph.VFreeProcNode;
	using VResetProcNode = de.unika.ipd.grgen.ast.stmt.graph.VResetProcNode;
	using AssertProcNode = de.unika.ipd.grgen.ast.stmt.procenv.AssertProcNode;
	using EmitProcNode = de.unika.ipd.grgen.ast.stmt.procenv.EmitProcNode;
	using GetEquivalentOrAddProcNode = de.unika.ipd.grgen.ast.stmt.procenv.GetEquivalentOrAddProcNode;
	using RecordProcNode = de.unika.ipd.grgen.ast.stmt.procenv.RecordProcNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ProcedureTypeNode = de.unika.ipd.grgen.ast.type.executable.ProcedureTypeNode;
	using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;

	public class ProcedureInvocationDecisionNode : ProcedureInvocationBaseNode
	{
		static ProcedureInvocationDecisionNode()
		{
			SetClassName(typeof(ProcedureInvocationDecisionNode), "procedure invocation decision");
		}

		internal static TypeNode procedureTypeNode = new ProcedureTypeNode();

		protected internal IdentNode procedureIdent;
		protected internal BuiltinProcedureInvocationBaseNode result;

		internal ParserEnvironment env;

		public ProcedureInvocationDecisionNode(IdentNode procedureIdent,
				CollectNode<ExprNode> arguments, int context, ParserEnvironment env)
			: base(procedureIdent.Coords, arguments, context)
		{
			this.procedureIdent = BecomeParent(procedureIdent);
			this.env = env;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
				children.Add(arguments);
				if(IsResolved())
					children.Add(result);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				//childrenNames.add("methodIdent");
				childrenNames.Add("params");
				if(IsResolved())
					childrenNames.Add("result");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, Coords);
			result = Decide(procedureIdent.ToString(), arguments, resolvingEnvironment);
			return result != null;
		}

		private static BuiltinProcedureInvocationBaseNode Decide(string procedureName, CollectNode<ExprNode> arguments, ResolvingEnvironment env)
		{
			switch(procedureName)
			{
			case "add":
				if(arguments.Size() == 1)
					return new GraphAddNodeProcNode(env.Coords, arguments.Get(0));
				else if(arguments.Size() == 3)
					return new GraphAddEdgeProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2));
				else
				{
					env.ReportError(procedureName + "() expects 1 or 3 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "retype":
				if(arguments.Size() == 2)
					return new GraphRetypeProcNode(env.Coords, arguments.Get(0), arguments.Get(1));
				else
				{
					env.ReportError(procedureName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "insert":
				if(arguments.Size() != 1)
				{
					env.ReportError("insert(.) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new InsertProcNode(env.Coords, arguments.Get(0));
			case "insertCopy":
				if(arguments.Size() != 2)
				{
					env.ReportError("insertCopy(.,.) expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new InsertCopyProcNode(env.Coords, arguments.Get(0), arguments.Get(1));
			case "insertInduced":
				if(arguments.Size() != 2)
				{
					env.ReportError("insertInduced(.,.) expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new InsertInducedSubgraphProcNode(env.Coords, arguments.Get(0), arguments.Get(1));
			case "insertDefined":
				if(arguments.Size() != 2)
				{
					env.ReportError("insertDefined(.,.) expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new InsertDefinedSubgraphProcNode(env.Coords, arguments.Get(0), arguments.Get(1));
			case "valloc":
				if(arguments.Size() != 0)
				{
					env.ReportError("valloc() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new VAllocProcNode(env.Coords);
			case "rem":
				if(arguments.Size() != 1)
				{
					env.ReportError("rem(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new GraphRemoveProcNode(env.Coords, arguments.Get(0));
			case "clear":
				if(arguments.Size() != 0)
				{
					env.ReportError("clear() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new GraphClearProcNode(env.Coords);
			case "vfree":
				if(arguments.Size() != 1)
				{
					env.ReportError("vfree(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new VFreeProcNode(env.Coords, arguments.Get(0));
			case "vfreenonreset":
				if(arguments.Size() != 1)
				{
					env.ReportError("vfreenonreset(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new VFreeNonResetProcNode(env.Coords, arguments.Get(0));
			case "vreset":
				if(arguments.Size() != 1)
				{
					env.ReportError("vreset(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new VResetProcNode(env.Coords, arguments.Get(0));
			case "record":
				if(arguments.Size() != 1)
				{
					env.ReportError("record(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new RecordProcNode(env.Coords, arguments.Get(0));
			case "emit":
				if(arguments.Size() >= 1)
				{
					EmitProcNode emit = new EmitProcNode(env.Coords, false);
					foreach(ExprNode param in arguments.ChildrenExact)
						emit.AddExpression(param);
					return emit;
				}
				else
				{
					env.ReportError("emit() expects at least one argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "emitdebug":
				if(arguments.Size() >= 1)
				{
					EmitProcNode emit = new EmitProcNode(env.Coords, true);
					foreach(ExprNode param in arguments.ChildrenExact)
						emit.AddExpression(param);
					return emit;
				}
				else
				{
					env.ReportError("emitdebug() expects at least one argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "addCopy":
				if(arguments.Size() == 1)
					return new GraphAddCopyNodeProcNode(env.Coords, arguments.Get(0), true);
				else if(arguments.Size() == 3)
					return new GraphAddCopyEdgeProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), true);
				else
				{
					env.ReportError(procedureName + "() expects 1 or 3 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "addClone":
				if(arguments.Size() == 1)
					return new GraphAddCopyNodeProcNode(env.Coords, arguments.Get(0), false);
				else if(arguments.Size() == 3)
					return new GraphAddCopyEdgeProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), false);
				else
				{
					env.ReportError(procedureName + "() expects 1 or 3 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "merge":
				if(arguments.Size() < 2 || arguments.Size() > 3)
				{
					env.ReportError("merge(target,source,oldSourceName) expects 2 or 3 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					if(arguments.Size() == 2)
						return new GraphMergeProcNode(env.Coords, arguments.Get(0), arguments.Get(1), null);
					else
						return new GraphMergeProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2));
				}
				goto case "redirectSource";
			case "redirectSource":
				if(arguments.Size() < 2 || arguments.Size() > 3)
				{
					env.ReportError("redirectSource(edge,newSource,oldSourceName) expects 2 or 3 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					if(arguments.Size() == 2)
						return new GraphRedirectSourceProcNode(env.Coords, arguments.Get(0), arguments.Get(1), null);
					else
						return new GraphRedirectSourceProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2));
				}
				goto case "redirectTarget";
			case "redirectTarget":
				if(arguments.Size() < 2 || arguments.Size() > 3)
				{
					env.ReportError("redirectTarget(edge,newTarget,oldTargetName) expects 2 two or 3 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					if(arguments.Size() == 2)
						return new GraphRedirectTargetProcNode(env.Coords, arguments.Get(0), arguments.Get(1), null);
					else
						return new GraphRedirectTargetProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2));
				}
				goto case "redirectSourceAndTarget";
			case "redirectSourceAndTarget":
				if(arguments.Size() != 3 && arguments.Size() != 5)
				{
					env.ReportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) expects 3 or 5 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					if(arguments.Size() == 3)
						return new GraphRedirectSourceAndTargetProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), null, null);
					else
						return new GraphRedirectSourceAndTargetProcNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), arguments.Get(3), arguments.Get(4));
				}
				goto case "getEquivalentOrAdd";
			case "getEquivalentOrAdd":
				if(arguments.Size() != 2)
				{
					env.ReportError("getEquivalentOrAdd(graph, array<graph>) expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new GetEquivalentOrAddProcNode(env.Coords, arguments.Get(0), arguments.Get(1), true);
			case "getEquivalentStructurallyOrAdd":
				if(arguments.Size() != 2)
				{
					env.ReportError("getEquivalentStructurallyOrAdd(graph, array<graph>) expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new GetEquivalentOrAddProcNode(env.Coords, arguments.Get(0), arguments.Get(1), false);
			case "assert":
				if(arguments.Size() >= 1)
				{
					AssertProcNode assert_ = new AssertProcNode(env.Coords, false);
					foreach(ExprNode param in arguments.ChildrenExact)
						assert_.AddExpression(param);
					return assert_;
				}
				else
				{
					env.ReportError("assert() expects at least one argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "assertAlways":
				if(arguments.Size() >= 1)
				{
					AssertProcNode assert_ = new AssertProcNode(env.Coords, true);
					foreach(ExprNode param in arguments.ChildrenExact)
						assert_.AddExpression(param);
					return assert_;
				}
				else
				{
					env.ReportError("assertAlways() expects at least one argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			default:
				env.ReportError("A procedure of name " + procedureName + " is not known.");
				return null;
			}
		}

		protected internal override bool CheckLocal()
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				if(IsEmitOrDebugProcedure()) // allowed exceptions
					return true;
				else
				{
					ReportError("A procedure call (built-in-procedure " + procedureIdent + ") is not allowed in function or pattern part context.");
					return false;
				}
			}
			return true;
		}

		// procedures for debugging purpose, allowed also on lhs
		public virtual bool IsEmitOrDebugProcedure()
		{
			return IsEmitProcedure() || IsDebugProcedure();
		}

		protected internal virtual bool IsEmitProcedure()
		{
			switch(procedureIdent.ToString())
			{
			case "emit":
			case "emitdebug":
				return true;
			default:
				return false;
			}
		}

		protected internal virtual bool IsDebugProcedure()
		{
			switch(procedureIdent.ToString())
			{
			case "assert":
			case "assertAlways":
				return true;
			default:
				return false;
			}
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal virtual ProcedureOrBuiltinProcedureInvocationBaseNode Result
		{
			get
			{
				return result;
			}
		}

		public override IList<TypeNode> Type
		{
			get
			{
				return result.Type;
			}
		}

		public virtual int NumReturnTypes
		{
			get
			{
				return result.Type.Count;
			}
		}

		public virtual string ProcedureName
		{
			get
			{
				return procedureIdent.ToString();
			}
		}

		protected internal override IR ConstructIR()
		{
			return result.IR;
		}
	}

}
