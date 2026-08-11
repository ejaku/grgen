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
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using ArrayAddAllNode = de.unika.ipd.grgen.ast.stmt.array.ArrayAddAllNode;
using ArrayAddItemNode = de.unika.ipd.grgen.ast.stmt.array.ArrayAddItemNode;
using ArrayClearNode = de.unika.ipd.grgen.ast.stmt.array.ArrayClearNode;
using ArrayRemoveItemNode = de.unika.ipd.grgen.ast.stmt.array.ArrayRemoveItemNode;
using DequeAddItemNode = de.unika.ipd.grgen.ast.stmt.deque.DequeAddItemNode;
using DequeClearNode = de.unika.ipd.grgen.ast.stmt.deque.DequeClearNode;
using DequeRemoveItemNode = de.unika.ipd.grgen.ast.stmt.deque.DequeRemoveItemNode;
using MapAddItemNode = de.unika.ipd.grgen.ast.stmt.map.MapAddItemNode;
using MapClearNode = de.unika.ipd.grgen.ast.stmt.map.MapClearNode;
using MapRemoveItemNode = de.unika.ipd.grgen.ast.stmt.map.MapRemoveItemNode;
using SetAddAllNode = de.unika.ipd.grgen.ast.stmt.set.SetAddAllNode;
using SetAddItemNode = de.unika.ipd.grgen.ast.stmt.set.SetAddItemNode;
using SetClearNode = de.unika.ipd.grgen.ast.stmt.set.SetClearNode;
using SetRemoveItemNode = de.unika.ipd.grgen.ast.stmt.set.SetRemoveItemNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
using IR = de.unika.ipd.grgen.ir.IR;

public class ProcedureMethodInvocationDecisionNode : ProcedureInvocationBaseNode
{
	static ProcedureMethodInvocationDecisionNode()
	{
		SetClassName(typeof(ProcedureMethodInvocationDecisionNode), "procedure method invocation decision statement");
	}

	private BaseNode target;
	private IdentNode methodIdent;
	private ProcedureOrBuiltinProcedureInvocationBaseNode result;

