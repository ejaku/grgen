/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;

public class MethodCallNode extends EvalStatementNode
{
	static {
		setName(MethodCallNode.class, "method call eval statement");
	}

	private BaseNode target;
	private IdentNode methodIdent;
	private CollectNode<ExprNode> params;
	private EvalStatementNode result;

	public MethodCallNode(BaseNode target, IdentNode methodIdent, CollectNode<ExprNode> params)
	{
		super(methodIdent.getCoords());
		this.target = becomeParent(target);
		this.methodIdent = becomeParent(methodIdent);
		this.params = becomeParent(params);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(params);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		if(!target.resolve()) return false;

		String methodName = methodIdent.toString();
		VarDeclNode targetVar = null;
		QualIdentNode targetQual = null;
		TypeNode targetType = null;
		if(target instanceof QualIdentNode) {
			targetQual = (QualIdentNode)target;
			targetType = targetQual.getDecl().getDeclType();
		} else {
			targetVar = (VarDeclNode)((IdentExprNode)target).decl;
			targetType = targetVar.getDeclType();
		}

		if(targetType instanceof MapTypeNode) {
			if(methodName.equals("add")) {
  				if(params.size() != 2) {
  					reportError("map<S,T>.add(key, value) takes two parameters.");
					return false;
				}
  				else {
  					if(targetQual!=null)
  						result = new MapAddItemNode(getCoords(), targetQual, params.get(0), params.get(1));
  					else
  						result = new MapVarAddItemNode(getCoords(), targetVar, params.get(0), params.get(1));
  				}
  			}
			else if(methodName.equals("rem")) {
				if(params.size() != 1) {
  					reportError("map<S,T>.rem(key) takes one parameter.");
					return false;
				}
  				else {
  					if(targetQual!=null)
  						result = new MapRemoveItemNode(getCoords(), targetQual, params.get(0));
  					else
  						result = new MapVarRemoveItemNode(getCoords(), targetVar, params.get(0));
  				}
			}
  			else {
  				reportError("map<S,T> does not have a statement method named \"" + methodName + "\"");
  				return false;
  			}
		}
		else if(targetType instanceof SetTypeNode) {
			if(methodName.equals("add")) {
				if(params.size() != 1) {
  					reportError("set<T>.add(value) takes one parameter.");
					return false;
				}
  				else {
  					if(targetQual!=null)
  						result = new SetAddItemNode(getCoords(), targetQual, params.get(0));
  					else
  						result = new SetVarAddItemNode(getCoords(), targetVar, params.get(0));
  				}
			}
			else if(methodName.equals("rem")) {
				if(params.size() != 1) {
  					reportError("set<T>.rem(value) takes one parameter.");
					return false;
				}
  				else {
  					if(targetQual!=null)
  						result = new SetRemoveItemNode(getCoords(), targetQual, params.get(0));
  					else
  						result = new SetVarRemoveItemNode(getCoords(), targetVar, params.get(0));
  				}
			}
  			else {
  				reportError("set<T> does not have a method named \"" + methodName + "\"");
  				return false;
  			}
		}
		else if(targetType instanceof ArrayTypeNode) {
			if(methodName.equals("add")) {
				if(params.size()!=1 && params.size()!=2) {
  					reportError("array<T>.add(value)/array<T>.add(value, index) takes one or two parameters.");
					return false;
				}
  				else {
  					if(targetQual!=null)
  						result = new ArrayAddItemNode(getCoords(), targetQual, params.get(0), params.size()!=1 ? params.get(1) : null);
  					else
  						result = new ArrayVarAddItemNode(getCoords(), targetVar, params.get(0), params.size()!=1 ? params.get(1) : null);
  				}
			}
			else if(methodName.equals("rem")) {
				if(params.size()!=1 && params.size()!=0) {
  					reportError("array<T>.rem()/array<T>.rem(index) takes zero or one parameter.");
					return false;
				}
  				else {
  					if(targetQual!=null)
  						result = new ArrayRemoveItemNode(getCoords(), targetQual, params.size()!=0 ? params.get(0) : null);
  					else
  						result = new ArrayVarRemoveItemNode(getCoords(), targetVar, params.size()!=0 ? params.get(0) : null);
  				}
			}
		}
		else {
			reportError(targetType.toString() + " does not have any methods");
			return false;
		}
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}
