/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit, Moritz Kroll
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAndNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsDequeNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsMapNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsSetNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAsStringNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayAvgNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayDevNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayEmptyNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayExtractNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayGroupByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayGroupNode;
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
import de.unika.ipd.grgen.ast.expr.array.ArrayOrNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderAscendingByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderAscendingNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderDescendingByNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayOrderDescendingNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayPeekNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayProdNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayReverseNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayShuffleNode;
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
import de.unika.ipd.grgen.ast.expr.set.SetMaxNode;
import de.unika.ipd.grgen.ast.expr.set.SetMinNode;
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
import de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
import de.unika.ipd.grgen.ir.IR;

public class FunctionMethodInvocationDecisionNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionMethodInvocationDecisionNode.class, "function method invocation decision expression");
	}

	static TypeNode methodTypeNode = new TypeNode() {
		@Override
		public Collection<BaseNode> getChildren()
		{
			Vector<BaseNode> children = new Vector<BaseNode>();
			// no children
			return children;
		}

		@Override
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

	@Override
	protected boolean resolveLocal()
	{
		if(!targetExpr.resolve())
			return false;

		String methodName = methodIdent.toString();
		TypeNode targetType = targetExpr.getType();

		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(null, error, getCoords());
		if(targetType == BasicTypeNode.stringType) {
			result = decideString(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof MapTypeNode) {
			result = decideMap(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof SetTypeNode) {
			result = decideSet(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof ArrayTypeNode) {
			result = decideArray(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof DequeTypeNode) {
			result = decideDeque(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		} else if(targetType instanceof InheritanceTypeNode && !(targetType instanceof ExternalObjectTypeNode)) {
			if(targetExpr instanceof FunctionMethodInvocationDecisionNode) {
				reportError("Method call chains are not supported, assign to a temporary def variable and invoke the method on it.");
				return false;
			}
			result = new FunctionMethodInvocationExprNode(((IdentExprNode)targetExpr).getIdent(), methodIdent, arguments);
		} else if(targetType instanceof ExternalObjectTypeNode) {
			result = new ExternalFunctionMethodInvocationExprNode(targetExpr, methodIdent, arguments);
		} else if(targetType instanceof UntypedExecVarTypeNode) {
			result = new UntypedFunctionMethodInvocationExprNode(methodIdent.getCoords(), arguments);
		} else {
			reportError(targetType.getTypeName() + " does not have any function methods.");
		}
		
		return result != null;
	}

	private static BuiltinFunctionInvocationBaseNode decideString(ExprNode targetExpr, IdentNode attributeIdent,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "length":
			if(arguments.size() != 0) {
				env.reportError("string.length() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringLengthNode(env.getCoords(), targetExpr);
		case "toUpper":
			if(arguments.size() != 0) {
				env.reportError("string.toUpper() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringToUpperNode(env.getCoords(), targetExpr);
		case "toLower":
			if(arguments.size() != 0) {
				env.reportError("string.toLower() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringToLowerNode(env.getCoords(), targetExpr);
		case "substring":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("string.substring(startIndex, length) expects 2 arguments, or 1 if the length is omitted (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 2)
				return new StringSubstringNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
			else
				return new StringSubstringNode(env.getCoords(), targetExpr, arguments.get(0));
		case "indexOf":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("string.indexOf(strToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new StringIndexOfNode(env.getCoords(), targetExpr, arguments.get(0));
			else
				return new StringIndexOfNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "lastIndexOf":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("string.lastIndexOf(strToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new StringLastIndexOfNode(env.getCoords(), targetExpr, arguments.get(0));
			else
				return new StringLastIndexOfNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "startsWith":
			if(arguments.size() != 1) {
				env.reportError("string.startsWith(strToSearchFor) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringStartsWithNode(env.getCoords(), targetExpr, arguments.get(0));
		case "endsWith":
			if(arguments.size() != 1) {
				env.reportError("string.endsWith(strToSearchFor) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringEndsWithNode(env.getCoords(), targetExpr, arguments.get(0));
		case "replace":
			if(arguments.size() != 3) {
				env.reportError("string.replace(startIndex, length, replaceStr) expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringReplaceNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1), arguments.get(2));
		case "asArray":
			if(arguments.size() != 1) {
				env.reportError("string.asArray(separator) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StringAsArrayNode(env.getCoords(), targetExpr, arguments.get(0));
		default:
			env.reportError("string does not have a function method named " + methodName + ".");
			return null;
		}
	}

	private static BuiltinFunctionInvocationBaseNode decideMap(ExprNode targetExpr, IdentNode attributeIdent,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "size":
			if(arguments.size() != 0) {
				env.reportError("map<S,T>.size() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new MapSizeNode(env.getCoords(), targetExpr);
		case "empty":
			if(arguments.size() != 0) {
				env.reportError("map<S,T>.empty() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new MapEmptyNode(env.getCoords(), targetExpr);
		case "domain":
			if(arguments.size() != 0) {
				env.reportError("map<S,T>.domain() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new MapDomainNode(env.getCoords(), targetExpr);
		case "range":
			if(arguments.size() != 0) {
				env.reportError("map<S,T>.range() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new MapRangeNode(env.getCoords(), targetExpr);
		case "asArray":
			if(arguments.size() != 0) {
				env.reportError("map<int,T>.asArray() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new MapAsArrayNode(env.getCoords(), targetExpr);
		case "peek":
			if(arguments.size() != 1) {
				env.reportError("map<S,T>.peek(number in iteration sequence) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new MapPeekNode(env.getCoords(), targetExpr, arguments.get(0));
		default:
			env.reportError("map<S,T> does not have a function method named " + methodName + ".");
			return null;
		}
	}
	
	private static BuiltinFunctionInvocationBaseNode decideSet(ExprNode targetExpr, IdentNode attributeIdent,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "size":
			if(arguments.size() != 0) {
				env.reportError("set<T>.size() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SetSizeNode(env.getCoords(), targetExpr);
		case "empty":
			if(arguments.size() != 0) {
				env.reportError("set<T>.empty() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SetEmptyNode(env.getCoords(), targetExpr);
		case "peek":
			if(arguments.size() != 1) {
				env.reportError("set<T>.peek(number in iteration sequence) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SetPeekNode(env.getCoords(), targetExpr, arguments.get(0));
		case "min":
			if(arguments.size() != 0) {
				env.reportError("set<T>.min() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SetMinNode(env.getCoords(), targetExpr);
		case "max":
			if(arguments.size() != 0) {
				env.reportError("set<T>.max() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SetMaxNode(env.getCoords(), targetExpr);
		case "asArray":
			if(arguments.size() != 0) {
				env.reportError("set<T>.asArray() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SetAsArrayNode(env.getCoords(), targetExpr);
		default:
			env.reportError("set<T> does not have a function method named " + methodName + ".");
			return null;
		}
	}

	private static BuiltinFunctionInvocationBaseNode decideArray(ExprNode targetExpr, IdentNode attributeIdent,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "size":
			if(arguments.size() != 0) {
				env.reportError("array<T>.size() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArraySizeNode(env.getCoords(), targetExpr);
		case "empty":
			if(arguments.size() != 0) {
				env.reportError("array<T>.empty() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayEmptyNode(env.getCoords(), targetExpr);
		case "peek":
			if(arguments.size() != 0 && arguments.size() != 1) {
				env.reportError("array<T>.peek(index) expects 1 argument; or 0 arguments, then returning the value from the end (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(arguments.size() == 0)
					return new ArrayPeekNode(env.getCoords(), targetExpr);
				else
					return new ArrayPeekNode(env.getCoords(), targetExpr, arguments.get(0));
			}
		case "indexOf":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("array<T>.indexOf(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new ArrayIndexOfNode(env.getCoords(), targetExpr, arguments.get(0));
			else
				return new ArrayIndexOfNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "indexOfBy":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("array<T>.indexOfBy<attribute>(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new ArrayIndexOfByNode(env.getCoords(), targetExpr, attributeIdent, arguments.get(0));
			else
				return new ArrayIndexOfByNode(env.getCoords(), targetExpr, attributeIdent, arguments.get(0), arguments.get(1));
		case "indexOfOrdered":
			if(arguments.size() != 1) {
				env.reportError("array<T>.indexOfOrdered(valueToSearchFor) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayIndexOfOrderedNode(env.getCoords(), targetExpr, arguments.get(0));
		case "indexOfOrderedBy":
			if(arguments.size() != 1) {
				env.reportError("array<T>.indexOfOrderedBy<attribute>(valueToSearchFor) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayIndexOfOrderedByNode(env.getCoords(), targetExpr, attributeIdent, arguments.get(0));
		case "lastIndexOf":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("array<T>.lastIndexOf(valueToSearchFor) expects 1 argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new ArrayLastIndexOfNode(env.getCoords(), targetExpr, arguments.get(0));
			else
				return new ArrayLastIndexOfNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "lastIndexOfBy":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("array<T>.lastIndexOfBy<attribute>(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new ArrayLastIndexOfByNode(env.getCoords(), targetExpr, attributeIdent, arguments.get(0));
			else
				return new ArrayLastIndexOfByNode(env.getCoords(), targetExpr, attributeIdent, arguments.get(0), arguments.get(1));
		case "subarray":
			if(arguments.size() != 2) {
				env.reportError("array<T>.subarray(startIndex, length) expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArraySubarrayNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "orderAscending":
			if(arguments.size() != 0) {
				env.reportError("array<T>.orderAscending() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayOrderAscendingNode(env.getCoords(), targetExpr);
		case "orderDescending":
			if(arguments.size() != 0) {
				env.reportError("array<T>.orderDescending() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayOrderDescendingNode(env.getCoords(), targetExpr);
		case "group":
			if(arguments.size() != 0) {
				env.reportError("array<T>.group() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayGroupNode(env.getCoords(), targetExpr);
		case "keepOneForEach":
			if(attributeIdent == null) {
				if(arguments.size() != 0) {
					env.reportError("array<T>.keepOneForEach() expects 0 arguments (given are " + arguments.size() + " arguments).");
					return null;
				} else
					return new ArrayKeepOneForEachNode(env.getCoords(), targetExpr);
			} else {
				if(arguments.size() != 0) {
					env.reportError("array<T>.keepOneForEach<attribute>() expects 0 arguments (given are " + arguments.size() + " arguments).");
					return null;
				} else
					return new ArrayKeepOneForEachByNode(env.getCoords(), targetExpr, attributeIdent);
			}
		case "orderAscendingBy":
			if(arguments.size() != 0) {
				env.reportError("array<T>.orderAscendingBy<attribute>() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayOrderAscendingByNode(env.getCoords(), targetExpr, attributeIdent);
		case "orderDescendingBy":
			if(arguments.size() != 0) {
				env.reportError("array<T>.orderDescendingBy<attribute>() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayOrderDescendingByNode(env.getCoords(), targetExpr, attributeIdent);
		case "groupBy":
			if(arguments.size() != 0) {
				env.reportError("array<T>.groupBy<attribute>() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayGroupByNode(env.getCoords(), targetExpr, attributeIdent);
		case "reverse":
			if(arguments.size() != 0) {
				env.reportError("array<T>.reverse() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayReverseNode(env.getCoords(), targetExpr);
		case "shuffle":
			if(arguments.size() != 0) {
				env.reportError("array<T>.shuffle() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayShuffleNode(env.getCoords(), targetExpr);
		case "extract":
			if(arguments.size() != 0) {
				env.reportError("array<T>.extract<attribute>() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayExtractNode(env.getCoords(), targetExpr, attributeIdent);
		case "asSet":
			if(arguments.size() != 0) {
				env.reportError("array<T>.asSet() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayAsSetNode(env.getCoords(), targetExpr);
		case "asDeque":
			if(arguments.size() != 0) {
				env.reportError("array<T>.asDeque() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayAsDequeNode(env.getCoords(), targetExpr);
		case "asMap":
			if(arguments.size() != 0) {
				env.reportError("array<T>.asMap() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayAsMapNode(env.getCoords(), targetExpr);
		case "asString":
			if(arguments.size() != 1) {
				env.reportError("array<string>.asString(separator) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new ArrayAsStringNode(env.getCoords(), targetExpr, arguments.get(0));
		case "sum":
			if(arguments.size() != 0) {
				env.reportError("array<T>.sum() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArraySumNode(env.getCoords(), targetExpr);
			}
		case "prod":
			if(arguments.size() != 0) {
				env.reportError("array<T>.prod() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayProdNode(env.getCoords(), targetExpr);
			}
		case "min":
			if(arguments.size() != 0) {
				env.reportError("array<T>.min() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayMinNode(env.getCoords(), targetExpr);
			}
		case "max":
			if(arguments.size() != 0) {
				env.reportError("array<T>.max() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayMaxNode(env.getCoords(), targetExpr);
			}
		case "avg":
			if(arguments.size() != 0) {
				env.reportError("array<T>.avg() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayAvgNode(env.getCoords(), targetExpr);
			}
		case "med":
			if(arguments.size() != 0) {
				env.reportError("array<T>.med() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayMedNode(env.getCoords(), targetExpr);
			}
		case "medUnordered":
			if(arguments.size() != 0) {
				env.reportError("array<T>.medUnordered() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayMedUnorderedNode(env.getCoords(), targetExpr);
			}
		case "var":
			if(arguments.size() != 0) {
				env.reportError("array<T>.var() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayVarNode(env.getCoords(), targetExpr);
			}
		case "dev":
			if(arguments.size() != 0) {
				env.reportError("array<T>.dev() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayDevNode(env.getCoords(), targetExpr);
			}
		case "and":
			if(arguments.size() != 0) {
				env.reportError("array<T>.and() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayAndNode(env.getCoords(), targetExpr);
			}
		case "or":
			if(arguments.size() != 0) {
				env.reportError("array<T>.or() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ArrayOrNode(env.getCoords(), targetExpr);
			}
		default:
			env.reportError("array<T> does not have a function method named " + methodName + ".");
			return null;
		}
	}
	
	private static BuiltinFunctionInvocationBaseNode decideDeque(ExprNode targetExpr, IdentNode attributeIdent,
			String methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName) {
		case "size":
			if(arguments.size() != 0) {
				env.reportError("deque<T>.size() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new DequeSizeNode(env.getCoords(), targetExpr);
		case "empty":
			if(arguments.size() != 0) {
				env.reportError("deque<T>.empty() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new DequeEmptyNode(env.getCoords(), targetExpr);
		case "peek":
			if(arguments.size() != 0 && arguments.size() != 1) {
				env.reportError("deque<T>.peek(index) expects 1 argument; or 0 arguments, then returning the value from the begin (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(arguments.size() == 0)
					return new DequePeekNode(env.getCoords(), targetExpr);
				else
					return new DequePeekNode(env.getCoords(), targetExpr, arguments.get(0));
			}
		case "indexOf":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("deque<T>.indexOf(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new DequeIndexOfNode(env.getCoords(), targetExpr, arguments.get(0));
			else
				return new DequeIndexOfNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "lastIndexOf":
			if(arguments.size() != 1 && arguments.size() != 2) {
				env.reportError("deque<T>.lastIndexOf(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.size() + " arguments).");
				return null;
			} else if(arguments.size() == 1)
				return new DequeLastIndexOfNode(env.getCoords(), targetExpr, arguments.get(0));
			else
				return new DequeLastIndexOfNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "subdeque":
			if(arguments.size() != 2) {
				env.reportError("deque<T>.subdeque(startIndex, length) expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new DequeSubdequeNode(env.getCoords(), targetExpr, arguments.get(0), arguments.get(1));
		case "asSet":
			if(arguments.size() != 0) {
				env.reportError("deque<T>.asSet() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new DequeAsSetNode(env.getCoords(), targetExpr);
		case "asArray":
			if(arguments.size() != 0) {
				env.reportError("deque<T>.asArray() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new DequeAsArrayNode(env.getCoords(), targetExpr);
		default:
			env.reportError("deque<T> does not have a function method named " + methodName + ".");
			return null;
		}
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
