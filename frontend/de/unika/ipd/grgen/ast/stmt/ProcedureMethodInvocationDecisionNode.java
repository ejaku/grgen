/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.ExternalProcedureMethodInvocationNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
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
import de.unika.ipd.grgen.ast.typedecl.ArrayTypeNode;
import de.unika.ipd.grgen.ast.typedecl.DequeTypeNode;
import de.unika.ipd.grgen.ast.typedecl.ExternalTypeNode;
import de.unika.ipd.grgen.ast.typedecl.MapTypeNode;
import de.unika.ipd.grgen.ast.typedecl.SetTypeNode;
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

		if(targetType instanceof MapTypeNode) {
			if(methodName.equals("add")) {
				if(arguments.size() != 2) {
					reportError("map<S,T>.add(key, value) takes two parameters.");
					return false;
				} else {
					if(targetQual != null)
						result = new MapAddItemNode(getCoords(), targetQual, arguments.get(0), arguments.get(1));
					else
						result = new MapAddItemNode(getCoords(), targetVar, arguments.get(0), arguments.get(1));
				}
			} else if(methodName.equals("rem")) {
				if(arguments.size() != 1) {
					reportError("map<S,T>.rem(key) takes one parameter.");
					return false;
				} else {
					if(targetQual != null)
						result = new MapRemoveItemNode(getCoords(), targetQual, arguments.get(0));
					else
						result = new MapRemoveItemNode(getCoords(), targetVar, arguments.get(0));
				}
			} else if(methodName.equals("clear")) {
				if(arguments.size() != 0) {
					reportError("map<S,T>.clear() takes no parameters.");
					return false;
				} else {
					if(targetQual != null)
						result = new MapClearNode(getCoords(), targetQual);
					else
						result = new MapClearNode(getCoords(), targetVar);
				}
			} else {
				reportError("map<S,T> does not have a statement method named \"" + methodName + "\"");
				return false;
			}
		} else if(targetType instanceof SetTypeNode) {
			if(methodName.equals("add")) {
				if(arguments.size() != 1) {
					reportError("set<T>.add(value) takes one parameter.");
					return false;
				} else {
					if(targetQual != null)
						result = new SetAddItemNode(getCoords(), targetQual, arguments.get(0));
					else
						result = new SetAddItemNode(getCoords(), targetVar, arguments.get(0));
				}
			} else if(methodName.equals("rem")) {
				if(arguments.size() != 1) {
					reportError("set<T>.rem(value) takes one parameter.");
					return false;
				} else {
					if(targetQual != null)
						result = new SetRemoveItemNode(getCoords(), targetQual, arguments.get(0));
					else
						result = new SetRemoveItemNode(getCoords(), targetVar, arguments.get(0));
				}
			} else if(methodName.equals("clear")) {
				if(arguments.size() != 0) {
					reportError("set<T>.clear() takes no parameters.");
					return false;
				} else {
					if(targetQual != null)
						result = new SetClearNode(getCoords(), targetQual);
					else
						result = new SetClearNode(getCoords(), targetVar);
				}
			} else {
				reportError("set<T> does not have a method named \"" + methodName + "\"");
				return false;
			}
		} else if(targetType instanceof ArrayTypeNode) {
			if(methodName.equals("add")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("array<T>.add(value)/array<T>.add(value, index) takes one or two parameters.");
					return false;
				} else {
					if(targetQual != null) {
						result = new ArrayAddItemNode(getCoords(), targetQual, arguments.get(0),
								arguments.size() != 1 ? arguments.get(1) : null);
					} else {
						result = new ArrayAddItemNode(getCoords(), targetVar, arguments.get(0),
								arguments.size() != 1 ? arguments.get(1) : null);
					}
				}
			} else if(methodName.equals("rem")) {
				if(arguments.size() != 1 && arguments.size() != 0) {
					reportError("array<T>.rem()/array<T>.rem(index) takes zero or one parameter.");
					return false;
				} else {
					if(targetQual != null) {
						result = new ArrayRemoveItemNode(getCoords(), targetQual,
								arguments.size() != 0 ? arguments.get(0) : null);
					} else {
						result = new ArrayRemoveItemNode(getCoords(), targetVar,
								arguments.size() != 0 ? arguments.get(0) : null);
					}
				}
			} else if(methodName.equals("clear")) {
				if(arguments.size() != 0) {
					reportError("array<T>.clear() takes no parameters.");
					return false;
				} else {
					if(targetQual != null)
						result = new ArrayClearNode(getCoords(), targetQual);
					else
						result = new ArrayClearNode(getCoords(), targetVar);
				}
			} else {
				reportError("array<T> does not have a statement method named \"" + methodName + "\"");
				return false;
			}
		} else if(targetType instanceof DequeTypeNode) {
			if(methodName.equals("add")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("deque<T>.add(value)/deque<T>.add(value, index) takes one or two parameters.");
					return false;
				} else {
					if(targetQual != null) {
						result = new DequeAddItemNode(getCoords(), targetQual, arguments.get(0),
								arguments.size() != 1 ? arguments.get(1) : null);
					} else {
						result = new DequeAddItemNode(getCoords(), targetVar, arguments.get(0),
								arguments.size() != 1 ? arguments.get(1) : null);
					}
				}
			} else if(methodName.equals("rem")) {
				if(arguments.size() != 1 && arguments.size() != 0) {
					reportError("deque<T>.rem()/deque<T>.rem(index) takes zero or one parameter.");
					return false;
				} else {
					if(targetQual != null) {
						result = new DequeRemoveItemNode(getCoords(), targetQual,
								arguments.size() != 0 ? arguments.get(0) : null);
					} else {
						result = new DequeRemoveItemNode(getCoords(), targetVar,
								arguments.size() != 0 ? arguments.get(0) : null);
					}
				}
			} else if(methodName.equals("clear")) {
				if(arguments.size() != 0) {
					reportError("deque<T>.clear() takes no parameters.");
					return false;
				} else {
					if(targetQual != null)
						result = new DequeClearNode(getCoords(), targetQual);
					else
						result = new DequeClearNode(getCoords(), targetVar);
				}
			} else {
				reportError("deque<T> does not have a statement method named \"" + methodName + "\"");
				return false;
			}
		} else if(targetType instanceof InheritanceTypeNode && !(targetType instanceof ExternalTypeNode)) {
			// we don't support calling a method from a graph element typed attribute contained in a graph element, only calling method directly on the graph element
			result = new ProcedureMethodInvocationNode(((IdentExprNode)target).getIdent(), methodIdent, arguments, context);
			result.resolve();
		} else if(targetType instanceof ExternalTypeNode) {
			if(targetQual != null)
				result = new ExternalProcedureMethodInvocationNode(targetQual, methodIdent, arguments, context);
			else
				result = new ExternalProcedureMethodInvocationNode(targetVar, methodIdent, arguments, context);
			result.resolve();
		} else {
			reportError(targetType.toString() + " does not have any methods");
			return false;
		}
		return true;
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

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

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
