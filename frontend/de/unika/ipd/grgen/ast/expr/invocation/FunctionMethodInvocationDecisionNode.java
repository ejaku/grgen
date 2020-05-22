/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsDequeNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsMapNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsSetNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsStringNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAvgNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayDevNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayEmptyNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayExtractNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfOrderedByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfOrderedNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayKeepOneForEachByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayKeepOneForEachNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayLastIndexOfByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayLastIndexOfNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayMaxNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayMedNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayMedUnorderedNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayMinNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderAscendingByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderAscendingNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderDescendingByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderDescendingNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayPeekNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayProdNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayReverseNode;
import de.unika.ipd.grgen.ast.expr.array.ArraySizeNode;
import de.unika.ipd.grgen.ast.expr.array.ArraySubarrayNode;
import de.unika.ipd.grgen.ast.expr.array.ArraySumNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayVarNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeAsArrayNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeAsSetNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeEmptyNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeIndexOfNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeLastIndexOfNode;
import de.unika.ipd.grgen.ast.expr.deque.DequePeekNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeSizeNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeSubdequeNode;
import de.unika.ipd.grgen.ast.expr.map.MapAsArrayNode;
import de.unika.ipd.grgen.ast.expr.map.MapDomainNode;
import de.unika.ipd.grgen.ast.expr.map.MapEmptyNode;
import de.unika.ipd.grgen.ast.expr.map.MapPeekNode;
import de.unika.ipd.grgen.ast.expr.map.MapRangeNode;
import de.unika.ipd.grgen.ast.expr.map.MapSizeNode;
import de.unika.ipd.grgen.ast.expr.set.SetAsArrayNode;
import de.unika.ipd.grgen.ast.expr.set.SetEmptyNode;
import de.unika.ipd.grgen.ast.expr.set.SetPeekNode;
import de.unika.ipd.grgen.ast.expr.set.SetSizeNode;
import de.unika.ipd.grgen.ast.expr.string.StringAsArrayNode;
import de.unika.ipd.grgen.ast.expr.string.StringEndsWithNode;
import de.unika.ipd.grgen.ast.expr.string.StringIndexOfNode;
import de.unika.ipd.grgen.ast.expr.string.StringLastIndexOfNode;
import de.unika.ipd.grgen.ast.expr.string.StringLengthNode;
import de.unika.ipd.grgen.ast.expr.string.StringReplaceNode;
import de.unika.ipd.grgen.ast.expr.string.StringStartsWithNode;
import de.unika.ipd.grgen.ast.expr.string.StringSubstringNode;
import de.unika.ipd.grgen.ast.expr.string.StringToLowerNode;
import de.unika.ipd.grgen.ast.expr.string.StringToUpperNode;
import de.unika.ipd.grgen.ast.model.type.ExternalTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;

public class FunctionMethodInvocationDecisionNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionMethodInvocationDecisionNode.class, "function method invocation decision expression");
	}

	static TypeNode methodTypeNode = new TypeNode() {
		public Collection<BaseNode> getChildren()
		{
			Vector<BaseNode> children = new Vector<BaseNode>();
			// no children
			return children;
		}

		public Collection<String> getChildrenNames()
		{
			Vector<String> childrenNames = new Vector<String>();
			// no children
			return childrenNames;
		}
	};

	private ExprNode targetExpr;
	private IdentNode methodIdent;
	private IdentNode attributeIdent; // in most cases null, not used
	private FunctionOrBuiltinFunctionInvocationBaseNode result;

	public FunctionMethodInvocationDecisionNode(ExprNode targetExpr, IdentNode methodIdent, CollectNode<ExprNode> arguments,
			IdentNode attributeIdent)
	{
		super(methodIdent.getCoords(), arguments);
		this.targetExpr = becomeParent(targetExpr);
		this.methodIdent = becomeParent(methodIdent);
		this.attributeIdent = becomeParent(attributeIdent);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
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
		childrenNames.add("targetExpr");
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal()
	{
		if(!targetExpr.resolve())
			return false;

		String methodName = methodIdent.toString();
		TypeNode targetType = targetExpr.getType();

		///////////////////////////////////////////////////
		if(targetType == BasicTypeNode.stringType) {
		///////////////////////////////////////////////////
			if(methodName.equals("length")) {
				if(arguments.size() != 0) {
					reportError("string.length() does not take any parameters.");
					return false;
				} else
					result = new StringLengthNode(getCoords(), targetExpr);
			} else if(methodName.equals("toUpper")) {
				if(arguments.size() != 0) {
					reportError("string.toUpper() does not take any parameters.");
					return false;
				} else
					result = new StringToUpperNode(getCoords(), targetExpr);
			} else if(methodName.equals("toLower")) {
				if(arguments.size() != 0) {
					reportError("string.toLower() does not take any parameters.");
					return false;
				} else
					result = new StringToLowerNode(getCoords(), targetExpr);
			} else if(methodName.equals("substring")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("string.substring(startIndex, length) takes two parameters, or one if the length is omitted.");
					return false;
				} else if(arguments.size() == 2)
					result = new StringSubstringNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
				else
					result = new StringSubstringNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("indexOf")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("string.indexOf(strToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new StringIndexOfNode(getCoords(), targetExpr, arguments.get(0));
				else
					result = new StringIndexOfNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("lastIndexOf")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("string.lastIndexOf(strToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new StringLastIndexOfNode(getCoords(), targetExpr, arguments.get(0));
				else
					result = new StringLastIndexOfNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("startsWith")) {
				if(arguments.size() != 1) {
					reportError("string.startsWith(strToSearchFor) takes one parameter.");
					return false;
				} else
					result = new StringStartsWithNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("endsWith")) {
				if(arguments.size() != 1) {
					reportError("string.endsWith(strToSearchFor) takes one parameter.");
					return false;
				} else
					result = new StringEndsWithNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("replace")) {
				if(arguments.size() != 3) {
					reportError("string.replace(startIndex, length, replaceStr) takes three parameters.");
					return false;
				} else
					result = new StringReplaceNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1), arguments.get(2));
			} else if(methodName.equals("asArray")) {
				if(arguments.size() != 1) {
					reportError("string.asArray(separator) takes one parameter.");
					return false;
				} else
					result = new StringAsArrayNode(getCoords(), targetExpr, arguments.get(0));
			} else {
				reportError("string does not have a method named \"" + methodName + "\"");
				return false;
			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof MapTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
				if(arguments.size() != 0) {
					reportError("map<S,T>.size() does not take any parameters.");
					return false;
				} else
					result = new MapSizeNode(getCoords(), targetExpr);
			} else if(methodName.equals("empty")) {
				if(arguments.size() != 0) {
					reportError("map<S,T>.empty() does not take any parameters.");
					return false;
				} else
					result = new MapEmptyNode(getCoords(), targetExpr);
			} else if(methodName.equals("domain")) {
				if(arguments.size() != 0) {
					reportError("map<S,T>.domain() does not take any parameters.");
					return false;
				} else
					result = new MapDomainNode(getCoords(), targetExpr);
			} else if(methodName.equals("range")) {
				if(arguments.size() != 0) {
					reportError("map<S,T>.range() does not take any parameters.");
					return false;
				} else
					result = new MapRangeNode(getCoords(), targetExpr);
			} else if(methodName.equals("asArray")) {
				if(arguments.size() != 0) {
					reportError("map<int,T>.asArray() does not take any parameters.");
					return false;
				} else
					result = new MapAsArrayNode(getCoords(), targetExpr);
			} else if(methodName.equals("peek")) {
				if(arguments.size() != 1) {
					reportError("map<S,T>.peek(number in iteration sequence) takes one parameter.");
					return false;
				} else
					result = new MapPeekNode(getCoords(), targetExpr, arguments.get(0));
			} else {
				reportError("map<S,T> does not have a method named \"" + methodName + "\"");
				return false;
			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof SetTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
				if(arguments.size() != 0) {
					reportError("set<T>.size() does not take any parameters.");
					return false;
				} else
					result = new SetSizeNode(getCoords(), targetExpr);
			} else if(methodName.equals("empty")) {
				if(arguments.size() != 0) {
					reportError("set<T>.empty() does not take any parameters.");
					return false;
				} else
					result = new SetEmptyNode(getCoords(), targetExpr);
			} else if(methodName.equals("peek")) {
				if(arguments.size() != 1) {
					reportError("set<T>.peek(number in iteration sequence) takes one parameter.");
					return false;
				} else
					result = new SetPeekNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("asArray")) {
				if(arguments.size() != 0) {
					reportError("set<T>.asArray() takes no parameters.");
					return false;
				} else
					result = new SetAsArrayNode(getCoords(), targetExpr);
			} else {
				reportError("set<T> does not have a method named \"" + methodName + "\"");
				return false;
			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof ArrayTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
				if(arguments.size() != 0) {
					reportError("array<T>.size() does not take any parameters.");
					return false;
				} else
					result = new ArraySizeNode(getCoords(), targetExpr);
			} else if(methodName.equals("empty")) {
				if(arguments.size() != 0) {
					reportError("array<T>.empty() does not take any parameters.");
					return false;
				} else
					result = new ArrayEmptyNode(getCoords(), targetExpr);
			} else if(methodName.equals("peek")) {
				if(arguments.size() != 0 && arguments.size() != 1) {
					reportError("array<T>.peek(index) takes one parameter; or none parameter returning the value from the end.");
					return false;
				} else {
					if(arguments.size() == 0)
						result = new ArrayPeekNode(getCoords(), targetExpr);
					else
						result = new ArrayPeekNode(getCoords(), targetExpr, arguments.get(0));
				}
			} else if(methodName.equals("indexOf")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("array<T>.indexOf(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new ArrayIndexOfNode(getCoords(), targetExpr, arguments.get(0));
				else
					result = new ArrayIndexOfNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("indexOfBy")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("array<T>.indexOfBy<attribute>(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new ArrayIndexOfByNode(getCoords(), targetExpr, attributeIdent, arguments.get(0));
				else
					result = new ArrayIndexOfByNode(getCoords(), targetExpr, attributeIdent, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("indexOfOrdered")) {
				if(arguments.size() != 1) {
					reportError("array<T>.indexOfOrdered(valueToSearchFor) takes one parameter.");
					return false;
				} else
					result = new ArrayIndexOfOrderedNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("indexOfOrderedBy")) {
				if(arguments.size() != 1) {
					reportError("array<T>.indexOfOrderedBy<attribute>(valueToSearchFor) takes one parameter.");
					return false;
				} else
					result = new ArrayIndexOfOrderedByNode(getCoords(), targetExpr, attributeIdent, arguments.get(0));
			} else if(methodName.equals("lastIndexOf")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("array<T>.lastIndexOf(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new ArrayLastIndexOfNode(getCoords(), targetExpr, arguments.get(0));
				else
					result = new ArrayLastIndexOfNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("lastIndexOfBy")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("array<T>.lastIndexOfBy<attribute>(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new ArrayLastIndexOfByNode(getCoords(), targetExpr, attributeIdent, arguments.get(0));
				else
					result = new ArrayLastIndexOfByNode(getCoords(), targetExpr, attributeIdent, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("subarray")) {
				if(arguments.size() != 2) {
					reportError("array<T>.subarray(startIndex, length) takes two parameters.");
					return false;
				} else
					result = new ArraySubarrayNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("orderAscending")) {
				if(arguments.size() != 0) {
					reportError("array<T>.orderAscending() takes no parameters.");
					return false;
				} else
					result = new ArrayOrderAscendingNode(getCoords(), targetExpr);
			} else if(methodName.equals("orderDescending")) {
				if(arguments.size() != 0) {
					reportError("array<T>.orderDescending() takes no parameters.");
					return false;
				} else
					result = new ArrayOrderDescendingNode(getCoords(), targetExpr);
			} else if(methodName.equals("keepOneForEach")) {
				if(attributeIdent == null) {
					if(arguments.size() != 0) {
						reportError("array<T>.keepOneForEach() takes no parameters.");
						return false;
					} else
						result = new ArrayKeepOneForEachNode(getCoords(), targetExpr);
				} else {
					if(arguments.size() != 0) {
						reportError("array<T>.keepOneForEach<attribute>() takes no parameters.");
						return false;
					} else
						result = new ArrayKeepOneForEachByNode(getCoords(), targetExpr, attributeIdent);
				}
			} else if(methodName.equals("orderAscendingBy")) {
				if(arguments.size() != 0) {
					reportError("array<T>.orderAscendingBy<attribute>() takes no parameters.");
					return false;
				} else
					result = new ArrayOrderAscendingByNode(getCoords(), targetExpr, attributeIdent);
			} else if(methodName.equals("orderDescendingBy")) {
				if(arguments.size() != 0) {
					reportError("array<T>.orderDescendingBy<attribute>() takes no parameters.");
					return false;
				} else
					result = new ArrayOrderDescendingByNode(getCoords(), targetExpr, attributeIdent);
			} else if(methodName.equals("reverse")) {
				if(arguments.size() != 0) {
					reportError("array<T>.reverse() takes no parameters.");
					return false;
				} else
					result = new ArrayReverseNode(getCoords(), targetExpr);
			} else if(methodName.equals("extract")) {
				if(arguments.size() != 0) {
					reportError("array<T>.extract<attribute>() takes no parameters.");
					return false;
				} else
					result = new ArrayExtractNode(getCoords(), targetExpr, attributeIdent);
			} else if(methodName.equals("asSet")) {
				if(arguments.size() != 0) {
					reportError("array<T>.asSet() takes no parameters.");
					return false;
				} else
					result = new ArrayAsSetNode(getCoords(), targetExpr);
			} else if(methodName.equals("asDeque")) {
				if(arguments.size() != 0) {
					reportError("array<T>.asDeque() takes no parameters.");
					return false;
				} else
					result = new ArrayAsDequeNode(getCoords(), targetExpr);
			} else if(methodName.equals("asMap")) {
				if(arguments.size() != 0) {
					reportError("array<T>.asMap() takes no parameters.");
					return false;
				} else
					result = new ArrayAsMapNode(getCoords(), targetExpr);
			} else if(methodName.equals("asString")) {
				if(arguments.size() != 1) {
					reportError("array<string>.asString(separator) takes one parameter.");
					return false;
				} else
					result = new ArrayAsStringNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("sum")) {
				if(arguments.size() != 0) {
					reportError("array<T>.sum() takes no parameters.");
					return false;
				} else {
					result = new ArraySumNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("prod")) {
				if(arguments.size() != 0) {
					reportError("array<T>.prod() takes no parameters.");
					return false;
				} else {
					result = new ArrayProdNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("min")) {
				if(arguments.size() != 0) {
					reportError("array<T>.min() takes no parameters.");
					return false;
				} else {
					result = new ArrayMinNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("max")) {
				if(arguments.size() != 0) {
					reportError("array<T>.max() takes no parameters.");
					return false;
				} else {
					result = new ArrayMaxNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("avg")) {
				if(arguments.size() != 0) {
					reportError("array<T>.avg() takes no parameters.");
					return false;
				} else {
					result = new ArrayAvgNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("med")) {
				if(arguments.size() != 0) {
					reportError("array<T>.med() takes no parameters.");
					return false;
				} else {
					result = new ArrayMedNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("medUnordered")) {
				if(arguments.size() != 0) {
					reportError("array<T>.medUnordered() takes no parameters.");
					return false;
				} else {
					result = new ArrayMedUnorderedNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("var")) {
				if(arguments.size() != 0) {
					reportError("array<T>.var() takes no parameters.");
					return false;
				} else {
					result = new ArrayVarNode(getCoords(), targetExpr);
				}
			} else if(methodName.equals("dev")) {
				if(arguments.size() != 0) {
					reportError("array<T>.dev() takes no parameters.");
					return false;
				} else {
					result = new ArrayDevNode(getCoords(), targetExpr);
				}
			} else {
				reportError("array<T> does not have a method named \"" + methodName + "\"");
				return false;
			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof DequeTypeNode) {
		///////////////////////////////////////////////////
			if(methodName.equals("size")) {
				if(arguments.size() != 0) {
					reportError("deque<T>.size() does not take any parameters.");
					return false;
				} else
					result = new DequeSizeNode(getCoords(), targetExpr);
			} else if(methodName.equals("empty")) {
				if(arguments.size() != 0) {
					reportError("deque<T>.empty() does not take any parameters.");
					return false;
				} else
					result = new DequeEmptyNode(getCoords(), targetExpr);
			} else if(methodName.equals("peek")) {
				if(arguments.size() != 0 && arguments.size() != 1) {
					reportError("deque<T>.peek(index) takes one parameter; or none parameter returning the value from the begin.");
					return false;
				} else {
					if(arguments.size() == 0)
						result = new DequePeekNode(getCoords(), targetExpr);
					else
						result = new DequePeekNode(getCoords(), targetExpr, arguments.get(0));
				}
			} else if(methodName.equals("indexOf")) {
				if(arguments.size() != 1 && arguments.size() != 2) {
					reportError("deque<T>.indexOf(valueToSearchFor) takes one parameter, or a second startIndex parameter.");
					return false;
				} else if(arguments.size() == 1)
					result = new DequeIndexOfNode(getCoords(), targetExpr, arguments.get(0));
				else
					result = new DequeIndexOfNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("lastIndexOf")) {
				if(arguments.size() != 1) {
					reportError("deque<T>.lastIndexOf(valueToSearchFor) takes one parameter.");
					return false;
				} else
					result = new DequeLastIndexOfNode(getCoords(), targetExpr, arguments.get(0));
			} else if(methodName.equals("subdeque")) {
				if(arguments.size() != 2) {
					reportError("deque<T>.subdeque(startIndex, length) takes two parameters.");
					return false;
				} else
					result = new DequeSubdequeNode(getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			} else if(methodName.equals("asSet")) {
				if(arguments.size() != 0) {
					reportError("deque<T>.asSet() takes no parameters.");
					return false;
				} else
					result = new DequeAsSetNode(getCoords(), targetExpr);
			} else if(methodName.equals("asArray")) {
				if(arguments.size() != 0) {
					reportError("deque<T>.asArray() takes no parameters.");
					return false;
				} else
					result = new DequeAsArrayNode(getCoords(), targetExpr);
			} else {
				reportError("deque<T> does not have a method named \"" + methodName + "\"");
				return false;
			}
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof InheritanceTypeNode && !(targetType instanceof ExternalTypeNode)) {
		///////////////////////////////////////////////////
			if(targetExpr instanceof FunctionMethodInvocationDecisionNode) {
				reportError("method call chains are not supported, assign to a temporary def variable and invoke the method on it");
				return false;
			}
			result = new FunctionMethodInvocationExprNode(((IdentExprNode)targetExpr).getIdent(), methodIdent, arguments);
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof ExternalTypeNode) {
		///////////////////////////////////////////////////
			result = new ExternalFunctionMethodInvocationExprNode(targetExpr, methodIdent, arguments);
		}
		///////////////////////////////////////////////////
		else if(targetType instanceof UntypedExecVarTypeNode) {
		///////////////////////////////////////////////////
			result = new UntypedFunctionMethodInvocationExprNode(methodIdent.getCoords(), arguments);
		} else {
			reportError(targetType.toString() + " does not have any methods");
			return false;
		}
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return result.getType();
	}

	protected ExprNode getResult()
	{
		return result;
	}

	@Override
	protected IR constructIR()
	{
		return result.getIR();
	}
}
