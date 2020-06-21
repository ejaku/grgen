/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.model.type.ExternalTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.stmt.array.ArrayAddItemNode;
import de.unika.ipd.grgen.ast.stmt.array.ArrayClearNode;
import de.unika.ipd.grgen.ast.stmt.array.ArrayRemoveItemNode;
import de.unika.ipd.grgen.ast.stmt.deque.DequeAddItemNode;
import de.unika.ipd.grgen.ast.stmt.deque.DequeClearNode;
import de.unika.ipd.grgen.ast.stmt.deque.DequeRemoveItemNode;
import de.unika.ipd.grgen.ast.stmt.map.MapAddItemNode;
import de.unika.ipd.grgen.ast.stmt.map.MapClearNode;
import de.unika.ipd.grgen.ast.stmt.map.MapRemoveItemNode;
import de.unika.ipd.grgen.ast.stmt.set.SetAddItemNode;
import de.unika.ipd.grgen.ast.stmt.set.SetClearNode;
import de.unika.ipd.grgen.ast.stmt.set.SetRemoveItemNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
import de.unika.ipd.grgen.ir.IR;

public class ProcedureMethodInvocationDecisionNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureMethodInvocationDecisionNode.class, "procedure method invocation decision statement");
	}

	private BaseNode target;
	private IdentNode methodIdent;
	private ProcedureOrBuiltinProcedureInvocationBaseNode result;

	public ProcedureMethodInvocationDecisionNode(BaseNode target, IdentNode methodIdent, CollectNode<ExprNode> arguments,
			int context)
	{
		super(methodIdent.getCoords(), arguments, context);
		this.target = becomeParent(target);
		this.methodIdent = becomeParent(methodIdent);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(arguments);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		if(!target.resolve())
			return false;

		String methodName = methodIdent.toString();
		VarDeclNode targetVar = null;
		QualIdentNode targetQual = null;
		TypeNode targetType = null;
		if(target instanceof QualIdentNode) {
			targetQual = (QualIdentNode)target;
			targetType = targetQual.getDecl().getDeclType();
		} else if(((IdentExprNode)target).decl instanceof VarDeclNode) {
			targetVar = (VarDeclNode)((IdentExprNode)target).decl;
			targetType = targetVar.getDeclType();
		} else {
			targetType = ((IdentExprNode)target).getType();
		}

		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(null, error, getCoords());
		if(targetType instanceof MapTypeNode) {
			result = decideMap(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof SetTypeNode) {
			result = decideSet(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof ArrayTypeNode) {
			result = decideArray(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof DequeTypeNode) {
			result = decideDeque(targetQual, targetVar, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof InheritanceTypeNode && !(targetType instanceof ExternalTypeNode)) {
			// we don't support calling a method from a graph element typed attribute contained in a graph element, only calling the method directly on the graph element
			result = new ProcedureMethodInvocationNode(((IdentExprNode)target).getIdent(), methodIdent, arguments, context);
			result.resolve();
		} else if(targetType instanceof ExternalTypeNode) {
			if(targetQual != null)
				result = new ExternalProcedureMethodInvocationNode(targetQual, methodIdent, arguments, context);
			else
				result = new ExternalProcedureMethodInvocationNode(targetVar, methodIdent, arguments, context);
			result.resolve();
		} else {
			reportError(targetType.toString() + " does not have any procedure methods");
		}
		
		return result != null;
	}

	private static BuiltinProcedureInvocationBaseNode decideMap(QualIdentNode targetQual, VarDeclNode targetVar,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "add":
			if(arguments.size() != 2) {
				env.reportError("map<S,T>.add(key, value) takes two parameters.");
				return null;
			} else {
				if(targetQual != null)
					return new MapAddItemNode(env.getCoords(), targetQual, arguments.get(0), arguments.get(1));
				else
					return new MapAddItemNode(env.getCoords(), targetVar, arguments.get(0), arguments.get(1));
			}
		case "rem":
			if(arguments.size() != 1) {
				env.reportError("map<S,T>.rem(key) takes one parameter.");
				return null;
			} else {
				if(targetQual != null)
					return new MapRemoveItemNode(env.getCoords(), targetQual, arguments.get(0));
				else
					return new MapRemoveItemNode(env.getCoords(), targetVar, arguments.get(0));
			}
		case "clear":
			if(arguments.size() != 0) {
				env.reportError("map<S,T>.clear() takes no parameters.");
				return null;
			} else {
				if(targetQual != null)
					return new MapClearNode(env.getCoords(), targetQual);
				else
					return new MapClearNode(env.getCoords(), targetVar);
			}
		default:
			env.reportError("map<S,T> does not have a procedure method named \"" + methodName + "\"");
			return null;
		}
	}

	private static BuiltinProcedureInvocationBaseNode decideSet(QualIdentNode targetQual, VarDeclNode targetVar,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "add":
			if(arguments.size() != 1) {
				env.reportError("set<T>.add(value) takes one parameter.");
				return null;
			} else {
				if(targetQual != null)
					return new SetAddItemNode(env.getCoords(), targetQual, arguments.get(0));
				else
					return new SetAddItemNode(env.getCoords(), targetVar, arguments.get(0));
			}
		case "rem":
			if(arguments.size() != 1) {
				env.reportError("set<T>.rem(value) takes one parameter.");
				return null;
			} else {
				if(targetQual != null)
					return new SetRemoveItemNode(env.getCoords(), targetQual, arguments.get(0));
				else
					return new SetRemoveItemNode(env.getCoords(), targetVar, arguments.get(0));
			}
		case "clear":
			if(arguments.size() != 0) {
				env.reportError("set<T>.clear() takes no parameters.");
				return null;
			} else {
				if(targetQual != null)
					return new SetClearNode(env.getCoords(), targetQual);
				else
					return new SetClearNode(env.getCoords(), targetVar);
			}
		default:
			env.reportError("set<T> does not have a procedure method named \"" + methodName + "\"");
			return null;
		}
	}

	private static BuiltinProcedureInvocationBaseNode decideArray(QualIdentNode targetQual, VarDeclNode targetVar,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "add":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("array<T>.add(value)/array<T>.add(value, index) takes one or two parameters.");
				return null;
			} else {
				if(targetQual != null) {
					return new ArrayAddItemNode(env.getCoords(), targetQual, arguments.get(0),
							arguments.size() != 1 ? arguments.get(1) : null);
				} else {
					return new ArrayAddItemNode(env.getCoords(), targetVar, arguments.get(0),
							arguments.size() != 1 ? arguments.get(1) : null);
				}
			}
		case "rem":
			if(arguments.size() != 1 && arguments.size() != 0) {
				env.reportError("array<T>.rem()/array<T>.rem(index) takes zero or one parameter.");
				return null;
			} else {
				if(targetQual != null) {
					return new ArrayRemoveItemNode(env.getCoords(), targetQual,
							arguments.size() != 0 ? arguments.get(0) : null);
				} else {
					return new ArrayRemoveItemNode(env.getCoords(), targetVar,
							arguments.size() != 0 ? arguments.get(0) : null);
				}
			}
		case "clear":
			if(arguments.size() != 0) {
				env.reportError("array<T>.clear() takes no parameters.");
				return null;
			} else {
				if(targetQual != null)
					return new ArrayClearNode(env.getCoords(), targetQual);
				else
					return new ArrayClearNode(env.getCoords(), targetVar);
			}
		default:
			env.reportError("array<T> does not have a procedure method named \"" + methodName + "\"");
			return null;
		}
	}

	private static BuiltinProcedureInvocationBaseNode decideDeque(QualIdentNode targetQual, VarDeclNode targetVar,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "add":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("deque<T>.add(value)/deque<T>.add(value, index) takes one or two parameters.");
				return null;
			} else {
				if(targetQual != null) {
					return new DequeAddItemNode(env.getCoords(), targetQual, arguments.get(0),
							arguments.size() != 1 ? arguments.get(1) : null);
				} else {
					return new DequeAddItemNode(env.getCoords(), targetVar, arguments.get(0),
							arguments.size() != 1 ? arguments.get(1) : null);
				}
			}
		case "rem":
			if(arguments.size() != 1 && arguments.size() != 0) {
				env.reportError("deque<T>.rem()/deque<T>.rem(index) takes zero or one parameter.");
				return null;
			} else {
				if(targetQual != null) {
					return new DequeRemoveItemNode(env.getCoords(), targetQual,
							arguments.size() != 0 ? arguments.get(0) : null);
				} else {
					return new DequeRemoveItemNode(env.getCoords(), targetVar,
							arguments.size() != 0 ? arguments.get(0) : null);
				}
			}
		case "clear":
			if(arguments.size() != 0) {
				env.reportError("deque<T>.clear() takes no parameters.");
				return null;
			} else {
				if(targetQual != null)
					return new DequeClearNode(env.getCoords(), targetQual);
				else
					return new DequeClearNode(env.getCoords(), targetVar);
			}
		default:
			env.reportError("deque<T> does not have a procedure method named \"" + methodName + "\"");
			return null;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION
				&& !(result instanceof ProcedureMethodInvocationNode
						|| result instanceof ExternalProcedureMethodInvocationNode)
				&& target instanceof QualIdentNode) {
			reportError("procedure method call not allowed in function or lhs context (built-in-procedure-method)");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		return result.getType();
	}

	public int getNumReturnTypes()
	{
		return result.getType().size();
	}

	@Override
	protected IR constructIR()
	{
		return result.getIR();
	}
}
