/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.IR;

public class MethodInvocationExprNode extends ExprNode
{
	static {
		setName(MethodInvocationExprNode.class, "method invocation expression");
	}

	static TypeNode methodTypeNode = new TypeNode() {
		public Collection<BaseNode> getChildren() {
			Vector<BaseNode> children = new Vector<BaseNode>();
			// no children
			return children;
		}

		public Collection<String> getChildrenNames() {
			Vector<String> childrenNames = new Vector<String>();
			// no children
			return childrenNames;
		}
	};

	private ExprNode targetExpr;
	private IdentNode methodIdent;
	private IdentNode attributeIdent; // in most cases null, not used
	private CollectNode<ExprNode> params;
	private ExprNode result;

	public MethodInvocationExprNode(ExprNode targetExpr, IdentNode methodIdent, CollectNode<ExprNode> params, IdentNode attributeIdent)
	{
		super(methodIdent.getCoords());
		this.targetExpr  = becomeParent(targetExpr);
		this.methodIdent = becomeParent(methodIdent);
		this.attributeIdent = becomeParent(attributeIdent);
		this.params      = becomeParent(params);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(params);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		if(!targetExpr.resolve()) return false;

		String methodName = methodIdent.toString();
		TypeNode targetType = targetExpr.getType();

		///////////////////////////////////////////////////
		if(targetType == BasicTypeNode.stringType) {
		///////////////////////////////////////////////////
			if(methodName.equals("length")) {
				if(params.size() != 0) {
					reportError("string.length() does not take any parameters.");
					return false;
				}
				else
					result = new StringLengthNode(getCoords(), targetExpr);
			}
			else if(methodName.equals("toUpper")) {
				if(params.size() != 0) {
					reportError("string.toUpper() does not take any parameters.");
					return false;
				}
				else
					result = new StringToUpperNode(getCoords(), targetExpr);
			}
			else if(methodName.equals("toLower")) {
				if(params.size() != 0) {
					reportError("string.toLower() does not take any parameters.");
					return false;
				}
				else
					result = new StringToLowerNode(getCoords(), targetExpr);
			}
			else if(methodName.equals("substring")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("string.substring(startIndex, length) takes two parameters, or one if the length is omitted.");
					return false;
				}
  				else
  					if(params.size() == 2)
  						result = new StringSubstringNode(getCoords(), targetExpr, params.get(0), params.get(1));
  					else
  						result = new StringSubstringNode(getCoords(), targetExpr, params.get(0));
  			}
  			else if(methodName.equals("indexOf")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("string.indexOf(strToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new StringIndexOfNode(getCoords(), targetExpr, params.get(0));
  					else
  						result = new StringIndexOfNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
  			else if(methodName.equals("lastIndexOf")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("string.lastIndexOf(strToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new StringLastIndexOfNode(getCoords(), targetExpr, params.get(0));
  					else
  						result = new StringLastIndexOfNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
  			else if(methodName.equals("startsWith")) {
  				if(params.size() != 1) {
  					reportError("string.startsWith(strToSearchFor) takes one parameter.");
					return false;
				}
  				else
  					result = new StringStartsWithNode(getCoords(), targetExpr, params.get(0));
  			}
  			else if(methodName.equals("endsWith")) {
  				if(params.size() != 1) {
  					reportError("string.endsWith(strToSearchFor) takes one parameter.");
					return false;
				}
  				else
  					result = new StringEndsWithNode(getCoords(), targetExpr, params.get(0));
  			}
  			else if(methodName.equals("replace")) {
  				if(params.size() != 3) {
  					reportError("string.replace(startIndex, length, replaceStr) takes three parameters.");
					return false;
				}
  				else
  					result = new StringReplaceNode(getCoords(), targetExpr, params.get(0), params.get(1), params.get(2));
  			}
  			else if(methodName.equals("asArray")) {
  				if(params.size() != 1) {
  					reportError("string.asArray(separator) takes one parameter.");
					return false;
				}
  				else
  					result = new StringAsArrayNode(getCoords(), targetExpr, params.get(0));
  			}
  			else {
  				reportError("string does not have a method named \"" + methodName + "\"");
  				return false;
  			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof MapTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
  				if(params.size() != 0) {
  					reportError("map<S,T>.size() does not take any parameters.");
					return false;
				}
  				else
  					result = new MapSizeNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("empty")) {
  				if(params.size() != 0) {
  					reportError("map<S,T>.empty() does not take any parameters.");
					return false;
				}
  				else
  					result = new MapEmptyNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("domain")) {
  				if(params.size() != 0) {
  					reportError("map<S,T>.domain() does not take any parameters.");
					return false;
				}
  				else
  					result = new MapDomainNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("range")) {
  				if(params.size() != 0) {
  					reportError("map<S,T>.range() does not take any parameters.");
					return false;
				}
  				else
  					result = new MapRangeNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("asArray")) {
  				if(params.size() != 0) {
  					reportError("map<int,T>.asArray() does not take any parameters.");
					return false;
				}
  				else
  					result = new MapAsArrayNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("peek")) {
				if(params.size() != 1) {
  					reportError("map<S,T>.peek(number in iteration sequence) takes one parameter.");
					return false;
				}
  				else
  					result = new MapPeekNode(getCoords(), targetExpr, params.get(0));
			}
  			else {
  				reportError("map<S,T> does not have a method named \"" + methodName + "\"");
  				return false;
  			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof SetTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
  				if(params.size() != 0) {
  					reportError("set<T>.size() does not take any parameters.");
					return false;
				}
  				else
  					result = new SetSizeNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("empty")) {
  				if(params.size() != 0) {
  					reportError("set<T>.empty() does not take any parameters.");
					return false;
				}
  				else
  					result = new SetEmptyNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("peek")) {
				if(params.size() != 1) {
  					reportError("set<T>.peek(number in iteration sequence) takes one parameter.");
					return false;
				}
  				else
  					result = new SetPeekNode(getCoords(), targetExpr, params.get(0));
			}
			else if(methodName.equals("asArray")) {
  				if(params.size() != 0) {
  					reportError("set<T>.asArray() takes no parameters.");
					return false;
				}
  				else
  					result = new SetAsArrayNode(getCoords(), targetExpr);
  			}
  			else {
  				reportError("set<T> does not have a method named \"" + methodName + "\"");
  				return false;
  			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof ArrayTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
  				if(params.size() != 0) {
  					reportError("array<T>.size() does not take any parameters.");
					return false;
				}
  				else
  					result = new ArraySizeNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("empty")) {
  				if(params.size() != 0) {
  					reportError("array<T>.empty() does not take any parameters.");
					return false;
				}
  				else
  					result = new ArrayEmptyNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("peek")) {
				if(params.size() != 0 && params.size() != 1) {
  					reportError("array<T>.peek(index) takes one parameter; or none parameter returning the value from the end.");
					return false;
				}
  				else {
  					if(params.size() == 0 )
  						result = new ArrayPeekNode(getCoords(), targetExpr);
  					else
  						result = new ArrayPeekNode(getCoords(), targetExpr, params.get(0));
  				}
			}
  			else if(methodName.equals("indexOf")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("array<T>.indexOf(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new ArrayIndexOfNode(getCoords(), targetExpr, params.get(0));
  					else
  						result = new ArrayIndexOfNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
  			else if(methodName.equals("indexOfBy")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("array<T>.indexOfBy<attribute>(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new ArrayIndexOfByNode(getCoords(), targetExpr, attributeIdent, params.get(0));
  					else
  						result = new ArrayIndexOfByNode(getCoords(), targetExpr, attributeIdent, params.get(0), params.get(1));
  			}
  			else if(methodName.equals("indexOfOrdered")) {
  				if(params.size() != 1) {
  					reportError("array<T>.indexOfOrdered(valueToSearchFor) takes one parameter.");
					return false;
				}
  				else
					result = new ArrayIndexOfOrderedNode(getCoords(), targetExpr, params.get(0));
  			}
  			else if(methodName.equals("indexOfOrderedBy")) {
  				if(params.size() != 1) {
  					reportError("array<T>.indexOfOrderedBy<attribute>(valueToSearchFor) takes one parameter.");
					return false;
				}
  				else
					result = new ArrayIndexOfOrderedByNode(getCoords(), targetExpr, attributeIdent, params.get(0));
  			}
  			else if(methodName.equals("lastIndexOf")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("array<T>.lastIndexOf(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new ArrayLastIndexOfNode(getCoords(), targetExpr, params.get(0));
  					else
  						result = new ArrayLastIndexOfNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
  			else if(methodName.equals("lastIndexOfBy")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("array<T>.lastIndexOfBy<attribute>(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new ArrayLastIndexOfByNode(getCoords(), targetExpr, attributeIdent, params.get(0));
  					else
  						result = new ArrayLastIndexOfByNode(getCoords(), targetExpr, attributeIdent, params.get(0), params.get(1));
  			}
			else if(methodName.equals("subarray")) {
  				if(params.size() != 2) {
  					reportError("array<T>.subarray(startIndex, length) takes two parameters.");
					return false;
				}
  				else
  					result = new ArraySubarrayNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
			else if(methodName.equals("orderAscending")) {
  				if(params.size() != 0) {
  					reportError("array<T>.orderAscending() takes no parameters.");
					return false;
				}
  				else
  					result = new ArrayOrderAscendingNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("orderAscendingBy")) {
  				if(params.size() != 0) {
  					reportError("array<T>.orderAscendingBy<attribute>() takes no parameters.");
					return false;
				}
  				else
  					result = new ArrayOrderAscendingByNode(getCoords(), targetExpr, attributeIdent);
  			}
			else if(methodName.equals("reverse")) {
  				if(params.size() != 0) {
  					reportError("array<T>.reverse() takes no parameters.");
					return false;
				}
  				else
  					result = new ArrayReverseNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("asSet")) {
  				if(params.size() != 0) {
  					reportError("array<T>.asSet() takes no parameters.");
					return false;
				}
  				else
  					result = new ArrayAsSetNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("asDeque")) {
  				if(params.size() != 0) {
  					reportError("array<T>.asDeque() takes no parameters.");
					return false;
				}
  				else
  					result = new ArrayAsDequeNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("asMap")) {
  				if(params.size() != 0) {
  					reportError("array<T>.asMap() takes no parameters.");
					return false;
				}
  				else
  					result = new ArrayAsMapNode(getCoords(), targetExpr);
  			}
  			else if(methodName.equals("asString")) {
  				if(params.size() != 1) {
  					reportError("array<string>.asString(separator) takes one parameter.");
					return false;
				}
  				else
  					result = new ArrayAsStringNode(getCoords(), targetExpr, params.get(0));
  			}
  			else {
  				reportError("array<T> does not have a method named \"" + methodName + "\"");
  				return false;
  			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof DequeTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
  				if(params.size() != 0) {
  					reportError("deque<T>.size() does not take any parameters.");
					return false;
				}
  				else
  					result = new DequeSizeNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("empty")) {
  				if(params.size() != 0) {
  					reportError("deque<T>.empty() does not take any parameters.");
					return false;
				}
  				else
  					result = new DequeEmptyNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("peek")) {
				if(params.size() != 0 && params.size() != 1) {
  					reportError("deque<T>.peek(index) takes one parameter; or none parameter returning the value from the begin.");
					return false;
				}
  				else {
  					if(params.size() == 0 )
  						result = new DequePeekNode(getCoords(), targetExpr);
  					else
  						result = new DequePeekNode(getCoords(), targetExpr, params.get(0));
  				}
			}
  			else if(methodName.equals("indexOf")) {
  				if(params.size() != 1 && params.size() != 2) {
  					reportError("deque<T>.indexOf(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				}
  				else
  					if(params.size() == 1)
  						result = new DequeIndexOfNode(getCoords(), targetExpr, params.get(0));
  					else
  						result = new DequeIndexOfNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
  			else if(methodName.equals("lastIndexOf")) {
  				if(params.size() != 1) {
  					reportError("deque<T>.lastIndexOf(valueToSearchFor) takes one parameter.");
					return false;
				}
  				else
  					result = new DequeLastIndexOfNode(getCoords(), targetExpr, params.get(0));
  			}
			else if(methodName.equals("subdeque")) {
  				if(params.size() != 2) {
  					reportError("deque<T>.subdeque(startIndex, length) takes two parameters.");
					return false;
				}
  				else
  					result = new DequeSubdequeNode(getCoords(), targetExpr, params.get(0), params.get(1));
  			}
			else if(methodName.equals("asSet")) {
  				if(params.size() != 0) {
  					reportError("deque<T>.asSet() takes no parameters.");
					return false;
				}
  				else
  					result = new DequeAsSetNode(getCoords(), targetExpr);
  			}
			else if(methodName.equals("asArray")) {
  				if(params.size() != 0) {
  					reportError("deque<T>.asArray() takes no parameters.");
					return false;
				}
  				else
  					result = new DequeAsArrayNode(getCoords(), targetExpr);
  			}
  			else {
  				reportError("deque<T> does not have a method named \"" + methodName + "\"");
  				return false;
  			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof InheritanceTypeNode && !(targetType instanceof ExternalTypeNode)) {
		///////////////////////////////////////////////////
			if(targetExpr instanceof MethodInvocationExprNode) {
				reportError("method call chains are not supported, assign to a temporary def variable and invoke the method on it");
				return false;
			}
			result = new FunctionMethodInvocationExprNode(((IdentExprNode)targetExpr).getIdent(), methodIdent, params);
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof ExternalTypeNode) {
		///////////////////////////////////////////////////
			targetExpr.resolve();
			result = new ExternalFunctionMethodInvocationExprNode(targetExpr, methodIdent, params);
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
	public TypeNode getType() {
		return result.getType();
	}

	protected ExprNode getResult() {
		return result;
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}