	public ProcedureMethodInvocationDecisionNode(BaseNode target, IdentNode methodIdent, CollectNode<ExprNode> arguments,
			int context)
		: base(methodIdent.Coords, arguments, context)
	{
		this.target = BecomeParent(target);
		this.methodIdent = BecomeParent(methodIdent);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(target);
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
		childrenNames.Add("target");
		//childrenNames.add("methodIdent");
		childrenNames.Add("params");
		if(IsResolved())
			childrenNames.Add("result");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		if(!target.Resolve())
			return false;

		string methodName = methodIdent.ToString();
		VarDeclNode targetVar = null;
		QualIdentNode targetQual = null;
		TypeNode targetType = null;
		if(target is QualIdentNode)
		{
			targetQual = (QualIdentNode)target;
			targetType = targetQual.Decl.DeclType;
		}
		else if(((IdentExprNode)target).decl is VarDeclNode)
		{
			targetVar = (VarDeclNode)((IdentExprNode)target).decl;
			targetType = targetVar.DeclType;
		}
		else
			targetType = ((IdentExprNode)target).Type;

		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(null, error, Coords);
		if(targetType is MapTypeNode)
			result = DecideMap(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		else if(targetType is SetTypeNode)
			result = DecideSet(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		else if(targetType is ArrayTypeNode)
			result = DecideArray(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		else if(targetType is DequeTypeNode)
			result = DecideDeque(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		else if(targetType is InheritanceTypeNode && !(targetType is ExternalObjectTypeNode))
		{
			// we don't support calling a method from a graph element typed attribute contained in a graph element, only calling the method directly on the graph element
			result = new ProcedureMethodInvocationNode(((IdentExprNode)target).Ident, methodIdent, arguments, context);
			result.Resolve();
		}
		else if(targetType is ExternalObjectTypeNode)
		{
			if(targetQual != null)
				result = new ExternalProcedureMethodInvocationNode(targetQual, methodIdent, arguments, context);
			else
				result = new ExternalProcedureMethodInvocationNode(targetVar, methodIdent, arguments, context);
			result.Resolve();
		}
		else
			ReportError(targetType.TypeName + " does not have any procedure methods.");

		return result != null;
	}

	private static BuiltinProcedureInvocationBaseNode DecideMap(QualIdentNode targetQual, VarDeclNode targetVar,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "add":
			if(arguments.Size() != 2)
			{
				env.ReportError("map<S,T>.add(key, value) expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new MapAddItemNode(env.Coords, targetQual, arguments.Get(0), arguments.Get(1));
				else
					return new MapAddItemNode(env.Coords, targetVar, arguments.Get(0), arguments.Get(1));
			}
			goto case "rem";
		case "rem":
			if(arguments.Size() != 1)
			{
				env.ReportError("map<S,T>.rem(key) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new MapRemoveItemNode(env.Coords, targetQual, arguments.Get(0));
				else
					return new MapRemoveItemNode(env.Coords, targetVar, arguments.Get(0));
			}
			goto case "clear";
		case "clear":
			if(arguments.Size() != 0)
			{
				env.ReportError("map<S,T>.clear() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new MapClearNode(env.Coords, targetQual);
				else
					return new MapClearNode(env.Coords, targetVar);
			}
			goto default;
		default:
			env.ReportError("map<S,T> does not have a procedure method named " + methodName
					+ " (available are add, rem, clear).");
			return null;
		}
	}

	private static BuiltinProcedureInvocationBaseNode DecideSet(QualIdentNode targetQual, VarDeclNode targetVar,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "add":
			if(arguments.Size() != 1)
			{
				env.ReportError("set<T>.add(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new SetAddItemNode(env.Coords, targetQual, arguments.Get(0));
				else
					return new SetAddItemNode(env.Coords, targetVar, arguments.Get(0));
			}
			goto case "addAll";
		case "addAll":
			if(arguments.Size() != 1)
			{
				env.ReportError("set<T>.addAll(set<T>) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					env.ReportError("set<T>.addAll(set<T>) is not available on attributes (only variables; so you have to copy-assign or have to use a loop).");
				else
					return new SetAddAllNode(env.Coords, targetVar, arguments.Get(0));
			}
			goto case "rem";
		case "rem":
			if(arguments.Size() != 1)
			{
				env.ReportError("set<T>.rem(value) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new SetRemoveItemNode(env.Coords, targetQual, arguments.Get(0));
				else
					return new SetRemoveItemNode(env.Coords, targetVar, arguments.Get(0));
			}
			goto case "clear";
		case "clear":
			if(arguments.Size() != 0)
			{
				env.ReportError("set<T>.clear() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new SetClearNode(env.Coords, targetQual);
				else
					return new SetClearNode(env.Coords, targetVar);
			}
			goto default;
		default:
			env.ReportError("set<T> does not have a procedure method named " + methodName
					+ " (available are add, addAll, rem, clear).");
			return null;
		}
	}

	private static BuiltinProcedureInvocationBaseNode DecideArray(QualIdentNode targetQual, VarDeclNode targetVar,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "add":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("array<T>.add(value)/array<T>.add(value, index) expects 1 or 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
				{
					return new ArrayAddItemNode(env.Coords, targetQual, arguments.Get(0),
							arguments.Size() != 1 ? arguments.Get(1) : null);
				}
				else
				{
					return new ArrayAddItemNode(env.Coords, targetVar, arguments.Get(0),
							arguments.Size() != 1 ? arguments.Get(1) : null);
				}
			}
			goto case "addAll";
		case "addAll":
			if(arguments.Size() != 1)
			{
				env.ReportError("array<T>.addAll(array<T>) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					env.ReportError("array<T>.addAll(array<T>) is not available on attributes (only variables; so you have to copy-assign or have to use a loop).");
				else
					return new ArrayAddAllNode(env.Coords, targetVar, arguments.Get(0));
			}
			goto case "rem";
		case "rem":
			if(arguments.Size() != 1 && arguments.Size() != 0)
			{
				env.ReportError("array<T>.rem()/array<T>.rem(index) expects 0 or 1 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
				{
					return new ArrayRemoveItemNode(env.Coords, targetQual,
							arguments.Size() != 0 ? arguments.Get(0) : null);
				}
				else
				{
					return new ArrayRemoveItemNode(env.Coords, targetVar,
							arguments.Size() != 0 ? arguments.Get(0) : null);
				}
			}
			goto case "clear";
		case "clear":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.clear() expects no arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new ArrayClearNode(env.Coords, targetQual);
				else
					return new ArrayClearNode(env.Coords, targetVar);
			}
			goto default;
		default:
			env.ReportError("array<T> does not have a procedure method named " + methodName
					+ " (available are add, addAll, rem, clear).");
			return null;
		}
	}

	private static BuiltinProcedureInvocationBaseNode DecideDeque(QualIdentNode targetQual, VarDeclNode targetVar,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "add":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("deque<T>.add(value)/deque<T>.add(value, index) expects 1 or 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
				{
					return new DequeAddItemNode(env.Coords, targetQual, arguments.Get(0),
							arguments.Size() != 1 ? arguments.Get(1) : null);
				}
				else
				{
					return new DequeAddItemNode(env.Coords, targetVar, arguments.Get(0),
							arguments.Size() != 1 ? arguments.Get(1) : null);
				}
			}
			goto case "rem";
		case "rem":
			if(arguments.Size() != 1 && arguments.Size() != 0)
			{
				env.ReportError("deque<T>.rem()/deque<T>.rem(index) expects 0 or 1 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
				{
					return new DequeRemoveItemNode(env.Coords, targetQual,
							arguments.Size() != 0 ? arguments.Get(0) : null);
				}
				else
				{
					return new DequeRemoveItemNode(env.Coords, targetVar,
							arguments.Size() != 0 ? arguments.Get(0) : null);
				}
			}
			goto case "clear";
		case "clear":
			if(arguments.Size() != 0)
			{
				env.ReportError("deque<T>.clear() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(targetQual != null)
					return new DequeClearNode(env.Coords, targetQual);
				else
					return new DequeClearNode(env.Coords, targetVar);
			}
			goto default;
		default:
			env.ReportError("deque<T> does not have a procedure method named " + methodName
					+ " (available are add, rem, clear).");
			return null;
		}
	}

	protected internal override bool CheckLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION
				&& !(result is ProcedureMethodInvocationNode
						|| result is ExternalProcedureMethodInvocationNode)
				&& target is QualIdentNode)
		{
			ReportError("A procedure method call (built-in-procedure-method " + methodIdent + ") is not allowed in function or pattern part context.");
			return false;
		}
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
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

	public virtual IdentNode Ident
	{
		get
		{
		return methodIdent;
		}
	}

	protected internal override IR ConstructIR()
	{
		return result.IR;
	}
}

}
